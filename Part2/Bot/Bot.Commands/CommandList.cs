using Bot.Commands.Commands;
using Bot.Commands.Commands.BasicCommands;
using Bot.Commands.Commands.FirstCommands;
using Bot.Commands.Commands.PlayCommands;
using System.Collections.Generic;
using MagicFieldGame;

namespace Bot.Commands
{
    public class CommandList : ICommandList
    {
        private List<ICommand> commands;

        public CommandList()
        {
            commands = new List<ICommand>()
            {
                new StartCommand(),
                new HelpCommand(),
                new MeetCommand(),
                new ResetCommand(),
                new AboutCommand(),
                new StatisticsCommand(),
                new PlayCommand(),
                new RotateCommand(),
                new NameAWordCommand(),
                new NameALetterCommand(),
                new TakePrize(),
                new RepeatTaskCommand(),
                new WaitForAWordCommand(),
                new WaitForALetterCommand()  //Обязательно эта команда должна быть в конце списка!!!
            };
        }

        public ICommand GetCommand(long id, string text)
        {
            Game state = Game.LoadState(id);
            if (state.Id == 0)
            {
                state.SetId(id);
            }
            foreach (var command in commands)
            {
                if (command.IsThisComand(text, state))
                {
                    ICommand cmd = command.CreateCommand(state, text);                    
                    return cmd;
                }
            }
            foreach (var command in commands)
            {
                if (command.IsValidStateForCommand(state.State))
                {
                    ICommand cmd = command.CreateCommand(state, text);
                    return cmd;
                }
            }
            return new UnknownCommand(state, text);
        }

        public List<ICommand> GetListOfCommands()
        {
            return commands;
        }
    }
}
