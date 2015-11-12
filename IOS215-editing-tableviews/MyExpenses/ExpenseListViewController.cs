using Foundation;
using System;
using UIKit;
using MyExpenses.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;


namespace MyExpenses
{
    partial class ExpenseListViewController : UITableViewController, IUISearchResultsUpdating
	{
        const string CellIdentifier = "ExpenseCell";
        List<Expense> expenses;
        UITableViewRowAction[ ] editActions;

        // for adding new expense
        Expense newExpense;

        // for search bar
        UISearchController searchController;
        List<Expense> filteredExpenses;

		public ExpenseListViewController (IntPtr handle) : base (handle)
		{
		}

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.NavigationItem.RightBarButtonItem = this.EditButtonItem;

            var addButton = new UIBarButtonItem (UIBarButtonSystemItem.Add, OnAddExpense);
            this.NavigationItem.LeftBarButtonItem = addButton;

            UIRefreshControl refreshControl = new UIRefreshControl ();
            refreshControl.ValueChanged += async (object sender, EventArgs e) =>
            {
                // this happens in a background thread, so need to switch to main thread to update GUI
                expenses = (await new DataStore ().Reset ()).ToList ();

                await Task.Delay (3000); // to simulate a wait

                BeginInvokeOnMainThread ( () =>{
                    TableView.ReloadData ();
                    refreshControl.EndRefreshing ();
                });
                
            };

            this.RefreshControl = refreshControl;

            expenses = new List<Expense>();

            searchController = new UISearchController((UIViewController)null);
            searchController.SearchResultsUpdater = this;
            searchController.DimsBackgroundDuringPresentation = false;

            TableView.TableHeaderView = searchController.SearchBar;
            DefinesPresentationContext = true;
            searchController.SearchBar.SizeToFit();

            DataStore db = new DataStore();
            expenses.AddRange(await db.LoadExpenses());
            TableView.ReloadData();
        }

        public void UpdateSearchResultsForSearchController(UISearchController searchController)
        {
            if (searchController.Active)
            {
                filteredExpenses = new List<Expense> ();

            }
            else
                filteredExpenses = null;

            FilterContentForSearchText(searchController.SearchBar.Text);
        }

        void FilterContentForSearchText(string text)
        {
            // Make sure to add using statement for System.Linq if needed.
            if (filteredExpenses != null) {
                filteredExpenses.Clear();
                filteredExpenses.AddRange(
                    expenses.Where(e => 
                        string.IsNullOrWhiteSpace(text) 
                        || e.Title.ToUpper().Contains(text.ToUpper())));
            }

            TableView.ReloadData();
        }

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
            // if search filter is specified...
            if (filteredExpenses != null)
                return filteredExpenses.Count;

            // if inserting new expense...
            if (hasInsertionRow)
                return expenses.Count + 1;

            // default is normal
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
            //var expense = expenses[row];
            var expense = (filteredExpenses != null)
                ? filteredExpenses[row]
                : expenses[row];

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

        public override UITableViewRowAction[] EditActionsForRow (UITableView tableView, NSIndexPath indexPath)
        {
            if (editActions == null) {
                editActions = new[] {
                    UITableViewRowAction.Create(
                        UITableViewRowActionStyle.Normal, 
                        "Billable", OnFlipBillable),
                    UITableViewRowAction.Create(
                        UITableViewRowActionStyle.Normal, 
                        "Not Billable", OnFlipBillable),
                    UITableViewRowAction.Create(
                        UITableViewRowActionStyle.Destructive,
                        "Delete", OnDelete),
                };
                editActions[0].BackgroundColor = UIColor.Blue;
            }

            Expense expense = expenses[indexPath.Row];

            var rowActions = new UITableViewRowAction[2];
            rowActions[0] = (expense.Billable)
                ? editActions[1] : editActions[0];
            rowActions[1] = editActions[2];
            return rowActions;
        }

        async void OnFlipBillable(UITableViewRowAction rowAction, NSIndexPath indexPath)
        {
            Expense expense = expenses[indexPath.Row];
            expense.Billable = !expense.Billable;
            TableView.ReloadRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
            await new DataStore().Update(expense);
        }

        async void OnDelete(UITableViewRowAction rowAction, NSIndexPath indexPath)
        {
            Expense expense = expenses[indexPath.Row];
            expenses.RemoveAt(indexPath.Row);
            TableView.DeleteRows(new[] { indexPath }, UITableViewRowAnimation.Automatic);
            await new DataStore().Delete(expense);
        }

	}
}
