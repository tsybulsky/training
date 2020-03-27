using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.BLL.DTOModels;

namespace Notes.BLL.Services
{
    public interface IUserRoleService
    {
        IEnumerable<UserRoleDTO> GetList();
        IEnumerable<UserRoleDTO> GetListByUser(int userId);
        UserRoleDTO GetItemById(int id);
        void Delete(int id);
        void Update(UserRoleDTO userRole);
        void Create(UserRoleDTO userRole);        
    }
}
