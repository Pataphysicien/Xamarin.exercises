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
        // Exercise-2 
        // 1st approach - using UITableViewController to populate a TableView 
        EmailServer _emailServer = new EmailServer (1000);

        public override nint RowsInSection (UITableView tableView, nint section)
        {
            return _emailServer.Email.Count;
        }

        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            //UITableViewCell cell = GetCell_DefaultStyle (tableView, indexPath);
            UITableViewCell cell = GetCell_SubTitleStyle (tableView, indexPath);
            //UITableViewCell cell = GetCell_Value1Style (tableView, indexPath);
            //UITableViewCell cell = GetCell_Value2Style (tableView, indexPath);


            return cell;
        }



        UITableViewCell GetCell_DefaultStyle (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = CreateReusableCell (tableView, UITableViewCellStyle.Default, indexPath);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;
            cell.ImageView.Image = emailItem.GetImage ();

            return cell;
        }

        UITableViewCell GetCell_SubTitleStyle (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = CreateReusableCell (tableView, UITableViewCellStyle.Subtitle, indexPath);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;
            cell.DetailTextLabel.Text = emailItem.Body;
            cell.ImageView.Image = emailItem.GetImage ();

            return cell;
        }

        UITableViewCell GetCell_Value1Style (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = CreateReusableCell (tableView, UITableViewCellStyle.Value1, indexPath);

            var emailItem = _emailServer.Email [indexPath.Row];

            // *********************************************************************
            // TextLabel takes priority over DetailTextLabel in the cell display 
            cell.TextLabel.Text = emailItem.Subject.Substring (0, 20) + "...";
            // *********************************************************************

            cell.DetailTextLabel.Text = emailItem.Body;
            cell.ImageView.Image = emailItem.GetImage ();

            return cell;
        }

       
        UITableViewCell GetCell_Value2Style (UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = CreateReusableCell (tableView, UITableViewCellStyle.Value2, indexPath);

            var emailItem = _emailServer.Email [indexPath.Row];
            cell.TextLabel.Text = emailItem.Subject;

            // *********************************************************************
            // DetailTextLabel takes priority over TextLabel in the cell display 
            cell.DetailTextLabel.Text = emailItem.Body; 
            // *********************************************************************

            if( cell.ImageView != null )
                cell.ImageView.Image = emailItem.GetImage ();

             return cell;
        }
        // ------------------------------------------------------------------------------
    
        // ------------------------------------------------------------------------------
        // Exercise-4 
// Removed in Exercise-5 and do it in Storyboard
//        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
//        {
//            var emailItem = _emailServer.Email [indexPath.Row];
//
//            var storyboard = UIStoryboard.FromName ("Main", null);
//            var detailsViewController = 
//                (DetailsViewController)storyboard.InstantiateViewController ("DetailsViewController");
//
//            detailsViewController.Item = emailItem;
//            PresentViewController (detailsViewController, true, null);
//        }

        public override void AccessoryButtonTapped (UITableView tableView, NSIndexPath indexPath)
        {
            var emailItem = _emailServer.Email [indexPath.Row];

            var controller = UIAlertController.Create (
                                 emailItem.Subject,
                                 emailItem.Body,
                                 UIAlertControllerStyle.Alert);
            controller.AddAction (UIAlertAction.Create ("OK",
                                                       UIAlertActionStyle.Default, 
                                                        null)
                                );
            PresentViewController (controller, true, null);
            
        }

        [Action("UnwindToTableViewController:")]
        public void UnwindToTableViewController(UIStoryboardSegue seque)
        {
        }

        public override void ViewDidLoad ()
        {
            this.TableView.ContentInset = new UIEdgeInsets (20, 0, 0, 0); // ** push this tableview down by 20
        }

        // ------------------------------------------------------------------------------
        // Exercise-5 - 1st approach
        //            - create reusable cell via code-behind
        const string CellId = "EmailCell";
        static nint _reusableCellCount = 0;
        UITableViewCell CreateReusableCell(UITableView tableView, UITableViewCellStyle style, NSIndexPath indexPath)
        {
            //return CreateReusableCell_CodeBehind (tableView, style, indexPath);
            return CreateReusableCell_Storyboard (tableView, style, indexPath);

        }

        UITableViewCell CreateReusableCell_Storyboard(UITableView tableView, UITableViewCellStyle style, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell (CellId, indexPath);
            if (cell == null)
            {
                // properties are set in the Storyboard
            }
            else
            {
                if (cell.ImageView != null &&
                    cell.ImageView.Image != null)
                {
                    cell.ImageView.Image.Dispose ();
                }

            }
            return cell;
        }

        UITableViewCell CreateReusableCell_CodeBehind(UITableView tableView, UITableViewCellStyle style, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell (CellId, indexPath);
            if (cell == null)
            {
                cell = new UITableViewCell (style, CellId);

                cell.TextLabel.TextColor = UIColor.Blue;
                cell.Accessory = UITableViewCellAccessory.DetailDisclosureButton;

                if( cell.DetailTextLabel != null)
                    cell.DetailTextLabel.TextColor = UIColor.Gray;

                ++_reusableCellCount;
                Console.WriteLine ("Reusable cell count: {0}", _reusableCellCount);
            }
            else
            {
                if (cell.ImageView != null)
                {
                    cell.ImageView.Image.Dispose ();
                }

            }
            return cell;
        }

        public override void PrepareForSegue (UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "ShowDetails")
            {
                var emailItem = _emailServer.Email [TableView.IndexPathForSelectedRow.Row];
    
                var detailsViewController = 
                    segue.DestinationViewController as DetailsViewController;

                if (detailsViewController != null)
                {
                    detailsViewController.item = emailItem;
                }

            }
        }
    }
}
