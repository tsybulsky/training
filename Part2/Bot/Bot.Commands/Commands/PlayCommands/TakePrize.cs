using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class TakePrize : CustomCommand
    {
        public TakePrize(): base()
        {
            name = Vocabulary.TakePrize;
        }

        public override bool IsThisComand(string text, Game state)
        {
            return text.IndistinctMatching(Name) >= 75;
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            string text = "Нам не удалось завершить эту игру, но зато вы получаете " +
                Game.TakePrize();
            Game.StoreState();
            List<string> keyboard = new List<string>() { Vocabulary.Play, Vocabulary.HelpReadable };
            return new Answer(text, keyboard);
        }
    }
}
