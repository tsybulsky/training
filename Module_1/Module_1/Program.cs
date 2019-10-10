using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = 1;
            var b = 2;
            Console.WriteLine($"before: a= {a}, b = {b}");
            var c = a;
            a = b;
            b = c;
            Console.WriteLine($"after: a = {a}, b = {b}");
            Console.ReadKey();
        }
    }
}
