using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes.App.ViewModels.Account;
using Notes.BLL.DTOModels;
using Notes.BLL;

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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (model != null)
            {
                if (ModelState.IsValid)
                {
                    if (_bl.Users.Login(model.UserName, model.Password) != null)
                    {
                        return Redirect("~/Home/Index");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Invalid user name or password");                        
                    }
                }
                else
                    ModelState.AddModelError("Error", "Something wrong");
            }
            return View();
        }

        public ActionResult Logout()
        {
            _bl.Users.Logout();
            return View();
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