using System;
using System.Collections.Generic;

namespace FinanceLibrary
{
    public class Ledger
    {
        private const int MESSAGE_INCOME_NOT_FOUND = 0;
        private const int MESSAGE_EXPENSE_NOT_FOUND = 1;
        private const int MESSAGE_NOT_ENOUGH_MONEY = 2;
        private const int MESSAGE_INVALID_TAX_RATE = 3;
        private const int MESSAGE_INVALID_OPERATION_DATE = 4;
        private readonly string[] messages =
        {
            "Указанный доход не найден",
            "Указ" +
                "анный расход не найден",
            "Не достаточно денег для такого расхода",
            "Неверная ставка подоходного налога"
        };
        
        private Operations incomes = new Operations();
        private Operations expenses = new Operations();

        public DateTime MinWorkingDate { get; private set; }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                string retValue = errorMessage;
                errorMessage = "";
                return retValue;
            }
        }

        public Operations Incomes
        {
            get
            {
                return incomes;
            }
        }

        public Operations Expenses
        {
            get
            {
                return expenses;
            }
        }
        public Ledger()
        {
            MinWorkingDate = new DateTime(2000, 1, 1);
        }
        public bool AddIncome(DateTime date, decimal value, string article, double rate, string notes)
        {
            errorMessage = "";
            if ((rate >= 0) && (rate < 100))
            {
                if (incomes.Count == 0)
                {
                    MinWorkingDate = date.AddDays(-1);
                }
                if ((date <= MinWorkingDate) ||(date > DateTime.Today))
                {
                    errorMessage = messages[MESSAGE_INVALID_OPERATION_DATE];
                    return false;
                }
                incomes.Add(new IncomeOperation(date, value, article, rate) { Notes = notes});
                return true;
            }
            else
            {
                errorMessage = messages[MESSAGE_INVALID_TAX_RATE];
                return false;
            }
        }

        public bool AddIncome(DateTime date, decimal value, string article, double rate)
        {
            return AddIncome(date, value, article, rate,"");
        }

        public bool RemoveIncome(DateTime date, decimal value)
        {
            errorMessage = "";
            int index = incomes.FindIndex(x => ((x.Date == date) && (x.Value == value)));
            if (index != -1)
            {
                incomes.RemoveAt(index);
                return true;
            }
            else
            {
                errorMessage = messages[MESSAGE_INCOME_NOT_FOUND];
                return false;
            }
        }

        public bool AddExpense(DateTime date, decimal value, string article, string notes)
        {
            decimal restValue = incomes.GetValueOnDate(date) - expenses.GetValueOnDate(date);
            if (value > restValue)
            {
                errorMessage = messages[MESSAGE_NOT_ENOUGH_MONEY];
                return false;
            }
            else
            {
                expenses.Add(new ExpenseOperation(date, value, article) { Notes = notes });
                return true;
            }
        }

        public bool AddExpense(DateTime date, decimal value, string article)
        {
            return AddExpense(date, value, article,"");
        }

        public bool RemoveExpense(DateTime date, decimal value)
        {
            errorMessage = "";
            int index = expenses.FindIndex(x => (x.Date == date) && (x.Value == value));
            if (index == -1)
            {
                errorMessage = messages[MESSAGE_EXPENSE_NOT_FOUND];
                return false;
            }
            else
            {
                expenses.RemoveAt(index);
                return true;
            }
        }

        public Balance GetBalance(DateTime startDate, DateTime endDate)
        {
            decimal initialIncome = 0;
            incomes.ForEachConditional(x => x.Date < startDate, x => initialIncome += x.Value);
            decimal initialExpense = 0;
            expenses.ForEachConditional(x => x.Date < startDate, x => initialExpense += x.Value);
            return new Balance(initialIncome - initialExpense,
                incomes.GetOperationsByRange(startDate, endDate),
                expenses.GetOperationsByRange(startDate, endDate));
        }

        public List<(DateTime, decimal)> GetTaxes(DateTime startDate, DateTime endDate)
        {
            List<(DateTime, decimal)> retValue = new List<(DateTime, decimal)>();
            incomes.ForEachConditional(x => (x.Date >= startDate) && (x.Date <= endDate),
               x=> retValue.Add((x.Date, ((IncomeOperation)x).GetTaxValue())));
            return retValue;
        }

        public Operations GetTaxFree(DateTime startDate, DateTime endDate)
        {
            Operations retValue = new Operations();
            incomes.ForEachConditional(x => x.Date.Between(startDate, endDate) && (((IncomeOperation)x).TaxRate == 0),
                delegate (Operation operation)
                {
                    retValue.Add((Operation)operation.Clone());
                });
            return retValue;
        }
    }
}
