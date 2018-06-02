// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace Temp_with_Settings
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField FahrenheitField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField HumidityField { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISwitch HumiditySwitch { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImageView1 { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel ResultLabel { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UISlider WindSlider { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel WindSpeedLabel { get; set; }

        [Action ("switchActionSheet:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void switchActionSheet (UIKit.UISwitch sender);

        void ReleaseDesignerOutlets ()
        {
            if (FahrenheitField != null) {
                FahrenheitField.Dispose ();
                FahrenheitField = null;
            }

            if (HumidityField != null) {
                HumidityField.Dispose ();
                HumidityField = null;
            }

            if (HumiditySwitch != null) {
                HumiditySwitch.Dispose ();
                HumiditySwitch = null;
            }

            if (ImageView1 != null) {
                ImageView1.Dispose ();
                ImageView1 = null;
            }

            if (ResultLabel != null) {
                ResultLabel.Dispose ();
                ResultLabel = null;
            }

            if (WindSlider != null) {
                WindSlider.Dispose ();
                WindSlider = null;
            }

            if (WindSpeedLabel != null) {
                WindSpeedLabel.Dispose ();
                WindSpeedLabel = null;
            }
        }
    }
}