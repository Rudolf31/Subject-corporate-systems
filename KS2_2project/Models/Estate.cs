using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS2_2project.Models
{
    public class Estate
    {
        [Key]
        public int Id { get; set; }
        public Area Area { get; set; }
        public int AreaId { get; set; } 
        public string Address { get; set; }
        public int Floor { get; set; }
        public int Rooms { get; set; }
        public Type Type { get; set; }
        public int TypeId { get; set; }
        public bool Status {get; set;}
        public float Cost { get; set; }
        public string Description { get; set; }
        public Material Material { get; set; }
        public int MaterialId { get; set; }
        public float Space { get; set; }
        public long Date { get; set; }
    }
}
