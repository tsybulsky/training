using System;
using System.Text.Json;

namespace FinanceLibrary
{
    public class Operation
    {
        private DateTime date;
        private decimal value;
        public string Article {get; protected set;}

        public string Notes { get; set; }
        public DateTime Date
        {
            get
            {
                return date;
            }
            protected set
            {
                if (value <= DateTime.Today)
                {
                    date = value;
                }
            }
        }
        public decimal Value
        {
            get
            {
                return value;
            }
            set
            {
                if (Value >= 0)
                {
                    this.value = value;
                }
            }
        }

        public Operation(): this(DateTime.Today,0.1m,"")
        {

        }

        public Operation(DateTime date, decimal value, string article)
        {
            Date = date;
            this.value = value;
            Article = article ?? "";
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this, GetType());
        }

        public virtual void Assign(Operation source)
        {
            if (source != null)
            {
                this.date = source.Date;
                this.value = source.Value;
                this.Article = source.Article;
                this.Notes = source.Notes;
            }
        }
    }
}
