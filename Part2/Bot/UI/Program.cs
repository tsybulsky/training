using System;
using Common;
using Bot.ConsoleBot;
using Bot.TelegramBot;

namespace UI
{
    class Program
    {

        private static void ConsoleRead(Object sender, ConsoleEventArgs args)
        {
            args.Text = Console.ReadLine();
        }

        private static void ConsoleWrite(Object sender, ConsoleEventArgs args)
        {
            Console.WriteLine(args.Text);
        }

        private static void TelegramLog(Object sender, BotLogEventArgs args)
        {
            Console.WriteLine($"{args.ChatId:5}: {args.Text}");
        }

        static void Main(string[] args)
        {
            ICustomBot bot = null;
            Console.WriteLine("Please, select telegram or console bot type");
            string inputValue = "";
            while (bot == null)
            {
                inputValue = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(inputValue))
                {
                    if (inputValue.ToLower() == "console")
                    {
                        bot = new ConsoleBot();
                        ((ConsoleBot)bot).OnInputValue += ConsoleRead;
                        ((ConsoleBot)bot).OnOutputValue += ConsoleWrite;
                    }
                    else if (inputValue.ToLower() == "telegram")
                    {
                        bot = new TelegramBot();
                        ((TelegramBot)bot).OnLog += TelegramLog;
                    }
                }
            }
            bot.Start();
            if (inputValue == "telegram")
                Console.ReadKey();
            bot.Run();
            bot.Stop();
        }
    }
}
