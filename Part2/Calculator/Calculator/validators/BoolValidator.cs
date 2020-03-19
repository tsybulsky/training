using System;

namespace calculatorlib
{
    public class BoolValidator : INumberValidator<bool>
    {
        public bool ValidateNumber(string inputValue, out bool value)
        {
            inputValue = inputValue.Trim();
            if ((inputValue == "0") || (inputValue == "1"))
            {
                value = inputValue == "1";
                return true;
            }
            else
                return bool.TryParse(inputValue, out value);
        }

        public bool ValidateTwoNumbers(string inputValue1, string inputValue2, out bool value1, out bool value2)
        {
            value1 = false;
            value2 = false;
            return ValidateNumber(inputValue1, out value1) &&
                ValidateNumber(inputValue2, out value2);
        }

        public bool ValidateTwoNumbers(string inputValue, out bool value1, out bool value2)
        {
            string[] values = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return ValidateTwoNumbers(values[0], values[1], out value1, out value2);
            }
            value1 = false;
            value2 = false;
            return false;
        }
    }
}
