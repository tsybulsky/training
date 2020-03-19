using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.Entities;

namespace Notes.DAL.Repositories.Interfaces
{
    public interface IUserRepository
    {
        User Validate(string login, string password);
        void UpdatePassword(int id, string password);
    }
}
