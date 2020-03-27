using Notes.App.ViewModels.Account;
using Notes.BLL;
using Notes.BLL.DTOModels;
using Notes.Common.Exceptions;
using System;
using System.Web.Mvc;

namespace Notes.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessLogic _bl;
        public AccountController(IBusinessLogic bl)
        {
            _bl = bl;
        }

        public ActionResult Index()
        {
            return new EmptyResult();
        }

        public ActionResult Login(string returnUrl)
        {
            if (!String.IsNullOrWhiteSpace(returnUrl))
                ViewData["returnUrl"] = returnUrl;
            else
                ViewData["returnUrl"] = "";
            return View();
        }

        [HttpPost]       
        public ActionResult DoLogin(string username, string password)
        {
            JsonResult result = new JsonResult();
            if ((!String.IsNullOrWhiteSpace(username))&&
                (!String.IsNullOrEmpty(password)))
            {
                if (_bl.Users.Login(username, password) != null)
                {
                    result.Data = new { Code = 0, Message = "OK" };
                }
                else
                {
                    result.Data = new { Code = -3, Message = "Неверное имя пользователя или пароль" };
                }
            }
            else
                result.Data = new { Code = -1, Message = "Неверные параметры вызова" };
            return result;
        }
        [HttpPost]
        public ActionResult Logout()
        {
            JsonResult result = new JsonResult();
            try
            {
                _bl.Users.Logout();
                result.Data = new { Code = 0, Message = "OK" };
            }
            catch (NoteCustomException e)
            {
                result.Data = new { Code = -1, Message = e.Message };
            }
            catch (Exception e)
            {
                result.Data = new { Code = -2, Message = e.Message };
            }
            return result;
        }

        public ActionResult Details()
        {
            LoggedUser user = (HttpContext.User as LoggedUser);
            if (user != null)
            {
                return PartialView(user);
            }
            else
            {
                return Content("<div class=\"text-danger\">Возможно вход не выполнен</div>");
            }
        }

        [HttpGet]
        public ActionResult ChangePassword(int? id)
        {
            int id2 = id ?? (HttpContext.User as LoggedUser).Id;
            if (id2 > 0)
            {
                UserDTO user = _bl.Users.GetItemById(id2);
                ChangePasswordViewModel model = new ChangePasswordViewModel()
                {
                    Id = id2,
                    Login = user.Login,
                    NewPassword = "",
                    ConfirmPassword = ""
                };
                return PartialView(model);
            }
            else
            {
                return Content("<div class=\"text-danger\"></div>");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            JsonResult result = new JsonResult();
            if (ModelState.IsValid)
            {
                try
                {
                    _bl.Users.ChangePassword(model.Id, model.OldPassword, model.NewPassword);
                    result.Data = new { Code = 0, Message = "OK" };
                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = 1, Message = e.Message };
                }
            }
            else
            {
                result.Data = new { Code = 2, Message = "Ошибка смены пароля. Проверьте все параметры и повторите попытку" };
            }
            return result;
        }

        public ActionResult AdminSetPassword(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AdminSetPassword(AdminSetPasswordViewModel model)
        {
            JsonResult result = new JsonResult();
            result.Data = new { Code = 0, Message = "Еще не реализовано" };
            return result;
        }
    }
}