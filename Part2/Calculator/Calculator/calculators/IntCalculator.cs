using System;

namespace calculatorlib
{
    public class IntCalculator : ICalculator<int>
    {
        public int Add(int term1, int term2)
        {
            return term1 + term2;
        }

        public int Divide(int dividend, int divider)
        {
            if (divider != 0)
                return dividend / divider;
            else
                throw new DivideByZeroException();
        }

        public int Multiply(int multiplied, int factor)
        {
            return multiplied * factor;
        }

        public int Subtract(int minuend, int subtrahend)
        {
            return minuend - subtrahend;
        }
    }
}
