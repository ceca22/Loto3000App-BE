using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.DataAccess.Implementations
{
    public class UserRoleRepository:IUserRoleRepository
    {
        private Loto3000AppDbContext _loto3000AppDbContext;

        public UserRoleRepository(Loto3000AppDbContext loto3000AppDbContext)
        {
            _loto3000AppDbContext = loto3000AppDbContext;
        }

        public void Add(UserRole entity)
        {
            _loto3000AppDbContext.UserRoles.Add(entity);

        }

        public void Delete(UserRole entity)
        {
            _loto3000AppDbContext.UserRoles.Remove(entity);

        }

        public IQueryable<UserRole> GetAll()
        {
            return _loto3000AppDbContext
                .UserRoles
                .AsQueryable();
        }

        public UserRole GetById(int id)
        {
            return _loto3000AppDbContext
                .UserRoles
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveChanges()
        {
            _loto3000AppDbContext.SaveChanges();
        }

        public void Update(UserRole entity)
        {
            _loto3000AppDbContext.UserRoles.Update(entity);

        }
    }
}
