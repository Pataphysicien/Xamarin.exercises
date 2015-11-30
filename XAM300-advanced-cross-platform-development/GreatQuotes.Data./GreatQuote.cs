using GreatQuotes.Data;

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

        public void SayQuote()
        {
            ITextToSpeech tts = ServiceLocator.Instance.Resolve<ITextToSpeech>();

            if(tts != null)
            {
                string text = QuoteText;
                if(!string.IsNullOrEmpty(Author))
                {
                    text += " by " + Author;
                }
                tts.Speak(text);
            }
        }
	}
}