using Foundation;
using System;
using UIKit;
using MyExpenses.Data;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyExpenses
{
	partial class ExpenseListViewController : UITableViewController
	{
        const string CellIdentifier = "ExpenseCell";
        List<Expense> expenses;

		public ExpenseListViewController (IntPtr handle) : base (handle)
		{
		}

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.RightBarButtonItem = this.EditButtonItem;

            var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, OnAddExpense);
            this.NavigationItem.LeftBarButtonItem = addButton;

            expenses = new List<Expense>();

            DataStore db = new DataStore();
            expenses.AddRange(await db.LoadExpenses());
            TableView.ReloadData();
        }


        Expense newExpense;

        void OnAddExpense(object sender, EventArgs e)
        {
            newExpense = new Expense ();

            PerformSegue ("showDetail", this);
        }

        public override bool CanEditRow (UITableView tableView, NSIndexPath indexPath)
        {
            return true;
        }

        bool hasInsertionRow = false;

        public override UITableViewCellEditingStyle EditingStyleForRow (UITableView tableView, NSIndexPath indexPath)
        {
            if (hasInsertionRow)
            {
                if (indexPath.Row == 0)
                    return UITableViewCellEditingStyle.Insert;
            }
            return UITableViewCellEditingStyle.Delete;
        }

        public override void SetEditing (bool editing, bool animated)
        {
            base.SetEditing (editing, animated); // need to call base class

            using (var indexPath = NSIndexPath.FromRowSection (0, 0))
            {
                if (editing)
                {
                    hasInsertionRow = true;
                    TableView.InsertRows (new[]{ indexPath }, UITableViewRowAnimation.Automatic);
                }
                else if( hasInsertionRow )
                {
                    hasInsertionRow = false;
                    TableView.DeleteRows (new[]{ indexPath }, UITableViewRowAnimation.Automatic);
                }
            }
        }

        public override void WillBeginEditing (UITableView tableView, NSIndexPath indexPath)
        {
            if (hasInsertionRow)
            {
                hasInsertionRow = false;

                using ( indexPath = NSIndexPath.FromRowSection (0, 0))
                {
                    TableView.DeleteRows (new[]{ indexPath }, UITableViewRowAnimation.Automatic);
                }

            }
        }

        public override async void CommitEditingStyle (UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
        {
            if (editingStyle == UITableViewCellEditingStyle.Delete)
            {
                var expense = expenses [indexPath.Row];
                expenses.RemoveAt (indexPath.Row);
                tableView.DeleteRows (new [] { indexPath }, UITableViewRowAnimation.Automatic);

                await new DataStore ().Delete (expense);
            }
            else if (editingStyle == UITableViewCellEditingStyle.Insert)
            {
                Debug.Assert (indexPath.Row == 0);
                OnAddExpense (this, EventArgs.Empty);
            }
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            if (hasInsertionRow)
                return expenses.Count + 1;
            
            return expenses.Count;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CellIdentifier, indexPath);

            int row = indexPath.Row;
            if (hasInsertionRow)
            {
                if (row == 0)
                {
                    cell.TextLabel.Text = "Add an Expense";
                    cell.DetailTextLabel.Text = "";
                    cell.TextLabel.TextColor = UIColor.Gray;
                    return cell;
                }
                row--;
            }
            var expense = expenses[row];

            cell.TextLabel.Text = expense.Title;
            cell.DetailTextLabel.Text = expense.Amount.ToString("C");

             if (expense.Billable) {
                cell.TextLabel.TextColor = UIColor.Blue;
            }
            else {
                cell.TextLabel.TextColor = UIColor.Black;
            }

            return cell;
        }

        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            if (segue.Identifier == "showDetail") {
                var detailViewController = segue.DestinationViewController as ExpenseDetailViewController;
                if (detailViewController != null)
                {
                    var selectedExpense = newExpense 
                                            ?? expenses [TableView.IndexPathForSelectedRow.Row];
                    detailViewController.SelectedExpense = selectedExpense;
                }
            }
            
        }

        public override void ViewWillAppear (bool animated)
        {
            if (newExpense != null)
            {
                if (newExpense.Id != 0)
                {
                    expenses.Add (newExpense);
                    TableView.ReloadData ();
                }
                newExpense = null;
            }
        }

	}
}
