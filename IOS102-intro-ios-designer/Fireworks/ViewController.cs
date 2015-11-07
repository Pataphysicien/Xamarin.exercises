using System;

using UIKit;

namespace Fireworks
{
    public partial class ViewController : UIViewController
    {
        SimpleParticleGen _fireworks;

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            _fireworks = new SimpleParticleGen (
                UIImage.FromFile ("xamlogo.png"), 
                this.View, 
                this.View.Center);

            NightValueChanged (this.switchNight);

            buttonStart.TouchUpInside += (sender, e) => 
            {
                _fireworks.Start ();
            };

            //buttonAbout.TouchUpInside += OnAbout;
        }

        void OnAbout (object sender, EventArgs e)
        {
            var storyboard = this.Storyboard;
            var aboutViewController = 
                (AboutViewController)storyboard.InstantiateViewController ("AboutViewController");

            aboutViewController.ModalTransitionStyle = UIModalTransitionStyle.CoverVertical;

            this.PresentViewController (aboutViewController, true, null);
        }

        partial void NightValueChanged (UISwitch sender)
        {
            if( switchNight.On )
            {
                this.View.BackgroundColor = UIColor.FromRGB (25, 25, 112);
            }
            else
            {
                this.View.BackgroundColor = UIColor.White;
            }
 
        }

        partial void SliderSizeValueChanged (UISlider sender)
        {
            _fireworks.scaleMax = sender.Value;
            
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

