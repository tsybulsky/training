using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class PlayCommand : CustomCommand
    {
        public PlayCommand(): base()
        {
            name = Vocabulary.Play;
        }

        public PlayCommand(Game game, string commandText): base(game, commandText)
        {
            name = Vocabulary.Play;
        }

        public override bool IsThisComand(string text, Game state)
        {
            return (text.IndistinctMatching(Name) >= 75) || (text.IndistinctMatching("play") >= 75);
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            List<string> keyboard = new List<string>()
            {
                Vocabulary.Rotate,
                Vocabulary.NameAWord,
                Vocabulary.HelpReadable
            };
            string text = "";
            Game.NewGame();
            Game.StoreState();
            text = $"Вот Вам, {Game.PlayerName}, задание на эту игру\r\n"+
                $"{Game.Question} ({Game.Word.Length} букв)\r\n"+
                $"{Game.Mask}";
            return new Answer(text, keyboard);
        }
    }
}
