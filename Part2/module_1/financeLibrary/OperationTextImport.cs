using System;
using System.Text;
using System.Globalization;
using System.IO;

namespace FinanceLibrary
{
    public class OperationTextImport: OperationImport
    {
        public override bool ImportFromStream(Stream stream, Operations items)
        {
            int lineNo = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                if (reader is null)
                {
                    ErrorMessage = "Ошибка создания объекта";
                    return false;
                }
                
                items.Clear();
                string inputValue;
                while ((inputValue = reader.ReadLine()) != null)
                {                    
                    lineNo++;
                    if (String.IsNullOrWhiteSpace(inputValue))
                        continue;
                    string[] values = inputValue.Split(';', StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length < 2)
                    {
                        ErrorMessage = $"Строка {lineNo} содержит слишком мало значений";
                        return false;
                    }
                    DateTimeFormatInfo dateTimeFormat = new DateTimeFormatInfo();
                    dateTimeFormat.ShortDatePattern = "dd/MM/yyyy";                    
                    if (!DateTime.TryParse(values[0], dateTimeFormat, DateTimeStyles.None, out DateTime date))
                    {
                        ErrorMessage = $"Неверная дата в строке {lineNo}";
                        return false;
                    }
                    if (!decimal.TryParse(values[1], out decimal value))
                    {
                        ErrorMessage = $"Неверное значение суммы в строке {lineNo}";
                        return false;
                    }
                    Operation item = new Operation(date, value, values[2]);
                    if (values.Length > 3)
                    {
                        StringBuilder notes = new StringBuilder(256);
                        for (int i = 3; i < values.Length; i++)
                            notes.Append(values[i]);
                        item.Notes = notes.ToString();
                    }
                    items.Add(item);
                }
            }
            ErrorMessage = $"Всего прочитено {lineNo} строк";
            return true;
        }

        public override bool ImportFromFile(string filename, Operations items)
        {
            if (!File.Exists(filename))
            {
                ErrorMessage = $"Файл {filename} не существует";
                return false;
            }
            using (FileStream stream = new FileStream(filename,FileMode.Open))
            {
                return ImportFromStream(stream,items);
            }
        }
    }
}
