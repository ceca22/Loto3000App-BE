using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Mappers;
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
using System.Text.RegularExpressions;

namespace Loto3000App.Services.Implementations
{
    public class UserService : IService<UserRegisterModel>
    {
        private IUserRepository _userRepository;


        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }

        public void DeleteEntity(int id)
        {
            User deleteUser = _userRepository.GetById(id);
            if (deleteUser == null)
            {
                throw new NotFoundException($"User with Id {id} was not found");
            }
            _userRepository.Delete(deleteUser);
        }

        public List<UserRegisterModel> GetAllEntities()
        {
            List<User> userDb =  _userRepository.GetAll().ToList();
            List<UserRegisterModel> registerUserModels = new List<UserRegisterModel>();
            foreach (User user in userDb)
            {
                registerUserModels.Add(user.ToUserRegisterModel());
            }
            return registerUserModels;

        }

        public UserRegisterModel GetEntityById(int id)
        {
            User userDb = _userRepository.GetById(id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with id {id} was not found");
            }
            return userDb.ToUserRegisterModel();
            
        }


        
        public void UpdateEntity(UserRegisterModel userRegisterModel)
        {
            User userDb = _userRepository.GetById(userRegisterModel.Id);
            if (userDb == null)
            {
                throw new NotFoundException($"User with Id {userRegisterModel.Id} was not found");
            }

            if (string.IsNullOrEmpty(userRegisterModel.Username) || string.IsNullOrEmpty(userRegisterModel.Password) || string.IsNullOrEmpty(userRegisterModel.FirstName) || string.IsNullOrEmpty(userRegisterModel.LastName))
            {
                throw new UserException("The properties Username,Password,Firstname and Lastname are required");
            }
            if (userRegisterModel.Username.Length > 30)
            {
                throw new UserException("The property Username can't be longer than 30 characters!");
            }
            if (userRegisterModel.FirstName.Length > 50 || userRegisterModel.LastName.Length > 50)
            {
                throw new UserException("Firstname and Lastname can contain maximum 50 characters!");
            }



            if (!IsPasswordValid(userRegisterModel.Password))
            {
                throw new UserException("The password should be more than 5 character and must contain numbers as well!");
            }

            var md5 = new MD5CryptoServiceProvider();

            var md5Data = md5.ComputeHash(Encoding.ASCII.GetBytes(userRegisterModel.Password));

            var hashedPassword = Encoding.ASCII.GetString(md5Data);

            userDb.FirstName = userRegisterModel.FirstName;
            userDb.LastName = userRegisterModel.LastName;
            userDb.Username = userRegisterModel.Username;
            userDb.Password = hashedPassword;

            _userRepository.Update(userDb);


        }

        public void AddEntity(UserRegisterModel entity)
        {
            throw new NotImplementedException();
        }

        private bool IsPasswordValid(string password)
        {
            Regex passwordRegex = new Regex("^(?=.*[0-9])(?=.*[a-z]).{6,20}$");
            return passwordRegex.Match(password).Success;
        }
    }
}
