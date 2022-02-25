using Loto3000App.DataAccess.Implementations;
using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.UserEntity;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Custom;
using Loto3000App.Shared.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Loto3000App.Services.Implementations
{
    public class LoginService:ILoginService
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;

        private IOptions<AppSettings> _options;

        public LoginService(IUserRepository userRepository, IUserRoleRepository userRoleRepository, IOptions<AppSettings> options)
        {
            _userRoleRepository = userRoleRepository;
            _userRepository = userRepository;
            _options = options;
        }

        public string Login(AuthenticateRequest userLoginModel)
        {
            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userLoginModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);


            string username = _userRepository
                            .GetAll()
                            .Where(x => x.Username == userLoginModel.Username)
                            .Select(y=>y.Username)
                            .ToString();

            string password = _userRepository
                            .GetAll()
                            .Where(x => x.Password == userLoginModel.Password)
                            .Select(y => y.Password)
                            .ToString();

            ValidateInput(username, password);

            User userDb = _userRepository.Login(userLoginModel.Username, hashedPassword);
            List<UserRole> userRolesDb = _userRoleRepository.GetAll().Where(x=>x.UserId == userDb.Id).ToList();

            List<Claim> list = userRolesDb.Select(x => new Claim(ClaimTypes.Role, x.RoleId.ToString())).ToList();
            Claim[] listToArray = list.ToArray();

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //get the SecretKey from AppSettings
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_options.Value.SecretKey);
            //configure the token
            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddHours(10),
                //signature definition
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                //payload   

                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, userDb.Id.ToString()),
                        new Claim(ClaimTypes.Name, userDb.Username),
                        new Claim("userFullName", $"{userDb.FirstName} {userDb.LastName}"),

                    }

            )

           

            };

            foreach (Claim claim in listToArray)
            {
                securityTokenDescriptor.Subject.AddClaim(claim);
            }


            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            // convert it to string
            string tokenString = jwtSecurityTokenHandler.WriteToken(token);

            return tokenString;
            

        }




        private void ValidateInput(string username, string password)
        {
            if (username == null && password != null)
            {
                throw new NotFoundException($"Incorrect username!");
            }
            if (password == null && username != null)
            {
                throw new NotFoundException($"Incorrect password!");
            }
            if (password == null && username == null)
            {
                throw new NotFoundException($"No User found!");
            }

        }

    }
}
