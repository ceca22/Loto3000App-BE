using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    
    public class SessionRepository:ISessionRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public SessionRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }

        public void Add(Session entity)
        {
            _loto3000AppDbContext.Sessions.Add(entity);
        }

        public void Delete(Session entity)
        {
            _loto3000AppDbContext.Sessions.Remove(entity);
        }

        public IQueryable<Session> GetAll()
        {
            return _loto3000AppDbContext
                .Sessions
                .Include(x=>x.User)
                .AsQueryable();
        }

        public Session GetById(int id)
        {
            return _loto3000AppDbContext
                .Sessions
                .Include(x=>x.User)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Session entity)
        {
            _loto3000AppDbContext.Sessions.Update(entity);
        }


        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }
    }
}
