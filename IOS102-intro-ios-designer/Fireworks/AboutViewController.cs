using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Fireworks
{
	partial class AboutViewController : UIViewController
	{
		public AboutViewController (IntPtr handle) : base (handle)
		{
		}

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            this.buttonClose.TouchUpInside += (sender, e) =>
            {
                this.DismissViewController(true, null);

            };
        }

	}
}
