using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_3
{
    class Program
    {
        static void IncreaseVariables(ref int a, ref int b, ref int c)
        {
            a += 10;
            b += 10;
            c += 10;
        }

        static void CalculateCircleParams(double r, out double p, out double s)
        {
            p = 2 * Math.PI * r;
            s = Math.PI * r * r;
        }

        static void GetArrayCharacteristics(int[] array, out int max, out int min, out int sum)
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
            var rand = new Random();
            int a = rand.Next(-100,100);
            int b = rand.Next(-100,100);
            int c = rand.Next(-100,100);
            Console.WriteLine($"До вызова IncreaseVariables(ref a, ref b, ref c) a={a}, b={b}, c={c}");
            IncreaseVariables(ref a, ref b, ref c);
            Console.WriteLine($"После вызова IncreaseVariables(ref a, ref b, ref c) a={a}, b={b}, c={c}");
            Console.Write("Введите радиус круга: ");
            var inputValue = Console.ReadLine();            
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
            GetArrayCharacteristics(array, out int min, out int max, out int sum);
            Console.WriteLine("");
            Console.WriteLine($"Минимальный элемент: {min}, максимальный элемент: {max}, сумма элементов: {sum}");
            Console.ReadKey();
        }
    }
}
