using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public interface INumberValidator<T>
    {
        bool ValidateNumber(string inputValue, out T value);
        //bool ValidateNumber(string inputValue, out double value);
        bool ValidateTwoNumbers(string inputValue1, string inputValue2, out T value1, out T value2);
        //bool ValidateTwoNumbers(string inputValue1, string inputValue2, out double value1, out double value2);
        bool ValidateTwoNumbers(string inputValue, out T value1, out T value2);

        //bool ValidateTwoNumbers(string inputValue, out double value1, out double value2);
    }
}
