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


        public bool Metric
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


            if (!Metric)
            {
                int result = HumiditySwitch.On ? (int)Math.Round(calculate.getHeatIndex(temp, humidity))
                            : (int)Math.Round(calculate.getWindChill(temp, windspeed));

                //if celsius is selected
                //if (Metric)
                    //result = (int)calculate.ToCelsius(result);

                ResultLabel.Text = $"Result: {result} F";

                if (HumiditySwitch.On)
                    if (temp != 0 && humidity != 0)
                        CalculationHistoryController.AddData($"Temp: {temp}, Humidity: {humidity}, Result: {result} F");
                
                else
                    if (temp != 0 && windspeed != 0)
                        CalculationHistoryController.AddData($"Temp: {temp}, Wind speed: {windspeed}, Result: {result} F");
            }

            //Metric
            else 
            {
                //calculate to english, get result, return to metric
                //too lazy to write metric equation
                int result = HumiditySwitch.On ? (int)Math.Round(calculate.MetricHeatIndex(temp, humidity)) :
                                           (int)Math.Round(calculate.MetricWindChill(temp, windspeed));
                
                ResultLabel.Text = $"Result: {result} C";

                if (HumiditySwitch.On)
                    if (temp != 0 && humidity != 0)
                        CalculationHistoryController.AddData($"Temp: {temp}, Humidity: {humidity}, Result: {result} C");

                    else
                    if (temp != 0 && windspeed != 0)
                        CalculationHistoryController.AddData($"Temp: {temp}, Wind speed: {windspeed}, Result: {result} C");
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
            RefreshFields();
            //dismiss the keyboard on background touch
            View.AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                FahrenheitField.ResignFirstResponder();
                HumidityField.ResignFirstResponder();
                compute(null, null);
            }));

            //after editing, compute 
            HumidityField.EditingDidEnd += compute;
            FahrenheitField.EditingDidEnd += compute;
            HumiditySwitch.ValueChanged += compute;

            //when slider value is changed, update UI
            WindSlider.ValueChanged += (sender, e) =>
            {
                //lazy, clean up kmph/mph
                WindSpeedLabel.Text =  Metric ? $"Wind Speed (0-100 kph) {WindSlider.Value}" :
                    $"Wind Speed (0-100 mph) {WindSlider.Value}";
                
                compute(sender, e);
            };

        }

        NSObject observer = null;
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            RefreshFields();

            // Subscribe to the applicationWillEnterForeground notification
            var app = UIApplication.SharedApplication;
            observer = NSNotificationCenter.DefaultCenter.AddObserver(aName: UIApplication.WillEnterForegroundNotification, notify: ApplicationWillEnterForeground, fromObject: app);
        }

        // We will subscribe to the applicationWillEnterForeground notification
        // so that this method is called when that notification occurs
        private void ApplicationWillEnterForeground(NSNotification notification)
        {
            var defaults = NSUserDefaults.StandardUserDefaults;
            defaults.Synchronize();
            RefreshFields();
        }

        private void RefreshFields()
        {
            NSUserDefaults defaults = NSUserDefaults.StandardUserDefaults;

            HumiditySwitch.On = defaults.BoolForKey(Constants.HUMID_SWITCH_KEY);
            if (HumiditySwitch.On)
            {
                HumidityField.Enabled = true;
                HumidityField.Text = defaults.StringForKey(Constants.HUMID_PERCENT_KEY);
            }

            WindSlider.Value = defaults.IntForKey(Constants.WIND_SPEED_KEY);
            WindSpeedLabel.Text = Metric ? $"Wind Speed (0-100 kph) {WindSlider.Value}" :
                    $"Wind Speed (0-100 mph) {WindSlider.Value}";
            Metric = defaults.StringForKey(Constants.MEASUREMENT_KEY) == "Metric";
            string bgColor = defaults.StringForKey(Constants.BACKGROUND_KEY);
            switch (bgColor)
            {
                case "White":
                    View.BackgroundColor = UIColor.White;
                    break;
                case "Green":
                    View.BackgroundColor = UIColor.Green;
                    break;
                case "Blue":
                    View.BackgroundColor = UIColor.Blue;
                    break;
            }
            compute(null, null);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
