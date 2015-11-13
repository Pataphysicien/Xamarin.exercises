using System;

using UIKit;
using Foundation;

namespace MonkeyInfo
{
    public partial class ViewController : UIViewController
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();
            // Release any cached data, images, etc that aren't in use.
        }

        public override void WillRotate (UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillRotate (toInterfaceOrientation, duration);
            SetOrientation (
                toInterfaceOrientation == UIInterfaceOrientation.LandscapeLeft ||
                toInterfaceOrientation == UIInterfaceOrientation.LandscapeRight);
        }

        void SetOrientation (bool isLandscape)
        {
            //remove the constraints we want to change
            this.View.RemoveConstraint (constLeftText);
            this.View.RemoveConstraint (constTopText);

            //create new constraints based on orientation
            if (isLandscape == true) 
            {
                // Going landscape. Move text to the right of the monkey.
                constLeftText = GetConstraint (constraint: constLeftText,
                    // Text left constraint now attaches to monkey instead of parent view...
                    object2: imgMonkey,
                    // ...and constrains to the monkey image's right edge instead of the parent view left edge.
                    attribute2: NSLayoutAttribute.Trailing
                );

                constTopText = GetConstraint (constraint: constTopText,
                    // Top edge constraint of the text no longer attaches to monkey but to parent view now...
                    object2: this.View,
                    // ...and to top layout attribute instead of bottom of monkey image.
                    attribute2: NSLayoutAttribute.Top
                );
            } 
            else 
            {
                constLeftText = GetConstraint (constraint: constLeftText,
                    // Text left constraint new attaches to the parent view...
                    object2: this.View,
                    // ...and the left edge of the parent view and no longer to the right edge of the monkey.
                    attribute2: NSLayoutAttribute.Leading
                );

                constTopText = GetConstraint (constraint: constTopText,
                    // Constrain the text to the monkey and no longer to the parent view...
                    object2: imgMonkey,
                    // ...and use the bottom edge of the monkey instead of the top of the parent view.
                    attribute2: NSLayoutAttribute.Bottom
                );
            }

            //add the new constraints
            this.View.AddConstraint (constLeftText);
            this.View.AddConstraint (constTopText);
        }

        //helper method to create constraints based on existing constraints
        NSLayoutConstraint GetConstraint (
            NSLayoutConstraint constraint,
            NSObject object1 = null,
            NSLayoutAttribute? attribute1 = null,
            NSLayoutRelation? relation = null,
            NSObject object2 = null,
            NSLayoutAttribute? attribute2 = null,
            nfloat? multiplier = null,
            nfloat? constant = null)
        {
            if (constraint == null)
                return null;
            return NSLayoutConstraint.Create(
                object1 ?? constraint.FirstItem,
                (attribute1 == null) ? constraint.FirstAttribute : attribute1.Value,
                (relation == null) ? constraint.Relation : relation.Value,
                object2 ?? constraint.SecondItem, 
                (attribute2 == null) ? constraint.SecondAttribute : attribute2.Value,
                (multiplier == null) ? constraint.Multiplier : multiplier.Value,
                (constant == null) ? constraint.Constant : constant.Value);
        }
    }
}

