using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Corpa4Sem4.Database.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public int From { get; set; }
        public int To { get; set; }
        public string Title { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }

        public bool Status { get; set; }

    }
}
