using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;
using System.Collections.Generic;

namespace WeatherApp
{
	partial class WeatherTVC : UITableViewController
	{
		const string CELL_ID = "id";
		List<Weather> data;

		public WeatherTVC (IntPtr handle) : base (handle)
		{
			data = WeatherFactory.GetWeatherData ();

			TableView.ContentInset = new UIEdgeInsets (this.TopLayoutGuide.Length, 0, 0, 0);
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = tableView.DequeueReusableCell (CELL_ID);

            if (cell == null)
            {
                cell = new UITableViewCell (UITableViewCellStyle.Subtitle, CELL_ID); // change cell style from default

                // ---------------------------------------------------------------
                // customize a cell's font and color
                cell.TextLabel.Font = UIFont.SystemFontOfSize (20, UIFontWeight.Bold);
                cell.TextLabel.TextColor = UIColor.FromRGB (59, 102, 136);
            
                cell.DetailTextLabel.Font = UIFont.ItalicSystemFontOfSize (12);
                cell.DetailTextLabel.TextColor = UIColor.FromRGB (0, 142, 255);
            }

            var weather = data [indexPath.Row];

            cell.TextLabel.Text = weather.City;
            cell.DetailTextLabel.Text = weather.ToString ();

            // always use UIImage.FromBundle() because it implements caching to give slightly better performance.
            cell.ImageView.Image = UIImage.FromBundle (weather.CurrentConditions.ToString () + ".png");

           	return cell;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return data.Count;
		}





	}
}
