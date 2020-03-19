using System;
using calculatorlib;

namespace CalculatorConsole
{
    static class CalculatorTester
    {
        private static string operations = "+-*/";
        public static void TestDoubleCalculator()
        {
            Console.Clear();
            ICalculator<double> calc = new DoubleCalculator();
            INumberValidator<double> validator = new DoubleValidator();
            double value1, value2, result;
            while (true)
            {
                try
                {
                    Console.Write("Введите операцию (+ - * /): ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    char operation = key.KeyChar;
                    if (operations.IndexOf(operation) == -1)
                    {
                        Console.WriteLine("\r\nА вот и не правильно");
                        continue;
                    }
                    Console.Write("\r\nВведите 2 числа разделенных пробелом: ");
                    if (!validator.ValidateTwoNumbers(Console.ReadLine(), out value1, out value2))
                    {
                        Console.WriteLine("Неверный ввод. Придется повторить");
                        continue;
                    }
                    switch (operation)
                    {
                        case '+':
                            result = calc.Add(value1, value2);
                            break;
                        case '-':
                            result = calc.Subtract(value1, value2);
                            break;
                        case '*':
                            result = calc.Multiply(value1, value2);
                            break;
                        case '/':
                            result = calc.Divide(value1, value2);
                            break;
                        default:
                            continue;
                    }
                    Console.WriteLine($"{ value1}{ operation}{ value2}={ result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.GetType().Name}: {e.Message}");
                }
            }
        }

        public static void TestIntCalculator()
        {
            Console.Clear();
            ICalculator<int> calc = new IntCalculator();
            INumberValidator<int> validator = new IntValidator();
            int value1, value2, result;
            while (true)
            {
                try
                {
                    Console.Write("Введите операцию (+ - * /): ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    char operation = key.KeyChar;
                    if (operations.IndexOf(operation) == -1)
                    {
                        Console.WriteLine("\r\nА вот и не правильно");
                        continue;
                    }
                    Console.Write("\r\nВведите 2 целых числа разделенных пробелом: ");
                    if (!validator.ValidateTwoNumbers(Console.ReadLine(), out value1, out value2))
                    {
                        Console.WriteLine("Неверный ввод. Придется повторить");
                        continue;
                    }
                    switch (operation)
                    {
                        case '+':
                            result = calc.Add(value1, value2);
                            break;
                        case '-':
                            result = calc.Add(value1, value2);
                            break;
                        case '*':
                            result = calc.Multiply(value1, value2);
                            break;
                        case '/':
                            result = calc.Divide(value1, value2);
                            break;
                        default:
                            continue;
                    }
                    Console.WriteLine($"{ value1}{ operation}{ value2}={ result}");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Exception {e.GetType().Name}: {e.Message}");
                }
            }
        }

        public static void TestCharCalculator()
        {
            Console.Clear();
            ICalculator<char> calc = new CharCalculator();
            INumberValidator<char> validator = new CharValidator();
            char value1, value2, result;
            while (true)
            {
                try
                {
                    Console.Write("Введите операцию (+ - * /): ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    char operation = key.KeyChar;
                    if (operations.IndexOf(operation) == -1)
                    {
                        Console.WriteLine("\r\nА вот и не правильно");
                        continue;
                    }
                    Console.Write("\r\nВведите 2 символа разделенных пробелом: ");
                    if (!validator.ValidateTwoNumbers(Console.ReadLine(), out value1, out value2))
                    {
                        Console.WriteLine("Неверный ввод. Придется повторить");
                        continue;
                    }
                    switch (operation)
                    {
                        case '+':
                            result = calc.Add(value1, value2);
                            break;
                        case '-':
                            result = calc.Add(value1, value2);
                            break;
                        case '*':
                            result = calc.Multiply(value1, value2);
                            break;
                        case '/':
                            result = calc.Divide(value1, value2);
                            break;
                        default:
                            continue;
                    }
                    Console.WriteLine($"{ value1}{ operation}{ value2}={ result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.GetType().Name}: {e.Message}");
                }
            }
        }

        public static void TestBoolCalculator()
        {
            Console.Clear();
            ICalculator<bool> calc = new BoolCalculator();
            INumberValidator<bool> validator = new BoolValidator();
            bool value1, value2, result;
            while (true)
            {
                try
                {
                    Console.Write("Введите операцию (+ - * /): ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    char operation = key.KeyChar;
                    if (operations.IndexOf(operation) == -1)
                    {
                        Console.WriteLine("\r\nА вот и не правильно");
                        continue;
                    }
                    Console.Write("\r\nВведите 2 булевых значения (false, true, 0, 1) разделенных пробелом: ");
                    if (!validator.ValidateTwoNumbers(Console.ReadLine(), out value1, out value2))
                    {
                        Console.WriteLine("Неверный ввод. Придется повторить");
                        continue;
                    }
                    switch (operation)
                    {
                        case '+':
                            result = calc.Add(value1, value2);
                            break;
                        case '-':
                            result = calc.Add(value1, value2);
                            break;
                        case '*':
                            result = calc.Multiply(value1, value2);
                            break;
                        case '/':
                            result = calc.Divide(value1, value2);
                            break;
                        default:
                            continue;
                    }
                    Console.WriteLine($"{ value1}{ operation}{ value2}={ result}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception {e.GetType().Name}: {e.Message}");
                }
            }
        }

        public static void TestStringCalculator()
        {
            Console.Clear();
            ICalculator<string> calc = new StringCalculator();
            INumberValidator<string> validator = new StringValidator();
            string value1, value2, result;
            while (true)
            {
                try
                {
                    Console.Write("Введите операцию (+ - * /): ");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                        return;
                    char operation = key.KeyChar;
                    if (operations.IndexOf(operation) == -1)
                    {
                        Console.WriteLine("\r\nА вот и не правильно");
                        continue;
                    }
                    Console.Write("\r\nВведите 2 слова разделенных пробелом: ");
                    if (!validator.ValidateTwoNumbers(Console.ReadLine(), out value1, out value2))
                    {
                        Console.WriteLine("Неверный ввод. Придется повторить");
                        continue;
                    }
                    switch (operation)
                    {
                        case '+':
                            result = calc.Add(value1, value2);
                            break;
                        case '-':
                            result = calc.Add(value1, value2);
                            break;
                        case '*':
                            result = calc.Multiply(value1, value2);
                            break;
                        case '/':
                            result = calc.Divide(value1, value2);
                            break;
                        default:
                            continue;
                    }
                    Console.WriteLine($"{ value1}{ operation}{ value2}={ result}");
                }
                catch(Exception e)
                {
                    Console.WriteLine($"Exception {e.GetType().Name}: {e.Message}");
                }
            }
        }
    }
}
