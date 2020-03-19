using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Notes.BLL;
using Notes.BLL.DTOModels;
using Notes.App.ViewModels.Note;

namespace Notes.App.Controllers
{    
    public class NoteController : Controller
    {
        private readonly IBusinessLogic _bl;
        public NoteController (IBusinessLogic bl)
        {
            _bl = bl;
        }
        // GET: Note
        public ActionResult Index()
        {
            NoteIndexViewModel model = new NoteIndexViewModel()
            {
                Notes = _bl.Notes.GetList(1, 10),
                Categories = _bl.Categories.GetList()
            };
            return View(model);
        }

        public ActionResult GetImage(int id)
        {
            return new EmptyResult();            
        }

        public ActionResult Details(int id)
        {
            NoteDTO note = _bl.Notes.GetItemById(id);
            if (note != null)
            {
                return PartialView("Details", note);
            }
            else
                return View("Index");
        }
        [HttpGet]
        public ActionResult Search(string searchText)
        {
            List<NoteDTO> notes = _bl.Notes.GetList().Where(n => n.Title.Contains(searchText)).ToList();
            JsonResult json = new JsonResult();
            json.Data = notes;
            return json;
        }

        public ActionResult ByCategory(int id)
        {
            NoteIndexViewModel model;
            CategoryDTO category = _bl.Categories.GetItemById(id);
            if (category == null)
            {
                model = new NoteIndexViewModel()
                {
                    Title = $"Заметки",
                    Categories = _bl.Categories.GetList(),
                    Notes = _bl.Notes.GetList()
                };                
            }
            else
            {
                model = new NoteIndexViewModel()
                {
                    Title = $"Заметки по категории {category.Name}",
                    Categories = _bl.Categories.GetList(),
                    Notes = _bl.Notes.GetNotesByCategoryId(id)
                };
            }
            return View("Index", model);
        }
    }
}