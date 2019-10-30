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
                else
                    result[i] = 0;
            }
            return result;
        }

        static void Main()
        {
            var rand = new Random();
            int a = rand.Next(-100, 100);
            int b = rand.Next(-100, 100);
            int c = rand.Next(-100, 100);
            Console.WriteLine($"AddThreeIntegers({a}, {b}, {c}) =  {AddThreeIntegers(a, b, c)}");
            Console.WriteLine($"AddTwoIntegers({a}, {b}) = {AddTwoIntegers(a, b)}");
            double ad = rand.NextDouble();
            double bd = rand.NextDouble();
            double cd = rand.NextDouble();
            Console.WriteLine($"AddThreeDoubles({ad:N3}, {bd:N3}, {cd:N3})={AddThreeDoubles(ad, bd, cd):N3}");
            Console.Write("Введите первую строку: ");
            var s1 = Console.ReadLine();
            Console.Write("Введите вторую строку: ");
            var s2 = Console.ReadLine();
            Console.WriteLine($"ConcatTwoStrings(\"{s1}\", \"{s2}\")=\"{ConcatTwoStrings(s1,s2)}\"");
            Console.Write("Введите размер первого массива (1..20): ");
            s1 = Console.ReadLine();            
            if ((!int.TryParse(s1,out int Len1)) || (Len1 <= 0) || (Len1 > 20))
            {
                Console.WriteLine("Неверная длина массива");
                Console.ReadKey();
                return;
            }            
            int[] array1 = new int[Len1];
            for (var i = 0; i < Len1; i++)
            {
                array1[i] = rand.Next(-100, 100);
            }
            Console.Write("Введите размер второго массива (1..20): ");
            s2 = Console.ReadLine();
            if ((!int.TryParse(s2,out int Len2)) || (Len2 <= 0) || (Len2 > 20))
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
