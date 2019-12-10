using System;
using FinanceLibrary;
using System.Globalization;

namespace FinanceApp
{
    class FinanceApplication
    {
        private Operations incomes;
        private Operations expenses;
        private Analysis analysis;
        private DateTime startWorkingDate;        
        private readonly string topHeaderBorderSymbols = "\u250C\u252C\u2510";
        private readonly string bottomHeaderBorderSymbols = "\u251C\u253C\u2524";
        private readonly string footerBorderSymbols = "\u2514\u2534\u2518";
        public FinanceApplication()
        {
            incomes = new Operations();
            expenses = new Operations();
            analysis = new Analysis(incomes, expenses);
        }

        public void Initialize()
        {
            decimal value = InputDecimal("Введите начальный остаток: ",false)??0;
            DateTime startDate = InputDate("Введите начальную дату: ",false)??DateTime.Today;            
            incomes.Add(new Operation(startDate.AddDays(-1), value, "Начальный остаток"));
            startWorkingDate = startDate;
            Console.Clear();
        }

        public void Run()
        {
            string[] menuItems ={
                "ввод данных о доходах", 
                "вывод данных о доходах",                
                "ввод данных о расходах", 
                "вывод данных о расходах",
                "анализ расходов и доходов"
            };
            bool running = true;
            while (running)
            {
                int choice = ShowMenu("Выберите действие: ", menuItems, false, true);
                switch (choice)
                {
                    case 0:
                        {
                            Console.Write("Вы действительно хотите выйти? [Y/N]");
                            ConsoleKeyInfo key;
                            do
                            {
                                key = Console.ReadKey();
                                Console.Write("\b");
                                running = key.Key != ConsoleKey.Y;
                            } while ((key.Key != ConsoleKey.Y) && (key.Key != ConsoleKey.N));                       
                            break;
                        }
                    case 1:
                        {
                            InputIncomes();
                            break;
                        }
                    case 2:
                        {
                            OutputIncomes();
                            break;
                        }
                    case 3:
                        {
                            InputExpenses();
                            break;
                        }
                    case 4:
                        {
                            OutputExpenses();
                            break;
                        }
                    case 5:
                        {
                            Analyst();
                            break;
                        }
                }
            }
        }

