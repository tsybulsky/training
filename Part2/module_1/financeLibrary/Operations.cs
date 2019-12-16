using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace FinanceLibrary
{
    public class Operations:List<Operation>
    {
        public Operations()
        {

        }
        public Operations(List<Operation> operations)
        {
            foreach(var item in operations)
            {
                Add(new Operation(item.Date, item.Value, item.Article) { Notes = item.Notes });
            }
        }

        public new void Add(Operation value)
        {
            int itemIndex = this.FindIndex(x => x.Date > value.Date);
            if (itemIndex == -1)
            {
                base.Add(value);
            }
            else
            {
                base.Insert(itemIndex, value);
            }
        }

        public bool RemoveIncome(DateTime date)
        {
            int index = FindIndex(x => x.Date == date);
            if (index == -1)
            {
                return false;
            }
            else
            {
                RemoveAt(index);
                return true;
            }
        }

        public bool RemoveIncome(DateTime date, decimal value)
        {
            int index = FindIndex(item => (item.Date == date) && (item.Value == value));
            if (index == -1)
            {
                return false;
            }
            else
            {
                RemoveAt(index);
                return true;
            }
        }

        public Operations GetOperationsByRange(DateTime? startDate, DateTime? endDate)
        {
            return new Operations(FindAll(item => ((startDate == null) || ((startDate != null) && (item.Date >= startDate))) &&
            ((endDate == null) || (endDate != null) && (item.Date <= endDate))));
        }

        public decimal GetValueOnDate(DateTime date)
        {
            decimal result = 0;
            foreach(Operation item in this)
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
        
        public override string ToString()
        {
            return JsonSerializer.Serialize(this, GetType(), new JsonSerializerOptions() { WriteIndented = true });
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
        
        public void ForEachConditional(Predicate<Operation> predicate, Action<Operation> action)
        {
            foreach(var item in this)
            {
                if (predicate(item))
                {
                    action(item);
                }
            }
        }      
    }
}
