using System;

namespace Task2_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите число N: ");
            string inputValue = Console.ReadLine();
            if (!Int32.TryParse(inputValue, out int n))
            {
                Console.WriteLine("Введено неверное число N");
                Console.ReadKey();
                return;
            }
            if ((n >= 18) && (n % 2 == 0))
            {
                Console.WriteLine("Поздравляю Вас с 18-тилетием");
            }
            else if ((n < 18) && (n > 13) && (n % 2 == 1))
            {
                Console.WriteLine("Поздравляю Вас с переходом в старшую школу");
            }
            else
            {
                Console.WriteLine("Мне Вас не с чем поздравить");
            }

            Console.ReadKey();
        }
    }
}
