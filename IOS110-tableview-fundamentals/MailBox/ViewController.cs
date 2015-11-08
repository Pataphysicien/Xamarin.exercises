using System;

using UIKit;
using Mailbox;
using Foundation;
using CoreGraphics;

namespace MailBox
{
    public partial class ViewController : UIViewController
    {

        public ViewController (IntPtr handle) : base (handle)
        {
        }

        UITableView _tableView;

        // ------------------------------------------------------------------------------
        // Exercise-2 
        // 2nd approach - using UITableViewSource to populate a TableView 
        class EmailServerDataSource : UITableViewSource
        {
            EmailServer _emailServer = new EmailServer ();

            public override nint RowsInSection (UITableView tableView, nint section)
            {
                return _emailServer.Email.Count;
            }

            public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
            {
                UITableViewCell cell = new UITableViewCell (CGRect.Empty);

                var emailItem = _emailServer.Email [indexPath.Row];

                cell.TextLabel.Text = emailItem.Subject;

                return cell;
            }
        }
        // ------------------------------------------------------------------------------

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.

            // 1) create tableview programmatically
            _tableView = new UITableView (this.View.Frame);

            _tableView.Source = new EmailServerDataSource (); // exercise-2

            // 2) add to UI structure
            this.Add(_tableView);
            //this.View.Add (_tableView);
            //this.View.AddSubview (_tableView); // iOS way

            // 3) add constraints so that the width will change when you rotate the phone
            _tableView.TranslatesAutoresizingMaskIntoConstraints = false;
            this.View.AddConstraint(NSLayoutConstraint.Create(_tableView, NSLayoutAttribute.Top,
                NSLayoutRelation.Equal, this.View, NSLayoutAttribute.TopMargin, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_tableView, NSLayoutAttribute.Left,
                NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Left, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_tableView, NSLayoutAttribute.Width,
                NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Width, 1, 0));
            this.View.AddConstraint(NSLayoutConstraint.Create(_tableView, NSLayoutAttribute.Height,
                NSLayoutRelation.Equal, this.View, NSLayoutAttribute.Height, 1, 0));

        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}

