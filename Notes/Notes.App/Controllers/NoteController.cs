using AutoMapper;
using Notes.App.ViewModels.Note;
using Notes.BLL;
using Notes.BLL.DTOModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Notes.App.ViewModels.Common;
using Notes.Common.Exceptions;
using System.Web;
using System.IO;
using System;

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
                note.Category = _bl.Categories.GetItemById(note.CategoryId)?.Name;
                note.Owner = _bl.Users.GetItemById(note.OwnerId)?.NameOrLogin;
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteDTO, NoteDetailViewModel>()).CreateMapper();
                NoteDetailViewModel model = mapper.Map<NoteDTO, NoteDetailViewModel>(note);
                model.References = new List<NoteReferenceViewModel>();
                mapper = new MapperConfiguration(c => c.CreateMap<NoteReferenceDTO, NoteReferenceViewModel>()).CreateMapper();
                model.References = mapper.Map<IEnumerable<NoteReferenceDTO>,List<NoteReferenceViewModel>>(_bl.NoteReferences.GetReferencesTo(id));
                return PartialView("Details", model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel("Запись заметки не найдена");
                return PartialView("PartialError",error);
            }
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

        public ActionResult Create()
        {
            try
            {
                NoteEditViewModel model = new NoteEditViewModel();
                model.OwnerId = ((UserPrinciple)(HttpContext.User)).Id;
                model.Owner = _bl.Users.GetItemById(model.OwnerId)?.NameOrLogin;
                model.CreationDate = DateTime.Now;
                model.Categories = _bl.Categories.GetList().OrderBy(c => c.Name).ToList();
                return PartialView(model);
            }
            catch (Exception e)
            {
                return PartialView("Error", new ErrorViewModel(e.Message));
            }
        }
        
        [HttpPost]
        public ActionResult Create(NoteEditViewModel model, HttpPostedFileBase uploadedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteEditViewModel, NoteDTO>()).CreateMapper();
                    NoteDTO note = mapper.Map<NoteEditViewModel, NoteDTO>(model);
                    if (uploadedFile != null)
                    {
                        using (BinaryReader reader = new BinaryReader(uploadedFile.InputStream))
                        {
                            note.Image = reader.ReadBytes(uploadedFile.ContentLength);
                        }
                        note.PictureMimeType = uploadedFile.ContentType;
                    }
                    else
                    {
                        note.Image = null;
                        note.PictureMimeType = "";
                    }
                    _bl.Notes.Create(note);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return View("Error", e.Message);
                }
            }
            else
                return View("Error");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (id > 0)
            {
                try
                {

                    NoteDTO note = _bl.Notes.GetItemById(id);
                    if (note != null)
                    {
                        IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteDTO, NoteEditViewModel>()).CreateMapper();
                        NoteEditViewModel model = mapper.Map<NoteDTO, NoteEditViewModel>(note);
                        model.Categories = _bl.Categories.GetList().OrderBy(c => c.Name).ToList();
                        model.Owner = _bl.Users.GetItemById(model.OwnerId)?.NameOrLogin;
                        return PartialView(model);
                    }
                    else
                    {
                        return PartialView("Error", new ErrorViewModel("Заметка не найдена"));
                    }
                }
                catch (NoteCustomException e)
                {
                    ErrorViewModel errorModel = new ErrorViewModel(e.Message);
                    return PartialView("Error", errorModel);
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
                return PartialView("Error", new ErrorViewModel("Неверные параметры"));
        }

        [HttpPost]
        public ActionResult Edit(NoteEditViewModel model, HttpPostedFileBase image)
        {
            if ((model != null) && (ModelState.IsValid))
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteEditViewModel, NoteDTO>()).CreateMapper();
                    NoteDTO note = mapper.Map<NoteEditViewModel, NoteDTO>(model);
                    if (image != null)
                    {
                        using (BinaryReader reader = new BinaryReader(image.InputStream))
                        {
                            note.Image = reader.ReadBytes(image.ContentLength);
                        }
                    }
                    else
                        note.Image = null;
                    _bl.Notes.Update(note);
                    return RedirectToAction(nameof(Index));
                }
                catch (NoteCustomException e)
                {
                    return View("Error", e.Message);
                }
            }
            else
                return PartialView(model);
        }

        public ActionResult Delete(int id, int? page)
        {
            if (id > 0)
            {
                try
                {
                    _bl.Notes.Delete(id);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    return View("Error", new ErrorViewModel(e.Message));
                }
            }
            else
                return View("Error", new ErrorViewModel("Неверные параметры вызова"));
        }
    }
}