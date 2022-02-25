using Loto3000App.DataAccess.Implementations;
using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.Draw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class DrawMapper
    {
        private static IDrawDetailsRepository _drawDetailsRepository;
        public static Draw ToDraw(this DrawModel drawModel)
        {
            return new Draw
            {
                Id = drawModel.Id,
                SessionId = drawModel.SessionId


            };
        }

        public static DrawModel ToDrawModel(this Draw draw)
        {
            return new DrawModel
            {
                Id = draw.Id,
                SessionId = draw.SessionId,

            };
        }

        public static DrawCombinationModel ToDrawCombinationModel(this Draw draw, string drawList)
        {
            

            return new DrawCombinationModel
            {
                Id = draw.Id,
                Combination = drawList

            };
        }
    }
}
