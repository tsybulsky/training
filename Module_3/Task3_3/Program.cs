using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите N: ");
            var inputValue = Console.ReadLine();
            if ((!int.TryParse(inputValue, out int n)) || (n <= 0))
            {
                Console.WriteLine("Введено неверное натуральное число");
                Console.ReadKey();
                return;
            }
            var numbers = new int[n];
            numbers[0] = 0;
            if (n > 1)
            {
                numbers[1] = 1;                
            }
            for (var i = 2; i < n; i++)
            {
                numbers[i] = numbers[i - 2] + numbers[i - 1];
            }
            Console.Write("Первые N чисел Фибонначи:");
            foreach (var number in numbers)
            {
                Console.Write($" {number}");                
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
