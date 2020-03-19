using System.Collections.Generic;
using MagicFieldGame;
using Common;

namespace Bot.Commands.Commands.PlayCommands
{
    class WaitForALetterCommand : CustomCommand
    {
        public override bool IsThisComand(string text, Game state)
        {
            return false;
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return state == GameState.WaitingLetter;
        }

        public override Answer OnMessage()
        {
            string[] nums = new string[]
            {
                "две", "три", "четыре", "пять", "шесть","семь"
            };
            List<string> keyboard = new List<string>();
            string text = "";
                char letter = Message[0];
                CheckLetterResult checkResult = Game.CheckALetter(letter, out int count);
            switch (checkResult)
            {
                case CheckLetterResult.NotALetter:
                    {
                        text = $"Ну, что же Вы, {Game.PlayerName}, так не осторожны\r\n" +
                            $"Придется повторить ввод буквы";
                        break;
                    }
                case CheckLetterResult.Repeated:
                    {
                        text = $"Внимательнее, {Game.PlayerName}, надо быть\r\n" +
                            "Вы уже называли эту букву";
                        keyboard.AddRange(new string[] { Vocabulary.Rotate, Vocabulary.NameAWord, Vocabulary.RepeatTask });
                        break;
                    }
                case CheckLetterResult.Invalid:
                    {
                        text = $"К сожалению, но буквы \"{letter}\" нет в этом слове\r\n"+
                            $"{Game.Mask}";
                        keyboard.AddRange(new string[] { Vocabulary.Rotate, Vocabulary.NameAWord, Vocabulary.RepeatTask });
                        break;
                    }
                case CheckLetterResult.Valid:
                    {
                        if (Game.State == GameState.Winned)
                        {
                            text = $"Невероятно, но Вы разгадали это слово {Game.Word}\r\n" +
                                $"Вы выиграли {Game.GetWinnedPrize()}\r\n";
                            keyboard.AddRange(new string[] { Vocabulary.Play });
                        }
                        else
                        {
                            text = $"Да, Вы правы. Есть такая буква в этом слове. ";
                            if (count > 1)
                            {
                                text += $"Да и не одна, а целых {nums[count - 2]}";
                            }
                            text += $"\r\nИ Вы увеличили свое количество очков до {Game.Score}\r\n"+
                                $"{Game.Mask}";
                            keyboard.AddRange(new string[] { Vocabulary.Rotate, Vocabulary.NameAWord, Vocabulary.RepeatTask });
                        }
                        break;
                    }
            }
            Game.StoreState();
            keyboard.Add(Vocabulary.HelpReadable);
            return new Answer(text, keyboard);
        }
    }
}
