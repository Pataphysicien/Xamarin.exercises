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

namespace MonkeyInfo
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint constLeftText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint constMonkeyEdge { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		NSLayoutConstraint constTopText { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView imgMonkey { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (constLeftText != null) {
				constLeftText.Dispose ();
				constLeftText = null;
			}
			if (constMonkeyEdge != null) {
				constMonkeyEdge.Dispose ();
				constMonkeyEdge = null;
			}
			if (constTopText != null) {
				constTopText.Dispose ();
				constTopText = null;
			}
			if (imgMonkey != null) {
				imgMonkey.Dispose ();
				imgMonkey = null;
			}
		}
	}
}
