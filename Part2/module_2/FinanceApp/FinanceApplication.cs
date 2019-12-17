using System;
using FinanceLibrary;
using System.Globalization;

namespace FinanceApp
{
    class FinanceApplication
    {
        private readonly Ledger ledger;
        private readonly IncomesUI incomesUI;
        private readonly ExpensesUI expensesUI;
        private readonly AnalysisUI analysisUI;        

        public FinanceApplication()
        {
            ledger = new Ledger();
            incomesUI = new IncomesUI(ledger);
            expensesUI = new ExpensesUI(ledger);
            analysisUI = new AnalysisUI(ledger);
        }

        public void Run()
        {
            string[] menuItems ={
                "Обработка доходов",
                "Обработка расходов",
                "Анализ"
            };
            bool running = true;
            while (running)
            {
                int choice = CustomActionUI.ShowMenu("Выберите действие: ", menuItems, true);
                switch (choice)
                {
                    case 0:
                        {
                            running = !CustomActionUI.ShowYesNo("Вы действительно хотите выйти?");                   
                            break;
                        }
                    case 1:
                        {
                            incomesUI.Show();
                            break;
                        }
                    case 2:
                        {
                            expensesUI.Show();
                            break;
                        }
                    case 3:
                        {
                            analysisUI.ShowAnalysis();
                            break;
                        }
                }
            }
        }
    }
}
