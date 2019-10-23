using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите размер массива (1..100): ");
            var inputValue = Console.ReadLine();            
            if ((!int.TryParse(inputValue, out int Len)) || (Len <= 0) || (Len > 100))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var rand = new Random();
            var data = new int[Len];
            for (var i = 0; i < Len; i++)
            { 
                data[i] = rand.Next(0, 999);
            }
            Console.Write("Начальный массив: ");
            foreach (var item in data)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
            Console.Write("Выходные числа: ");            
            for (var i = 1; i < data.Length; i++)
            {
                if (data[i] > data[i - 1])
                {
                    Console.Write($" {data[i]}");
                }
            }
            Console.ReadKey();
        }
    }
}
