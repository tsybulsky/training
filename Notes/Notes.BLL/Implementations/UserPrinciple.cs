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
        public UserPrinciple(string username)
        {
            Identity = new GenericIdentity(username);
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (role != null)
            {
                return role.ToLower() == Role.ToLower();
            }
            else
                return false;
        }
    }
}
