using System.Collections.Generic;

namespace Common
{
    public class Answer
    {
        public List<string> Keyboard { get; private set; }

        public string Text { get; private set; }

        public Answer(string text, List<string> keyboard)
        {
            Text = text;

            Keyboard = keyboard;
        }
    }
}
