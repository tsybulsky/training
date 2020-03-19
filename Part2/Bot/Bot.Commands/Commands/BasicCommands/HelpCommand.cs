using System.Collections.Generic;
using Common;
using MagicFieldGame;

namespace Bot.Commands.Commands.BasicCommands
{
    class HelpCommand : CustomCommand
    {
        public HelpCommand(Game state, string message): base (state, message)
        {
            name = "/help";
        }

        public HelpCommand(): base()
        {
            name = "/help";
        }
        public override bool IsThisComand(string text, Game state)
        {
            return (text.IndistinctMatching(Name) >= 75) || (text.IndistinctMatching("Помощь") >= 75);
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return false;
        }

        public override Answer OnMessage()
        {
            List<string> keyboard = new List<string>();
            string answerText = "";
            switch(Game.State)
            {
                case GameState.Initial:
                    {
                        answerText = "Эти команды можно вводить текстом в любое время.\r\n"+
                            $"{Vocabulary.Help} - показать справку\r\n" +
                            $"{Vocabulary.About} - показать информацию о боте\r\n" +
                            $"{Vocabulary.Reset} - очистить информацию о себе в боте. Текущая игра также завершится в любой стадии\r\n" +
                            "А сейчас просто введите свое имя";
                        break;
                    }
                case GameState.NotPlaying:
                    {
                        answerText = "Наберите в текстовом поле следующую команду\r\n" +
                            $"{Vocabulary.Play} - начать новую игру в \"поле чудес\"\r\n" +
                            "Вы также можете воспользоваться кнопкой \"Играть\" для автоматического набора и отправки этой команды";
                        break;
                    }
                case GameState.Playing:
                    {
                        answerText = "Вы можете использовать в текстовом поле следующие команды\r\n" +
                            $"{Vocabulary.Rotate} - крутить барабан\r\n" +
                            $"{Vocabulary.RepeatTask} - показать заданий еще раз\r\n" +
                            $"{Vocabulary.NameAWord} слово - ввести слово полностью\r\n" +
                            "Или воспользоваться соответствующими кнопками";
                        break;
                    }
                case GameState.Prize:
                    {
                        answerText = $"{Vocabulary.TakePrize} - забрать приз и закончить текущую игру\r\n" +
                            $"{Vocabulary.NameALetter} - назвать букву за 250 очков и продолжить игру";
                        break;
                    }
                case GameState.WaitingLetter:
                    {
                        answerText = "Введите букву на клавиатуре. Если будет введено более одной буквы, правильной считаться будет только первая";
                        break;
                    }
                case GameState.WaitingWord:
                    {
                        answerText = "Введите отгадываемое слово на клавиатуре";
                        break;
                    }
                case GameState.Winned:
                    {
                        answerText = $"{Vocabulary.Play} - начать новую игру";                        
                        break;
                    }                   
            }
            keyboard.Add(Vocabulary.HelpReadable);
            return new Answer(answerText, keyboard);
        }
    }
}
