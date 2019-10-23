using System;

namespace Task2_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите количество компаний: ");
            string inputValue = Console.ReadLine();
            if ((!Int32.TryParse(inputValue, out int companiesCount))&&(companiesCount < 0))
            {
                Console.WriteLine("Введено неверное целое число");
                Console.ReadKey();
                return;
            }
            Console.Write("Введите ставку налога (%): ");
            inputValue = Console.ReadLine();
            if (!Double.TryParse(inputValue, out double taxRate))
            {
                Console.WriteLine("Введена неверная ставка налога");
                Console.ReadKey();
                return;
            }
            if ((taxRate <= 0) || (taxRate >= 100))
            {
                Console.WriteLine("Ставка налога должна быть от 0% (государство ничего не получит) до 100% (компании ничего не останется)");
                Console.ReadKey();
                return;
            }
            double[] incomes = new double[companiesCount];
            var entered = 0;
            while (entered < companiesCount)
            { 
                Console.Write($"Годовой доход каждой компании (осталось {companiesCount-entered} компаний): ");
                inputValue = Console.ReadLine();
                var values = inputValue.Split(' ');
                var i = 0;
                while ((i < values.Length) && (entered < companiesCount))
                {
                    if ((Double.TryParse(values[i++], out incomes[entered]))&&(incomes[entered] >= 0))
                    {
                        entered++;
                    }
                }
            }
            var tax = 0.0;
            foreach (var income in incomes)
                tax += income * taxRate / 100;
            Console.WriteLine($"Государство получит {tax}$ налогов от этих компаний");
            Console.ReadKey();
        }
    }
}
