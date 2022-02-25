using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.Session
{
    public class SessionModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Start { get; set; } 
        public DateTime End { get; set; } 
    }
}
