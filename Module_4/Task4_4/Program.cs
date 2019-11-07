using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_4
{
    class Program
    {
        static (int,int, int) IncreaseTuple((int, int, int) values)
        {
            var result = (values.Item1 + 10, values.Item2 + 10, values.Item3 + 10);
            return result;
        }

        static (double p, double s) CalculateCircleParams(double r)
        {
            (double p, double s) result = (2 * Math.PI * r, Math.PI * r * r);
            return result;
        }
        
        static (int min, int max, int sum) GetArrayMinMaxAndSum(int[] array)
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
            Console.Write("Введите 3 целых числа: ");
            var inputValue = Console.ReadLine();
            string[] values = inputValue.Split(' ');
            if (values.Length < 3)
            {
                Console.WriteLine("Введено слишком мало чисел");
                Console.ReadKey();
                return;
            }
            int[] numbers = new int[3];
            for (var i = 0; i < 3; i++)
            {
                if (!int.TryParse(values[i], out numbers[i]))
                {
                    Console.WriteLine($"'{values[i]}' не является целым числом");
                    Console.ReadKey();
                    return;
                }
            }            
            (int, int, int) tuple = (numbers[0], numbers[1], numbers[2]);
            tuple = IncreaseTuple(tuple);
            Console.WriteLine($"Увеличенные переменные: {tuple.Item1}, {tuple.Item2}, c={tuple.Item3}");
            Console.Write("Введите радиус круга: ");
            inputValue = Console.ReadLine();            
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
            if ((!int.TryParse(inputValue, out int len)) || (len <= 0) || (len > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var rand = new Random();
            int[] array = new int[len];
            for (var i = 0; i < len; i++)
            {
                array[i] = rand.Next(-100, 100);
            }
            Console.Write("Исходный массив:");
            foreach (var item in array)
            {
                Console.Write($" {item}");
            }
            (int min, int max, int sum) = GetArrayMinMaxAndSum(array);
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент: {min}, максимальный элемент: {max}, сумма элементов: {sum}");
            Console.ReadKey();
        }
    }
}
