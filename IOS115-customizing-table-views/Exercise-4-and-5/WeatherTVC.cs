using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;
using System.Linq;

namespace WeatherApp
{
	partial class WeatherTVC : UITableViewController
	{
		const string CELL_ID = "cell_id";
		//List<Weather> data;
        IGrouping<char, Weather>[] grouping;
        string[] indices;

		public WeatherTVC (IntPtr handle) : base (handle)
		{
			var data = WeatherFactory.GetWeatherData ();

            grouping = (from w in data
                                 orderby w.City[0] ascending
                                 group w by w.City [0] into g
                                 select g).ToArray ();
         
            indices =  (from w in data
                orderby w.City[0] ascending
                group w by w.City [0] into g
                select g.Key.ToString ()).ToArray ();

		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.ContentInset = new UIEdgeInsets (20, 0, 0, 0);
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
            var cell = tableView.DequeueReusableCell (CELL_ID) as WeatherTableCell;

            var weather = grouping [indexPath.Section].ElementAt (indexPath.Row);

            cell.UpdateData (weather);

			return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			//return data.Count;
            return grouping [section].Count ();
		}

        public override nint NumberOfSections (UITableView tableView)
        {
            return grouping.Count ();
        }

        public override string TitleForHeader (UITableView tableView, nint section)
        {
            return grouping [section].Key.ToString ();
        }

        public override string TitleForFooter (UITableView tableView, nint section)
        {
            return "Number of cities: " + grouping [section].Count ();
        }

        public override string[] SectionIndexTitles (UITableView tableView)
        {
            return indices;
        }
	}
}
