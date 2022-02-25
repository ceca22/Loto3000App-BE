using Loto3000App.Models.Draw;
using Loto3000App.Models.Winning;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface IDrawService
    {
        
        string AddEntity(string id, List<string> roles);
        void DeleteEntity(int id);

        List<DrawCombinationModel> GetAllEntities();

        DrawCombinationModel GetEntityById(int id);

        void UpdateEntity(DrawModel entity);

        bool DrawIsMade();

        string GetLastDraw();





    }
}
