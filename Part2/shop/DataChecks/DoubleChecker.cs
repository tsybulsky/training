using System;
using System.Globalization;

namespace DataChecks
{
    public class DoubleChecker : INumberCheck<double>
    {
        private bool Convert(string value, out double result)
        {
            
            NumberFormatInfo format = CultureInfo.CurrentCulture.NumberFormat;
            value = value.Replace(format.NumberDecimalSeparator, ".").Replace(format.NumberDecimalSeparator, ",");
             
            return double.TryParse(value, NumberStyles.Float, format, out result);
        }

        public bool Check(string inputValue)
        {            
            return Convert(inputValue, out double _);
        }

        public bool NegativeCheck(string inputValue, out double value)
        {
            if (Convert(inputValue, out value))
            {
                return value < 0;
            }
            else
                return false;
        }

        public bool PositiveCheck(string inputValue, out double value)
        {
            if (Convert(inputValue, out value))
            {
                return value > 0;
            }
            else
                return false;
        }

        public bool RangeCheck(string inputValue, double lowBound, double highBound, out double value)
        {
            if (Convert(inputValue, out value))
            {
                return (value >= lowBound) && (value <= highBound);
            }
            else
                return false;
        }
    }
}
