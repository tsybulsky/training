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
        private readonly IUserRoleService _userRoles;
        
        public UserService(INotesDbContext db, IUserRoleService userRoles)
        {
            _db = db;
            _userRoles = userRoles;
        }

        public void Create(UserDTO user)
        {
            if (user != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, User>()).CreateMapper();
                    User userEntity = mapper.Map<User>(user);
                    _db.Users.Save(userEntity);
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

                    _db.Users.Delete(id);
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

        public UserDTO GetItemById(int id)
        {
            try
            {
                User userEntity = _db.Users.GetItemById(id);
                if (userEntity == null)
                    throw new NoteNotFoundException("User not found");
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()).CreateMapper();
                UserDTO user = mapper.Map<User,UserDTO>(userEntity);
                return user;
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

        public IEnumerable<UserDTO> GetList()
        {
            try
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<User, UserDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_db.Users.GetAll());
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

        public LoggedUser Login(string login, string password)
        {
            User validUser = _db.Users.Validate(login, password);
            if (validUser != null)
            {                
                LoggedUser currentUser = new LoggedUser()
                {
                    Id = validUser.Id,
                    Login = validUser.Login,                    
                    Name = validUser.Name,
                    Email = validUser.Email,
                    Roles = _userRoles.GetListByUser(validUser.Id)
                };
                string userData = JsonConvert.SerializeObject(currentUser);
                var ticket = new FormsAuthenticationTicket(1, currentUser.Login, DateTime.Now, DateTime.Now.AddMinutes(20), false, userData);
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

        public void Update(UserDTO user)
        {
            try
            {
                if (user != null)
                {                    
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, User>()).CreateMapper();
                    User newUser = mapper.Map<UserDTO, User>(user);
                    newUser.Password = "";
                    _db.Users.Save(newUser);
                }
                else
                {
                    throw new NoteArgumentException();
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
                        if (user.Password != PasswordHash.HashString(oldPassword))
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
