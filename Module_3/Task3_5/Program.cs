using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module3_5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите натуральное число: ");
            var inputValue = Console.ReadLine().Trim();
            var data = inputValue.ToCharArray();
            foreach (var ch in data)
            {
                if (!char.IsDigit(ch))
                {
                    Console.WriteLine($"Неверный символ {ch} во введенной последовательности");
                    Console.ReadKey();
                    return;
                }
            }
            Console.Write("Введите цифру: ");
            var digits = Console.ReadLine().Trim();
            if ((digits.Length != 1) || (!char.IsDigit(digits[0])))
            {
                Console.WriteLine("Неверная цифра");
                Console.ReadKey();
                return;
            }                        
            int j = 0;
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] != digits[0])
                {
                    data[j++] = data[i];
                }
            }
            Array.Resize(ref data, j);
            Console.WriteLine($"Исходное число с удаленной цифрой {digits[0]}: {new String(data)}");
            Console.ReadKey();
        }
    }
}
