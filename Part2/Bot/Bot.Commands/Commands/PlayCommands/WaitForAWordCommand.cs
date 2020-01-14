using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class WaitForAWordCommand : CustomCommand
    {
        public override bool IsThisComand(string text, Game state)
        {
            return false;
        }


        public override bool IsValidStateForCommand(GameState state)
        {
            return state == GameState.WaitingWord;
        }

        public override Answer OnMessage()
        {
            string text = "";
            if (Game.CheckAWord(Message))
            {
                text = $"Вы выиграли, поздравляю Вас, {Game.PlayerName}!!!\r\n" +
                    $"У Вас всего {Game.Score} очков и ваш выигрыш {Game.GetWinnedPrize()}";

            }
            else
            {
                text = $"К сожалению вы не правы, и на этом игра заканчивается\r\n" +
                    $"Правильное слово было {Game.Word}";
            }
            Game.StoreState();
            List<string> keyboard = new List<string>();
            keyboard.AddRange(new string[] { Vocabulary.Play, Vocabulary.HelpReadable });
            return new Answer(text, keyboard);
        }
    }
}
