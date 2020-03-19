using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public class CharCalculator : ICalculator<char>
    {
        public char Add(char term1, char term2)
        {
            return (char)((int)term1 + (int)term2);
        }

        public char Divide(char dividend, char divider)
        {
            if ((int)divider == 0)
                throw new DivideByZeroException();
            else
                return (char)((int)dividend / (int)divider);
        }

        public char Multiply(char multiplied, char factor)
        {
            return (char)((int)multiplied * (int)factor);
        }

        public char Subtract(char minuend, char subtrahend)
        {
            return (char)((int)minuend - (int)subtrahend);
        }
    }
}
