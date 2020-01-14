using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public class CharValidator : INumberValidator<char>
    {
        public bool ValidateNumber(string inputValue, out char value)
        {
            if (String.IsNullOrEmpty(inputValue))
            {
                value = '\x00';
                return false;
            }
            else
            {
                value = inputValue[0];
                return true;
            }
        }

        public bool ValidateTwoNumbers(string inputValue1, string inputValue2, out char value1, out char value2)
        {
            value1 = '\x00';
            value2 = '\x00';
            return ValidateNumber(inputValue1, out value1) &&
                ValidateNumber(inputValue2, out value2);
        }

        public bool ValidateTwoNumbers(string inputValue, out char value1, out char value2)
        {
            string[] values = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return ValidateTwoNumbers(values[0], values[1], out value1, out value2);
            }
            value1 = '\0';
            value2 = '\0';
            return false;
        }
    }
}
