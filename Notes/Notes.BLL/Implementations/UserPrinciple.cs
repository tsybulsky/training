using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace Notes.BLL
{
    public class UserPrinciple : IPrincipal
    {
        public const string ADMIN_ROLE_NAME = "Администраторы";
        public const string EDITOR_ROLE_NAME = "Пользователи";
        public const string USER_ROLE_NAME = "Пользователи";

        public UserPrinciple(string username)
        {
            Identity = new GenericIdentity(username);
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEditor { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (role != null)
            {
                return (IsAdmin && (role == ADMIN_ROLE_NAME)) || (IsEditor && (role == EDITOR_ROLE_NAME)) || 
                    ((role == USER_ROLE_NAME)&&(!(IsAdmin || IsEditor)));
            }
            else
                return false;
        }
    }
}
