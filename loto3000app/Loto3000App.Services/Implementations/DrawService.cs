using Loto3000App.DataAccess.Implementations;
using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
using Loto3000App.Models.Draw;
using Loto3000App.Models.Session;
using Loto3000App.Models.Winning;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class DrawService:IDrawService
    {
        private IUserRepository _userRepository;
        private ITicketRepository _ticketRepository;
        private ITicketDetailsRepository _ticketDetailsRepository;
        private ISessionRepository _sessionRepository;
        private IDrawRepository _drawRepository;
        private IDrawDetailsRepository _drawDetailsRepository;
        private IPrizeRepository _prizeRepository;
        private IWinningRepository _winningRepository;


        public DrawService(IUserRepository userRepository, ITicketRepository ticketRepository, ITicketDetailsRepository ticketDetailsRepository, ISessionRepository sessionRepository, IDrawRepository drawRepository, IDrawDetailsRepository drawDetailsRepository, IPrizeRepository prizeRepository, IWinningRepository winningRepository)
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

        public string AddEntity(string id, List<string> roles)
        {
            if (!roles.Contains("1"))
            {
                throw new TicketException("Only Administrators are authorized for this action!");
            }

            Session lastSessionDb = _sessionRepository
                                    .GetAll()
                                    .ToList()
                                    .LastOrDefault();

            User admin = _userRepository
                            .GetAll()
                            .ToList()
                            .FirstOrDefault(x => x.Id == Convert.ToInt64(id));
            
            DrawValidations(id, lastSessionDb);

            List<int> drawList = GenerateAutomatic();



            Draw draw = new Draw()
            {
                Session = lastSessionDb,
                DrawDetails = drawList.Select(dl => new DrawDetails()
                {
                    Number = dl
                }).ToList()
            };
            

            _drawRepository.Add(draw);
            _drawRepository.SaveChanges();
            _drawDetailsRepository.SaveChanges();



            string combination = GetCombinationDraw(drawList);
            return combination;
        }

       

        private void DrawValidations(string id, Session lastSession)
        {
            Draw lastDraw = _drawRepository.GetAll().ToList().LastOrDefault();
            
            if (lastSession.Start.Minute == lastSession.End.Minute && lastSession.Start.Hour == lastSession.End.Hour && lastSession.Start.Date == lastSession.End.Date)
            {
                throw new SessionException("You can't initiate a draw if the session is still open!");

            }
            if(lastDraw != null)
            {
                if (lastDraw.SessionId == lastSession.Id)
                {
                    throw new DrawException($"Draw has already been made for session {lastSession.Id}!");
                }
            }
            
            if (Convert.ToInt64(id) != lastSession.UserId)
            {
                throw new SessionException($"Only the Admin that started the session can start a draw!");
            }
            
            
            
        }
        
       
        public void DeleteEntity(int id)
        {
            Draw drawDelete = _drawRepository.GetById(id);
            if (drawDelete == null)
            {
                throw new NotFoundException($"Draw with id {id} was not found");
            }
            _drawRepository.Delete(drawDelete);

        }

        public List<DrawCombinationModel> GetAllEntities()
        {
            List<Draw> drawDb = _drawRepository.GetAll().ToList();
            List<DrawCombinationModel> drawModels = new List<DrawCombinationModel>();

            
            foreach (Draw draw in drawDb)
            {
                List<int> drawList = _drawDetailsRepository
                .GetAll()
                .Where(x => x.DrawId == draw.Id)
                .Select(x => x.Number)
                .ToList();

                string drawString = GetCombination(drawList);


                drawModels.Add(draw.ToDrawCombinationModel(drawString));
            }
            return drawModels;
        }

        public bool DrawIsMade()
        {
            Draw lastDraw = _drawRepository.GetAll().ToList().LastOrDefault();
            Session lastSession = _sessionRepository.GetAll().ToList().LastOrDefault();
            if(lastDraw.SessionId == lastSession.Id)
            {
                return true;
            }
            return false;
        }

        public DrawCombinationModel GetEntityById(int id)
        {
            Draw draw = _drawRepository.GetById(id);
            if (draw == null)
            {
                throw new NotFoundException($"Draw with id {id} was not found");
            }

            List<int> drawList = _drawDetailsRepository
            .GetAll()
            .Where(x => x.DrawId == draw.Id)
            .Select(x => x.Number)
            .ToList();

            string drawString = GetCombination(drawList);
            return draw.ToDrawCombinationModel(drawString);
        }

        public void UpdateEntity(DrawModel entity)
        {
            throw new DrawException("No such option available!");
        }


        //private methods
        private List<int> GenerateAutomatic()
        {
            List<int> drawNumbers = new List<int>();
            System.Random random = new System.Random();

            for (int i = 0; i <= 7; i++)
            {
                int number = random.Next(1, 37);
                while (drawNumbers.Contains(number))
                {
                    number = random.Next(1, 37);
                }
                drawNumbers.Add(number);
            }


            return drawNumbers;
        }

        private string GetCombinationDraw(List<int> listOfNumbers)
        {
            List<string> listOfStrings = new List<string>();
            foreach (int number in listOfNumbers)
            {
                string numberToString = number.ToString();
                listOfStrings.Add(numberToString);
            }
            return string.Join(",", listOfStrings);


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





    }

    

}
