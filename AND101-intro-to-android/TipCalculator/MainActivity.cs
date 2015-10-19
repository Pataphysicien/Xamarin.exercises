using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TipCalculator
{
	[Activity (Label = "TipCalculator", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		EditText _inputBill;
		Button _calculateButton;
		TextView _outputTip;
		TextView _outputTotal;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);


			_inputBill = FindViewById<EditText> (Resource.Id.inputBill);
			_calculateButton = FindViewById<Button> (Resource.Id.calculateButton);
			_outputTip = FindViewById<TextView> (Resource.Id.outputTip);
			_outputTotal = FindViewById<TextView> (Resource.Id.outputTotal);

			_calculateButton.Click += OnClick;
		}

		void OnClick(object sender, EventArgs e)
		{
			var s = _inputBill.Text;

			var bill = double.Parse (s);

			var tip = bill * 0.15;
			var total = bill + tip;

			_outputTotal.Text = total.ToString ();
			_outputTip.Text = tip.ToString ();
		}
	}
}


