using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.DataAccess.Interfaces
{
    public interface IUserRepository:IRepository<User>
    {
       User GetByUsername(string username);
       User Login(string username, string password);
    }
}
