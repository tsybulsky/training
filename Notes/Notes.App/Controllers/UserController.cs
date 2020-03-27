using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Notes.BLL.DTOModels;
using Notes.App.ViewModels.User;
using Notes.BLL;
using Notes.Common.Exceptions;

namespace Notes.App.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        private readonly IBusinessLogic _bl;

        public UserController(IBusinessLogic bl)
        {
            _bl = bl;
        }

        [Route("")]
        public ActionResult Index()
        {
            IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, UserIndexViewModel>()).CreateMapper();
            IEnumerable<UserDTO> users = _bl.Users.GetList();
            return View(mapper.Map<IEnumerable<UserDTO>,IEnumerable<UserIndexViewModel>>(users));
        }

        public ActionResult Details(int id)
        {
            string errorContent;
            if (id > 0)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, UserDetailViewModel>()).CreateMapper();
                    UserDTO user = _bl.Users.GetItemById(id);
                    if (user != null)
                    {
                        UserDetailViewModel model = mapper.Map<UserDTO, UserDetailViewModel>(user);
                        model.Roles = _bl.UserRoles.GetListByUser(model.Id).ToList();
                        return PartialView(model);
                    }
                    else
                    {
                        errorContent = "<div class=\"text-danger\">Пользователь не найден в базе данных</div>";
                    }
                }
                catch (NoteCustomException e)
                {
                    errorContent = $"<div class=\"text-danger\">{e.Message}</div>";
                }
                catch (Exception e)
                {
                    errorContent = $"<div class=\"text-danger\">Системная ошибка: {e.Message}</div>";
                }
            }
            else
            {
                errorContent = "<div class=\"text-danger\">Неверный индентификатор пользователя</div>";
            }
            return Content(errorContent);
        }

        public ActionResult Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserDTO, UserEditViewModel>()).CreateMapper();
                    UserDTO user = _bl.Users.GetItemById(id);
                    if (user == null)
                    {
                        return Content("<div class=\"text-danger\">Пользователь не найден</div>");
                    }
                    UserEditViewModel model = mapper.Map<UserDTO, UserEditViewModel>(user);
                    model.Roles = _bl.UserRoles.GetListByUser(id);
                    return PartialView(model);
                }
                catch (NoteCustomException e)
                {
                    return Content($"<div class=\"text-danger\">{e.Message}</div>");
                }
                catch (Exception e)
                {
                    return Content($"<div class=\"text-danger\">System error: {e.Message}</div>");
                }
            }
            else
            {
                return Content($"<div class=\"text-danger\">Ooops!!! Not found</div>");
            }
        }

        [HttpPost]
        public ActionResult DoEdit(UserEditViewModel model)
        {
            JsonResult result = new JsonResult();
            if ((model != null) && (ModelState.IsValid))
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<UserEditViewModel, UserDTO>()).CreateMapper();
                    UserDTO user = mapper.Map<UserEditViewModel, UserDTO>(model);
                    if (user != null)
                    {
                        _bl.Users.Update(user);
                        result.Data = new { Code = 0, Message = "OK" };
                    }
                    else
                    {
                        result.Data = new { Code = -1, Message = "Ошибка обработки данных сервером" };
                    }
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -2, Message = e.Message };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -3, Message = $"Системная ошибка: " + e.Message };
                }
            }
            else
            {
                result.Data = new { Code = -3, Message = "Неверные параметры" };
            }
            return result;
        }

        [HttpPost]
        public ActionResult ListRoles(int userId)
        {
            JsonResult result = new JsonResult();
            if (userId > 0)
            {
                try
                {
                    IEnumerable<UserRoleDTO> roles = _bl.UserRoles.GetListByUser(userId);
                    result.Data = new { Code = 0, Message = "OK", Roles = roles };
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -1, Message = e.Message };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -2, Message = $"Системная ошибка: {e.Message}" };
                }
            }
            else
            {
                result.Data = new { Code = -3, Message = "Неверный идентификатор пользователя" };
            }
            return result;
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            JsonResult result = new JsonResult();
            if (id > 0)
            {
                try
                {
                    _bl.Users.Delete(id);
                    result.Data = new { Code = 0, Message = "OK" };
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -1, Message = e.Message };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -2, Message = $"Системная ошибка: " };
                }
            }
            else
            {
                result.Data = new { Code = -3, Message = "Неверные параметры вызова" };
            }
            return result;
        }
        
        public ActionResult AddUserRole(int id)
        {
            if (id > 0)
            {
                try
                {
                    UserDTO user = _bl.Users.GetItemById(id);
                    if (user != null)
                    {
                        AddUserRoleViewModel model = new AddUserRoleViewModel()
                        {
                            UserId = id,
                            NameOrLogin = user.NameOrLogin,
                            SelectedRole = 0,
                            Roles = _bl.Roles.GetList()
                        };
                        return PartialView(model);
                    }
                    else
                        return Content($"<div class=\"text-danger\">Пользователь не найден</div>");
                }
                catch (NoteCustomException e)
                {
                    return Content($"<div class=\"text-danger\">{e.Message}</div>");
                }
                catch (Exception e)
                {
                    return Content($"<div class=\"text-danger\">Системная ошибка: {e.Message}</div>");
                }
            }
            else
            {
                return Content($"<div class=\"text-danger\">Неверные параметры вызова</div>");
            }
        }

        [HttpPost] 
        public ActionResult DoAddUserRole(AddUserRoleViewModel model)
        {
            JsonResult result = new JsonResult();
            if ((model != null)&& ModelState.IsValid)
            {
                try
                {
                    UserRoleDTO userRole = new UserRoleDTO
                    {
                        UserId = model.UserId,
                        RoleId = model.SelectedRole
                    };
                    _bl.UserRoles.Create(userRole);
                    result.Data = new { Code = 0, Message = "OK" };
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -1, e.Message };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -2, Message = $"Системная ошибка: {e.Message}" };
                }
            }
            else
            {
                result.Data = new { Code = -3, Message = "Неверные параметы" };
            }
            return result;
        }

        [HttpPost]
        public ActionResult DoDeleteUserRole(int id)
        {
            JsonResult result = new JsonResult();
            if (id > 0)
            {
                try
                {
                    _bl.UserRoles.Delete(id);
                    result.Data = new { Code = 0, Message = "OK" };
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -1, e.Message };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -2, Message = $"Системная ошибка: {e.Message}" };
                }
            }
            else
            {
                result.Data = new { Code = -3, Message = "Неверный параметры" };
            }
            return result;
        }
    }
}