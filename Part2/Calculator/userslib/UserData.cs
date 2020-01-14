using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.IO;
using System;
using System.Threading.Tasks;

namespace userslib
{
    public class UserData : IUserData
    {
#if DEBUG
        private string usersPath = Path.GetFullPath(@"..\..\..\users\");
#else
        private string usersPath = Path.GetFullPath(@"..\users\");
#endif
        public User AddUser(User user)
        {
            if (File.Exists($@"{usersPath}{user.Name}.json"))
                return null;
            else
            {
                string json = JsonSerializer.Serialize<User>(user);
                using (FileStream stream = new FileStream($@"{usersPath}{user.Name}.json",FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteAsync(json);
                    }
                }
                return user;
            }
        }

        public bool DeleteUser(User user)
        {
            string fullFilename = $@"{usersPath}{user.Name}.json";
            if (File.Exists(fullFilename))
            {
                File.Delete(fullFilename);
                return true;
            }
            else
                return false;
        }

        public User EditUser(User user)
        {
            if (!File.Exists($@"{usersPath}{user.Name}.json"))
                return null;
            else
            {
                string json = JsonSerializer.Serialize<User>(user);
                using (FileStream stream = new FileStream($@"{usersPath}{user.Name}.json", FileMode.OpenOrCreate))
                {
                    stream.SetLength(0);
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteAsync(json);
                    }
                }
                return user;
            }
        }

        public User GetUser(string name)
        {
            string fullFilename = $@"{usersPath}{name}.json";
            if (File.Exists(fullFilename))
            {
                using (FileStream stream = new FileStream(fullFilename,FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        return JsonSerializer.Deserialize<User>(json);
                    }
                }
            }
            else
                return null;
        }

        async public Task<User[]> ListUsers()
        {
            string[] fileNames = Directory.GetFiles(usersPath, "*.json");
            User[] result = new User[fileNames.Length];
            int j = 0;
            for (int i = 0; i < fileNames.Length;i++)
            {
                using (FileStream stream = new FileStream(fileNames[i], FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = await reader.ReadToEndAsync();
                        User user = JsonSerializer.Deserialize<User>(json);
                        if (user != null)
                        {
                            result[j++] = user;
                        }
                    }
                }
            }
            if (j != fileNames.Length)
            {
                Array.Resize(ref result, j);
            }
            return result;
        }
        public bool Login(string name, string password)
        {
            string passwordHash = PasswordHash.GetHash(password);
            User user = GetUser(name);
            if (user == null)
                return false;
            return user.Password == passwordHash;
        }
    }
}
