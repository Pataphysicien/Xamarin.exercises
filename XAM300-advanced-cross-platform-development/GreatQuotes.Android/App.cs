using Android.App;
using System;
using Android.Runtime;
using System.Collections.Generic;
using System.Linq;
using GreatQuotes.Data;

namespace GreatQuotes
{
	[Application(Icon="@drawable/icon", Label="@string/app_name")]
	public class App : Application
	{
//		static QuoteLoader quoteLoader;
//		public static List<GreatQuote> Quotes { get; private set; }
//
		public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
		{
		}

		public override void OnCreate()
		{
            QuoteRepositoryFactory.Create = () => new QuoteLoader();


			base.OnCreate();
//			quoteLoader = new QuoteLoader();
//			Quotes = quoteLoader.Load().ToList();
//
//            QuoteRepositoryFactory.Create = CreateRepository;
        }

//		public static void Save()
//		{
//			quoteLoader.Save(Quotes);
//		}
//
//        IQuoteRepository CreateRepository()
//        {
//            return new QuoteLoader();
//        }  
	}
}

