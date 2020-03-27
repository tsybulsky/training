using Notes.BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using AutoMapper;

namespace Notes.BLL.Services
{
    public class RoleService : IRoleService
    {
        private readonly INotesDbContext _db;
        public RoleService(INotesDbContext db)
        {
            _db = db;
        }
        public IEnumerable<RoleDTO> GetList()
        {
            IMapper mapper = new MapperConfiguration(c => c.CreateMap<Role, RoleDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Role>, IEnumerable<RoleDTO>>(_db.Roles.GetAll());
        }
    }
}
