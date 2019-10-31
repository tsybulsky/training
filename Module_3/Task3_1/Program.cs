using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_1
{
    class Program
    {
        // Метод разбора входной строки. Разбивает на элементы массива и 
        // удаляет пустые строки.
        static string[] ParseInput(string value)
        {
            var values = value.Trim().Split(' ');
            var j = 0;
            for (var i = 0; i < values.Length; i++)
            {
                values[j] = values[i].Trim();
                if (values[j] != "")
                {
                    j++;
                }                    
            }
            Array.Resize(ref values, j);
            return values;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите 2 числа: ");
            var values = ParseInput(Console.ReadLine());            
            if (values.Length != 2)
            {
                Console.WriteLine("Ошибка ввода. Требуется 2 числа");
                return;
            }            
            if ((!int.TryParse(values[0],out int a)) || (!int.TryParse(values[1],out int b)))
            {
                Console.WriteLine("Ошибка ввода. Одно из значений не является целым числом");
                Console.ReadKey();
                return;
            }
            bool isResultNegative = ((a < 0) ^ (b < 0));
            if (a < 0)
            {
                Console.Write($"({a})*");
                a = -a;
            }
            else
                Console.Write($"{a}*");
            if (b < 0)
            {
                Console.Write($"({b})=");
                b = -b;
            }
            else
                Console.Write($"{b}=");
            Int64 product = 0;
            if ((a != 0) && (b != 0))
            {
                for (var i = 0; i < b; i++)
                {
                    product += a;
                }
            }
            if (isResultNegative)
            {
                product = -product;
            }
            Console.WriteLine($"{product}");
            Console.ReadKey();
        }
    }
}
