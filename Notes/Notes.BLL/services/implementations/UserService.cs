using AutoMapper;
using Newtonsoft.Json;
using Notes.BLL.DTOModels;
using Notes.Common;
using Notes.Common.Exceptions;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data;
namespace Notes.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly INotesDbContext _db;
        
        public UserService(INotesDbContext db)
        {
            _db = db;
        }

        public bool Create(UserDTO user)
        {
            if (user != null)
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, User>()).CreateMapper();
                User userEntity = mapper.Map<User>(user);
                _db.Users.Save(userEntity);
                return true;
            }
            else
                throw new NoteArgumentException("");
        }

        public bool Delete(int id)
        {
            try
            {
                _db.Users.Delete(id);
                return true;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public UserDTO GetItemById(int id)
        {
            try
            {
                User userEntity = _db.Users.GetItemById(id);
                if (userEntity == null)
                    throw new NoteNotFoundException("User not found");
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, User>()).CreateMapper();
                UserDTO user = mapper.Map<UserDTO>(userEntity);
                Role role = _db.Roles.GetItemById(user.RoleId);
                if (role != null)
                {
                    user.Role = role.Name;
                    user.IsAdmin = role.IsAdmin;
                    user.IsEditor = role.IsEditor;
                }
                return user;
            }
            catch(Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public IEnumerable<UserDTO> GetList()
        {
            try
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.Users.GetAll());
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public LoggedUser Login(string login, string password)
        {
            User validUser = _db.Users.Validate(login, password);
            if (validUser != null)
            {
                Role role = _db.Roles.GetItemById(validUser.RoleId);
                LoggedUser currentUser = new LoggedUser()
                {
                    Id = validUser.Id,
                    UserName = validUser.UserName,
                    Role = role.Name,
                    IsAdmin = role.IsAdmin,
                    IsEditor = role.IsEditor
                };
                string userData = JsonConvert.SerializeObject(currentUser);
                var ticket = new FormsAuthenticationTicket(1, currentUser.UserName, DateTime.Now, DateTime.Now.AddMinutes(20), false, userData);
                var encryptTicket = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptTicket);
                HttpContext.Current.Response.Cookies.Add(cookie);
                return currentUser;
            }
            else
            {
                return null;
            }
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }

        public bool Update(UserDTO user)
        {
            try
            {
                if (user != null)
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()).CreateMapper();
                    _db.Users.Save(mapper.Map<UserDTO,User>(user));
                    return true;
                }
                else
                {
                    throw new NoteArgumentException("Invalid parameter value");
                }
            }
            catch(Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public void ChangePassword(int id, string oldPassword, string newPassword)
        {
            if ((id > 0) && (oldPassword != null) && (newPassword != null))
            {
                try
                {
                    User user = _db.Users.GetItemById(id);
                    if (user != null)
                    {
                        if (user.Password != oldPassword)
                        {
                            throw new NoteInvalidPasswordException("Неверный старый пароль");
                        }
                        _db.Users.UpdatePassword(id, newPassword);
                    }
                    else
                    {
                        throw new NoteNotFoundException("Пользователь не найден");
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
                throw new NoteArgumentException("Неверное значение параметра");
        }
    }
}
