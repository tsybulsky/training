namespace Bot.ConsoleBot
{
    public class ConsoleEventArgs
    {
        public string Text { get; set; }

        public ConsoleEventArgs() : this("") { }

        public ConsoleEventArgs(string text)
        {
            Text = text;
        }
    }
}
