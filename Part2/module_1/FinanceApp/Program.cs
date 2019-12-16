using System;

namespace FinanceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            FinanceApplication application = new FinanceApplication();
            application.Initialize();
            application.Run();
        }
    }
}
