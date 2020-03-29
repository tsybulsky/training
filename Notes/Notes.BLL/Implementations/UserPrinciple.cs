using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;
using Notes.BLL.DTOModels;

namespace Notes.BLL
{
    public class UserPrinciple : LoggedUser, IPrincipal
    {
        public const string ADMIN_ROLE_NAME = "admin";
        public const string EDITOR_ROLE_NAME = "editor";
        public const string USER_ROLE_NAME = "user";

        public UserPrinciple(string username): base()
        {
            Identity = new GenericIdentity(username);
        }


        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            UserRoleDTO userRole = Roles.FirstOrDefault(ur => ur.RoleName.ToLower() == role);
            return userRole != null;
        }
    }
}
