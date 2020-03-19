using System;
using System.Globalization;

namespace calculatorlib
{
    public class DoubleValidator : INumberValidator<double>
    {
        public bool ValidateNumber(string inputValue, out double value)
        {
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            if (numberFormat.NumberDecimalSeparator == ".")
                inputValue = inputValue.Replace(',', '.');
            else
                inputValue = inputValue.Replace('.', ',');
            return double.TryParse(inputValue, NumberStyles.Any, numberFormat, out value);
        }

        
        public bool ValidateTwoNumbers(string inputValue, out double value1, out double value2)
        {
            string[] values = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return ValidateTwoNumbers(values[0], values[1], out value1, out value2);
            }
            value1 = 0.0d;
            value2 = 0.0d;
            return false;
        }

        public bool ValidateTwoNumbers(string inputValue1, string inputValue2, out double value1, out double value2)
        {
            value1 = 0.0d;
            value2 = 0.0d;
            return ValidateNumber(inputValue1, out value1) && 
                ValidateNumber(inputValue2, out value2);
        }
    }
}
