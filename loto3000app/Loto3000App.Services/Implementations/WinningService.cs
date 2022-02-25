using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
using Loto3000App.Models.Winning;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class WinningService:IWinningService
    {
        private IUserRepository _userRepository;
        private ITicketRepository _ticketRepository;
        private ITicketDetailsRepository _ticketDetailsRepository;
        private ISessionRepository _sessionRepository;
        private IDrawRepository _drawRepository;
        private IDrawDetailsRepository _drawDetailsRepository;
        private IPrizeRepository _prizeRepository;
        private IWinningRepository _winningRepository;


        public WinningService(IUserRepository userRepository, ITicketRepository ticketRepository, ITicketDetailsRepository ticketDetailsRepository, ISessionRepository sessionRepository, IDrawRepository drawRepository, IDrawDetailsRepository drawDetailsRepository, IPrizeRepository prizeRepository, IWinningRepository winningRepository)
        {
            _userRepository = userRepository;
            _ticketRepository = ticketRepository;
            _ticketDetailsRepository = ticketDetailsRepository;
            _sessionRepository = sessionRepository;
            _drawRepository = drawRepository;
            _drawDetailsRepository = drawDetailsRepository;
            _prizeRepository = prizeRepository;
            _winningRepository = winningRepository;


        }

        public void AddEntity(WinningModel entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }
        public List<WinningModel> GetAllEntities()
        {
            throw new NotImplementedException();
        }

        public WinningModel GetEntityById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateEntity(WinningModel entity)
        {
            throw new NotImplementedException();
        }

        //additional methods
        public bool FindWinners()
        {
            Draw lastDrawDb = _drawRepository
                                .GetAll()
                                .ToList()
                                .LastOrDefault();


            List<int> drawList = _drawDetailsRepository
                                    .GetAll()
                                    .ToList()
                                    .Where(x => x.DrawId == lastDrawDb.Id)
                                    .Select(x => x.Number)
                                    .ToList();

            List<Ticket> ticketDb = _ticketRepository
                                    .GetAll()
                                    .ToList()
                                    .Where(x => x.SessionId == lastDrawDb.SessionId)
                                    .ToList();

            if (ticketDb == null)
            {
                throw new WinningException("No tickets for the last session!");
            }

            List<TicketDetails> listOfTicketNumbers = _ticketDetailsRepository.GetAll().ToList();

            foreach (Ticket ticket in ticketDb)
            {

                List<int> ticketInPlay = listOfTicketNumbers
                                            .Where(x => x.TicketId == ticket.Id)
                                            .Select(x => x.Number)
                                            .ToList();

                int counter = 0;
                foreach (int number in ticketInPlay)
                {
                    if (drawList.Contains(number))
                    {
                        counter++;
                    }
                }


                //User userDb = _userRepository.GetById(ticket.UserId);

                Winning newWinner = new Winning()
                {
                    
                    TicketId = ticket.Id
                };

                if (counter > 2)
                {
                    if (counter == 3)
                    {
                        newWinner.PrizeId = 1;
                    }
                    if (counter == 4)
                    {
                        newWinner.PrizeId = 2;
                    }
                    if (counter == 5)
                    {
                        newWinner.PrizeId = 3;
                    }
                    if (counter == 6)
                    {
                        newWinner.PrizeId = 4;
                    }
                    if (counter == 7)
                    {
                        newWinner.PrizeId = 5;
                    }
                    int prizeId = newWinner.PrizeId;
                    _winningRepository.Add(newWinner);
                }

                counter = 0;

            }

            _winningRepository.SaveChanges();
            return true;
        }

       

        public List<BoardModel> WinnersBoard()
        {
            //get the winning draw combination
            Draw drawDb = _drawRepository
                            .GetAll()
                            .ToList()
                            .LastOrDefault();


            List<int> drawList = _drawDetailsRepository
                                        .GetAll()
                                        .ToList()
                                        .Where(x => x.DrawId == drawDb.Id)
                                        .Select(x => x.Number)
                                        .ToList();

            string drawCombination = GetCombination(drawList);

            //get the WINNING TICKETS
            Session lastSession = _sessionRepository
                                        .GetAll()
                                        .ToList()
                                        .LastOrDefault();

            List<Ticket> lastSessionTickets = _ticketRepository
                                            .GetAll()
                                            .ToList()
                                            .Where(x => x.SessionId == lastSession.Id)
                                            .ToList();



            List<Winning> winningTickets = new List<Winning>();
            
            foreach(Ticket ticket in lastSessionTickets)
            {
                Winning winningTicket = _winningRepository
                .GetAll()
                .ToList()
                .FirstOrDefault(x => x.TicketId == ticket.Id);

                if(winningTicket != null)
                {
                    winningTickets.Add(winningTicket);
                }
            }
           
            
            //create board model
            List<BoardModel> winningsModels = new List<BoardModel>();

            

            foreach (Winning winning in winningTickets)
            {
                List<int> winningTicketDetails = _ticketDetailsRepository
                    .GetAll()
                    .ToList()
                    .Where(x => x.TicketId == winning.TicketId)
                    .Select(x => x.Number)
                    .ToList();


                
                string winningTicket = GetCombination(winningTicketDetails);

                Ticket ticket = _ticketRepository
                            .GetAll()
                            .ToList()
                            .FirstOrDefault(x => x.Id == winning.TicketId);

                User user = _userRepository
                                .GetAll()
                                .ToList()
                                .FirstOrDefault(x => x.Id == ticket.UserId);

                Prize prize = _prizeRepository.GetById(winning.PrizeId);
                winningsModels.Add(winning.ToBoardModel(drawCombination, winningTicket, user, prize, ticket));

            }

            return winningsModels;
        }


        //private methods
        private string GetCombination(List<int> listOfNumbers)
        {
            List<string> listOfStrings = new List<string>();
            foreach (int number in listOfNumbers)
            {
                string numberToString = number.ToString();
                listOfStrings.Add(numberToString);
            }
            return string.Join(",", listOfStrings);


        }

        public int GetGiftCardFifty()
        {
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            List<Ticket> tickets = _ticketRepository.GetAll().ToList().Where(x => x.SessionId == lastSession.Id).ToList(); 
            List<Winning> winnings = _winningRepository.GetAll().ToList();
            List<Winning> giftCardWinnings = new List<Winning>();
            foreach(Winning winning in winnings)
            {
                foreach(Ticket ticket in tickets)
                {
                    if(winning.TicketId == ticket.Id && winning.PrizeId == 1)
                    {
                        giftCardWinnings.Add(winning);
                    }
                }
            }
            return giftCardWinnings.Count;
        }

        public int GetGiftCardHundred()
        {
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            List<Ticket> tickets = _ticketRepository.GetAll().ToList().Where(x => x.SessionId == lastSession.Id).ToList();
            List<Winning> winnings = _winningRepository.GetAll().ToList();
            List<Winning> giftCard100Winnings = new List<Winning>();
            foreach (Winning winning in winnings)
            {
                foreach (Ticket ticket in tickets)
                {
                    if (winning.TicketId == ticket.Id && winning.PrizeId == 2)
                    {
                        giftCard100Winnings.Add(winning);
                    }
                }
            }
            return giftCard100Winnings.Count;
        }

        public int GetVacation()
        {
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            List<Ticket> tickets = _ticketRepository.GetAll().ToList().Where(x => x.SessionId == lastSession.Id).ToList();
            List<Winning> winnings = _winningRepository.GetAll().ToList();
            List<Winning> vacationWinnings = new List<Winning>();
            foreach (Winning winning in winnings)
            {
                foreach (Ticket ticket in tickets)
                {
                    if (winning.TicketId == ticket.Id && winning.PrizeId == 4)
                    {
                        vacationWinnings.Add(winning);
                    }
                }
            }
            return vacationWinnings.Count;
        }

        public int GetTv()
        {
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            List<Ticket> tickets = _ticketRepository.GetAll().ToList().Where(x => x.SessionId == lastSession.Id).ToList();
            List<Winning> winnings = _winningRepository.GetAll().ToList();
            List<Winning> tvWinnings = new List<Winning>();
            foreach (Winning winning in winnings)
            {
                foreach (Ticket ticket in tickets)
                {
                    if (winning.TicketId == ticket.Id && winning.PrizeId == 3)
                    {
                        tvWinnings.Add(winning);
                    }
                }
            }
            return tvWinnings.Count;
        }

        public int GetCar()
        {
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            List<Ticket> tickets = _ticketRepository.GetAll().ToList().Where(x => x.SessionId == lastSession.Id).ToList();
            List<Winning> winnings = _winningRepository.GetAll().ToList();
            List<Winning> carWinnings = new List<Winning>();
            foreach (Winning winning in winnings)
            {
                foreach (Ticket ticket in tickets)
                {
                    if (winning.TicketId == ticket.Id && winning.PrizeId == 2)
                    {
                        carWinnings.Add(winning);
                    }
                }
            }
            return carWinnings.Count;
        }
    }
}
