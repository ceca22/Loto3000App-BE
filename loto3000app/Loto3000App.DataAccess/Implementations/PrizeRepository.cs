using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class PrizeRepository:IPrizeRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public PrizeRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }

        public void Add(Prize entity)
        {
            _loto3000AppDbContext.Prizes.Add(entity);
        }

        public void Delete(Prize entity)
        {
            _loto3000AppDbContext.Prizes.Remove(entity);
        }

        public IQueryable<Prize> GetAll()
        {
            return _loto3000AppDbContext
                    .Prizes
                    .AsQueryable();
        }

        public Prize GetById(int id)
        {
            return _loto3000AppDbContext
                .Prizes
                .FirstOrDefault(x => x.Id == id);
        }

        public void Update(Prize entity)
        {
            _loto3000AppDbContext.Prizes.Update(entity);
        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }
    }
}
