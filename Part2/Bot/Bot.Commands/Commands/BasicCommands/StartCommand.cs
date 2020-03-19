using Common;
using System.Collections.Generic;
using MagicFieldGame;
namespace Bot.Commands.Commands.BasicCommands
{
    public class StartCommand : CustomCommand
    {
        public StartCommand(): base()
        {
            name = Vocabulary.Start;
        }

        public StartCommand(Game state, string commandText): base(state, commandText)
        {
            name = Vocabulary.Start;
        }

        public override bool IsThisComand(string name, Game state)
        {
            return name.ToLower() == this.Name;
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            Game.Reset();
            Game.StoreState();
            string text = "Добро пожаловать, мудрейший\r\n"+
                "Как мне Вас называть?";
            return new Answer(text, new List<string>() { Vocabulary.HelpReadable });
        }
    }
}
