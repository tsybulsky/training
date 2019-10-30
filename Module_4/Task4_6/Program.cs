﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_6
{
    class Program
    {
        static void IncreaseItems(int[] array)
        {
            for (var i = 0; i < array.Length; i++)
            {
                array[i] += 5;
            }            
        }

        static void OutputArray(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
        }
        static void Main()
        {
            var rand = new Random();
            Console.Write("Введите размер массива (1..20): ");
            var inputValue = Console.ReadLine();
            if ((!Int32.TryParse(inputValue,out int Len)) || (Len <= 1) || (Len > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var data = new int[Len];
            for (var i = 0; i < Len; i++)
            {
                data[i] = rand.Next(-100, 100);
            }
            Console.Write("Исходный массив:");
            OutputArray(data);
            IncreaseItems(data);
            Console.Write("Обработанный массив:");
            OutputArray(data);
            Console.ReadKey();
        }
    }
}
