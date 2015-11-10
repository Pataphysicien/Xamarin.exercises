// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WeatherSettings
{
	[Register ("SettingsTableViewController")]
	partial class SettingsTableViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITableViewCell CellDefaultCity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UISwitch SwitchMetric { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (CellDefaultCity != null) {
				CellDefaultCity.Dispose ();
				CellDefaultCity = null;
			}
			if (SwitchMetric != null) {
				SwitchMetric.Dispose ();
				SwitchMetric = null;
			}
		}
	}
}
