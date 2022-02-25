using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.UserEntity
{
    public class AuthenticateResponse
    {

        //public int Id { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(string token)
        {
           
            Token = token;
        }

    }
}
