namespace Corp4Sem4.Models
{
	public class MessageViewModel
	{
		public int Id { get; set; }
		public string ?Title { get; set; }
		public string ?Text { get; set; }
		public string ?From { get; set; }
		public DateTime Date { get; set; }

		public bool Status { get; set; }

	}
}
