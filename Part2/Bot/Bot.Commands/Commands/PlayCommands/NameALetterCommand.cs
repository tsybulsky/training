using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class NameALetterCommand : CustomCommand
    {
        public NameALetterCommand(): base()
        {
            name = Vocabulary.NameALetter;
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
            Game.State = GameState.WaitingLetter;
            Game.LastScore = 250;
            Game.StoreState();
            string text = "Ваш вариант буквы\r\n" +
                $"{Game.Mask}";
            List<string> keyboard = new List<string>() { Vocabulary.HelpReadable };
            return new Answer(text, keyboard);
        }
    }
}
