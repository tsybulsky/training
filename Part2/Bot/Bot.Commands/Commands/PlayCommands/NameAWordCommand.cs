using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class NameAWordCommand : CustomCommand
    {
        public NameAWordCommand(): base()
        {
            name = Vocabulary.NameAWord;
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
            List<string> keyboard = new List<string>() { Vocabulary.HelpReadable };
            Game.State = GameState.WaitingWord;
            Game.StoreState();
            string text = $"{Game.Question}\r\n" +
                $"{Game.Mask}\r\n" +
                "И Ваш вариант ответа...";
            return new Answer(text, keyboard);
        }
    }
}
