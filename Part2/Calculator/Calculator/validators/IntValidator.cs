using System;

namespace calculatorlib
{
    public class IntValidator : INumberValidator<int>
    {
        public bool ValidateNumber(string inputValue, out int value)
        {
            return int.TryParse(inputValue, out value);
        }

        public bool ValidateTwoNumbers(string inputValue1, string inputValue2, out int value1, out int value2)
        {
            value1 = 0;
            value2 = 0;
            return ValidateNumber(inputValue1, out value1) & ValidateNumber(inputValue2, out value2);
        }

        public bool ValidateTwoNumbers(string inputValue, out int value1, out int value2)
        {
            string[] values = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (values.Length > 1)
            {
                return ValidateTwoNumbers(values[0], values[1], out value1, out value2);
            }
            value1 = 0;
            value2 = 0;
            return false;
        }
    }
}
