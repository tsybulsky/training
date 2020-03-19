using System;
using System.Collections.Generic;
using userslib;

namespace CalculatorConsole
{
    class UserManager
    {
        private UserData users;
        public UserManager(UserData users)
        {
            this.users = users;
        }

        private void AddUser()
        {
            User user = new User();
            do
            {
                Console.Write("Введите имя пользователя: ");
                user.Name = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(user.Name));
            Console.Write("Пароль: ");
            user.Password = PasswordHash.GetHash(Console.ReadLine());
            if (users.AddUser(user) == null)
            {
                Console.WriteLine("Ошибка добавления пользователя. Возможно пользователь уже существует");
            }
            else
            {
                Console.WriteLine("Пользователь успешно добавлен");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void ChangeUser()
        {
            User user;
            Console.WriteLine("Введите имя существующего пользователя: ");
            string existingUsername = Console.ReadLine();
            user = users.GetUser(existingUsername);
            if (user == null)
            {
                Console.WriteLine("Указанный пользователь не существует");
                return;
            }                
            Console.Write("Введите новый пароль: ");
            user.Password = PasswordHash.GetHash(Console.ReadLine());
            if (users.EditUser(user) == null)
            {
                Console.WriteLine("Что-то пошло не так, при изменении пароля пользователя");
            }
            else
            {
                Console.WriteLine("Успешно завершено");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void DeleteUser()
        {            
            string username;
            do
            {
                Console.WriteLine("Введите имя удаляемого пользователя: ");
                username = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(username));
            User user = new User() { Name = username };
            if (users.DeleteUser(user))
            {
                Console.WriteLine("Успешно завершено");
            }
            else
            {
                Console.WriteLine("Ошибка удаления пользователя. Возможно пользователь был удален ранее");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void FindUser()
        {
            string username;
            do
            {
                Console.Write("Введите имя пользователя: ");
                username = Console.ReadLine();
            } while (String.IsNullOrWhiteSpace(username));
            User user = users.GetUser(username);
            if (user == null)
            {
                Console.WriteLine("Пользователь не найден");
            }
            else
            {
                Console.WriteLine($"Пользователь найден в базе. Пароль {user.Password}");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();
        }

        private void ListUsers()
        {
            User[] listOfUsers = users.ListUsers().Result;
            Console.Clear();
            Console.WriteLine($"{"Имя пользователя",-20} Пароль");
            foreach (var user in listOfUsers)
            {
                Console.WriteLine($"{user.Name,-20} {user.Password}");
                if (Console.CursorTop == 24)
                {
                    Console.Write("Нажмите любую клавишу для продолжения");
                    Console.ReadKey();
                    Console.Clear();
                    Console.WriteLine($"{"Имя пользователя",-20} Пароль");
                }
            }
            Console.WriteLine("Нажмите любую клавишу для завершения");
            Console.ReadKey();
        }
        public void ManageUsers()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие с пользователем:\r\nF2 - добавить\r\nF3 - редактировать\r\nF4 - удалить\r\nF5 - найти\r\nESC - выйти");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.F2:
                        {
                            AddUser();
                            break;
                        }
                    case ConsoleKey.F3:
                        {
                            ChangeUser();
                            break;
                        }
                    case ConsoleKey.F4:
                        {
                            DeleteUser();
                            break;
                        }
                    case ConsoleKey.F5:
                        {
                            FindUser();
                            break;
                        }
                    case ConsoleKey.F6:
                        {
                            ListUsers();
                            break;
                        }
                    case ConsoleKey.Escape:
                        return;
                }
            }
        }
    }
}
