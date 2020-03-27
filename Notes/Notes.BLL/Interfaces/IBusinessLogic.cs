using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.Interfaces;
using Notes.DAL.Entities;
using Notes.BLL.Services;

namespace Notes.BLL
{
    public interface IBusinessLogic
    {
        IRoleService Roles { get; }        
        IUserService Users { get; }
        IUserRoleService UserRoles { get; }
        INoteService Notes { get; }
        ICategoryService Categories { get; }
        INoteReferenceService NoteReferences { get; }
        
    }
}
