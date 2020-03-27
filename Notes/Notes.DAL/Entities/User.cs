using Notes.Common;
using System;

namespace Notes.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }        
        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (!string.IsNullOrEmpty(_password))
                    _password = PasswordHash.HashString(value);
                else
                    _password = value;
            }
        }
        public string Email { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOnDate { get; set; }
        public DateTime? LastLogin { get; set; }
        public string NameOrLogin { get; set; }
    }
}
