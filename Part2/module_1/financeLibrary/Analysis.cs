using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    public class Analysis
    {
        private Operations incomes;
        private Operations expenses;

        public Analysis(Operations incomes, Operations expenses)
        {
            this.incomes = incomes;
            this.expenses = expenses;
        }

        public Balance GetBalance(DateTime startDate, DateTime endDate)
        {
            decimal initialIncome = 0;
            incomes.ForEachConditional(x => x.Date < startDate, x => initialIncome += x.Value);
            decimal initialExpense = 0;
            expenses.ForEachConditional(x => x.Date < startDate, x => initialExpense += x.Value);
            return new Balance(initialIncome-initialExpense,
                incomes.GetOperationsByRange(startDate, endDate),
                expenses.GetOperationsByRange(startDate, endDate));
        }
    }
}
