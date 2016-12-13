using UIKit;
using System.Linq;
using System.Collections.Generic;
using System;

namespace MyTunes
{
	public class MyTunesViewController : UITableViewController
	{
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			if (UIDevice.CurrentDevice.CheckSystemVersion(7,0)) {
				TableView.ContentInset = new UIEdgeInsets (20, 0, 0, 0);
			}
		}

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
            IDisposable viewController = this.TableView.Source 
                                       = new ViewControllerSource<Song>(TableView)
            {
                DataSource = (await SongLoader.Load()).ToList(),
                TextProc = (s => s.Name),
                DetailTextProc = (s => string.Format("{0} - {1}", s.Artist, s.Album))
            };
            lock (_disposeLock)
                _disposableList.Add(viewController);
		}

        #region IDisposable

        //TODO: check if IDisposable is necessary of if TableView.Source handles it correctly.
        private readonly List<IDisposable> _disposableList = new List<IDisposable>();
        private readonly object _disposeLock = new object();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            lock(_disposeLock)
            {
                foreach (var iDisposable in _disposableList)
                    iDisposable.Dispose();
                _disposableList.Clear();
            }
        }

        #endregion IDisposable
    }

}

