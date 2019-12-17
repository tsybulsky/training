using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    public class Balance: List<BalanceItem>
    {    
        private void UpdateBalance()
        {            
            for (int i = 1; i < Count; i++)
            {
                this[i].InitialBalance = this[i - 1].FinalBalance;
            }
        }

        public new void Add(BalanceItem item)
        {
            int index = FindIndex(x => x.Date > item.Date);
            if (index == -1)
            {
                base.Add(item);
            }
            else
            {
                Insert(index, item);
            }
            UpdateBalance();
        }

        public new void Insert(int index, BalanceItem item)
        {
            base.Insert(index, item);
            UpdateBalance();
        }

        public new void RemoveAt(int index)
        {
            base.RemoveAt(index);
            UpdateBalance();
        }

        public Balance(decimal initialBalance, Operations incomes, Operations expenses)
        {
            foreach(Operation income in incomes)
            {
                BalanceItem item = Find(x => x.Date == income.Date);
                if (item == null)
                {
                    Add(new BalanceItem(income.Date, income.Value, 0));
                }
                else
                {
                    item.Update(item.Credit + income.Value, item.Debet);
                }
            }

            foreach(Operation expense in expenses)
            {
                BalanceItem item = Find(x => x.Date == expense.Date);
                if (item == null)
                {
                    int index = FindIndex(x => x.Date > expense.Date);
                    if (index == -1)
                    {
                        Add(new BalanceItem(expense.Date, 0m, expense.Value));
                    }
                    else
                    {
                        Insert(index, new BalanceItem(expense.Date, 0m, expense.Value));
                    }
                }
                else
                {
                    item.Update(item.Credit, item.Debet+expense.Value);
                }
            }
            if (Count > 0)
            {
                this[0].InitialBalance = initialBalance;
                for (int i = 1; i < Count; i++)
                {
                    this[i].InitialBalance = this[i - 1].FinalBalance;
                }
            }
        }
    }
}
