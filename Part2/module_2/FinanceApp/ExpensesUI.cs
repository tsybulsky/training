using System;
using FinanceLibrary;

namespace FinanceApp
{
    public class ExpensesUI
    {
        private Ledger ledger;
        
        public ExpensesUI(Ledger ledger)
        {
            this.ledger = ledger;
        }

        private void ShowTable(DateTime startDate, DateTime endDate, bool showTotals)
        {
            int[] columnWidths = { 6, 12, 11, 45
            };
            void ShowTitle()
            {
                Console.Clear();
                Console.WriteLine($"Расходы за период с {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")}");
                CustomActionUI.DrawBorders(columnWidths, CustomActionUI.HEADER_BORDER_SYMBOLS);
                Console.WriteLine($"\u2502 №п/п \u2502    Дата    \u2502   Сумма   \u2502 {"Статья",-43} \u2502");
                CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            }

            Operations selectedItems = ledger.Expenses.GetOperationsByRange(startDate, endDate);
            ShowTitle();
            int index = 1;
            foreach (Operation item in selectedItems)
            {
                if (!(item is ExpenseOperation))
                    continue;
                ExpenseOperation expense = item as ExpenseOperation;
                string articleAndNotes = item.Article;
                if (!String.IsNullOrWhiteSpace(item.Notes))
                {
                    articleAndNotes += $" ({item.Notes})";
                    if (articleAndNotes.Length > 43)
                        articleAndNotes = articleAndNotes.Substring(0, 40) + "...";
                }
                Console.WriteLine($"\u2502 {index++,4} \u2502 {expense.Date.ToString("dd.MM.yyy")} \u2502 {expense.Value,9:F2} \u2502 {articleAndNotes,-43} \u2502");
            }
            if (showTotals)
            {
                CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
                decimal sum = 0;
                selectedItems.ForEach(x => sum += x.Value);
                Console.WriteLine($"\u2502 {" ",4} \u2502 Всего:     \u2502 {sum,9:F2} \u2502 {" ",43} \u2502");
            }
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
        }

        private void ImportFromFile()
        {
            Console.Write("Введите имя файла с данными: ");
            OperationImport importer = new OperationTextImport();
            if (importer.ImportFromFile(Console.ReadLine(), ledger.Expenses))
            {
                Console.WriteLine("Успешно выполнено. " + importer.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Ошибка: {importer.ErrorMessage}");
            }
            Console.ReadKey();
        }
        private void InputFromConsole()
        {
            Console.Clear();
            bool working = true;
            DateTime? date;
            decimal? value;
            string incomeFrom;
            string notes;            
            while (working)
            {
                date = CustomActionUI.InputDate("Введите дату расхода (пустая строка - завершить): ", true);
                if (date == null)
                {
                    return;
                }
                if (date < ledger.MinWorkingDate)
                {
                    Console.WriteLine($"Минимальная допустимая дата {ledger.MinWorkingDate.ToString("dd.MM.yyyy")}");
                    continue;
                }
                value = CustomActionUI.InputDecimal($"Укажите сумму расхода: ", false);
                string[] articleItems = {
                    "Питание",
                    "Коммунальные платежи",
                    "Проезд",
                    "Одежда/обувь",
                    "Хобби",
                    "Обустройство дома, ремонт",
                    "Развлечение",
                    "Подардки",
                    "Прочие"
                };
                int choice = CustomActionUI.ShowMenu("Выберите статью расхода: ", articleItems, false);
                if (choice == articleItems.Length - 1)
                {
                    Console.WriteLine("Укажите иную статью расхода: ");
                    incomeFrom = Console.ReadLine();
                }
                else
                {
                    incomeFrom = articleItems[choice - 1];
                }
                Console.Write("Примечание: ");
                notes = Console.ReadLine();
                try
                {
                    if (!ledger.AddExpense((DateTime)date, (decimal)value, incomeFrom, notes))
                    {
                        Console.WriteLine(ledger.ErrorMessage);
                        Console.ReadKey();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                }
            }
        }

        private void ExportToFile()
        {

            Console.Write("Введите имя файла: ");
            OperationExport exporter = new OperationTextExport();
            if (exporter.ExportToFile(Console.ReadLine(), ledger.Expenses))
            {
                Console.WriteLine("Успешно завершено");
            }
            else
            {
                Console.WriteLine($"Ошибка: {exporter.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void OutputToConsole()
        {
            int[] columnWidths = { 12, 11, 47 };
            DateTime startDate = CustomActionUI.InputDate("Введите начальную дату периода: ", true) ?? ledger.MinWorkingDate;
            DateTime endDate = CustomActionUI.InputDate("Введите конечную дату периода: ", true) ?? DateTime.Today;
            ShowTable(startDate, endDate, true);            
            Console.Write("Нажмите любую клавишу для возврата в главное меню");
            Console.ReadKey();
        }

        private void DeleteByIndex()
        {
            while (true)
            {
                ShowTable(ledger.MinWorkingDate, DateTime.Today,true);
                if (!CustomActionUI.InputIntegers("Введите индексы удаляемых записей: ",
                    0, ledger.Expenses.Count, out int[] indexes))
                {
                    return;
                }
                Array.Sort(indexes);
                for (int i = indexes.Length - 1; i >= 0; i--)
                {
                    if ((indexes[i] - 1 >= 0) && (indexes[i] - 1 < ledger.Expenses.Count))
                    {
                        ledger.Expenses.RemoveAt(indexes[i] - 1);
                    }
                }
            }
        }

        private void DeleteByData()
        {
            DateTime? date = CustomActionUI.InputDate("Введите дату расхода: ", true);
            if (date == null)
                return;
            decimal? value = CustomActionUI.InputDecimal("Введите израсходованную сумму: ", true);
            if (value == null)
                return;
            if (!ledger.RemoveExpense((DateTime)date, (decimal)value))
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
                int choice = CustomActionUI.ShowMenu("Меню работы с расходами\r\nВыберите действие: ", menuItems, true);
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
