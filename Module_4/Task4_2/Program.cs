using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_2
{
    class Program
    {

        static int AddThreeIntegers(int a, int b, int c)
        {
            return (a + b + c);
        }

        static int AddTwoIntegers(int a, int b)
        {
            return (a + b);
        }

        static double AddThreeDoubles(double a, double b, double c)
        {
            return a + b + c;
        }

        static string ConcatTwoStrings(string s1, string s2)
        {
            return s1 + s2;
        }

        static int[] AddArrays(int[] a, int[] b)
        {
            int Len1 = a.Length;
            int Len2 = b.Length;
            int Len = Math.Max(Len1, Len2);
            int[] result = new int[Len];
            for (var i = 0; i < Len; i++)
            {
                if (i < Len1)
                {
                    if (i < Len2)
                        result[i] = a[i] + b[i];
                    else
                        result[i] = a[i];
                }
                else if (i < Len2)
                {
                    result[i] = b[i];
                }
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
            int result = AddThreeIntegers(numbers[0], numbers[1], numbers[2]);
            Console.WriteLine($"AddThreeIntegers({numbers[0]}, {numbers[1]}, {numbers[2]}) = {result}");
            result = AddTwoIntegers(numbers[0], numbers[1]);
            Console.WriteLine($"AddTwoIntegers({numbers[0]}, {numbers[1]}) = {result}");
            Console.Write("Введите 3 числа: ");
            inputValue = Console.ReadLine();
            values = inputValue.Split(' ');
            if (values.Length < 3)
            {
                Console.WriteLine("Введено слишком мало чисел");
                Console.ReadKey();
                return;
            }
            double[] numbers2 = new double[3];
            for (var i = 0; i < 3; i++)
            {
                if (!double.TryParse(values[i], out numbers2[i]))
                {
                    Console.WriteLine($"'{values[i]}' не является числом");
                    Console.ReadKey();
                    return;
                }
            }
            double result2 = AddThreeDoubles(numbers2[0], numbers2[1], numbers[2]);
            Console.WriteLine($"AddThreeDoubles({numbers2[0]:N3}, {numbers2[1]:N3}, {numbers2[3]:N3})={result2:N3}");
            Console.Write("Введите первую строку: ");
            var line1 = Console.ReadLine();
            Console.Write("Введите вторую строку: ");
            var line2 = Console.ReadLine();
            Console.WriteLine($"ConcatTwoStrings(\"{line1}\", \"{line2}\")=\"{ConcatTwoStrings(line1,line2)}\"");
            Console.Write("Введите размер первого массива (1..20): ");
            inputValue = Console.ReadLine().Trim();            
            if ((!int.TryParse(inputValue, out int Len1)) || (Len1 <= 0) || (Len1 > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            var rand = new Random();
            int[] array1 = new int[Len1];
            for (var i = 0; i < Len1; i++)
            {
                array1[i] = rand.Next(-100, 100);
            }
            Console.Write("Введите размер второго массива (1..20): ");
            inputValue = Console.ReadLine().Trim();
            if ((!int.TryParse(inputValue, out int Len2)) || (Len2 <= 0) || (Len2 > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }
            int[] array2 = new int[Len2];
            for (var i = 0; i < Len2; i++)
            {
                array2[i] = rand.Next(-100, 100);
            }
            Console.Write("Первый массив:");
            for (var i = 0; i < Len1; i++)
            {
                Console.Write($" {array1[i]}");
            }
            Console.WriteLine("");
            Console.Write("Второй массив:");
            for (var i = 0; i < Len2; i++)
            {
                Console.Write($" {array2[i]}");
            }
            Console.WriteLine("");
            int[] resultArray = AddArrays(array1, array2);
            Console.Write("Результирующий массив:");
            for (var i = 0; i < resultArray.Length; i++)
            {
                Console.Write($" {resultArray[i]}");
            }
            Console.WriteLine("");
            Console.ReadKey();
        }
    }
}
