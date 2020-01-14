using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class RepeatTaskCommand : CustomCommand
    {
        public RepeatTaskCommand(): base()
        {
            name = Vocabulary.RepeatTask;
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
            string text = $"Напомню Вам задание\r\n" +
                $"{Game.Question} ({Game.Word.Length} букв)";
            List<string> keyboard = new List<string>() 
            { 
                Vocabulary.Rotate, 
                Vocabulary.NameAWord, 
                Vocabulary.RepeatTask,
                Vocabulary.HelpReadable
            };
            return new Answer(text, keyboard);
        }
    }
}
