using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
using Loto3000App.Models.Session;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private IUserRepository _userRepository;
        private ISessionRepository _sessionRepository;
        private IDrawRepository _drawRepository;


        public SessionService( IUserRepository userRepository, ISessionRepository sessionRepository, IDrawRepository drawRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _drawRepository = drawRepository;

    }

        //when starting the session SessionController will activate this method
        public void AddEntity(List<string> roles, string id)
        {
            if(roles.Contains("1"))
            {
                Session lastSessionDb = _sessionRepository
                                        .GetAll()
                                        .ToList()
                                        .LastOrDefault();

               
                if (lastSessionDb != null)
                {
                    CheckSessionStatus(lastSessionDb);
                    CheckDraw(lastSessionDb);
                } 

                User admin = _userRepository
                            .GetAll()
                            .ToList()
                            .FirstOrDefault(x => x.Id == Convert.ToInt16(id));

                Session createSession = new Session()
                {
                    Start = DateTime.Now,
                    End = DateTime.Now,
                    User = admin

                };
                _sessionRepository.Add(createSession);
                _sessionRepository.SaveChanges();
            }
            else
            {
                throw new SessionException("Something went wrong!");
            }
        }

       

        public void DeleteEntity(int id)
        {
            Session deleteSession = _sessionRepository.GetById(id);
            if (deleteSession == null)
            {
                throw new NotFoundException($"Session with id {id} was not found");
            }
            _sessionRepository.Delete(deleteSession);
            _sessionRepository.SaveChanges();
        }

        public List<SessionModel> GetAllEntities()
        {
            List<Session> sessionsDb = _sessionRepository.GetAll().ToList();
            List<SessionModel> sessionModels = new List<SessionModel>();
            foreach (Session session in sessionsDb)
            {
                sessionModels.Add(session.ToSessionModel());
            }
            return sessionModels;
        }

        public SessionModel GetEntityById(int id)
        {
            Session sessionsDb = _sessionRepository.GetById(id);
            if (sessionsDb == null)
            {
                throw new NotFoundException($"Session with Id {id} was not found");
            }
            return sessionsDb.ToSessionModel();
            
        }

        

        //when ending the session SessionController will activate this method 
        public void UpdateEntity(string id)
        {
            Session lastSessionDb = _sessionRepository
                                    .GetAll()
                                    .ToList()
                                    .LastOrDefault(); 

            if(lastSessionDb.Start.Minute == DateTime.Now.Minute)
            {
               throw new SessionException("You can't end the session in less than a minute when you started it!");
            }

            EndSessionValidations(id, lastSessionDb);

            lastSessionDb.End = DateTime.Now;
            _sessionRepository.Update(lastSessionDb);
            _sessionRepository.SaveChanges();
        }

        public SessionModel Info()
        {
            return _sessionRepository
                    .GetAll()
                    .ToList()
                    .LastOrDefault()
                    .ToSessionModel();
            
        }

        public bool SessionStatus()
        
        {
            Session session = _sessionRepository
                    .GetAll()
                    .ToList()
                    .LastOrDefault();
            if(session == null)
            {
                throw new NotFoundException($"No such data!");

            }

            if (session.Start.Minute == session.End.Minute && session.Start.Hour == session.End.Hour && session.Start.Date == session.End.Date)
            {
                return true;
            }
            return false;
        }



        //validation
        private void CheckSessionStatus(Session session)
        {
           
                
            if (session.Start.Minute == session.End.Minute && session.Start.Hour == session.End.Hour && session.Start.Date == session.End.Date)
            {
                throw new SessionException("You can't start a new session before ending the last one!");

            }
            
        }

        

        private void CheckDraw(Session lastSession)
        {
            Draw draw = _drawRepository
                        .GetAll()
                        .ToList()
                        .LastOrDefault();

            if (draw.SessionId != lastSession.Id)
            {
                throw new SessionException("You can't start a new session before having a Draw for the last session!");
            }
        }

        private void EndSessionValidations(string id, Session lastSession)
        {

            if (lastSession.UserId.ToString() != id)
            {
                throw new SessionException($"Only the Admin that started the session can end the session!");
            }
            if (lastSession.Start.Hour != lastSession.End.Hour || lastSession.Start.Minute != lastSession.End.Minute)
            {
                throw new SessionException($"There are no open sessions at the moment!");
            }


        }


        


    }
}
