using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите N: ");
            var inputValue = Console.ReadLine().Trim();            
            if ((!int.TryParse(inputValue,out int n)) || (n <= 0))
            {
                Console.WriteLine("Введено неверное натуральное число");
                return;
            }
            Console.Write($"Первые {n} четных чисел: ");
            for (var i = 0; i < n; i++)
            {
                Console.Write($" {(i + 1) * 2}");
            }
            Console.ReadKey();
        }
    }
}
