using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loto3000App.Domain.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        //[ForeignKey("Session")]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public ICollection<TicketDetails> TicketDetails { get; set; }  = new List<TicketDetails>();
        public ICollection<Winning> Winnings { get; set; } = new List<Winning>();

    }
}
