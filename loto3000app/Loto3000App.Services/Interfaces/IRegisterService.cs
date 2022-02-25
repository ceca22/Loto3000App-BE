using Loto3000App.Models.UserEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface IRegisterService
    {
        void Register(UserRegisterModel userEntityRegisterModel);
    }
}
