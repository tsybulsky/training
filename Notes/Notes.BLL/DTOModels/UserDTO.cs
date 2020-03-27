using System.Collections.Generic;
using Notes.DAL.Entities;
using Notes.Common;
using System;

namespace Notes.BLL.DTOModels
{
    public class UserDTO
    {
        private bool _passwordLoaded = false;
        private string _password;
        public int Id { get; set; }        
        public string Login { get; set; }        
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_passwordLoaded)
                {
                    _password = PasswordHash.HashString(value);
                }
                else
                {
                    _password = value;
                }
                _passwordLoaded = !_passwordLoaded;
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
