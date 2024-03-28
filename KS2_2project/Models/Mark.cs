using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KS2_2project.Models
{
    public class Mark
    {
        [Key]
        public int Id { get; set; }
        public int Points { get; set; }
        public Criteria Criteria { get; set; }
        public int CriteriaId { get; set; }
        public float Date { get; set; }
        public Estate Estate { get; set; }
        public int EstateId { get; set; }
    }
}
