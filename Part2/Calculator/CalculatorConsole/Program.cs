using System;
using userslib;

namespace CalculatorConsole
{
    class Program
    {
        private static UserData users = new UserData();             

        private static void Calculator()
        {
            Console.Clear();
            while (true)
            { 
                Console.Write("Выберите тип калькулятора\r\nF2-int\r\nF3 - double\r\nF4 - char\r\nF5 - bool\r\nF6 - string");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F2:
                        {
                            CalculatorTester.TestIntCalculator();
                            break;
                        }
                    case ConsoleKey.F3:
                        {
                            CalculatorTester.TestDoubleCalculator();
                            break;
                        }
                    case ConsoleKey.F4:
                        {
                            CalculatorTester.TestCharCalculator();
                            break;
                        }
                    case ConsoleKey.F5:
                        {
                            CalculatorTester.TestBoolCalculator();
                            break;
                        }
                    case ConsoleKey.F6:
                        {
                            CalculatorTester.TestStringCalculator();
                            break;
                        }
                    case ConsoleKey.Escape:
                        {
                            return;
                        }
                }
            }
        }

        private static bool DoLogin()
        {
            string password;
            string username;
            do
            {
                Console.Clear();
                Console.Write("Введите имя пользователя: ");
                username = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(username))
                {
                    return false;
                }
                Console.Write("Введите пароль: ");
                password = Console.ReadLine();
            } while (!users.Login(username, password));
            return true;
        }

        static void Main(string[] args)
        {
            UserData users = new UserData();
            if (!DoLogin())
                return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите опцию:\r\nF2 - калькулятор\r\nF3 - управление пользователями\r\nESC - выход");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        return;
                    case ConsoleKey.F2:
                        {
                            Calculator();
                            break;
                        }
                    case ConsoleKey.F3:
                        {
                            UserManager manager = new UserManager(users);
                            manager.ManageUsers();
                            break;
                        }
                }
            }            
        }
    }
}
