using System;
using System.IO;
using System.Text.Json;

namespace MagicFieldGame
{
    class UserStatistics
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public int TotalGames { get; set; }
        public int Winned { get; set; }

        public int Losed { get; set; }

        public int TookPrize { get; set; }

        public int Scores { get; set; }

        public int MaxScores { get; set; }

        public int AvgScores { get; set; }

        public int Steps { get; set; }

        public int AvgSteps { get; set; }

        public UserStatistics()
        {

        }

        public static UserStatistics Load(long Id)
        {
            if (!File.Exists($"stat\\{Id}.json"))
                return new UserStatistics();
            using (FileStream stream = new FileStream($"stat\\{Id}.json", FileMode.OpenOrCreate))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string jsonValue = reader.ReadToEnd();
                    if (!String.IsNullOrWhiteSpace(jsonValue))
                    {
                        return JsonSerializer.Deserialize<UserStatistics>(jsonValue);
                    }
                    else
                        return new UserStatistics();
                }
            }
        }

        public void Store()
        {
            using (FileStream stream = new FileStream($"stat\\{Id}.json",FileMode.OpenOrCreate))
            {
                stream.SetLength(0);
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string jsonValue = JsonSerializer.Serialize<UserStatistics>(this);
                    writer.Write(jsonValue);
                }
            }
        }
    }
}
