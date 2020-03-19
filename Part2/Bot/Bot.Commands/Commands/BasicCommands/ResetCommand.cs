using System;
using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.BasicCommands
{
    class ResetCommand : CustomCommand
    {

        public ResetCommand(Game state, string message) : base(state, message)
        {
            name = "/reset";
        }

        public ResetCommand(): base()

        {
            name = "/reset";
        }

        public  override bool IsThisComand(string text, Game state)
        {
            return (text.IndistinctMatching(Name) >= 75) || (text.IndistinctMatching("Сброс") >= 75);

        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            Game.Reset();
            Game.StoreState();
            return new Answer("Вы обнулены\r\nДавайте познакомимся. Как мне к Вам обращаться?", new List<string>() { Vocabulary.HelpReadable });
        }
    }
}
