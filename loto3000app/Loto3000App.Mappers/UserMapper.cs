using Loto3000App.Domain.Models;
using Loto3000App.Models.UserEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class UserMapper
    {

        public static User ToUser(this UserRegisterModel userRegisterModel)
        {
            return new User
            {
                Id = userRegisterModel.Id,
                FirstName = userRegisterModel.FirstName,
                LastName = userRegisterModel.LastName,
                Username = userRegisterModel.Username,
                Password = userRegisterModel.Password
                

            };
        }

        public static UserRegisterModel ToUserRegisterModel(this User user)
        {
            return new UserRegisterModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Password = user.Password,
                ConfirmedPassword = user.Password,
            };
        }
    }
}
