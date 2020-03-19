using System;
using System.Collections.Generic;
using System.Text;

namespace DataChecks
{
    public interface INumberCheck<T>
    {
        bool Check(string inputValue);
        bool PositiveCheck(string inputValue, out T value);
        bool NegativeCheck(string inputValue, out T value);

        bool RangeCheck(string inputValue, T lowBound, T highBound, out T value);
    }
}
