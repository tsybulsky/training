
using System.Collections.Generic;
using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands.BasicCommands
{
    class AboutCommand : CustomCommand
    {
        public AboutCommand(): base()
        {
            name = Vocabulary.About;
        }

        public AboutCommand(Game state, string commandText): base(state, commandText)
        {
            name = Vocabulary.About;
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
            string text = "Этот телеграм бот поможет Вам поиграть в очень старую и нудную игру - \"Поле чудес\"\r\n" +
                "Разрабатывалось это чудо программисткой мысли в силу наседания не разработчика сильных мира сего,  а именно Старшего\r\n" +
                "Во имя его же безопасности называть его не стану.\r\n" +
                "Если что-то криво работает - не ругайтесь, у программистов тоже руки могут расти из... правильно, туловища";
            return new Answer(text, new List<string>() { Vocabulary.HelpReadable });                
        }
    }
}
