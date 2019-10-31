using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_1
{
    class Program
    {
        static int[] CreateAndFillArray(int size)
        {
            var array = new int[size];
            var rand = new Random();
            for (var i = 0; i < size; i ++)
            {
                array[i] = rand.Next(-100, 100);
            }
            return array;
        }

        static int MaxOfArray(int[] array)
        {
            if (array.Length == 0)
                return 0;
            var max = int.MinValue;
            for (var i = 0; i < array.Length;i++)
            {
                if (max < array[i])
                    max = array[i];
            }
            return max;
        }

        static int MinOfArray(int[] array)
        {
            if (array.Length == 0)
                return 0;
            var min = int.MaxValue;
            for (var i =0;i < array.Length;i++)
            {
                if (array[i] < min)
                    min = array[i];
            }
            return min;
        }
        
        static int SumOfArray(int[] array)
        {
            int sum = 0;
            for (var i = 0; i < array.Length;i++)
            {
                sum += array[i];
            }
            return sum;
        }

        static int DiffMaxMin(int[] array)
        {
            return MaxOfArray(array) - MinOfArray(array);
        }

        static void MethodD(int[] array)
        {
            int min = MinOfArray(array);
            int max = MaxOfArray(array);
            for (var i = 0; i < array.Length; i++)
            {
                if (array[i] % 2 == 0)
                    array[i] += max;
                else
                    array[i] -= min;
            }
        }

        static void Main()
        {
            Console.Write("Введите размер массива (1..100): ");
            var inputValue = Console.ReadLine();            
            if ((!int.TryParse(inputValue,out int Len)) || (Len <= 0) || (Len > 100))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }         
            int[] data = CreateAndFillArray(Len);
            Console.Write("Исходный массив: ");
            foreach(var item in data)
            {
                Console.Write($" {item}");
            }
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент массива {MinOfArray(data)}");
            Console.WriteLine($"Максимальный элемент массива {MaxOfArray(data)}");
            Console.WriteLine($"Сумма всех элементов массива {SumOfArray(data)}");
            Console.WriteLine($"Разность между максимальным и минимальным элементом массива {DiffMaxMin(data)}");
            MethodD(data);
            Console.Write("Результирующий массив по пункту д) ");
            foreach (var item in data)
            {
                Console.Write($" {item}");
            }
            Console.ReadKey();

        }
    }
}
