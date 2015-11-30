using System;
using System.Collections.Generic;
using System.Linq;

namespace GreatQuotes.Data
{
    public class QuoteManager
    {
//        static readonly Lazy<QuoteManager> _instance = 
//            new Lazy<QuoteManager>(() => new QuoteManager());
//

//        public static QuoteManager Instance
//        {         
//            get { return _instance.Value; }
//        }
//
        public static QuoteManager Instance { get; private set; }

        readonly IQuoteRepository _repo;

//        QuoteManager()
//        {
//            _repo = QuoteRepositoryFactory.Create();
//            Quotes = _repo.Load().ToList();
//        }

        public QuoteManager(IQuoteRepository repo)
        {
            if (Instance != null)
                throw new Exception("Can only create a single QuoteManager.");
            Instance = this;

            this._repo = repo;
            Quotes = _repo.Load().ToList();
        }

            
        public List<GreatQuote> Quotes{ get; private set; }

        public void Save()
        {
            _repo.Save(Quotes);
        }
    }
}

