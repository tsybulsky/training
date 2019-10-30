using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_3
{
    class Program
    {
        static void IncreaseVariables(ref int var1, ref int var2, ref int var3)
        {
            var1 += 10;
            var2 += 10;
            var3 += 10;
        }

        static void CalculateCircleParams(double r, out double p, out double s)
        {
            p = 2 * Math.PI * r;
            s = Math.PI * r * r;
        }

        static void GetArrayMinMaxAndSum(int[] array, out int max, out int min, out int sum)
        {            
            min = int.MaxValue;
            max = int.MinValue;
            sum = 0;
            foreach (var item in array)
            {
                if (item > max)
                    max = item;
                if (item < min)
                    min = item;
                sum += item;
            }
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
            IncreaseVariables(ref numbers[0], ref numbers[1], ref numbers[2]);                                    
            Console.WriteLine($"Увеличенные переменные: {numbers[0]}, {numbers[1]}, c={numbers[2]}");
            Console.Write("Введите радиус круга: ");
            inputValue = Console.ReadLine();            
            if ((!double.TryParse(inputValue, out double r)) || (r <= 0))
            {
                Console.WriteLine("Неверный радиус круга");
                Console.ReadKey();
                return;
            }            
            CalculateCircleParams(r, out double p, out double s);
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
            GetArrayMinMaxAndSum(array, out int min, out int max, out int sum);
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент: {min}, максимальный элемент: {max}, сумма элементов: {sum}");
            Console.ReadKey();
        }
    }
}
