using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.Winning
{
    public class WinningModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TicketId { get; set; }
        public int DrawId { get; set; }
        public int PrizeId { get; set; }

    }
}
