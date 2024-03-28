using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS2_2project.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }
        public Estate Estate { get; set; }
        public int EstateId { get; set; }
        public Agent Agent { get; set; }
        public int AgentId { get; set; }
        public float Date { get; set; }
        public float Cost { get; set; }

    }
}
