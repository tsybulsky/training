using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.BLL.DTOModels;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using Notes.Common.Exceptions;
using AutoMapper;

namespace Notes.BLL.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly INotesDbContext _db;
        public UserRoleService(INotesDbContext db)
        {
            _db = db;
        }


        public void Create(UserRoleDTO userRole)
        {
            if (userRole != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserRoleDTO, UserRole>()).CreateMapper();
                    _db.UserRoles.Save(mapper.Map<UserRoleDTO, UserRole>(userRole));
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    _db.UserRoles.Delete(id);
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }

        public UserRoleDTO GetItemById(int id)
        {
            if (id > 0)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserRole, UserRoleDTO>()).CreateMapper();
                    UserRole userRole = _db.UserRoles.GetItemById(id);
                    if (userRole != null)
                    {
                        return mapper.Map<UserRole, UserRoleDTO>(userRole);
                    }
                    else
                    {
                        throw new NoteNotFoundException("Роль пользователя не найдена");
                    }
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }

        public IEnumerable<UserRoleDTO> GetList()
        {
            try
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserRole, UserRoleDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(_db.UserRoles.GetAll());
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch(Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public IEnumerable<UserRoleDTO> GetListByUser(int userId)
        {
            try
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserRole, UserRoleDTO>()).CreateMapper();
                IEnumerable<UserRole> userRoles = _db.UserRoles.GetListByUser(userId);
                return mapper.Map<IEnumerable<UserRole>, IEnumerable<UserRoleDTO>>(userRoles);
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public void Update(UserRoleDTO userRole)
        {
            if (userRole != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserRoleDTO, UserRole>()).CreateMapper();
                    UserRole userRoleEntity = mapper.Map<UserRoleDTO, UserRole>(userRole);
                    if (userRoleEntity != null)
                    {
                        _db.UserRoles.Save(userRoleEntity);
                    }
                    else
                        throw new NoteDataException("Ошибка преобразования объекта");
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }
    }
}
