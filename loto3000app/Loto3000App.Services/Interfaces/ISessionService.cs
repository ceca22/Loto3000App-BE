using Loto3000App.Models.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface ISessionService
    {

        void AddEntity(List<string> roles, string username);
        void DeleteEntity(int id);

        List<SessionModel> GetAllEntities();

        SessionModel GetEntityById(int id);

        void UpdateEntity(string id);

        SessionModel Info();

        bool SessionStatus();

    }
}
