using Loto3000App.Domain.Models;
using Loto3000App.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class SessionMapper
    {
        public static Session ToSession(this SessionModel sessionModel)
        {
            return new Session
            {
                Id = sessionModel.Id,
                Start = DateTime.Now,
                End = DateTime.Now,
                UserId = sessionModel.UserId


            };
        }




        public static SessionModel ToSessionModel(this Session session)
        {
            return new SessionModel
            {
                Id = session.Id,
                Start = session.Start,
                End = session.End,
                UserId = session.UserId

            };
        }

    }
}
