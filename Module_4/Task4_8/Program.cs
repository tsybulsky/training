using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_8
{
    class Program
    {
        static double F(double x)
        {
            return x * Math.Cos(x) - 5 * Math.Log10(Math.Abs(x));
        }

        static bool Solve(double lowBound, double highBound, out double x, ref int callCount)
        {
            callCount++;
            x = (lowBound + highBound) / 2;
            if (F(lowBound) * F(highBound) > 0)
                return false;
            if (Math.Abs(lowBound - highBound) > 0.0001)
            {
                if (F(lowBound) * F(x) < 0)
                    highBound = x;
                else if (F(highBound) * F(x) < 0)
                    lowBound = x;
                else
                {
                    return true;
                }
                return Solve(lowBound, highBound, out x, ref callCount);
            }
            else
                return true;
        }

        static void Main()
        {
            Console.Write("Введите начало и конец отрезка поиска решения уравнения: ");
            var inputValue = Console.ReadLine();
            var values = inputValue.Split(' ');
            if (values.Length < 2)
            {
                Console.WriteLine("Надо ввести 2 числа");
                Console.ReadKey();
                return;
            }
            if (!double.TryParse(values[0], out double lowBound))
            {
                Console.WriteLine("Неверно указано число начала отрезка");
                Console.ReadKey();
                return;
            }
            if (!double.TryParse(values[1], out double highBound))
            {
                Console.WriteLine("Неверно указано число конца отрезка");
                Console.ReadKey();
                return;
            }
            if (lowBound > highBound)
            {
                Console.WriteLine("Начало отрезка находится левее его конца");
                Console.ReadKey();
                return;
            }
            int callCount = 0;
            if (Solve(lowBound, highBound, out double x, ref callCount))
            {
                Console.WriteLine($"Уравнение x*sin(x)-5*lg(|x|)=0 решено за {callCount} вызовов: x={x:F3} ");
            }
            else
                Console.WriteLine("Не удалось найти решение уравнения");
            Console.ReadKey();
        }
    }
}
