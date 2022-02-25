using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class UserRepository : IUserRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;

        public UserRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }
        public void Add(User entity)
        {
            _loto3000AppDbContext.Users.Add(entity);
            
        }

        public void Delete(User entity)
        {
            _loto3000AppDbContext.Users.Remove(entity);
        }

        public IQueryable<User> GetAll()
        {
            return _loto3000AppDbContext
                .Users
                .AsQueryable();
        }

        public User GetById(int id)
        {
            return _loto3000AppDbContext
                .Users
                .FirstOrDefault(x => x.Id == id);
        }

        

        public void Update(User entity)
        {
            _loto3000AppDbContext.Users.Update(entity);
        }

        public void SaveChanges() 
        {
            _loto3000AppDbContext.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return _loto3000AppDbContext
                .Users
                .FirstOrDefault(x => x.Username == username);
        }

        public User Login(string username, string password)
        {
            return _loto3000AppDbContext
                .Users
                .FirstOrDefault(x => x.Username == username && x.Password == password);
        }

    }
}
