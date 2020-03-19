using Bot.ConsoleBot;
using System;

namespace UI
{
    public class ConsoleBotEx
    {
        private ConsoleBot customBot = new ConsoleBot();


        
        public void End()
        {
            Console.WriteLine("Bot stop work");
        }

        public void GetListOfCommands()
        {
            foreach(var item in customBot.GetListOfCommands())
            {
                Console.WriteLine(item.Name);
            }
        }
    }
}
