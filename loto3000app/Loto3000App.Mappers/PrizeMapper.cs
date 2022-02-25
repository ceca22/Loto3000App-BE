using Loto3000App.Domain.Models;
using Loto3000App.Models.Prize;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class PrizeMapper
    {
        public static Prize ToPrize(this PrizeModel prizeModel)
        {
            return new Prize
            {
                Id = prizeModel.Id,
                PrizeType = prizeModel.PrizeType


            };
        }




        public static PrizeModel ToPrizeModel(this Prize prize)
        {
            return new PrizeModel
            {
                Id = prize.Id,
                PrizeType = prize.PrizeType

            };
        }
    }
}
