using System;
using System.Globalization;

namespace calculatorlib
{
    public class StringCalculator : ICalculator<string>
    {
        public string Add(string term1, string term2)
        {
            INumberValidator<double> validator = new DoubleValidator();
            if (validator.ValidateNumber(term1, out double value1) &&
                validator.ValidateNumber(term2, out double value2))
                return (value1 + value2).ToString();
            else
                throw new ArgumentException();
        }

        public string Divide(string dividend, string divider)
        {
            INumberValidator<double> validator = new DoubleValidator();
            if (validator.ValidateNumber(dividend, out double value1) &&
                validator.ValidateNumber(divider, out double value2))
            {
                if (value2 == 0)
                    throw new DivideByZeroException();
                else
                    return (value1 / value2).ToString();
            }
            else
                throw new ArgumentException();
        }

        public string Multiply(string multiplied, string factor)
        {
            INumberValidator<double> validator = new DoubleValidator();
            if (validator.ValidateNumber(multiplied, out double value1) &&
                validator.ValidateNumber(factor, out double value2))
            {
                return (value1 * value2).ToString();
            }
            else
                throw new ArgumentException();
        }

        public string Subtract(string minuend, string subtrahend)
        {
            INumberValidator<double> validator = new DoubleValidator();
            if (validator.ValidateNumber(minuend, out double value1) &&
                validator.ValidateNumber(subtrahend, out double value2))
            {
                return (value1 - value2).ToString();
            }
            else
                throw new ArgumentException();
        }
    }
}
