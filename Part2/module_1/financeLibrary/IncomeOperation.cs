using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    class IncomeOperation: Operation
    {
        public double TaxRate { get; set; }

        public IncomeOperation(DateTime date, decimal value, string article, double rate) : base(date, value, article)
        {
            TaxRate = rate;
            Value = Value * (100m - (decimal)rate) / 100;
        }
    }
}
