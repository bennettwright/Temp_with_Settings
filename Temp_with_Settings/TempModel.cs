using System;
using UIKit;
namespace Temp_with_Settings
{
    public class TempModel : UIPickerViewModel
    {
        private string[] style = { "Fahrenheit", "Celsius" };

        private ViewController controller;

        public TempModel(ViewController controller)
        {
            this.controller = controller;
        }

        public override nint GetComponentCount(UIPickerView pickerView)
        {
            return 1;
        }

        public override nint GetRowsInComponent(UIPickerView pickerView, nint component)
        {
            return style.Length;
        }

        public override string GetTitle(UIPickerView pickerView, nint row, nint component)
        {
            if (component == 0)
                return style[row];
            else
                return row.ToString();
        }

        public override void Selected(UIPickerView pickerView, nint row, nint component) =>
            controller.Celsius = (style[pickerView.SelectedRowInComponent(0)] == "Celsius");
        

        public override nfloat GetComponentWidth(UIPickerView picker, nint component)
        {
            if (component == 0)
                return 240f;
            else
                return 40f;
        }

        public override nfloat GetRowHeight(UIPickerView picker, nint component)
        {
            return 40f;
        }
    }
}
