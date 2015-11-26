using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinUniversity
{
	[Activity (Label = "XamarinUniversity", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var lv = FindViewById<ListView> (Resource.Id.instructorListView);

			//lv.Adapter = new ArrayAdapter<Instructor> (this, Android.Resource.Layout.SimpleListItem1, InstructorData.Instructors);
			lv.Adapter = new InstructorAdapter(InstructorData.Instructors);

			lv.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => {
				var pos = e.Position;

				var instructor = InstructorData.Instructors[pos];

				var dlg = new AlertDialog.Builder(this);
				dlg.SetMessage(instructor.Name);
				dlg.SetNeutralButton("OK", delegate{});
				dlg.Show();
			};

		}
	}
}


