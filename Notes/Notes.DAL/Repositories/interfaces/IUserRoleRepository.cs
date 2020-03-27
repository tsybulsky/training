using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.Entities;

namespace Notes.DAL.Repositories.Interfaces
{
    public interface IUserRoleRepository
    {
        IEnumerable<UserRole> GetListByUser(int userId);
    }
}
