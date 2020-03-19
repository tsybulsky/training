using System;
using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands
{
    public abstract class CustomCommand : ICommand
    {
        protected string name;
        public string Name 
        {
            get
            {
                return name;
            }
        }
        public string Message { get; private set; }

        public Game Game { get; protected set; }

        protected void SetCommandText(string message)
        {
            Message = message;
        }

        public CustomCommand()
        {

        }
        public CustomCommand(Game state, string commandText): this()
        {
            Game = state;
            Message = commandText;
        }

        public ICommand CreateCommand(Game game, string message)
        {
            ICommand command = (ICommand)Activator.CreateInstance(GetType());
            command.SetState(game, message);
            return command;
        }

        public abstract bool IsThisComand(string text, Game state);
        public abstract bool IsValidStateForCommand(GameState state);
        public abstract Answer OnMessage();

        public void SetState(Game game, string message)
        {
            Game = game;
            Message = message;
        }
    }
}
