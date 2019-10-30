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
            return x * Math.Sin(x) - 5 * Math.Log10(Math.Abs(x));
        }

        static bool Solve(double a, double b, out double x, ref int count)
        {
            count++;
            x = (a + b) / 2;
            if (F(a) * F(b) > 0)
                return false;
            if (Math.Abs(a - b) > 0.0001)
            {
                if (F(a) * F(x) < 0)
                    b = x;
                else if (F(b) * F(x) < 0)
                    a = x;
                else
                {
                    return true;
                }
                return Solve(a, b, out x, ref count);
            }
            else
                return true;
        }

        static void Main()
        {
            int iterationCount = 0;
            if (Solve(0, 50, out double x, ref iterationCount))
            {
                Console.WriteLine($"Уравнение x*sin(x)-5*log10(|x|)=0 решено: x={x:F3} за {iterationCount} итераций");
            }
            else
                Console.WriteLine("Не удалось найти решение уравнения");
            Console.ReadKey();
        }
    }
}
