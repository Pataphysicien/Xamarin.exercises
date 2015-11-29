using System;
using System.Collections.Generic;
using System.Linq;

namespace GreatQuotes.Data
{
    public class QuoteManager
    {
        static readonly Lazy<QuoteManager> _instance = 
            new Lazy<QuoteManager>(() => new QuoteManager());


        public static QuoteManager Instance
        {         
            get { return _instance.Value; }
        }



        readonly IQuoteRepository _repo;

        QuoteManager()
        {
            _repo = QuoteRepositoryFactory.Create();
            Quotes = _repo.Load().ToList();
        }
            
        public List<GreatQuote> Quotes{ get; set; }

        public void Save()
        {
            _repo.Save(Quotes);
        }
    }
}

