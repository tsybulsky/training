using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.TelegramBot
{
    public class BotLogEventArgs
    {
        public string ChatId { get; private set; }
        public string Text { get; private set; }

        public BotLogEventArgs(string chatId, string text)
        {
            ChatId = chatId;
            Text = text;
        }
    }
}
