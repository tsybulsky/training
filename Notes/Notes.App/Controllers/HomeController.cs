using Notes.App.ViewModels.Home;
using Notes.BLL;
using System.Web.Mvc;

namespace Notes.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessLogic _bl;
        public HomeController(IBusinessLogic bl)
        {
            _bl = bl;
        }

        public ActionResult Index()
        {
            IndexViewModel model = new IndexViewModel();
            model.Categories = _bl.Categories.GetList();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }


    }
}