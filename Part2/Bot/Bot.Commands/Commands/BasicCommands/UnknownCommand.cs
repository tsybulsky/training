using System.Collections.Generic;
using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands.BasicCommands
{
    class UnknownCommand : CustomCommand
    {
        public UnknownCommand(): base()
        {
            name = "";
        }
        public UnknownCommand(Game state, string commandText): base(state, commandText)
        {
            name = "";
        }
        public override bool IsThisComand(string text, Game state)
        {
            return true;
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return true;
        }

        public override Answer OnMessage()
        {
            /* Google DialogFlow
             * Client access token      d54b196bc65b4f3097bb88fdccad4e21
             * Developer access token   f62ea12a5b564213aaf263d72d3f250b
             */
            List<string> keyboard = new List<string>();
            switch (Game.State)
            {
                case GameState.NotPlaying:
                    {
                        keyboard.Add(Vocabulary.Play);
                        break;
                    }
                case GameState.Playing:
                    {
                        keyboard.AddRange(new string[] { Vocabulary.Rotate, Vocabulary.NameAWord, Vocabulary.RepeatTask });
                        break;
                    }
                case GameState.Prize:
                    {
                        keyboard.AddRange(new string[] { Vocabulary.TakePrize, Vocabulary.NameALetter });
                        break;
                    }
            }
            keyboard.Add(Vocabulary.HelpReadable);
            return new Answer("К сожалению, я не понимаю Вас. Наберите \"помощь\"", keyboard);
        }
    }
}
