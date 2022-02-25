using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.Winning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class WinningMapper
    {

       

        public static Winning ToWinning(this WinningModel winningModel)
        {
            return new Winning
            {
                Id = winningModel.Id,
                TicketId = winningModel.TicketId,
                PrizeId = winningModel.PrizeId


            };
        }

        public static WinningModel ToWinningModel(this Winning winning)
        {
            return new WinningModel
            {
                Id = winning.Id,
                TicketId = winning.TicketId,
                PrizeId = winning.PrizeId


            };
        }

        public static BoardModel ToBoardModel(this Winning winning, string drawCombination, string winningCombination, User user, Prize prize, Ticket ticket)
        {
            

            return new BoardModel
            {
                Draw = drawCombination,
                Id = winning.Id,
                User = user,
                WinningTicket = winningCombination,
                Prize = prize.PrizeType,
                SessionId = ticket.SessionId,
                
            };
        }
    }

}

