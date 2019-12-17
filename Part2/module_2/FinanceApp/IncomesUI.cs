using System;
using System.Collections.Generic;
using FinanceLibrary;

namespace FinanceApp
{
    public class IncomesUI
    {
        private Ledger ledger;
        
        public IncomesUI(Ledger ledger)
        {
            this.ledger = ledger;
        }

        private void ShowTable(DateTime startDate, DateTime endDate, bool ShowTotals)
        {
            int[] columnWidths = { 6, 12, 11, 10, 34 };
            Operations selectedItems = ledger.Incomes.GetOperationsByRange(startDate, endDate);
            Console.Clear();
            Console.WriteLine($"Доходы за период с {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")}");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.HEADER_BORDER_SYMBOLS);
            Console.WriteLine($"\u2502 №п/п \u2502    Дата    \u2502   Сумма   \u2502   Налог  \u2502 {"Статья",-32} \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            int index = 1;
            foreach (Operation item in selectedItems)
            {
                if (!(item is IncomeOperation))
                    continue;
                IncomeOperation income = item as IncomeOperation;
                string articleAndNotes = item.Article;
                if (!String.IsNullOrWhiteSpace(item.Notes))
                {
                    articleAndNotes += $" ({item.Notes})";
                    if (articleAndNotes.Length > 32)
                        articleAndNotes = articleAndNotes.Substring(0, 29) + "...";
                }
                Console.WriteLine($"\u2502 {index++,4} \u2502 {income.Date.ToString("dd.MM.yyy")} \u2502 {income.Value,9:F2} \u2502 {income.GetTaxValue(),8:F2} \u2502 {articleAndNotes,-32} \u2502");
            }
            if (ShowTotals)
            {
                CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
                decimal sum = 0, taxSum = 0;
                selectedItems.ForEach(x => { sum += x.Value; taxSum += ((IncomeOperation)x).TaxValue; });
                Console.WriteLine($"\u2502 {" ",4} \u2502 Всего:     \u2502 {sum,9:F2} \u2502 {taxSum,8:F2} \u2502 {" ",32} \u2502");
            }
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
        }
        private void ImportFromFile()
        {
            Console.Write("Введите имя файла с данными: ");
            OperationImport importer = new OperationTextImport();
            if (importer.ImportFromFile(Console.ReadLine(), ledger.Incomes))
            {
                Console.WriteLine("Импорт завершен. " + importer.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Error: {importer.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void InputFromConsole()
        {            
            Console.Clear();
            bool working = true;
            DateTime? date;
            decimal? value;
            string incomeFrom = "";
            string notes;            
            while (working)
            {
                date = CustomActionUI.InputDate("Введите дату получения дохода (пустая строка - завершить): ", true);
                if (date == null)
                {
                    return;
                }
                if (date < ledger.MinWorkingDate)
                {
                    Console.WriteLine($"Минимальная допустимая дата {ledger.MinWorkingDate.ToString("dd.MM.yyyy")}");
                    continue;
                }
                value = CustomActionUI.InputDecimal($"Укажите сумму дохода: ", false);
                string[] articles = {
                    "Заработная плата",
                    "по договору подряда",
                    "кредит в банке",
                    "проценты по депозиту",
                    "выигрыш в казино",
                    "прочие" };
                int choice = CustomActionUI.ShowMenu("Выберите источник дохода", articles, false);
                if (choice < articles.Length - 1)
                {
                    incomeFrom = articles[choice - 1];
                }
                else if (choice == articles.Length)
                {
                    Console.Write("Укажите иной источник дохода: ");
                    incomeFrom = Console.ReadLine();
                }                
                double taxRate = (CustomActionUI.ShowYesNo("Облагается налогом?")) ? 13 : 0;
                Console.Write("\r\nПримечание: ");
                notes = Console.ReadLine();
                try
                {
                    ledger.AddIncome((DateTime)date, (decimal)value, incomeFrom, taxRate, notes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void ExportToFile()
        {
            if (!CustomActionUI.InputFilename("Введите имя файла для экспорта: ", false, out string filename))
                return;
            OperationExport exporter = new OperationTextExport();
            if (exporter.ExportToFile(filename, ledger.Incomes))
            {
                Console.WriteLine("Экспорт в файл завершен. " + exporter.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Error: {exporter.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void OutputToConsole()
        {            
            DateTime startDate = CustomActionUI.InputDate("Введите начальную дату периода", true) ?? ledger.MinWorkingDate;
            DateTime endDate = CustomActionUI.InputDate("Введите конечную дату периода", true) ?? DateTime.Today;
            ShowTable(startDate, endDate, true);
            Console.Write("Нажмите любую клавишу для возврата в главное меню");
            Console.ReadKey();
        }

        private void DeleteByIndex()
        {
            while (true)
            {                
                ShowTable(ledger.MinWorkingDate, DateTime.Today,false);
                if (!CustomActionUI.InputIntegers("Введите индексы удаляемых записей: ", 
                    0, ledger.Incomes.Count,out int[] indexes))
                {
                    return;
                }
                Array.Sort(indexes);
                for (int i = indexes.Length-1;i>= 0;i--)
                {
                    if ((indexes[i] >= 0)&&(indexes[i] < ledger.Incomes.Count))
                    {
                        ledger.Incomes.RemoveAt(indexes[i]-1);
                    }
                }
            }
        }

        private void DeleteByData()
        {
            DateTime? date = CustomActionUI.InputDate("Введите дату прихода: ",true);
            if (date == null)
                return;
            decimal? value = CustomActionUI.InputDecimal("Введите сумму: ", true);
            if (value == null)
                return;
            if (!ledger.RemoveIncome((DateTime)date, (decimal)value))
            {
                Console.WriteLine(ledger.ErrorMessage);
            }
            
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }
        public void Show()
        {
            string[] menuItems =
            {
                "Ввод данных с консоли",
                "Импорт данных",
                "Вывод данных",
                "Экспорт данных",
                "Удаление по индексу",
                "Удаление по сумме"                
            };
            
            bool running = true;
            while (running)
            {
                Console.Clear();
                int choice = CustomActionUI.ShowMenu("Меню работы с доходами\r\nВыберите действие: ", menuItems, true);
                switch (choice)
                {
                    case 0:
                        {
                            running = false;
                            break;
                        }
                    case 1:
                        {
                            InputFromConsole();
                            break;
                        }
                    case 5:
                        {
                            DeleteByIndex();
                            break;
                        }
                    case 6:
                        {
                            DeleteByData();
                            break;
                        }
                    case 3:
                        {
                            OutputToConsole();
                            break;
                        }
                    case 2:
                        {
                            ImportFromFile();
                            break;
                        }
                    case 4:
                        {
                            ExportToFile();
                            break;
                        }
                }
            }
        }
    }
}
