using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Models.Draw
{
    public class DrawModel
    {
        public int Id { get; set; }
        public int One { get; set; }
        public int Two { get; set; }
        public int Three { get; set; }
        public int Four { get; set; }
        public int Five { get; set; }
        public int Six { get; set; }
        public int Seven { get; set; }
        public int Eight { get; set; }
        public string Combination { get; set; }
        public int SessionId { get; set; }
        public int UserId { get; set; }

    }
}
