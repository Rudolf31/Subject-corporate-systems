using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corpa4Sem4.Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
		public string Login { get; set; }
		public string Password { get; set; }
		public string FullName { get; set; }
	}
}
