using Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.Winning
{
    public class BoardModel
    {
        public string Draw { get; set; }
        public int Id { get; set; }
        public User User { get; set; }
        public string WinningTicket { get; set; }
        public string Prize { get; set; }
        public int SessionId { get; set; }
    }
}
