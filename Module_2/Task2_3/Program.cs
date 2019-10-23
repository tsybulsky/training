using System;
using System.Globalization;

namespace Task2_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите два числа: ");
            string inputValue = Console.ReadLine();
            string decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            string[] values = inputValue.Split(' ');
            if (values.Length != 2)
            {
                Console.WriteLine("Требовалось ввести 2 числа");
                Console.ReadKey();
                return;
            }
            values[0] = (decimalSeparator == ".") ? values[0].Replace(",", ".") : values[0].Replace(".", ",");
            values[1] = (decimalSeparator == ".") ? values[1].Replace(",", ".") : values[1].Replace(".", ",");
            if (!Double.TryParse(values[0], out double number1))
            {
                Console.WriteLine("Первое введенное значение не является числом");
                Console.ReadKey();
                return;
            }
            if (!Double.TryParse(values[1], out double number2))
            {
                Console.WriteLine("Второе введенное значение не является числом");
                Console.ReadKey();
                return;
            }
            double temp = number1;
            number1 = number2;
            number2 = temp;
            Console.WriteLine($"Первое число {number1}, второе число {number2}");
            Console.ReadKey();
        }
    }
}
