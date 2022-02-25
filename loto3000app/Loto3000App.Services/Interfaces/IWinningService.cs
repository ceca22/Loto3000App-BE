using Loto3000App.Models.Winning;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Services.Interfaces
{
    public interface IWinningService:IService<WinningModel>
    {
        void FindWinners();
        List<BoardModel> WinnersBoard();

        int GetGiftCardFifty();
        int GetGiftCardHundred();
        int GetVacation();
        int GetTv();

        int GetCar();



    }
}
