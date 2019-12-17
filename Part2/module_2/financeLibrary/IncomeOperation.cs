using System;
using System.Xml;
using System.Globalization;

namespace FinanceLibrary
{
    public class IncomeOperation: Operation
    {
        public double TaxRate { get; set; }
        public decimal ValueWithTax { get; private set; }
        public decimal TaxValue
        {
            get
            {
                return ValueWithTax - Value;
            }
        }
        public IncomeOperation()
        {

        }

        public IncomeOperation(DateTime date, decimal value, string article, double rate) : base(date, value, article)
        {
            TaxRate = rate;
            ValueWithTax = value;
            Value = value * (100m - (decimal)rate) / 100;
        }

        public decimal GetTaxValue()
        {
            return ValueWithTax * (decimal)(TaxRate / 100);
        }

        protected override string GetOperationName()
        {
            return "income";
        }

        public override bool LoadFromXml(XmlNode node)
        {
            if (!base.LoadFromXml(node))
                return false;
            XmlAttribute attribute = node.Attributes["taxRate"];
            if (attribute == null)
            {
                TaxRate = 0;
            }
            else
            {
                NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
                numberFormat.NumberDecimalSeparator = ".";
                if (!double.TryParse(attribute.Value, NumberStyles.Any, numberFormat, out double tempRate))
                    return false;
                if ((tempRate >= 0) && (tempRate < 100))
                    TaxRate = tempRate;
                else
                    return false;
            }
            return true;
        }

        public override bool StoreToXml(XmlNode node)
        {
            if (!base.StoreToXml(node))
                return false;
            XmlDocument doc = node.OwnerDocument;
            XmlAttribute attribute = doc.CreateAttribute("taxRate");
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            numberFormat.NumberDecimalSeparator = ".";
            attribute.Value = TaxRate.ToString(numberFormat);
            return true;
        }

        protected override int LoadCustomFields(string[] values)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            if (values.Length > 3)
            {
                if (double.TryParse(values[3],NumberStyles.Any,culture.NumberFormat,out double tempValue))
                {
                    TaxRate = tempValue;
                    ValueWithTax = Value / ((decimal)((100 - TaxRate) / 100));
                    return 1;
                }
            }
            return 0;
        }

        protected override string StoreCustomFields()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            return ";"+ TaxRate.ToString(culture.NumberFormat);
        }

        public override object Clone()
        {
            return new IncomeOperation(Date, ValueWithTax, Article, TaxRate) { Notes = this.Notes };
        }
    }
}
