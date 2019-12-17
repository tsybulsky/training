using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    public static class DateTimeExtensions
    {
        public static bool Between(this DateTime value, DateTime startDate, DateTime endDate)
        {
            return (value >= startDate) && (value <= endDate);
        }
    }
}
