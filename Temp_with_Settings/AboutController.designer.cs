// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Temp_with_Settings
{
    [Register ("AboutController")]
    partial class AboutController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel AboutLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (AboutLabel != null) {
                AboutLabel.Dispose ();
                AboutLabel = null;
            }
        }
    }
}