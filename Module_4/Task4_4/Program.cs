using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_4
{
    class Program
    {
        static (int,int, int) IncreaseTuple((int a, int b, int c) values)
        {
            var result = (values.a + 10, values.b + 10, values.c + 10);
            return result;
        }

        static (double p, double s) CalculateCircleParams(double r)
        {
            (double p, double s) result = (2 * Math.PI * r, Math.PI * r * r);
            return result;
        }


        static (int min, int max, int sum) GetArrayCharacteristics(int[] array)
        {
            (int min, int max, int sum) result = (min:int.MaxValue, max:int.MinValue, sum:0);
            foreach(var item in array)
            {
                if (item > result.max)
                    result.max = item;
                if (item < result.min)
                    result.min = item;
                result.sum += item;
            }
            return result;
        }

        static void Main()
        {
            var rand = new Random();
            (int, int, int) tuple = (rand.Next(-100, 100), rand.Next(-100, 100), rand.Next(-100, 100));
            Console.WriteLine($"До вызова IncreaseTuple((int a, int b, int c) values) values.a={tuple.Item1}, values.b={tuple.Item2}, value.c={tuple.Item3}");
            tuple = IncreaseTuple(tuple);
            Console.WriteLine($"После вызова IncreaseTuple((int a, int b, int c)values) value.b={tuple.Item1}, values.b={tuple.Item2}, values.c={tuple.Item3}");
            Console.Write("Введите радиус круга: ");
            var inputValue = Console.ReadLine();            
            if ((!double.TryParse(inputValue, out double r)) || (r <= 0))
            {
                Console.WriteLine("Неверный радиус круга");
                Console.ReadKey();
                return;
            }
            (double p, double s) = CalculateCircleParams(r);
            Console.WriteLine($"Для радиуса r={r:N3}, периметр равен P={p:N3}, а площадь круга S={s:N3}");
            Console.Write("Введите размер массива (1..20): ");
            inputValue = Console.ReadLine();
            if ((!int.TryParse(inputValue, out int Len)) || (Len <= 0) || (Len > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            int[] array = new int[Len];
            for (var i = 0; i < Len; i++)
            {
                array[i] = rand.Next(-100, 100);
            }
            Console.Write("Исходный массив:");
            foreach (var item in array)
            {
                Console.Write($" {item}");
            }
            var (min, max, sum) = GetArrayCharacteristics(array);
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент: {min}, максимальный элемент: {max}, сумма элементов: {sum}");
            Console.ReadKey();
        }
    }
}
