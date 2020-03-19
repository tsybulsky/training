using System;
using System.Collections.Generic;
using System.Text;

namespace calculatorlib
{
    public interface ICalculator<T>
    {
        T Add(T term1, T term2);
        T Subtract(T minuend, T subtrahend);

        T Multiply(T multiplied, T factor);
        T Divide(T dividend, T divider);
    }
}
