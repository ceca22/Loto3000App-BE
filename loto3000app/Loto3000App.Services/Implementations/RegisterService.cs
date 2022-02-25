using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.UserEntity;
using Loto3000App.Services.Interfaces;
using Loto3000App.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Loto3000App.Services.Implementations
{
    public class RegisterService:IRegisterService
    {
       
        private IUserRepository _userRepository;
        //private IUserRoleRepository<UserRole> _userRoleRepository;


        public RegisterService(IUserRepository userRepository)
        {
            
            _userRepository = userRepository;
        }

        public void Register(UserRegisterModel userRegisterModel)
        {
            ValidateUser(userRegisterModel);

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userRegisterModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            User user = new User()
            {
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Username = userRegisterModel.Username,
                Password = hashedPassword,
                UserRoles = new List<UserRole>()
                {
                    new UserRole()
                    {
                        RoleId = 2
                    }
                }
            };


            _userRepository.Add(user);
            _userRepository.SaveChanges();
           


        }

        private void ValidateUser(UserRegisterModel userRegisterModel)
        {
            if (string.IsNullOrEmpty(userRegisterModel.Username) || string.IsNullOrEmpty(userRegisterModel.Password) || string.IsNullOrEmpty(userRegisterModel.FirstName) || string.IsNullOrEmpty(userRegisterModel.LastName))
            {
                throw new UserException("The properties Username,Password,Firstname and Lastname are required fields");
            }
            if (userRegisterModel.Username.Length > 30)
            {
                throw new UserException("Username can contain max 30 characters");
            }
            if (userRegisterModel.FirstName.Length > 50 || userRegisterModel.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }
            if (!IsUserNameUnique(userRegisterModel.Username, _userRepository))
            {
                throw new UserException("A user with this username already exists!");
            }
            if (userRegisterModel.Password != userRegisterModel.ConfirmedPassword)
            {
                throw new UserException("The passwords do not match!");
            }
            if (!IsPasswordValid(userRegisterModel.Password))
            {
                throw new UserException("The password should be more than 5 character and should contain numbers as well!");
            }
        }


        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }

        private bool IsUserNameUnique(string username, IUserRepository userRepository)
        {
            if (userRepository.GetByUsername(username) != null)
            {
                return false;
            }
            return true;
        }




    }
}
