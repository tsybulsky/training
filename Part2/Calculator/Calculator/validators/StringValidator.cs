using System;

namespace calculatorlib
{
    public class StringValidator : INumberValidator<string>
    {
        public bool ValidateNumber(string inputValue, out string value)
        {
            value = inputValue;
            return !String.IsNullOrEmpty(value);
        }

        public bool ValidateTwoNumbers(string inputValue1, string inputValue2, out string value1, out string value2)
        {
            value1 = "";
            value2 = "";
            return ValidateNumber(inputValue1, out value1) &&
                ValidateNumber(inputValue2, out value2);
        }

        public bool ValidateTwoNumbers(string inputValue, out string value1, out string value2)
        {
            string[] values = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return ValidateTwoNumbers(values[0], values[1], out value1, out value2);
            }
            value1 = "";
            value2 = "";
            return false;
        }
    }
}
