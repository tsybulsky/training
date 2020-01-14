using System;
using FinanceLibrary;

namespace FinanceApp
{
    public class AnalysisUI
    {
        private readonly Ledger ledger;
        public AnalysisUI(Ledger ledger)
        {
            this.ledger = ledger;
        }

        private void OutputIncomesByArticle()
        {
            int[] columnWidths = { 11, 59 };
            DateTime startDate = CustomActionUI.InputDate("Введите начало периода: ", true) ?? ledger.MinWorkingDate;
            DateTime? endDate = CustomActionUI.InputDate("Введите конец периода: ", true);
            Operations selectedItems = ledger.Incomes.GetOperationsByArticle(startDate, endDate);
            Console.Clear();
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.HEADER_BORDER_SYMBOLS);
            Console.WriteLine($"\u2502   Сумма   \u2502 {"Статья дохода",-57} \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            foreach (Operation item in selectedItems)
            {
                string article = (item.Article.Length > 57) ? item.Article.Substring(0, 54) + "..." : item.Article;
                Console.WriteLine($"\u2502 {item.Value,9:F2} \u2502 {article,-57} \u2502");
            }
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
            Console.ReadKey();
        }

        private void OutputExpensesByArticle()
        {
            int[] columnWidths = { 11, 59 };
            DateTime startDate = CustomActionUI.InputDate("Введите начало периода: ", true) ?? ledger.MinWorkingDate;
            DateTime? endDate = CustomActionUI.InputDate("Введите конец периода: ", true);
            Console.Clear();
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.HEADER_BORDER_SYMBOLS);
            Console.WriteLine($"\u2502   Сумма   \u2502 {"Статья расхода",-57} \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            Operations selectedItems = ledger.Expenses.GetOperationsByArticle(startDate, endDate);
            foreach (Operation item in selectedItems)
            {
                string article = (item.Article.Length > 57) ? item.Article.Substring(0, 54) + "..." : item.Article;
                Console.WriteLine($"\u2502 {item.Value,9:F2} \u2502 {article,-57} \u2502");
            }
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
            Console.ReadKey();
        }

        private void ShowBalance()
        {
            int[] columnWidths = { 12, 11, 11, 11, 11 };
            DateTime startDate = CustomActionUI.InputDate("Введите начало периода: ", true) ?? ledger.MinWorkingDate.AddDays(1);
            DateTime endDate = CustomActionUI.InputDate("Введите конец периода: ", true) ?? DateTime.Today;
            Balance balance = ledger.GetBalance(startDate, endDate);
            Console.Clear();
            Console.WriteLine($"Обороты за период с {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")}");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.HEADER_BORDER_SYMBOLS);
            Console.WriteLine("\u2502    Дата    \u2502 Начальный \u2502  Приход   \u2502  Расход   \u2502 Конечный  \u2502");
            Console.WriteLine("\u2502            \u2502  остаток  \u2502           \u2502           \u2502  остаток  \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            decimal totalIncome = 0, totalExpense = 0;
            foreach (var item in balance)
            {
                Console.WriteLine($"\u2502 {item.Date.ToString("dd.MM.yyyy")} \u2502 " +
                    $"{item.InitialBalance,9:F2} \u2502 " +
                    $"{((item.Credit == 0) ? "" : item.Credit.ToString("F2")),9} \u2502 " +
                    $"{((item.Debet == 0) ? "" : item.Debet.ToString("F2")),9} \u2502 " +
                    $"{item.FinalBalance,9:F2} \u2502");
                totalIncome += item.Credit;
                totalExpense += item.Debet;
            }
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            Console.WriteLine($"\u2502 Всего:     \u2502           \u2502 {totalIncome,9:F2} \u2502           \u2502 {totalExpense,9:F2} \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
            Console.ReadKey();
        }

        private void ShowTaxByPeriod()
        {
            int[] columnWidths = { 12, 11, 11, 11, 11 };
            DateTime startDate = CustomActionUI.InputDate("Введите начало периода: ", true) ?? ledger.MinWorkingDate.AddDays(1);
            DateTime endDate = CustomActionUI.InputDate("Введите конец периода: ", true) ?? DateTime.Today;
            decimal tax = 0;
            ledger.Incomes.ForEachConditional(x => x.Date.Between(startDate, endDate),
                x => tax += ((IncomeOperation)x).TaxValue);
            Console.WriteLine($"За период с {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")} уплачено {tax:F2} рублей подоходного налога");
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void ShowTaxFreeIncomes()
        {
            int[] columnWidths = { 6, 12, 11, 10, 34 };
            DateTime startDate = CustomActionUI.InputDate("Введите начало периода: ", true) ?? ledger.MinWorkingDate.AddDays(1);
            DateTime endDate = CustomActionUI.InputDate("Введите конец периода: ", true) ?? DateTime.Today;
            Operations selectedItems = ledger.GetTaxFree(startDate, endDate);
            Console.Clear();
            Console.WriteLine($"Необлагаемые доходы за период с {startDate.ToString("dd.MM.yyyy")} по {endDate.ToString("dd.MM.yyyy")}");
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
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.MIDDLE_BORDER_SYMBOLS);
            decimal sum = 0, taxSum = 0;
            selectedItems.ForEach(x => { sum += x.Value; taxSum += ((IncomeOperation)x).TaxValue; });
            Console.WriteLine($"\u2502 {" ",4} \u2502 Всего:     \u2502 {sum,9:F2} \u2502 {taxSum,8:F2} \u2502 {" ",32} \u2502");
            CustomActionUI.DrawBorders(columnWidths, CustomActionUI.FOOTER_BORDER_SYMBOLS);
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        public void ShowAnalysis()
        {
            string[] menuItems =
            {
                "анализ доходов за период по источнику",
                "анализ расходов за период по статье",
                "обороты за период",
                "Уплачено налогов за период",
                "Необрлагаемый доход"
            };
            while (true)
            {
                int result = CustomActionUI.ShowMenu("Анализ доходов и расходов", menuItems, true);
                switch (result)
                {
                    case 1:
                        {
                            OutputIncomesByArticle();
                            break;
                        }
                    case 2:
                        {
                            OutputExpensesByArticle();
                            break;
                        }
                    case 3:
                        {
                            ShowBalance();
                            break;
                        }
                    case 4:
                        {
                            ShowTaxByPeriod();
                            break;
                        }
                    case 5:
                        {
                            ShowTaxFreeIncomes();
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
