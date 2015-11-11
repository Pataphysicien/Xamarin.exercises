using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Mapping
{
    public partial class MappingViewController : UIViewController
    {
        public MappingViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }

        partial void btnStandard_Activated (UIBarButtonItem sender)
        {
            map.MapType = MonoTouch.MapKit.MKMapType.Standard;
        }

        partial void btnSatellite_Activated (UIBarButtonItem sender)
        {
            map.MapType = MonoTouch.MapKit.MKMapType.Satellite;
        }

        partial void btnHybrid_Activated (UIBarButtonItem sender)
        {
            map.MapType = MonoTouch.MapKit.MKMapType.Hybrid;
        }
    }
}