        private int ShowMenu(string title, string[] items, bool groupping, bool showExit)
        {            
            if (items.Length > 22)
            {
                groupping = true;
            }            
            string keys = "123456789ABCDEFGHJIKLMNOPQRSTUVWXYZ";                        
            Console.Clear();
            if (!String.IsNullOrWhiteSpace(title))
                Console.WriteLine(title);
            if (groupping)
            {
                for (int i = 0; i < items.Length; i += 3)
                {
                    Console.Write($"{keys[i]} - {items[i],24} ");
                    if (i < items.Length - 1)
                    {
                        Console.Write($"{ keys[i + 1]} - {items[i + 1],24} ");
                        if (i < items.Length - 2)
                        {
                            Console.Write($"{keys[i + 2]} - {items[i + 2],24} ");
                        }
                    }
                    Console.WriteLine();
                }
                if (showExit)
                {
                    Console.WriteLine("ESC - выход");
                }
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine($"{ keys[i]} - {items[i]}");
                }
                if (showExit)
                {
                    Console.WriteLine("ESC - выход");
                }
            }
            int index;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorTop -= 1;
                }
                else
                {
                    Console.Write("\b");
                }
                if ((showExit) && (key.Key == ConsoleKey.Escape))
                {
                    return 0;
                }
                index = keys.IndexOf(key.KeyChar);
            }
            while ((index < 0) || (index >= items.Length));
            return index + 1;
        }

        private DateTime? InputDate(string message, bool allowEmpty)
        {
            Console.Write(message);
            do
            {
                DateTime dateValue;                
                string inputValue = Console.ReadLine();
                if ((!allowEmpty) || (!String.IsNullOrWhiteSpace(inputValue)))
                {
                    if (!DateTime.TryParse(inputValue, out dateValue))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("Неверная дата. Придется повторить: ");
                    }
                    else
                    {
                        return dateValue;
                    }
                }
                else
                {
                    return null;
                }
            }
            while (true);
        }

        private decimal? InputDecimal(string message, bool allowEmpty)
        {
            decimal value;
            Console.Write(message);
            do
            {
                string inputValue = Console.ReadLine();
                if ((!allowEmpty) || (String.IsNullOrWhiteSpace(inputValue)))
                {
                    if (!decimal.TryParse(inputValue, out value))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("Неверная сумма дохода. Повторите: ");
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    return null;
                }
            }
            while (true);
        }
        
        private void InputIncomesFromFile()
        {
            Console.Write("Введите имя файла с данными: ");
            OperationImport importer = new OperationTextImport();
            if (importer.ImportFromFile(Console.ReadLine(), incomes))
            {
                Console.WriteLine("Импорт завершен. " + importer.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Error: {importer.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void InputIncomesFromConsole()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            bool working = true;
            DateTime? date;
            decimal? value;
            string incomeFrom = "";
            string notes;
            DateTimeFormatInfo dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (working)
            {
                date = InputDate("Введите дату получения дохода (пустая строка - завершить): ", true);
                if (date == null)
                {
                    return;
                }
                if (date < startWorkingDate)
                {
                    Console.WriteLine($"Минимальная допустимая дата {startWorkingDate.ToString("dd.MM.yyyy")}");
                    continue;
                }
                value = InputDecimal($"Укажите сумму дохода: ", false);
                string[] articles = {
                    "Заработная плата",
                    "по договору подряда",
                    "кредит в банке",
                    "проценты по депозиту",
                    "выигрыш в казино",
                    "прочие" };
                int choice = -1;
                choice = ShowMenu("Выберите источник дохода", articles, false, false);
                if (choice < articles.Length - 1)
                {
                    incomeFrom = articles[choice - 1];
                }
                else if (choice == articles.Length)
                {
                    Console.Write("Укажите иной источник дохода: ");
                    incomeFrom = Console.ReadLine();
                }                                    
                Console.Write("Примечание: ");
                notes = Console.ReadLine();
                try
                {                       
                    incomes.Add(new Operation((DateTime)date, (decimal)value, incomeFrom) { Notes = notes });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void InputIncomes()
        {
            string[] menuItems =
            {
                "ввод из файла",
                "ввод с консоли"
            };
            int choice = ShowMenu("Выберите источник ввода данных: ", menuItems, false, true);
            if (choice == 1)
                InputIncomesFromFile();
            else if (choice == 2)
                InputIncomesFromConsole();
        }

        private void DrawBorders(int[] columnWidths, string borderChars)
        {
            if (borderChars.Length < 3)
            {
                return;
            }
            Console.Write(borderChars[0]);
            for (int i = 0; i < columnWidths.Length-1;i++)
            {
                for (int j = 0; j < columnWidths[i]; j++)
                {
                    Console.Write("\u2500");
                }
                Console.Write(borderChars[1]);
            }
            for (int j = 0; j < columnWidths[columnWidths.Length-1];j++)
            {
                Console.Write("\u2500");
            }
            Console.WriteLine(borderChars[2]);
        }

        private void OutputIncomesToConsole()
        {
            int[] columnWidths = { 12, 11, 47 };
            Console.Clear();
            DateTime startDate = InputDate("Введите начальную дату периода",true)??startWorkingDate;
            DateTime? endDate = InputDate("Введите конечную дату периода",true);
            Console.Clear();
            Console.WriteLine($"Доходы за период с {startDate.ToString("dd.MM.yyyy")} по {(endDate??DateTime.Today).ToString("dd.MM.yyyy")}");
            DrawBorders(columnWidths, topHeaderBorderSymbols);
            Console.WriteLine($"\u2502    Дата    \u2502   Сумма   \u2502 {"Статья",-45} \u2502");
            DrawBorders(columnWidths, bottomHeaderBorderSymbols);
            Operations selectedItems = incomes.GetOperationsByRange(startDate, endDate);
            foreach(Operation item in selectedItems)
            {
                string article = (item.Article.Length > 45) ? item.Article.Substring(0, 42) + "..." : item.Article;
                Console.WriteLine($"\u2502 {item.Date.ToString("dd.MM.yyy")} \u2502 {item.Value,9:F2} \u2502 {article,-45} \u2502");
                if (Console.CursorTop == 21)
                {
                    Console.Write("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                    DrawBorders(columnWidths, topHeaderBorderSymbols);
                    Console.WriteLine($"\u2502    Дата    \u2502   Сумма   \u2502 {"Статья",-45} \u2502");
                    DrawBorders(columnWidths, bottomHeaderBorderSymbols);
                }
            }
            DrawBorders(columnWidths, bottomHeaderBorderSymbols);
            decimal sum = 0;
            selectedItems.ForEach(x => sum += x.Value);
            Console.WriteLine($"\u2502 Всего:     \u2502 {sum,9:F2} {" ",49} \u2502");
            DrawBorders(columnWidths, footerBorderSymbols);
            
            Console.Write("Нажмите любую клавишу для возврата в главное меню");
            Console.ReadKey();
        }

        private void OutputIncomesToFile()
        {
            Console.Write("Введите имя файла для экспорта: ");
            OperationExport exporter = new OperationTextExport();
            if (exporter.ExportToFile(Console.ReadLine(), incomes))
            {
                Console.WriteLine("Импорт завершен. " + exporter.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Error: {exporter.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void OutputIncomes()
        {
            string[] menuItems =
            {
                "вывод в файл (экспорт)",
                "вывод на консоль"
            };
            int choice = ShowMenu("Выберите действие:", menuItems, false, true);
            if (choice == 1)
                OutputIncomesToFile();
            else if (choice == 2)
                OutputIncomesToConsole();
        }
        private void InputExpensesFromConsole()
        {
            Console.Clear();
            bool working = true;
            DateTime? date;
            decimal? value;
            string incomeFrom;
            string notes;
            DateTimeFormatInfo dateFormat = CultureInfo.CurrentCulture.DateTimeFormat;
            while (working)
            {
                date = InputDate("Введите дату расхода (пустая строка - завершить): ", true);
                if (date == null)
                {
                    return;
                }
                if (date < startWorkingDate)
                {
                    Console.WriteLine($"Минимальная допустимая дата {startWorkingDate.ToString("dd.MM.yyyy")}");
                    continue;
                }
                value = InputDecimal($"Укажите сумму расхода: ", false);
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
                int choice = ShowMenu("Выберите статью расхода: ", articleItems, false, false);
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
                    Operation expense = new Operation((DateTime)date, (decimal)value, incomeFrom) { Notes = notes };
                    expenses.Add(expense);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void InputExpensesFromFile()
        {
            Console.Write("Введите имя файла с данными: ");
            OperationImport importer = new OperationTextImport();
            if (importer.ImportFromFile(Console.ReadLine(),expenses))
            {
                Console.WriteLine("Успешно выполнено. " + importer.ErrorMessage);
            }
            else
            {
                Console.WriteLine($"Ошибка: {importer.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void InputExpenses()
        {
            string[] menuItems =
            {
                "ввод из файла",
                "ввод с консоли"
            };
            int choice = ShowMenu("Выберите действие: ", menuItems, false, true);
            if (choice == 1)
                InputExpensesFromFile();
            else if (choice == 2)
                InputExpensesFromConsole();
        }
        
        private void OutputExpensesToConsole()
        {            
            Console.ForegroundColor = ConsoleColor.Green;
            DateTime startDate = InputDate("Введите начальную дату периода: ", true)??startWorkingDate;
            DateTime? endDate = InputDate("Введите конечную дату периода: ", true);
            Console.Clear();
            Console.WriteLine($"Расходы за период с {startDate.ToString("dd.MM.yyyy")} по {(endDate ?? DateTime.Today).ToString("dd.MM.yyyy")}");
            Operations selectedExpenses = expenses.GetOperationsByRange(startDate, endDate);
            foreach (Operation expense in selectedExpenses)
            {
                Console.WriteLine($"{expense.Date.ToString("dd.MM.yyy")}: {expense.Value,10:F2} {expense.Article}");
                if (Console.CursorTop == 24)
                {
                    Console.Write("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            decimal sum = 0;
            selectedExpenses.ForEach(x => sum += x.Value);
            Console.WriteLine($"Всего израсходовано за период {sum} рублей");
            Console.Write("Нажмите любую клавишу для возврата в главное меню");
            Console.ReadKey();
        }

        private void OutputExpensesToFile()
        {
            Console.Write("Введите имя файла: ");
            OperationExport exporter = new OperationTextExport();
            if (exporter.ExportToFile(Console.ReadLine(),expenses))
            {
                Console.WriteLine("Успешно завершено");
            }
            else
            {
                Console.WriteLine($"Ошибка: {exporter.ErrorMessage}");
            }
            Console.ReadKey();
        }

        private void OutputExpenses()
        {
            string[] menuItems =
            {
                "вывод в файл (экспорт)",
                "вывод на консоль"
            };
            int choice = ShowMenu("Выберите действие: ", menuItems, false, true);
            if (choice == 1)
                OutputExpensesToFile();
            else if (choice == 2)
                OutputExpensesToConsole();
        }

        private void OutputIncomesByArticle()
        {
            int[] columnWidths = { 11, 59 };
            DateTime startDate = InputDate("Введите начало периода: ", true)??startWorkingDate;
            DateTime? endDate = InputDate("Введите конец периода: ", true);
            Operations selectedItems = incomes.GetOperationsByArticle(startDate, endDate);
            Console.Clear();
            DrawBorders(columnWidths, topHeaderBorderSymbols);
            Console.WriteLine($"\u2502   Сумма   \u2502 {"Статья дохода",-57} \u2502"); 
            DrawBorders(columnWidths, bottomHeaderBorderSymbols);
            foreach (var item in selectedItems)
            {
                string article = (item.Article.Length > 57) ? item.Article.Substring(0, 54) + "..." : item.Article;
                Console.WriteLine($"\u2502 {item.Value,9:F2} \u2502 {article,-57} \u2502");
            }
            DrawBorders(columnWidths, footerBorderSymbols);
            Console.ReadKey();
        }

        private void OutputExpensesByArcticle()
        {
            int[] columnWidths = { 11, 59 };
            DateTime startDate = InputDate("Введите начало периода: ", true)??startWorkingDate;
            DateTime? endDate = InputDate("Введите конец периода: ", true);
            Console.Clear();
            DrawBorders(columnWidths, topHeaderBorderSymbols);
            Console.WriteLine($"\u2502   Сумма   \u2502 {"Статья расхода",-57} \u2502");
            DrawBorders(columnWidths, bottomHeaderBorderSymbols);
            Operations selectedItems = expenses.GetOperationsByArticle(startDate, endDate);
            foreach (var item in selectedItems)
            {
                string article = (item.Article.Length > 57)?item.Article.Substring(0,54)+"...":item.Article;
                Console.WriteLine($"\u2502 {item.Value,9:F2} \u2502 {article,-57} \u2502");
            }
            DrawBorders(columnWidths, footerBorderSymbols);
            Console.ReadKey();
        }

        private void ShowBalance()
        {
            int[] columnWidths = { 12, 11, 11, 11, 11 };
            DateTime startDate = InputDate("Введите начало периода: ", true)??startWorkingDate;
            DateTime endDate = InputDate("Введите конец периода: ", true) ?? DateTime.Today;
            Balance balance = analysis.GetBalance(startDate, endDate);
            Console.Clear();
            DrawBorders(columnWidths, topHeaderBorderSymbols);
            Console.WriteLine("\u2502    Дата    \u2502 Начальный \u2502  Приход   \u2502  Расход   \u2502 Конечный  \u2502");
            Console.WriteLine("\u2502            \u2502  остаток  \u2502           \u2502           \u2502  остаток  \u2502");
            DrawBorders(columnWidths, bottomHeaderBorderSymbols);
            foreach(var item in balance)
            {
                Console.WriteLine($"\u2502 {item.Date.ToString("dd.MM.yyyy")} \u2502 "+
                    $"{item.InitialBalance,9:F2} \u2502 "+
                    $"{((item.Credit==0)?"":item.Credit.ToString("F2")),9} \u2502 "+
                    $"{((item.Debet==0)?"":item.Debet.ToString("F2")),9} \u2502 "+
                    $"{item.FinalBalance,9:F2} \u2502");
            }
            DrawBorders(columnWidths, footerBorderSymbols);
            Console.ReadKey();
        }

        private void Analyst()
        {
            string[] menuItems =
            {
                "анализ доходов за период по источнику",
                "анализ расходов за период по статье",
                "обороты за период"
            };
            while (true)
            {
                int result = ShowMenu("Анализ доходов и расходов", menuItems, false, true);
                switch (result)
                {
                    case 1:
                        {
                            OutputIncomesByArticle();
                            break;
                        }
                    case 2:
                        {
                            OutputExpensesByArcticle();
                            break;
                        }
                    case 3:
                        {
                            ShowBalance();
                            break;
                        }
                    default:
                        {
                            return;
                        }
                }
            }
        }
    }
}
