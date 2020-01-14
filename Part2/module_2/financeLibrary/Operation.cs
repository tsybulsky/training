using System;
using System.Globalization;
using System.Xml;

namespace FinanceLibrary
{
    public abstract class Operation: ICloneable, IComparable
    {
        private DateTime date;
        private decimal value;
        public string Article { get; protected set; }

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

        public Operation() : this(DateTime.Today, 0.1m, "")
        {

        }

        public Operation(DateTime date, decimal value, string article)
        {
            Date = date;
            this.value = value;
            Article = article ?? "";
        }

        protected virtual string GetOperationName()
        {
            return "custom";
        }

        public virtual bool LoadFromXml(XmlNode node)
        {
            XmlAttribute attribute = node.Attributes["date"];
            if (attribute == null)
                return false;
            DateTimeFormatInfo dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            dateFormat.ShortDatePattern = "dd/MM/yyyy";
            if (!DateTime.TryParse(attribute.Value, dateFormat, DateTimeStyles.None, out date))
                return false;
            attribute = node.Attributes["value"];
            if (attribute == null)
                return false;
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            numberFormat.NumberDecimalSeparator = ".";
            if (!decimal.TryParse(attribute.Value, NumberStyles.Number, numberFormat, out value))
                return false;
            attribute = node.Attributes["article"];
            if (attribute != null)
                Article = attribute.Value;
            Notes = node.InnerText;
            return true;
        }

        public virtual bool StoreToXml(XmlNode node)
        {
            if (node == null)
                return false;
            XmlDocument doc = node.OwnerDocument;
            XmlAttribute attribute = doc.CreateAttribute("date");
            attribute.Value = date.ToString("dd/MM/yyyy");
            node.Attributes.Append(attribute);
            attribute = doc.CreateAttribute("value");
            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            numberFormat.NumberDecimalSeparator = ".";
            attribute.Value = value.ToString(numberFormat);
            node.Attributes.Append(attribute);
            attribute = doc.CreateAttribute("article");
            attribute.Value = Article;
            node.Attributes.Append(attribute);
            node.InnerText = Notes;
            return true;
        }

        protected virtual int LoadCustomFields(string[] values)
        {
            return 0;
        }

        protected virtual string StoreCustomFields()
        {
            return "";
        }

        public bool LoadFromString(string value)
        {
            if (String.IsNullOrWhiteSpace(value))
                return false;
            value = value.Trim();
            if (value[0] == '#')
                return false;
            string[] values = value.Split(";", StringSplitOptions.RemoveEmptyEntries);            
            if (values.Length < 3)
                return false;
            if (values[0] != GetOperationName())
                return false;
            CultureInfo culture = CultureInfo.InvariantCulture;
            if (!DateTime.TryParse(values[1], culture.DateTimeFormat, DateTimeStyles.None, out this.date))
                return false;
            if (!decimal.TryParse(values[2], NumberStyles.Any, culture.NumberFormat, out this.value))
                return false;
            int offset = LoadCustomFields(values);
            Article = (values.Length > (offset+3)) ? values[offset+3] : "";
            Notes = (values.Length > (offset+4)) ? values[offset+4] : "";
            return true;
        }
        public virtual string StoreToString()
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            string value = String.Format("{0};{1};{2}",
                new object[]{
                    GetOperationName(),
                    date.ToString(culture.DateTimeFormat),
                    Value.ToString(culture.NumberFormat)});
            value += StoreCustomFields();
            return value + $";{Article};{Notes}";
        }
        
        public abstract object Clone();
        public int CompareTo(object obj)
        {
            return Date.CompareTo(((Operation)obj).Date);
        }
    }
}
