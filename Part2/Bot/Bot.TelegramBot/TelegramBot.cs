using Bot.Commands;
using System;
using Common;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Bot.TelegramBot
{
    public class TelegramBot: ICustomBot
    {
        TelegramBotClient bot;


        ICommandList commands;

        public event EventHandler<BotLogEventArgs> OnLog = null;
        public TelegramBot()
        {
            bot = new TelegramBotClient("959998721:AAGeoAdV8y8ApBCKMI57vO9ofjl6o6Xb7i4");

            commands = new CommandList();

            bot.OnMessage += OnMessage;
            OnLog?.Invoke(this, new BotLogEventArgs("", "Бот создан"));
        }

        private async void OnMessage(object sender, MessageEventArgs messageEventArgs)
        {
            if (messageEventArgs.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                var message = messageEventArgs.Message;
                var command = commands.GetCommand(message.Chat.Id, message.Text);
                OnLog?.Invoke(this,
                    new BotLogEventArgs(
                        message.Chat.Id.ToString(),
                        command.GetType().ToString()+": "+message.Text));
                await MessageSender.SendMessage(command, message, bot);
            }
            else
            {
                OnLog?.Invoke(this,
                  new BotLogEventArgs(
                       messageEventArgs.Message.Chat.Id.ToString(),
                       messageEventArgs.Message.Type.ToString()));
            }
        }

        public void Start()
        {
            bot.StartReceiving();
        }

        public void Run()
        {
            
        }
        public void Stop()
        {
            bot.StartReceiving();
        }
    }
}
