using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KS2_2project.Models
{
    public class Type
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
