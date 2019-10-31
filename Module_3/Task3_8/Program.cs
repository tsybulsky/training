using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_8
{
    class Program
    {
        // Нелинейная функция, которая представляет уравнение.
        static double F(double x)
        {            
            return x * Math.Sin(x) - 5 * Math.Cos(x);
        }

        static void Main(string[] args)
        {
            double a = 0.0, b = 5.0;
            double x;
            var iterationCount = 0;            
            while (b-a > 0.00001)
            {
                iterationCount++;
                x = (a + b) / 2;
                if (F(a) * F(x) < 0)
                    b = x;
                else if (F(b) * F(x) < 0)
                    a = x;
                else
                    break;
            }
            x = (a + b) / 2;
            Console.WriteLine($"Решение уравнения x*sin(x)-5*cos(x)=0, x={x:F3}");
            Console.WriteLine($"Решение было найдено за {iterationCount} итераций цикла");
            Console.Write("Введите размер матрицы (1..19): ");
            var inputValue = Console.ReadLine();            
            if ((!int.TryParse(inputValue,out int n)) || (n < 1) ||(n > 19))
            {
                Console.WriteLine("Неверный размер матрицы");
                Console.ReadKey();
                return;
            }
            // Заполняемая матрица.
            var matrix = new int[n, n];
            // Крайние заполняемые столбцы и строки.
            int left = 0, right = n-1, top = 0, bottom = n-1;            
            // Текущее вставляемое значение.
            var counter = 1;            
            while ((left <= right) || (top < bottom))
            {
                for (var i = left; i <= right; i++)
                {                    
                    matrix[i, top] = counter++;
                }
                top++;
                for (var i = top; i <= bottom; i++)
                {
                    matrix[right, i] = counter++;
                }
                right--;
                for (var i = right; i >= left;i--)
                {
                    matrix[i, bottom] = counter++;
                }
                bottom--;
                for (var i = bottom; i >= top; i--)
                {
                    matrix[left, i] = counter++;
                }
                left++;
            }
            for (var j = 0; j < n; j++)
            {
                for (var i = 0; i < n; i++)
                {
                    Console.Write($"{matrix[i, j],3} ");
                }
                Console.WriteLine("");
            }
            Console.ReadKey();
        }
    }
}
