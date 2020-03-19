using System;
using System.Collections.Generic;
using MagicFieldGame;
using Common;


namespace Bot.Commands.Commands.FirstCommands
{
    public class MeetCommand : CustomCommand
    {     
        public MeetCommand(): base()
        {

        }
        public MeetCommand(Game state, string commandText): base(state, commandText)
        { }
        public override bool IsThisComand(string text, Game state)
        {
            return text == "/meet";
        }

        public override bool IsValidStateForCommand(GameState state)
        {
            return (state == GameState.Initial)|| (state == GameState.Cleared);
        }

        public override Answer OnMessage()
        {
            List<string> keyboard = new List<string>();
            string answerText = "";
            if (Message.IndistinctMatching("/meet") >= 75)
            {
                answerText = "Ну давайте познакомимся. Как Вас зовут?";
            }
            else if (String.IsNullOrWhiteSpace(Game.PlayerName))
            {
                Game.PlayerName = Message;
                Game.State = GameState.NotPlaying;
                Game.StoreState();                
                answerText = $"Очень приятно, {Game.PlayerName}!\r\n" +
                    "Теперь мы можем продолжать\r\n" +
                    $"Введите \"{Vocabulary.Play}\", чтобы начать играть";
                    keyboard.Add("Играть");                                    
            }
            else
            {
                keyboard.Add( Vocabulary.Reset);
                answerText = "Мы с Вами заблудились на просторах этого бота\r\n" +
                    "Это целиком моя вина, но давайте попробуем все исправить";
            }
            keyboard.Add(Vocabulary.HelpReadable);
            return new Answer(answerText, keyboard);
        }
    }
}
