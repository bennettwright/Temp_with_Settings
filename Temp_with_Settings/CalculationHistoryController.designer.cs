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
    [Register ("CalculationHistoryController")]
    partial class CalculationHistoryController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITableView HistoryTable { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (HistoryTable != null) {
                HistoryTable.Dispose ();
                HistoryTable = null;
            }
        }
    }
}