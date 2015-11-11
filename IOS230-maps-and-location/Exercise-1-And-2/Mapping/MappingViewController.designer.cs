// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System;
using System.CodeDom.Compiler;

namespace Mapping
{
	[Register ("MappingViewController")]
	partial class MappingViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem btnHybrid { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem btnSatellite { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIBarButtonItem btnStandard { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.MapKit.MKMapView map { get; set; }

		[Action ("btnHybrid_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnHybrid_Activated (UIBarButtonItem sender);

		[Action ("btnSatellite_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnSatellite_Activated (UIBarButtonItem sender);

		[Action ("btnStandard_Activated:")]
		[GeneratedCode ("iOS Designer", "1.0")]
		partial void btnStandard_Activated (UIBarButtonItem sender);

		void ReleaseDesignerOutlets ()
		{
			if (btnHybrid != null) {
				btnHybrid.Dispose ();
				btnHybrid = null;
			}
			if (btnSatellite != null) {
				btnSatellite.Dispose ();
				btnSatellite = null;
			}
			if (btnStandard != null) {
				btnStandard.Dispose ();
				btnStandard = null;
			}
			if (map != null) {
				map.Dispose ();
				map = null;
			}
		}
	}
}
