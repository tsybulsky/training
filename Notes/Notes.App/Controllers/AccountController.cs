using Notes.App.ViewModels.Account;
using Notes.BLL;
using Notes.BLL.DTOModels;
using System.Web.Mvc;
using System;
using Notes.Common.Exceptions;

namespace Notes.App.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBusinessLogic _bl;
        public AccountController(IBusinessLogic bl)
        {
            _bl = bl;
        }
        // GET: Account
        public ActionResult Index()
        {
            return View();
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
            return View();
        }

        [HttpGet]
        ActionResult ChangePassword(int? id)
        {
            int id2 = id ?? (HttpContext.User as LoggedUser).Id;
            if (id2 > 0)
            {
                UserDTO user = _bl.Users.GetItemById(id2);
                ChangePasswordModel model = new ChangePasswordModel()
                {
                    Id = id2,
                    UserName = user.UserName,
                    NewPassword = "",
                    ConfirmPassword = ""
                };
                return View(model);
            }
            else
            {
                return RedirectToRoute("/");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _bl.Users.ChangePassword(model.Id, model.OldPassword, model.NewPassword);
                    return RedirectToRoute("/");
                }
                catch
                {
                    return new HttpNotFoundResult();
                }
            }
            else
                return View(model);
        }
    }
}