using System;
using System.Text;
using System.IO;
using System.Text.Json;

namespace MagicFieldGame
{
    public class Game
    {
        public long Id { get; private set; }
        public string PlayerName { get; set; }

        private string lang = "EN";

        public string Lang
        {
            get
            {
                return lang;
            }
            set
            {                
                //if (LanguageStrings.IsLanguageSupported(value))
                    lang = value;
            }
        }
        public GameState State { get; set; }

        public string Word { get; set; }
        public string Mask { get; set; }

        public int Score { get; set; }

        public int LastScore { get; set; }
        public string Question { get; set; }

        GamePrizes prizes = new GamePrizes();

        /*private (int, string)[] prizes = new (int, string)[]
            {

            };*/
        public Game(): this(0)
        {

        }

        public Game(long id)
        {
            this.Id = id;
            NamedLetters = "";
            Word = "";
            Question = "";
            Mask = "";
        }

        public static Game LoadState(long id)
        {
            if (!File.Exists($"{id}.json"))
                return new Game(id);
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IgnoreNullValues = true;
            options.WriteIndented = true;
            using (FileStream stream = new FileStream($"{id}.json", FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonValue = reader.ReadToEnd();
                    if (!String.IsNullOrWhiteSpace(jsonValue))
                    {
                        object obj = JsonSerializer.Deserialize<Game>(jsonValue);
                        if (obj is Game)
                            return (Game)obj;
                        else
                            return new Game(id);
                    }
                    else
                        return new Game(id);
                }
            }
        }

        public bool StoreState()
        {
            if (State == GameState.Cleared)
            {
                return true;
            }
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.IgnoreNullValues = true;
            options.WriteIndented = true;
            using (FileStream stream = new FileStream($"{Id}.json", FileMode.OpenOrCreate))
            {
                stream.SetLength(0);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string jsonValue = JsonSerializer.Serialize<Game>(this, options);
                    writer.Write(jsonValue);
                    return true;
                }
            }
        }

        public void SetId(long id)
        {
            Id = id;
        }

        public void Clear()
        {
            State = GameState.Cleared;
            File.Delete($"{Id}.json");
        }

        public string NamedLetters { get; set; }
        public void Reset()
        {
            PlayerName = "";
            Score = 0;
            Word = "";
            Mask = "";
            Question = "";
            LastScore = 0;
            State = GameState.Initial;
        }

        public bool NewGame()
        {
            using (FileStream stream = new FileStream("questions.txt", FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string inputValue = reader.ReadLine();
                    if (!int.TryParse(inputValue,out int count))
                    {
                        return false;
                    }
                    Random random = Generator.GetGenerator();
                    int index = random.Next(0, count);
                    for (int i =0;i<index-1;i++)
                    {
                        reader.ReadLine();
                    }
                    inputValue = reader.ReadLine();
                    string[] questionParts = inputValue.Split('|');
                    if (questionParts.Length != 2)
                        return false;
                    Word = questionParts[0].ToUpper();
                    Question = questionParts[1];
                    StringBuilder builder = new StringBuilder();
                    for (int i = 0; i < Word.Length; i++)
                        builder.Append('-');
                    Mask = builder.ToString();
                    State = GameState.Playing;
                    LastScore = 0;
                    Score = 0;
                    NamedLetters = "";
                    return true;
                }
            }
        }

        public int Rotate()
        {
            //0 - Банкрот, -1 - приз, остальное - очки
            int[] scores = new int[]
            {
                190,   0, 200,  90, 120,
                200, 100, 150, 180,  50,
                250,  -1,  20,  50,   0,
                 50, 250, 100, 120, 140,
                 -1, 200, 170, 230,  80
            };
            Random random = new Random();
            int index = random.Next(0, scores.Length);
            LastScore = scores[index];
            if (LastScore == -1)
                State = GameState.Prize;
            else if (LastScore == 0)
            {
                Score = 0;
                State = GameState.Playing;
            }
            else
            {
                State = GameState.WaitingLetter;
            }
            return LastScore;
        }

        public CheckLetterResult CheckALetter(char value, out int count)
        {
            count = 0;
            value = Char.ToUpper(value);
            if (!char.IsLetter(value))
            {
                return CheckLetterResult.NotALetter;
            }
            if (NamedLetters.IndexOf(value) != -1)
            {
                State = GameState.Playing;
                return CheckLetterResult.Repeated;
            }
            NamedLetters += value;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Word.Length; i++)
            {
                if (Word[i] == value)
                {
                    builder.Append(value);
                    count++;
                }
                else
                    builder.Append(Mask[i]);
            }
            Mask = builder.ToString();
            if (count == 0)
            {
                State = GameState.Playing;
                return CheckLetterResult.Invalid;
            }
            Score += LastScore * count;
            if (Mask.IndexOf("-") == -1)
            {
                State = GameState.Winned;
            }
            else
                State = GameState.Playing;
            return CheckLetterResult.Valid;            
        }

        public bool CheckAWord(string word)
        {
            word = word.ToUpper().Replace('Ё','E');
            if (word == Word)
            {
                int count = 0;
                for (int i = 0; i < Mask.Length; i++)
                {
                    if (Mask[i] == '-')
                        count++;
                }
                Score += 500 * count;
                State = GameState.Winned;
                return true;
            }
            else
            {
                State = GameState.NotPlaying;
                return false;
            }

        }
        public string GetWinnedPrize()
        {
            return prizes.GetPrize(Score);
            //return "Да за такие очки что Вы набрали... можно только мороженое Вам купить";
        }

        public string TakePrize()
        {
            Random random = new Random();
            State = GameState.NotPlaying;
            return prizes.GetPrize(-1);
        }
    }
}
