using System;

using Foundation;
using UIKit;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using GreatQuotes.Data;

namespace GreatQuotes
{
	[Register("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
        readonly SimpleContainer container = new SimpleContainer();

		public override UIWindow Window { get; set; }

		public override void FinishedLaunching(UIApplication application)
		{
            //QuoteRepositoryFactory.Create = () => new QuoteLoader();

            container.Register<IQuoteRepository, QuoteLoader>();
            container.Create<QuoteManager>();

            ServiceLocator.Instance.Add<ITextToSpeech, TextToSpeechService>();
        }

		public async override void DidEnterBackground(UIApplication application)
		{
			CancellationTokenSource cts = new CancellationTokenSource();

			var taskId = UIApplication.SharedApplication.BeginBackgroundTask(() => cts.Cancel());

			try {
				await Task.Run(() => {
                    QuoteManager.Instance.Save();
				}, cts.Token);
			}
			catch (Exception ex) {
				Debug.WriteLine(ex.Message);
			}
			finally {
				UIApplication.SharedApplication.EndBackgroundTask(taskId);
			}
		}
	}
}

