using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Shared.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public TicketRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }
        public void Add(Ticket entity)
        {
            _loto3000AppDbContext.Tickets.Add(entity);
        }

        
        public void Delete(Ticket entity)
        {
            _loto3000AppDbContext.Tickets.Remove(entity);
        }

        
        
        public IQueryable<Ticket> GetAll()
        {
            return _loto3000AppDbContext
                .Tickets
                .AsQueryable();
        }

        
        //include session to have information in which session is this ticket created
        public Ticket GetById(int id)
        {
            return _loto3000AppDbContext
                .Tickets
                .FirstOrDefault(x => x.Id == id);
        }

       
        public void Update(Ticket entity)
        {
            _loto3000AppDbContext.Tickets.Update(entity);
            
        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }

    }
}
