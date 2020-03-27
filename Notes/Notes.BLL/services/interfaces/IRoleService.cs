using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.BLL.DTOModels;

namespace Notes.BLL.Services
{
    public interface IRoleService
    {
        IEnumerable<RoleDTO> GetList();
    }
}
