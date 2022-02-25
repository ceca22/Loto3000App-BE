using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loto3000App.Domain.Models
{
    public class Draw
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        //[ForeignKey("Session")]
        public int SessionId { get; set; }
        public Session Session { get; set; }

        public ICollection<DrawDetails> DrawDetails { get; set; } = new List<DrawDetails>();

    }
}
