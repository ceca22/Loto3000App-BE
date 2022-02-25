using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class DrawDetailsRepository:IDrawDetailsRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public DrawDetailsRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }


        public void Add(DrawDetails entity)
        {
            _loto3000AppDbContext.DrawDetails.Add(entity);
        }

        public void Delete(DrawDetails entity)
        {
            _loto3000AppDbContext.DrawDetails.Remove(entity);
        }

        public IQueryable<DrawDetails> GetAll()
        {
            return _loto3000AppDbContext
                .DrawDetails
                .AsQueryable();
        }

        public DrawDetails GetById(int id)
        {
            return _loto3000AppDbContext
                .DrawDetails
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(DrawDetails entity)
        {
            _loto3000AppDbContext.DrawDetails.Update(entity);

        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }

    }
}
