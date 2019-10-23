using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            Console.Write("Введите размер массива (1..100): ");
            var inputValue = Console.ReadLine();            
            if ((!int.TryParse(inputValue,out int Len)) || (Len <= 0) || (Len > 100))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var data = new int[Len];
            for (var i = 0; i < Len; i++)
            {
                data[i] = rand.Next(-999, 999);
            }
            Console.Write("Начальный массив: ");
            foreach (var item in data)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
            for (var i = 0; i < Len; i++)
            {
                data[i] = -data[i];
            }
            Console.Write("Результирующий массив: ");
            foreach (var item in data)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
