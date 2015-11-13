using Android.App;
using Android.OS;
using System.Linq;

namespace MyTunes
{
	[Activity(Label = "@string/app_name", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : ListActivity
	{
		protected override async void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

//			ListAdapter = new ListAdapter<string>() {
//				DataSource = new[] { "One", "Two", "Three" }
//			};

            var data = await SongLoader.Load ();
            var adapter = new ListAdapter<Song> ();
            adapter.DataSource = data.ToList ();
            adapter.TextProc = s => s.Name;
            adapter.DetailTextProc = s => s.Artist + " - " + s.Album;

            this.ListAdapter = adapter;
		}
	}
}


