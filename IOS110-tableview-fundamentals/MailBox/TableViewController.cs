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
            //UITableViewCell cell = GetCell_DefaultStyle (tableView, indexPath);
            //UITableViewCell cell = GetCell_SubTitleStyle (tableView, indexPath);
            //UITableViewCell cell = GetCell_Value1Style (tableView, indexPath);
            UITableViewCell cell = GetCell_Value2Style (tableView, indexPath);

            return cell;
        }

        UITableViewCell GetCell_DefaultStyle (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Default, null);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;
            cell.ImageView.Image = emailItem.GetImage ();

            return cell;
        }

        UITableViewCell GetCell_SubTitleStyle (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Subtitle, null);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;
            cell.DetailTextLabel.Text = emailItem.Body;
            cell.ImageView.Image = emailItem.GetImage ();

            cell.TextLabel.TextColor = UIColor.Blue;
            cell.DetailTextLabel.TextColor = UIColor.Gray;

            return cell;
        }

        UITableViewCell GetCell_Value1Style (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value1, null);

            var emailItem = _emailServer.Email [indexPath.Row];

            // *********************************************************************
            // TextLabel takes priority over DetailTextLabel in the cell display 
            cell.TextLabel.Text = emailItem.Subject.Substring (0, 20) + "...";
            // *********************************************************************

            cell.DetailTextLabel.Text = emailItem.Body;
            cell.ImageView.Image = emailItem.GetImage ();

            cell.TextLabel.TextColor = UIColor.Blue;
            cell.DetailTextLabel.TextColor = UIColor.Gray;

            return cell;
        }

        // *** 
        UITableViewCell GetCell_Value2Style (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = new UITableViewCell (UITableViewCellStyle.Value2, null);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;

            // *********************************************************************
            // DetailTextLabel takes priority over TextLabel in the cell display 
            cell.DetailTextLabel.Text = emailItem.Body; 
            // *********************************************************************

            if( cell.ImageView != null )
                cell.ImageView.Image = emailItem.GetImage ();

            cell.TextLabel.TextColor = UIColor.Blue;
            cell.DetailTextLabel.TextColor = UIColor.Gray;

            return cell;
        }
        // ------------------------------------------------------------------------------
    
    }
}
