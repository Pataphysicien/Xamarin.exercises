// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace WeatherApp
{
	[Register ("WeatherTableCell")]
	partial class WeatherTableCell
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImageWeather { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TextCity { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TextTempHigh { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TextTempLow { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ImageWeather != null) {
				ImageWeather.Dispose ();
				ImageWeather = null;
			}
			if (TextCity != null) {
				TextCity.Dispose ();
				TextCity = null;
			}
			if (TextTempHigh != null) {
				TextTempHigh.Dispose ();
				TextTempHigh = null;
			}
			if (TextTempLow != null) {
				TextTempLow.Dispose ();
				TextTempLow = null;
			}
		}
	}
}
