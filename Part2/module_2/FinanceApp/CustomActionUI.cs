using System;
using System.IO;
using System.Globalization;

namespace FinanceApp
{
    internal class CustomActionUI
    {
        public const string HEADER_BORDER_SYMBOLS = "\u250C\u252C\u2510";
        public const string MIDDLE_BORDER_SYMBOLS = "\u251C\u253C\u2524";
        public const string FOOTER_BORDER_SYMBOLS = "\u2514\u2534\u2518";
        public static int ShowMenu(string title, string[] items, bool showExit, string keys)
        {
            bool groupping = (items.Length > 22);
            Console.Clear();
            if (!String.IsNullOrWhiteSpace(title))
                Console.WriteLine(title);
            if (groupping)
            {
                for (int i = 0; i < items.Length; i += 3)
                {
                    Console.Write($"{keys[i]} - {items[i],24} ");
                    if (i < items.Length - 1)
                    {
                        Console.Write($"{ keys[i + 1]} - {items[i + 1],24} ");
                        if (i < items.Length - 2)
                        {
                            Console.Write($"{keys[i + 2]} - {items[i + 2],24} ");
                        }
                    }
                    Console.WriteLine();
                }
                if (showExit)
                {
                    Console.WriteLine("ESC - выход");
                }
            }
            else
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Console.WriteLine($"{ keys[i]} - {items[i]}");
                }
                if (showExit)
                {
                    Console.WriteLine("ESC - выход");
                }
            }
            int index;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.CursorTop -= 1;
                }
                else
                {
                    Console.Write("\b");
                }
                if ((showExit) && (key.Key == ConsoleKey.Escape))
                {
                    return 0;
                }
                index = keys.IndexOf(key.KeyChar);
            }
            while ((index < 0) || (index >= items.Length));
            return index + 1;
        }

        public static int ShowMenu(string title, string[] items, bool showExit)
        {
            return ShowMenu(title, items, showExit, "123456789ABCDEFGHJIKLMNOPQRSTUVWXYZ");
        }

        public static DateTime? InputDate(string message, bool allowEmpty)
        {
            Console.Write(message);
            do
            {
                DateTime dateValue;
                string inputValue = Console.ReadLine();
                if ((!allowEmpty) || (!String.IsNullOrWhiteSpace(inputValue)))
                {
                    if (!DateTime.TryParse(inputValue, out dateValue))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("Неверная дата. Придется повторить: ");
                    }
                    else
                    {
                        return dateValue;
                    }
                }
                else
                {
                    return null;
                }
            }
            while (true);
        }

        public static decimal? InputDecimal(string message, bool allowEmpty)
        {
            decimal value;
            Console.Write(message);
            do
            {
                string inputValue = Console.ReadLine();
                if ((!allowEmpty) || (!String.IsNullOrWhiteSpace(inputValue)))
                {
                    if (!decimal.TryParse(inputValue, out value))
                    {
                        Console.SetCursorPosition(0, Console.CursorTop - 1);
                        Console.Write("Неверная сумма дохода. Повторите: ");
                    }
                    else
                    {
                        return value;
                    }
                }
                else
                {
                    return null;
                }
            }
            while (true);
        }

        public static bool InputInteger(string message, out int value)
        {
            Console.Write(message);
            string inputValue = Console.ReadLine();
            return int.TryParse(inputValue, NumberStyles.Any, CultureInfo.CurrentCulture.NumberFormat, out value);
        }

        public static bool InputInteger(string message, int lowIndex, int highIndex, out int value)
        {
            if (InputInteger(message, out value))
            {
                return ((value >= lowIndex) && (value < highIndex));
            }
            else
                return false;
        }

        public static bool InputIntegers(string message, int lowIndex, int highIndex, out int[] values)
        {
            Console.Write(message);
            string inputValue = Console.ReadLine();
            string[] inputValues = inputValue.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            values = new int[inputValues.Length];
            int Index = 0;
            foreach(string item in inputValues)
            {
                if (int.TryParse(item,out int tempValue))
                {
                    tempValue--;
                    if ((tempValue >= lowIndex) && (tempValue < highIndex))
                        values[Index++] = tempValue;
                }
            };
            Array.Resize(ref values, Index);
            return Index > 0;
        }

        public static void DrawBorders(int[] columnWidths, string borderChars)
        {
            if (borderChars.Length < 3)
            {
                return;
            }
            Console.Write(borderChars[0]);
            for (int i = 0; i < columnWidths.Length - 1; i++)
            {
                for (int j = 0; j < columnWidths[i]; j++)
                {
                    Console.Write("\u2500");
                }
                Console.Write(borderChars[1]);
            }
            for (int j = 0; j < columnWidths[columnWidths.Length - 1]; j++)
            {
                Console.Write("\u2500");
            }
            Console.WriteLine(borderChars[2]);
        }

        public static bool ShowYesNo(string title)
        {
            bool yesKey = false;
            Console.Write($"{title} [Y/N]: ");
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                Console.Write("\b");
                yesKey = key.Key == ConsoleKey.Y;
            } while ((key.Key != ConsoleKey.Y) && (key.Key != ConsoleKey.N));
            return yesKey;
        }

        public static bool InputFilename(string message, bool mustExists, out string filename)
        {
            filename = "";
            while (true)
            {
                Console.Write(message);
                string inputValue = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(inputValue))
                    return false;
                filename = inputValue;
                if ((!mustExists)|| ((mustExists) && (File.Exists(filename))))
                    return true;
            };
        }

    }
}
