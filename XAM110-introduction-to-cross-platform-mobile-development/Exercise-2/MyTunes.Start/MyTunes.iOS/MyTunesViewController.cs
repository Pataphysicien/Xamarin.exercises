using UIKit;
using System.Linq;

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

		public override async void ViewDidLoad()
		{
			base.ViewDidLoad();

//			TableView.Source = new ViewControllerSource<string>(TableView) {
//				DataSource = new string[] { "One", "Two", "Three" },
//			};

            var data = await SongLoader.Load ();

            var viewControllerSource = new ViewControllerSource<Song> (this.TableView);

            viewControllerSource.DataSource = data.ToList ();

            viewControllerSource.TextProc = s => s.Name;
            viewControllerSource.DetailTextProc = s => s.Artist + " - " + s.Album;

            this.TableView.Source = viewControllerSource;
		}
	}

}

