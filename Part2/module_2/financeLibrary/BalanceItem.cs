using System;
using System.Collections.Generic;
using System.Text;

namespace FinanceLibrary
{
    public class BalanceItem
    {
        private decimal initialBalance;
        public decimal Debet { get; protected set; }
        public decimal Credit { get; protected set; }
        public decimal InitialBalance
        {
            get
            {
                return initialBalance;
            }
            set
            {
                if (value >= 0)
                {
                    initialBalance = value;
                }
            }
        }

        public decimal FinalBalance
        {
            get
            {
                return initialBalance + Credit - Debet;
            }
        }

        public decimal Difference 
        { 
            get
            {
                return Credit - Debet;
            }
        }

        public DateTime Date { get; set; }

        public void Update(decimal credit, decimal debet)
        {
            Debet = debet;
            Credit = credit;
        }

        public BalanceItem():this(DateTime.Today,0,0)
        {

        }

        public BalanceItem(DateTime date, decimal credit, decimal debet)
        {
            Date = date;
            Credit = credit;
            Debet = debet;
        }
    }
}
