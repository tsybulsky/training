using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class RotateCommand : CustomCommand
    {
        public RotateCommand(Game game, string commandText): base(game,commandText)
        {
            name = Vocabulary.Rotate;
        }

        public RotateCommand(): base()
        {
            name = Vocabulary.Rotate;
        }

        public override bool IsThisComand(string text, Game state)
        {
            return (text.IndistinctMatching(Name) >= 75);
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            List<string> keyboard = new List<string>();
            int score = Game.Rotate();
            string text = "";
            switch (score)
            {
                case -1:
                    {
                        text = "Сектор приз на барабане\r\n" +
                            $"Ваше решение, {Game.PlayerName}?";
                        keyboard.AddRange(new string[] { Vocabulary.TakePrize, Vocabulary.NameALetter, Vocabulary.RepeatTask });
                        break;
                    }
                case 0:
                    {
                        text = $"К сожалению, {Game.PlayerName}, Вы банкрот\r\n" +
                            "Но игра на этом не заканчивается, крутите барабан или называйте слово";
                        keyboard.AddRange(new string[] { Vocabulary.Rotate, Vocabulary.NameAWord, Vocabulary.RepeatTask });
                        break;
                    }
                default:
                    {
                        text = $"{score} очков на барабане и Ваша буква...";
                        break;
                    }
            }
            Game.StoreState();
            keyboard.Add(Vocabulary.HelpReadable);
            return new Answer(text, keyboard);
        }
    }
}
