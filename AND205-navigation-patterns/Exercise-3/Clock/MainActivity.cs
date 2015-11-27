using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;

namespace Clock
{
    [Activity(Label = "Clock", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ListView drawerListView;
        ActionBarDrawerToggle drawerToggle;
        DrawerLayout drawerLayout;

        Fragment[] fragments = new Fragment[] {
            new TimeFragment(),
            new StopwatchFragment(),
            new AlarmFragment(),
            new AboutFragment()
        };
        string[] titles = new string  [] {
            "Time",
            "Stopwatch",
            "Alarm",
            "About"
        };

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);


            // TODO: add your code here
            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);

            drawerToggle = new ActionBarDrawerToggle(this, drawerLayout, Resource.String.DrawerOpenDescription, Resource.String.DrawerCloseDescription);
            drawerLayout.SetDrawerListener(drawerToggle);

            ActionBar.SetDisplayHomeAsUpEnabled(true);


            drawerListView = FindViewById<ListView>(Resource.Id.drawerListView);
            drawerListView.Adapter = new ArrayAdapter<string>(this, Resource.Layout.ListViewMenuRow, Resource.Id.menuRowTextView, titles);
            drawerListView.ItemClick += (object sender, AdapterView.ItemClickEventArgs e) => OnMenuItemClick(e.Position);
            drawerListView.SetItemChecked(0, true);	// Highlight the first item at startup
            OnMenuItemClick(0);                     // Load Fragment 0 at startup
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            drawerToggle.SyncState();

            base.OnPostCreate(savedInstanceState);
        }

        void OnMenuItemClick(int position)
        {
            //
            // Show the selected Fragment to the user
            //
            base.FragmentManager
                .BeginTransaction()
                .Replace(Resource.Id.frameLayout, fragments[position])
                .Commit();

            //
            // Update the Activity title in the ActionBar
            //
            this.Title = titles[position];

            // TODO: add your code here
            drawerLayout.CloseDrawer(drawerListView);

        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if(drawerToggle.OnOptionsItemSelected(item))
                return true;
            
            return base.OnOptionsItemSelected(item);
        }
    }
}