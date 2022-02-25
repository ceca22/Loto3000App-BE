using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Loto3000App.Domain.Models
{
    public class DrawDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        

        //[ForeignKey("Draw")]
        public int DrawId { get; set; }
        public Draw Draw { get; set; }

        public int Number { get; set; }
    }
}
