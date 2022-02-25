using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class TicketDetailsRepository : ITicketDetailsRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public TicketDetailsRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }
        public void Add(TicketDetails entity)
        {
            _loto3000AppDbContext.TicketDetails.Add(entity);
        }


        public void Delete(TicketDetails entity)
        {
            _loto3000AppDbContext.TicketDetails.Remove(entity);
        }



        public IQueryable<TicketDetails> GetAll()
        {
            return _loto3000AppDbContext
                    .TicketDetails
                    .AsQueryable();
        }

        public TicketDetails GetById(int id)
        {
            return _loto3000AppDbContext
                    .TicketDetails
                    .FirstOrDefault(x => x.Id == id);
        }

        public void Update(TicketDetails entity)
        {
            _loto3000AppDbContext.TicketDetails.Update(entity);
        }


        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }
    }

}
