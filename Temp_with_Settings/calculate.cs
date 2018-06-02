using System;
namespace Temp_with_Settings
{
    public class calculate
    {
        public calculate()
        {
        }

        public static double getWindChill(double fDegrees, int windSpeed)
        {
            //temperature has to be below 50 degrees fahrenheit and
            //wind speed has to be above 3mph before wind chill can
            //take place
            if (fDegrees < 50 && windSpeed > 3)
                return 35.74 + (0.6215 * fDegrees) - 35.75 * Math.Pow(windSpeed, 0.16)
                 + (0.4275 * fDegrees) * Math.Pow(windSpeed, 0.16);

            else
                return fDegrees;
        }

        public static double getHeatIndex(double fTemp, double humidity)
        {
            //if temp is less than 80, Heat index is useless
			if (fTemp < 80)
				return fTemp;
			
			//equation:
			//http://www.wpc.ncep.noaa.gov/html/heatindex_equation.shtml
			double index = 0.5 * (fTemp + 61.0 + ((fTemp - 68.0) * 1.2) 
			                      + (humidity * 0.094));
                                  
			if(index >= 80)
					index = -42.379 + (2.04901523 * fTemp)
                         + (10.14333127 * humidity)
                         - (0.22475541 * fTemp * humidity)
                         - (.00683783 * fTemp * fTemp)
                         - (.05481717 * humidity * humidity)
                         + (.00122874 * fTemp * fTemp * humidity)
                         + (.00085282 * fTemp * humidity * humidity)
                         - (.00000199 * fTemp * fTemp * humidity * humidity);

            //adjustments according to the equation
			if (humidity < 13 && fTemp > 80 && fTemp < 112)
				index -= ((13 - humidity) / 4) * Math.Sqrt((17 - Math.Abs(fTemp - 95)) / 17);

			else if (humidity > 85 && fTemp > 80 && fTemp < 87)
				index += ((humidity - 85) / 10) * ((87 - fTemp) / 5);         

            return index;
        }

        public static double ToCelsius(int temp) => (temp - 32) * 5 / 9;
    }
}
