using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Module3_4
{
    class Program
    {
        static void Main()
        {
            Console.Write("Введите число: ");
            var inputValue = Console.ReadLine();
            inputValue = inputValue.Trim();
            if (!double.TryParse(inputValue, out _))
            {
                Console.WriteLine("Неверное число");
                Console.ReadKey();
                return;
            }
            var data = inputValue.ToCharArray();            
            var j = data.Length-1;
            var i = (char.IsDigit(data[0]))?0:1;
            while (i < j)
            {
                if (!char.IsDigit(data[i]))
                    i++;
                if (!char.IsDigit(data[j]))
                    j--;
                if (i < j)
                {
                    var temp = data[i];
                    data[i++] = data[j];
                    data[j--] = temp;
                }
            }
            Console.WriteLine(new String(data));
            Console.ReadKey();
        }
    }
}
