using Notes.Common;
using System;

namespace Notes.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
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
        public int Status { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
