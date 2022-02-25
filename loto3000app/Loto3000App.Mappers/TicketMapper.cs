using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.Ticket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Loto3000App.Mappers
{
    public static class Ticketmapper
    {
       

        public static Ticket ToTicket(this TicketModel ticketModel)
        {

            return new Ticket
            {
                Id = ticketModel.Id,
                UserId = int.Parse(ticketModel.UserId),
                SessionId = int.Parse(ticketModel.SessionId)


            };
        }


        public static TicketModel ToTicketModel(this Ticket ticket, List<int> ticketDb)
        {

            return new TicketModel
            {
                Id = ticket.Id,
                One = ticketDb[0].ToString(),
                Two = ticketDb[1].ToString(),
                Three = ticketDb[2].ToString(),
                Four = ticketDb[3].ToString(),
                Five = ticketDb[4].ToString(),
                Six = ticketDb[5].ToString(),
                Seven = ticketDb[6].ToString(),
                UserId = ticket.UserId.ToString(),
                SessionId = ticket.SessionId.ToString(),

            };
        }



        public static TicketCombinationModel ToTicketCombinationModel(this Ticket ticket, string ticketDb)
        {
            

            return new TicketCombinationModel
            {
                Id = ticket.Id,
                Combination = ticketDb,
                UserId = ticket.UserId,
                SessionId = ticket.SessionId,

            };
        }





    }
}
