using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WeatherApp
{
	partial class WeatherTableCell : UITableViewCell
	{
		public WeatherTableCell (IntPtr handle) : base (handle)
		{
		}

        public void UpdateData(Weather weather)
        {
            TextCity.Text = weather.City;
            TextTempHigh.Text = weather.High.ToString ();
            TextTempLow.Text = weather.Low.ToString ();

            ImageWeather.Image = UIImage.FromBundle (weather.CurrentConditions.ToString () + ".png");
        }
	}
}
