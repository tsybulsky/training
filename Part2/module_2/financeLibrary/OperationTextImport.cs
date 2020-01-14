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
            int importedObjects = 0;
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
                    inputValue = inputValue.Trim();
                    if (inputValue[0] == '#')
                        continue;
                    string[] values = inputValue.Split(";", StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 0)
                        continue;
                    Operation operation;
                    if (values[0] == "income")
                        operation = new IncomeOperation();
                    else if (values[0] == "expense")
                        operation = new ExpenseOperation();
                    else
                        continue;
                    if (operation.LoadFromString(inputValue))
                    {
                        items.Add(operation);
                        importedObjects++;
                    }                        
                }
            }
            ErrorMessage = $"Всего прочитено {lineNo} строк. Импортировано {importedObjects} объект(а,ов)";
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
