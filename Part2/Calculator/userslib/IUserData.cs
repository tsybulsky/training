using System;
using System.Collections.Generic;
using System.Text;

namespace userslib
{
    interface IUserData
    {
        User GetUser(string name);

        User AddUser(User user);

        User EditUser(User user);

        bool DeleteUser(User user);

        bool Login(string name, string password);
    }
}
