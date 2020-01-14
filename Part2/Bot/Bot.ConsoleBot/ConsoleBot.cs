using Bot.Commands;
using Bot.Commands.Commands;
using System.Collections.Generic;
using Common;
using System;

namespace Bot.ConsoleBot
{
    public class ConsoleBot: ICustomBot
    {
        
        ICommandList commands;
        private bool botRunning = false;
        public event EventHandler<ConsoleEventArgs> OnInputValue;
        public event EventHandler<ConsoleEventArgs> OnOutputValue;

        public void Start()
        {
            OnOutputValue?.Invoke(this, new ConsoleEventArgs("Bot start work"));
            botRunning = true;
        }
        
        public void Run()
        {
            if ((!botRunning) || (OnInputValue == null))
                return;
            while (botRunning)
            {
                ConsoleEventArgs args = new ConsoleEventArgs();
                OnInputValue(this, args);
                var inputValue = args.Text;
                if (inputValue == "stop")
                    botRunning = false;
                else
                {
                    var answer = DoCommand(inputValue);
                    Console.WriteLine(answer.Text);
                    foreach (var item in answer.Keyboard)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
        }

        public ConsoleBot()
        {
            commands = new CommandList();
        }

        public Answer DoCommand(string text)
        {
            var command = commands.GetCommand(1234567890,text);
            return command.OnMessage();
        }

        public List<ICommand> GetListOfCommands()
        {
            return commands.GetListOfCommands();
        }

        public void Stop()
        {
            botRunning = false;
        }
    }
}
