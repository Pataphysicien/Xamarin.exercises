using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WeatherSettings
{
	partial class SettingsTableViewController : UITableViewController
	{
		public SettingsTableViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.TableView.ContentInset = new UIEdgeInsets (20, 0, 0, 0); // lower the tableview, so it does not block the top carrier text

            this.CellDefaultCity.DetailTextLabel.Text = "Vancouver";
            this.SwitchMetric.On = false;
        }
	}


}
