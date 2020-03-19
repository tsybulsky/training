using System.Collections.Generic;
using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands.BasicCommands
{
    class StatisticsCommand: CustomCommand
    {
        public StatisticsCommand(): base()
        {
            name = Vocabulary.Statistics;
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
            return new Answer("Мне сегодня лень что-либо считать:-)", new List<string>());
            //Statistics stat = Game.QueryStatistics();
        }

       
    }
}
