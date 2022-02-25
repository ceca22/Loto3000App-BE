using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.UserEntity
{
    public class UserRegisterModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
        public int Role { get; set; }
    }
}
