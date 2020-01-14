using Bot.Commands.Commands;
using System.Collections.Generic;

namespace Bot.Commands
{
    public interface ICommandList
    {
        ICommand GetCommand(long id, string text);

        List<ICommand> GetListOfCommands();
    }
}
