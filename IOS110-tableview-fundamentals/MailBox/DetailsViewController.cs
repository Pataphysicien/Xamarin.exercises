using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Mailbox;

namespace MailBox
{
	partial class DetailsViewController : UIViewController
	{

        public EmailItem item;

        public EmailItem Item {
            get {
                return item;
            }
            set {
                item = value;
                UpdateItem ();
            }
        }

        void UpdateItem ()
        {
            if (EmailDetails != null)
            {
                EmailDetails.Text = Item != null ? Item.ToString () : "";
            }
        }

        public override void ViewWillAppear (bool animated)
        {

            UpdateItem ();
        }

		public DetailsViewController (IntPtr handle) : base (handle)
		{
		}

// ------------------------------------------------------------------------------
// Exercise-4 - 1st approach to dismiss view code-behind
//            - need to assign this method to an event via the Storyboard
//
//        partial void OnDismiss (UIButton sender)
//        {
//            this.DismissViewController (true, null);
//        }
	}
}
