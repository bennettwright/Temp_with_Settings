using System;
using Foundation;
using UIKit;
using System.Diagnostics;

namespace Temp_with_Settings{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }


        public bool Celsius
        {
            get;
            set;
        }

        private void compute(object sender, EventArgs args)
        {
            //check for empty textfields so no errors occur
            //when parsing
            CheckEmpty();

            double temp = Double.Parse(FahrenheitField.Text);
            double humidity = Double.Parse(HumidityField.Text);
            int windspeed = (int)WindSlider.Value;

            int result = HumiditySwitch.On ? (int)Math.Round(calculate.getHeatIndex(temp, humidity))
                          : (int)Math.Round(calculate.getWindChill(temp, windspeed));

            //if celsius is selected
            if (Celsius)
                result = (int)calculate.ToCelsius(result);
                                  
            if (HumiditySwitch.On)
            {
                ResultLabel.Text = String.Format("Result: {0} ", result);

                //make sure not all of them are zero (save memory)
                if (temp != 0 && windspeed != 0 && result != 0)
                    CalculationHistoryController.AddData(String.Format("Temp: {0}, Humidity: {1}, Result: {2} ",
                        temp, humidity, result), Celsius);
            }

            else
            {            
                ResultLabel.Text = String.Format("Result: {0} ", result);

                //make sure not all of them are zero (save memory)
                if(temp != 0 && windspeed != 0 && result != 0)
                    CalculationHistoryController.AddData(String.Format("Temp: {0}, Wind speed: {1}, Result: {2} ",
                        temp, windspeed, result), Celsius);
            }


        }

        //checks for empty textboxes so it can parse correctly
        //without throwing errors
        private void CheckEmpty()
        {
            if (FahrenheitField.Text == String.Empty)
                FahrenheitField.Text = "0";

            if (HumidityField.Text == String.Empty)
                HumidityField.Text = "0";
        }

        partial void switchActionSheet(UISwitch sender)
        {
            string title = HumiditySwitch.On ? "Turn on Humidity?" : "Turn off Humidity";
            var controller = UIAlertController.Create(title, null, UIAlertControllerStyle.ActionSheet);

            var yesAction = UIAlertAction.Create("Yes, I'm Sure!", UIAlertActionStyle.Default,
                (action) =>
                {
                    HumidityField.Enabled = HumiditySwitch.On;
                    compute(sender, null);
                });


            var noAction = UIAlertAction.Create("No way!", UIAlertActionStyle.Cancel,
                (action) =>
                {
                    HumidityField.Enabled = HumiditySwitch.On = !HumiditySwitch.On;
                    compute(sender, null);
                });

            controller.AddAction(yesAction);
            controller.AddAction(noAction);
            var ppc = controller.PopoverPresentationController;
            if (ppc != null)
            {
                ppc.SourceView = sender;
                ppc.SourceRect = sender.Bounds;
            }

            PresentViewController(controller, true, null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            //Check settings
            CheckSettings();
            //dismiss the keyboard on background touch
            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                FahrenheitField.ResignFirstResponder();
                HumidityField.ResignFirstResponder();
            }));

            //after editing, compute 
            HumidityField.EditingDidEnd += compute;
            FahrenheitField.EditingDidEnd += compute;
            HumiditySwitch.ValueChanged += compute;

            //when slider value is changed, update UI
            WindSlider.ValueChanged += (sender, e) =>
            {
                WindSpeedLabel.Text = String.Format("Wind Speed (0-100 mph): {0}",
                                        (int)WindSlider.Value);
              
                compute(sender, e);
            };

        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            CheckSettings();
        }

        private void CheckSettings()
        {
            NSUserDefaults defaults = NSUserDefaults.StandardUserDefaults;

            HumiditySwitch.On = defaults.BoolForKey("hswitch_pref");
            if (HumiditySwitch.On)
            {
                HumidityField.Enabled = true;
                HumidityField.Text = defaults.StringForKey("hpercent_pef");
            }

            Celsius = defaults.StringForKey("measurement_perf") == "Celsius";
            //debug
            Debug.WriteLine(Celsius);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
