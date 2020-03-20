using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes.App.ViewModels.Category;
using Notes.BLL;

namespace Notes.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBusinessLogic _bl;
        public CategoryController(IBusinessLogic bl)
        {
            _bl = bl;
        }
        // GET: Category
        public ActionResult Index()
        {
            CategoryIndexViewModel model = new CategoryIndexViewModel();
            model.Categories = _bl.Categories.GetList();
            return View(model);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            return PartialView("Details");
        }
    }
}