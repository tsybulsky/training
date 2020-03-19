using Bot.Commands.Commands;
using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Bot.TelegramBot
{
    public static class MessageSender
    {
        public static async Task SendMessage(ICommand command, Message message, TelegramBotClient bot)
        {
            if (message?.Type != MessageType.Text)
            {
                return;
            }
            Answer answer = command.OnMessage();
            await bot.SendTextMessageAsync(message.Chat.Id, answer.Text, replyMarkup: GetReplyKeyboardMarkup(answer.Keyboard));
        }

        private static ReplyKeyboardMarkup GetReplyKeyboardMarkup(List<string> list)
        {
            ReplyKeyboardMarkup replyKeyboard = new ReplyKeyboardMarkup();

            var rows = new List<KeyboardButton[]>();

            var cols = new List<KeyboardButton>();

            foreach (string item in list)
            {
                cols.Add(new KeyboardButton(item));
                rows.Add(cols.ToArray());
                cols = new List<KeyboardButton>();
            }

            replyKeyboard.Keyboard = rows.ToArray();

            return replyKeyboard;
        }
    }
}
