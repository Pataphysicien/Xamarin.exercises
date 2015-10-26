using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace XamarinUniversityInstructors
{
	[Activity(Label = "Instructors", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			var lv = FindViewById<ListView> (Resource.Id.instructorListView);

			lv.Adapter = new ArrayAdapter<Instructor> (this, Android.Resource.Layout.SimpleListItem1, InstructorData.Instructors);

			lv.ItemClick += onItemClick;

		}

		void onItemClick (object sender, AdapterView.ItemClickEventArgs e)
		{
			Console.WriteLine (e.Position);

			var instructor = InstructorData.Instructors [e.Position];

			var dlg = new AlertDialog.Builder (this);
			dlg.SetMessage (instructor.Name);
			dlg.SetNeutralButton ("OK", delegate {
				
			});

			dlg.Show ();
		}
	}
}


