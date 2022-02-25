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
    //public interface IDrawRepository : IRepository<Draw>
    //{
    //    IQueryable<Draw> GetAllQueryable();
    //}

    public class DrawRepository: IDrawRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public DrawRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }

        
        public void Add(Draw entity)
        {
            _loto3000AppDbContext.Draws.Add(entity);
        }

        public void Delete(Draw entity)
        {
            _loto3000AppDbContext.Draws.Remove(entity);
        }


        public IQueryable<Draw> GetAll()
        {
            return _loto3000AppDbContext
                .Draws
                .Include(x => x.Session)
                .AsQueryable();
        }

        public Draw GetById(int id)
        {
            return _loto3000AppDbContext
                .Draws
                .Include(x => x.Session)
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Draw entity)
        {
            _loto3000AppDbContext.Draws.Update(entity);

        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }

    }
}
