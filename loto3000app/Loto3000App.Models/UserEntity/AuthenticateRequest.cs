using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.UserEntity
{
    public class AuthenticateRequest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
