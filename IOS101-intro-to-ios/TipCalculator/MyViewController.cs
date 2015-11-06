using System;
using UIKit;
using CoreGraphics;

namespace TipCalculator
{
    public class MyViewController : UIViewController
    {
        public MyViewController ()
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();

            View.BackgroundColor = UIColor.Yellow;

            int currentY = 28;
            int currentHeight = 35;

            UITextField totalAmount = new UITextField (
                new CGRect (20, currentY, View.Bounds.Width - 40, currentHeight)) {
                KeyboardType = UIKeyboardType.DecimalPad,
                BorderStyle = UITextBorderStyle.RoundedRect,
                Placeholder = "Enter Total Amount"
            };


            currentY += currentHeight + 8;
            currentHeight = 45;

            UIButton calcButton = new UIButton (UIButtonType.Custom) {
                Frame = new CGRect (20, currentY, View.Bounds.Width - 40, currentHeight),
                BackgroundColor = UIColor.FromRGB (0, 0.5f, 0)
            };
            calcButton.SetTitle ("Calculate", UIControlState.Normal);


            currentY += currentHeight + 8;
            currentHeight = 40;

            UISegmentedControl tipAmount = new UISegmentedControl(new CGRect(20, currentY, View.Bounds.Width - 40, currentHeight) );
            tipAmount.InsertSegment ("10%", 0, false);
            tipAmount.InsertSegment ("15%", 1, false);
            tipAmount.InsertSegment ("20%", 2, false);
            tipAmount.InsertSegment ("25%", 3, false);
            tipAmount.SelectedSegment = 2;
                

            currentY += currentHeight + 8;
            currentHeight = 40;

            UILabel resultLabel = new UILabel (new CGRect (0, currentY, View.Bounds.Width, currentHeight)) {
                TextColor = UIColor.Blue,
                TextAlignment = UITextAlignment.Center,
                Font = UIFont.SystemFontOfSize (24),
                Text = "Tip is $0,00"
            };


            View.AddSubviews (totalAmount, calcButton, tipAmount, resultLabel);

            // add behavior logic 
            calcButton.TouchUpInside += (sender, e) => 
            {
                totalAmount.ResignFirstResponder (); // get rid of keyboard

                double value = 0;
                Double.TryParse (totalAmount.Text, out value);

                if( tipAmount.SelectedSegment == 0 )
                {
                    value *= 1.1; // add tips
                }
                else if( tipAmount.SelectedSegment == 1 )
                {
                    value *= 1.15; // add tips
                }
                else if( tipAmount.SelectedSegment == 2 )
                {
                    value *= 1.2; // add tips
                }
                else if( tipAmount.SelectedSegment == 3 )
                {
                    value *= 1.25; // add tips
                }
                else
                {
                }

                //double tipPercent = (10f + (tipAmount.SelectedSegment * 5)) / 100f; // this is tutorial version.


                resultLabel.Text = $"Tip is {value:C}"; // new C# 6.0 feature

            };


        }
    }
}

