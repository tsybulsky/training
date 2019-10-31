using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4_5
{
    class Program
    {
        private enum MathOperation  
        { 
            Add, 
            Subtract,
            Multiply, 
            Divide
        };

        static double DoOperation(double a, double b, MathOperation operation)
        {
            switch(operation)
            {
                case MathOperation.Add:
                    {
                        return a + b;
                    }
                case MathOperation.Subtract:
                    {
                        return a - b;
                    }
                case MathOperation.Multiply:
                    {
                        return a * b;
                    }
                case MathOperation.Divide:
                    {
                        return a/b;
                    }
                default:
                    throw new Exception("Неизвестная операция");
            }
        }

        static int DaysInMonth(int month, int year)
        {
            int[] days = { 31, 0, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            if ((month > 0)&&(month <= 12))
            {
                if (month != 2)
                    return days[month-1];
                if ((year % 4 == 0) && ((year % 100 == 0)&&(year % 400 == 0)))
                    return 29;
                else
                    return 28;
            }
            else
                return 0;
        }

        static void Main()
        {
            string inputValue;
            string[] values;
            while (true)
            {
                Console.Write("Введите выражения вид <Первый операнд><+-*/><Второй операнд>: ");
                inputValue = Console.ReadLine().Trim();
                if (inputValue == "")
                    break;
                values = inputValue.Split(new char[] { '+', '-', '*', '/' });
                if (values.Length < 2)
                {
                    Console.WriteLine("Требуется ввести минимум 2 значения");
                    continue;
                }
                if (!double.TryParse(values[0], out double operand1))
                {
                    Console.WriteLine($"Неверный первый операнд \"{values[0]}\"");
                    continue;
                }
                if (!double.TryParse(values[1], out double operand2))
                {
                    Console.WriteLine($"Неверный второй операнд \"{values[1]}\"");
                    continue;
                }
                MathOperation operation;
                if (inputValue.IndexOf('+') != -1)
                    operation = MathOperation.Add;
                else if (inputValue.IndexOf('-') != -1)
                    operation = MathOperation.Subtract;
                else if (inputValue.IndexOf('*') != -1)
                    operation = MathOperation.Multiply;
                else if (inputValue.IndexOf('/') != -1)
                    operation = MathOperation.Divide;
                else
                {
                    Console.WriteLine("Неизвестная операция");
                    continue;
                }
                var result = DoOperation(operand1, operand2, operation);
                Console.WriteLine($"{ result:F3}");
            }
            Console.Write("Введите числом месяц и год: ");
            inputValue = Console.ReadLine().Trim();
            values = inputValue.Split(' ');
            if (values.Length < 2)
            {
                Console.WriteLine("Требуется ввести 2 параметра");
                Console.ReadKey();
                return;
            }
            if (!int.TryParse(values[0], out int month))
            {
                Console.WriteLine("Месяц вводится числом");
                Console.ReadKey();
                return;
            }
            if ((month <= 0) || (month > 12))
            {
                Console.WriteLine("Месяц должен быть от 1 до 12");
                Console.ReadKey();
                return;
            }
            if (!int.TryParse(values[1], out int year))
            {
                Console.WriteLine("Неверное значение года");
                Console.ReadKey();
                return;
            }
            string[] monthNames = { "январе", "феврале", "марте", "апреле", "мае", "июне", "июле", "августе",
                "сентябре", "октябре", "ноябре", "декабре" };
            var days = DaysInMonth(month, year);
            Console.WriteLine($"В {monthNames[month-1]} {year} года будет {days} {(((days % 10) == 1) ? "день" : "дней")} ");
            Console.ReadKey();
        }
    }
}
