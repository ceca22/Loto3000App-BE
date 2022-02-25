using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.Ticket
{
    public class TicketCombinationModel
    {

        public int Id { get; set; }

        //public List<int> Combination = new List<int>();
        public string Combination { get; set; }
        public int UserId { get; set; }
        public int SessionId { get; set; }
    }
}
