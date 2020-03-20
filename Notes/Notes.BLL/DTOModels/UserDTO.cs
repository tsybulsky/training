using System.Collections.Generic;
using Notes.DAL.Entities;
using Notes.Common;
using System;

namespace Notes.BLL.DTOModels
{
    public class UserDTO
    {
        public UserDTO()
        {
            //Roles = new List<Role>();
        }
        private bool _passwordLoaded = false;
        private string _password;
        public int Id { get; set; }
        //public ICollection<Role> Roles { get; }
        public string Role { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
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
        public int Status { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEditor { get; set; }
        public DateTime CreatedOnDate { get; set; }
    }
}
