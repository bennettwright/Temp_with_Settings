using Foundation;
using System;
using UIKit;

namespace Temp_with_Settings
{
    public partial class AboutController : UIViewController
    {
        public AboutController (IntPtr handle) : base (handle)
        { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
             AboutLabel.Text = "This is a calculator for how it really feels " +
                "outside and a practice model for using multiple views. In this" +
                " app you can calculate the windchill temperature, or the heat" +
                " index. \n\nNote: Wind chill is only calculated when humidity is off" +
                " and the temp is below 50 degrees F. Heat index is only calculated" +
                " when humidity is turned on and the temperature is above 80 degrees F." +
                " \n\nDeveloped for CS 235 by Bennett Wright ";
        }

    }
}