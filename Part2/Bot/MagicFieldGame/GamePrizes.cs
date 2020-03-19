using System;
using System.IO;

namespace MagicFieldGame
{
    class GamePrizes
    {
        private GamePrize[] prizes;

        public int Length;
        public GamePrizes()
        {
            if (!File.Exists("prizes.txt"))
            {
                prizes = new GamePrize[1];
                prizes[0] = new GamePrize("0|Все что пожелаете");
                return;
            }
            using (FileStream stream = new FileStream("prizes.txt", FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string inputValue = reader.ReadLine();
                    if (int.TryParse(inputValue, out int count))
                    {
                        prizes = new GamePrize[count];
                        for (int i = 0; i < count; i++)
                        {
                            inputValue = reader.ReadLine();
                            prizes[i] = new GamePrize(inputValue);
                        }
                    }
                }
                Array.Sort(prizes);
            }
        }
        public string GetPrize(int Score)
        {
            int index = -1;
            if (Score == -1)
            {
                index = Generator.GetGenerator().Next(0, prizes.Length);
            }
            else
                for (int i = prizes.Length - 1; i >= 0; i--)
                {
                    if (prizes[i].Cost <= Score)
                    {
                        index = i;
                        break;
                    }
                }
            return prizes[index].Title;
        }       
    }
}
