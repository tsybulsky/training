using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public class BoolCalculator : ICalculator<bool>
    {
        public bool Add(bool term1, bool term2)
        {
            return term1 | term2;
        }

        public bool Divide(bool dividend, bool divider)
        {
            if (divider)
                return dividend;
            else
                throw new DivideByZeroException();
        }

        public bool Multiply(bool multiplied, bool factor)
        {
            return multiplied & factor;
        }

        public bool Subtract(bool minuend, bool subtrahend)
        {
            return minuend ^ subtrahend;
        }
    }
}
