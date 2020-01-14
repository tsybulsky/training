using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public class DoubleCalculator : ICalculator<double>
    {
        public double Add(double term1, double term2)
        {
            return term1 + term2;
        }

        public double Divide(double dividend, double divider)
        {
            if (divider == 0)
                throw new DivideByZeroException();
            else
                return dividend / divider;
        }

        public double Multiply(double multiplied, double factor)
        {
            return multiplied * factor;
        }

        public double Subtract(double minuend, double subtrahend)
        {
            return minuend - subtrahend;
        }
    }
}
