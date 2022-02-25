using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
using Loto3000App.Models.Ticket;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private IUserRepository _userRepository;
        private ITicketRepository _ticketRepository;
        private ITicketDetailsRepository _ticketDetailsRepository;
        private ISessionRepository _sessionRepository;

        public TicketService(IUserRepository userRepository, ISessionRepository sessionRepository, ITicketRepository ticketRepository, ITicketDetailsRepository ticketDetailsRepository)
        {
            
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _ticketDetailsRepository = ticketDetailsRepository;
            _sessionRepository = sessionRepository;
        }



        //kompjuterot generira ticket
        public string GenerateTicket(string id, string role)
        {
            ValidateUser(role);

            User user = _userRepository
                            .GetAll()
                            .ToList()
                            .FirstOrDefault(x => x.Id == Convert.ToInt64(id));


            Session lastSessionDb = _sessionRepository
                                .GetAll()
                                .ToList()
                                .LastOrDefault();

            CheckSession(lastSessionDb);


            List<int> ticketNumbers = GenerateAutomatic();

            Ticket ticket = new Ticket()
            {
                SessionId = lastSessionDb.Id,
                UserId = user.Id,
                TicketDetails = ticketNumbers.Select(x => new TicketDetails()
                {
                    Number = x
                }).ToList()
                

            };

            _ticketRepository.Add(ticket);
            _ticketRepository.SaveChanges();
            _ticketDetailsRepository.SaveChanges();


            return GetCombination(ticketNumbers);

        }


        
        
        public void AddEntity(TicketModel ticketModel, string id, string role)
        {

            ValidateUser(role);
            User usersDb = _userRepository
                            .GetAll()
                            .ToList()
                            .FirstOrDefault(x => x.Id == Convert.ToInt16(id));

            Session lastSessionDb = _sessionRepository
                                    .GetAll()
                                    .ToList()
                                    .LastOrDefault();

            CheckSession(lastSessionDb);

            ValidateTicket(ticketModel);
            List<int> ticketList = new List<int>();
            ticketList.Add(int.Parse(ticketModel.One));
            ticketList.Add(int.Parse(ticketModel.Two));
            ticketList.Add(int.Parse(ticketModel.Three));
            ticketList.Add(int.Parse(ticketModel.Four));
            ticketList.Add(int.Parse(ticketModel.Five));
            ticketList.Add(int.Parse(ticketModel.Six));
            ticketList.Add(int.Parse(ticketModel.Seven));

            //
            Ticket ticket = new Ticket()
            {
                SessionId = lastSessionDb.Id,
                UserId = usersDb.Id,
                TicketDetails = ticketList.Select(tl => new TicketDetails()
                {
                    Number = tl
                }).ToList()
                
            };

            _ticketRepository.Add(ticket);
            _ticketRepository.SaveChanges();
            _ticketDetailsRepository.SaveChanges();



        }


        public void DeleteEntity(int id)
        {
            Ticket deleteTicket = _ticketRepository.GetById(id);
            if (deleteTicket == null)
            {
                throw new NotFoundException($"Ticket with id {id} was not found");
            }
            _ticketRepository.Delete(deleteTicket);

        }

        public List<TicketModel> GetAllEntities()
        {
            List<Ticket> ticketDb = _ticketRepository.GetAll().ToList();
            List<TicketModel> ticketModels = new List<TicketModel>();
            foreach(Ticket ticket in ticketDb)
            {

                List<int> ticketDbList = _ticketDetailsRepository
                    .GetAll()
                    .Where(x => x.TicketId == ticket.Id)
                    .Select(x => x.Number)
                    .ToList();

                ticketModels.Add(ticket.ToTicketModel(ticketDbList));
            }
            return ticketModels;

        }

        public TicketModel GetEntityById(int id)
        {
            Ticket ticketDb = _ticketRepository.GetById(id);
            if (ticketDb == null)
            {
                throw new NotFoundException($"Ticket with id {id} was not found");
            }

            List<int> ticketDbList = _ticketDetailsRepository
                    .GetAll()
                    .Where(x => x.TicketId == ticketDb.Id)
                    .Select(x => x.Number)
                    .ToList();

            return ticketDb.ToTicketModel(ticketDbList);
        }

        public void UpdateEntity(TicketModel entity)
        {
            throw new TicketException("No such option available");
        }

        //additional options

        public List<TicketCombinationModel> CheckMyTickets(string id)
        {

            User userDb = _userRepository
                .GetById(int.Parse(id));

            List<Session> sessionDb = _sessionRepository.GetAll().ToList();
            Session lastSession = sessionDb.LastOrDefault();

            List<Ticket> ticketsDb = _ticketRepository.GetAll().ToList();
            List<Ticket> myTickets = ticketsDb.Where(x => x.UserId == userDb.Id && x.SessionId == lastSession.Id).ToList();
            List<TicketCombinationModel> myTicketsModels = new List<TicketCombinationModel>();
            foreach (Ticket ticketinDb in myTickets)
            {
                List<int> ticketDb = _ticketDetailsRepository.GetAll().Where(x => x.TicketId == ticketinDb.Id).Select(x => x.Number).ToList();
                string ticketString = GetCombination(ticketDb);
                myTicketsModels.Add(ticketinDb.ToTicketCombinationModel(ticketString));
            }
            return myTicketsModels;
        }


        public List<TicketCombinationModel> GetTickets()
        {
            List<Session> sessionDb = _sessionRepository.GetAll().ToList();
            Session lastSession = sessionDb.LastOrDefault();

            List<Ticket> ticketsDb = _ticketRepository.GetAll().ToList();
            List<Ticket> myTickets = ticketsDb.Where(x => x.SessionId == lastSession.Id).ToList();
            List<TicketCombinationModel> myTicketsModels = new List<TicketCombinationModel>();
            foreach (Ticket ticketinDb in myTickets)
            {
                List<int> ticketDb = _ticketDetailsRepository.GetAll().Where(x => x.TicketId == ticketinDb.Id).Select(x => x.Number).ToList();
                string ticketString = GetCombination(ticketDb);
                myTicketsModels.Add(ticketinDb.ToTicketCombinationModel(ticketString));
            }
            return myTicketsModels;
        }

        public int GetUsersEnrolled()
        {
            List<Session> sessionDb = _sessionRepository.GetAll().ToList();
            Session lastSession = sessionDb.LastOrDefault();

            List<Ticket> ticketsDb = _ticketRepository.GetAll().ToList();
            List<Ticket> myTickets = ticketsDb.Where(x => x.SessionId == lastSession.Id).ToList();
            int usersEnrolled = myTickets.GroupBy(x => x.UserId).Count();

            return usersEnrolled;
        }

        //validations

        private void ValidateTicket(TicketModel entity)
        {
            if (int.Parse(entity.One) <= 0 || int.Parse(entity.Two) <= 0 || int.Parse(entity.Three) <= 0 || int.Parse(entity.Four) <= 0 || int.Parse(entity.Five) <= 0 || int.Parse(entity.Six) <= 0 || int.Parse(entity.Seven) <= 0)
            {
                throw new TicketException("Number can't be 0 or below 0!");

            }
            if (int.Parse(entity.One) > 37 || int.Parse(entity.Two) > 37 || int.Parse(entity.Three) > 37 || int.Parse(entity.Four) > 37 || int.Parse(entity.Five) > 37 || int.Parse(entity.Six) > 37 || int.Parse(entity.Seven) > 37)
            {
                throw new TicketException("Number can't be above 37!");

            }


        }

        private void CheckSession(Session sessionDb)
        {

            if (sessionDb == null)
            {
                throw new NotFoundException($"This option is not available at the moment! Try again later!");
            };
            if (sessionDb.Start.Hour != sessionDb.End.Hour || sessionDb.Start.Minute != sessionDb.End.Minute)
            {
                throw new SessionException($"Session has ended, wait until the next session begins to apply for tickets!");
            };

        }

        private void ValidateUser(string role)
        {
            if (role != "2")
            {
                throw new TicketException("Invalid info! Administrators can not play the lottary!");
            }
        }

        //private methods

        private List<int> GenerateAutomatic()
        {
            List<int> ticketNumbers = new List<int>();
            System.Random random = new System.Random();
            for (int i = 0; i <= 6; i++)
            {
                int number = random.Next(1, 37);
                while (ticketNumbers.Contains(number))
                {
                    number = random.Next(1, 37);
                }
                ticketNumbers.Add(number);
            }


            return ticketNumbers;

        }


        private string GetCombination(List<int> ticketNumbers)
        {
            List<string> listOfStrings = new List<string>();
            foreach (int number in ticketNumbers)
            {

                string numberToString = number.ToString();
                listOfStrings.Add(numberToString);
            }

            return string.Join(",", listOfStrings);
        }

        public string GenerateTicket(TicketModel ticket, string id)
        {
            throw new NotImplementedException();
        }

    }
}
