using System;
using System.Xml;
using System.Globalization;

namespace FinanceLibrary
{
    public class ExpenseOperation: Operation
    {
        public ExpenseOperation()
        { 
        
        }
        public ExpenseOperation(DateTime date, decimal value, string article): base(date,value,article)
        { }
        protected override string GetOperationName()
        {
            return "expense";
        }
        public override object Clone()
        {
            return new ExpenseOperation(Date, Value, Article) { Notes = this.Notes };
        }
    }
}
