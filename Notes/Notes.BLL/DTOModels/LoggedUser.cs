using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.BLL.DTOModels
{
    public class LoggedUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEditor { get; set; }
    }
}
