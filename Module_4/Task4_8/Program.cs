using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_8
{
    class Program
    {
        static double GetFunction(double x)
        {
            return x * Math.Cos(x) - 5 * Math.Log10(Math.Abs(x));
        }

        static bool Solve(double left, double right, out double x)
        {
            x = (left + right) / 2;
            double leftValue = GetFunction(left);
            double rightValue = GetFunction(right);
            double xValue = GetFunction(x);
            if (leftValue * rightValue > 0)
                return false;
            if (Math.Abs(left - right) > 0.0001)
            {
                if (leftValue * xValue < 0)
                    right = x;
                else if (rightValue * xValue < 0)
                    left = x;
                else
                {
                    return true;
                }
                return Solve(left, right, out x);
            }
            else
                return true;
        }

        static void Main()
        {
            Console.Write("Введите начало и конец отрезка поиска решения уравнения: ");
            var inputValue = Console.ReadLine();
            string[] values = inputValue.Split(' ');
            if (values.Length < 2)
            {
                Console.WriteLine("Надо ввести 2 числа");
                Console.ReadKey();
                return;
            }
            if (!double.TryParse(values[0], out double left))
            {
                Console.WriteLine("Неверно указано число начала отрезка");
                Console.ReadKey();
                return;
            }
            if (!double.TryParse(values[1], out double right))
            {
                Console.WriteLine("Неверно указано число конца отрезка");
                Console.ReadKey();
                return;
            }
            if (left > right)
            {
                Console.WriteLine("Начало отрезка находится левее его конца");
                Console.ReadKey();
                return;
            }
            if (Solve(left, right, out double x))
            {
                Console.WriteLine($"Уравнение x*sin(x)-5*lg(|x|)=0 на отрезке [{left:N3}; {right:N3}] решено x={x:F3}");
            }
            else
                Console.WriteLine("Не удалось найти решение уравнения");
            Console.ReadKey();
        }
    }
}
