using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Clock
{
	[Activity(Label = "Clock", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Android.Support.V4.App.FragmentActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			SetContentView(Resource.Layout.Main);

			var fragments = new Android.Support.V4.App.Fragment[]
				{
					new TimeFragment     (),
					new StopwatchFragment(),
					new AboutFragment    ()
				};

			var titles = CharSequence.ArrayFromStringArray(new [] 
				{ 
					"Time", 
					"Stopwatch", 
					"About"
				});
			
			//
			// TODO: Use FindViewById to get a reference to the ViewPager in the UI
			//
            var viewpage = FindViewById<Android.Support.V4.View.ViewPager> (Resource.Id.viewPager);


			//
			// TODO: Create a ClockAdapter
			//
            var adapter = new ClockAdapter (base.SupportFragmentManager, fragments, titles);



			//
			// TODO: Assign the ClockAdapter to the ViewPager's Adapter property
			//
            viewpage.Adapter = adapter;



		}
	}
}