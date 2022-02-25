using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loto3000App.Domain.Models
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Start { get; set; } = new DateTime();
        public DateTime End { get; set; } = new DateTime();

        public User User { get; set; }
        public int UserId { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    }
}
