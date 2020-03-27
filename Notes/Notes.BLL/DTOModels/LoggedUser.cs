using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BLL.DTOModels
{
    public class LoggedUser
    {
        public LoggedUser()
        {
            Roles = new List<UserRoleDTO>();
        }
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserRoleDTO> Roles { get; set; }
    }
}
