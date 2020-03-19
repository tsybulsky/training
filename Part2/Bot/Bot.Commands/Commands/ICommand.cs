using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands
{
    public interface ICommand
    {
        string Name { get; }

        string Message { get; }
        Game Game { get;}

        bool IsThisComand(string text, Game game);

        bool IsValidStateForCommand(GameState state);

        ICommand CreateCommand(Game game, string message);

        void SetState(Game game, string message);

        Answer OnMessage();
    }
}
