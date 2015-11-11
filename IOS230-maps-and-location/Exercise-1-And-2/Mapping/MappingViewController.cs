using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreLocation;

namespace Mapping
{
    public partial class MappingViewController : UIViewController
    {
        CLLocationManager locMan = new CLLocationManager();

        public MappingViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            // Perform any additional setup after loading the view, typically from a nib.
            if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0) == true)
            {
                locMan.RequestWhenInUseAuthorization ();
            }
        }
        public override void ViewDidAppear (bool animated)
        {
            if (CLLocationManager.Status == CLAuthorizationStatus.Denied)
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion (8, 0) == true)
                {
                    var yesNoAlertController = UIAlertController.Create (
                                                   "Unable to determien location",
                                                   "This application works best when it can determine yoru current " +
                                                   "position.  Would you like to go to Settings to enable location data?",
                                                   UIAlertControllerStyle.Alert);

                    yesNoAlertController.AddAction (UIAlertAction.Create (
                        "Yes", UIAlertActionStyle.Default,
                        alert =>
                        {
                            var settingString = UIApplication.OpenSettingsUrlString;
                            var url = new NSUrl (settingString);
                            UIApplication.SharedApplication.OpenUrl (url);
                        }));

                    yesNoAlertController.AddAction (UIAlertAction.Create (
                        "No", UIAlertActionStyle.Cancel, null));

                    this.PresentViewController (yesNoAlertController, true, null);
                }
                else
                {
                    var alert = new UIAlertView ("Unabled to determine location",
                        "This application works best when it can determine your current " +
                        "position.  Please open the Settings and enable Location Services " +
                        "for this app.", 
                        null,"OK");
                    alert.Show ();
                }
            }
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

