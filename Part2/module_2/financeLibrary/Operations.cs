using System;
using System.Collections;
using System.Collections.Generic;

namespace FinanceLibrary
{
    public delegate void OperationNotify(Object sender, OperationEventArgs e);

    public class Operations: IEnumerable<Operation>
    {
        private List<Operation> items = new List<Operation>();        
        public event OperationNotify NotifyAddition;
        public event OperationNotify NotifyRemoving;

        public Operations()
        {

        }
        public Operations(List<Operation> operations)
        {
            foreach(var item in operations)
            {
                Add((Operation)item.Clone());
            }
        }

        public Operation this[int index]
        {
            get
            {
                if ((index >= 0) && (index < items.Count))
                    return items[index];
                else
                    throw new IndexOutOfRangeException();
            }
        }
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        public void Add(Operation value)
        {
            int itemIndex = items.FindIndex(x => x.Date > value.Date);
            if (itemIndex == -1)
            {
                items.Add(value);
            }
            else
            {
                items.Insert(itemIndex, value);
            }
            NotifyAddition?.Invoke(this, new OperationEventArgs(value, OperationAction.Add));
        }

        public bool Remove(DateTime date)
        {            
            int index = items.FindIndex(x => x.Date == date);
            
            if (index == -1)
            {
                return false;
            }
            else
            {
                Operation removingItem = items[index];
                items.RemoveAt(index);
                NotifyRemoving?.Invoke(this, new OperationEventArgs(removingItem, OperationAction.Remove));
                return true;
            }
        }

        public bool Remove(DateTime date, decimal value)
        {
            int index = items.FindIndex(item => (item.Date == date) && (item.Value == value));
            if (index == -1)
            {
                return false;
            }
            else
            {
                Operation removingItem = items[index];
                items.RemoveAt(index);
                NotifyRemoving?.Invoke(this, new OperationEventArgs(removingItem, OperationAction.Remove));
                return true;
            }
        }

        public bool RemoveAt(int index)
        {
            if ((index >= 0) && (index < items.Count))
            {
                Operation value = items[index];
                items.RemoveAt(index);
                NotifyRemoving?.Invoke(this, new OperationEventArgs(value, OperationAction.Remove));
                return true;
            }
            else
                return false;
        }
        public Operations GetOperationsByRange(DateTime? startDate, DateTime? endDate)
        {
            return new Operations(items.FindAll(item => ((startDate == null) || ((startDate != null) && (item.Date >= startDate))) &&
            ((endDate == null) || (endDate != null) && (item.Date <= endDate))));
        }

        public decimal GetValueOnDate(DateTime date)
        {
            decimal result = 0;
            foreach(ExpenseOperation item in this)
            {
                if (item.Date <= date)
                {
                    result += item.Value;
                }
                else
                    break;
            }
            return result;
        }
        
        public void Sort(Comparison<Operation> comparison)
        {
            items.Sort(comparison);
        }

        public void Sort()
        {
            items.Sort();
        }

        public Operations GetOperationsByArticle(DateTime? startDate, DateTime? endDate)
        {
            Operations items = GetOperationsByRange(startDate, endDate);
            items.Sort((x1, x2) => x1.Article.CompareTo(x2.Article));
            int i = 1;
            while (i < items.Count)
            {
                if (items[i - 1].Article == items[i].Article)
                {
                    items[i - 1].Value += items[i].Value;
                    items.RemoveAt(i);
                }
                else
                    i++;
            }
            return items;                        
        }
        
        public void ForEach(Action<Operation> action)
        {
            if (action != null)
            {
                foreach (Operation item in items)
                {
                    action(item);
                }
            }
        }
        public void ForEachConditional(Predicate<Operation> predicate, Action<Operation> action)
        {
            foreach(var item in items)
            {
                if (predicate(item))
                {
                    action(item);
                }
            }
        }    
        

        public int FindIndex(Predicate<Operation> predicate)
        {
            return items.FindIndex(predicate);
        }

        public void Clear()
        {
            items.Clear();
        }

        IEnumerator<Operation> IEnumerable<Operation>.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        private IEnumerator<Operation> GetEnumerator1()
        {
            return items.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return GetEnumerator1();
        }
    }
}
