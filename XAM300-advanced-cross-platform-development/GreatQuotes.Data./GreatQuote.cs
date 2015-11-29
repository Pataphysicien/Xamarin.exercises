namespace GreatQuotes
{
	public class GreatQuote
	{
		public string Author { get; set; }
		public string QuoteText { get; set; }

		public GreatQuote() : this("Unknown","Quote goes here..")
		{
		}

		public GreatQuote(string author, string quoteText)
		{
			Author = author;
			QuoteText = quoteText;
		}
	}
}