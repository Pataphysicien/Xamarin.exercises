using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using Mailbox;
using CoreGraphics;

namespace MailBox
{
	partial class TableViewController : UITableViewController
	{
        
		public TableViewController (IntPtr handle) : base (handle)
		{
		}

        // ------------------------------------------------------------------------------
        // 1st approach - using UITableViewController to populate a TableView 
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
        // ------------------------------------------------------------------------------
    
    }
}
