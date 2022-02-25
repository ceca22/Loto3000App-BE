using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class WinningRepository:IWinningRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;
        public WinningRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }

        public void Add(Winning entity)
        {
            _loto3000AppDbContext.Winnings.Add(entity);
        }

        public void Delete(Winning entity)
        {
            _loto3000AppDbContext.Winnings.Remove(entity);
        }

        public IQueryable<Winning> GetAll()
        {
            return _loto3000AppDbContext
                .Winnings
                .AsQueryable();
        }

        public Winning GetById(int id)
        {
            return _loto3000AppDbContext
                .Winnings
                .FirstOrDefault(x => x.Id == id);
            
        }

        
        public void Update(Winning entity)
        {
            _loto3000AppDbContext.Winnings.Update(entity);
        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }
    }
}
