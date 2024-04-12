using System.ComponentModel.DataAnnotations;

namespace Corp4Sem4.Models
{
    public class SendMessage
    {
        public string To { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
