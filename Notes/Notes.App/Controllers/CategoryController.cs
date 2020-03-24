using Notes.App.ViewModels.Category;
using Notes.App.ViewModels.Common;
using Notes.BLL;
using Notes.BLL.DTOModels;
using Notes.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            try
            {
                if (id > 0)
                {
                    CategoryDTO category = _bl.Categories.GetItemById(id);
                    CategoryDetailViewModel model = new CategoryDetailViewModel()
                    {
                        Id = category.Id,
                        Name = category.Name,
                    };
                    model.Notes = new List<CategoryNoteViewModel>();
                    IEnumerable<NoteDTO> notes = _bl.Notes.GetNotesByCategoryId(category.Id).OrderBy(note => note.Title);
                    int counter = 0;
                    IEnumerable<UserDTO> users = _bl.Users.GetList();
                    foreach (NoteDTO note in notes)
                    {
                        CategoryNoteViewModel noteModel = new CategoryNoteViewModel()
                        {
                            Id = note.Id,
                            Title = note.Title,
                            ActualTill = note.ActualTill,
                            OwnerId = note.OwnerId
                        };
                        UserDTO user = users.First(u => u.Id == note.OwnerId);
                        noteModel.Owner = (user != null) ? (user.UserName) : "не найден";
                        model.Notes.Add(noteModel);
                        if (++counter > 9)
                            break;
                    }
                    return PartialView("Details", model);
                }
                else
                {
                    return View("Index");
                }
            }
            catch (NoteCustomException e)
            {
                return new HttpNotFoundResult(e.Message);                
            }            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CategoryEditViewModel model)
        {
            if ((model != null) & (ModelState.IsValid))
            {
                try
                {
                    CategoryDTO category = new CategoryDTO() { Name = model.Name };
                    _bl.Categories.Create(category);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    return View("error", new ErrorViewModel(e.Message));
                }
            }
            else
                return View("Error", new ErrorViewModel("Неверные параметры"));

        }
        public ActionResult Edit(int id)
        {
            CategoryDTO category = _bl.Categories.GetItemById(id);
            if (category != null)
            {
                return PartialView(nameof(Edit), MapCategoryDTOToCategoryViewModel(category));
            }
            else
            {
                return PartialView(nameof(Index));
            }
        }


        [HttpPost]
        public ActionResult Edit(CategoryEditViewModel model)
        {
            if ((model != null)&&(ModelState.IsValid))
            {
                CategoryDTO category = MapCategoryViewModelToCategoryDTO(model);
                try
                {
                    _bl.Categories.Update(category);
                }
                catch (NoteCustomException e)
                {
                    model.Error = e.Message;
                    return View(model);
                }
                catch (Exception e)
                {
                    return new HttpNotFoundResult(e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                model.Error = "Что-то пошло не так";
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                _bl.Categories.Delete(id);                
            }
            catch (NoteCustomException e)
            {
                ModelState.AddModelError("error", e.Message);
            }
            catch (Exception e)
            {
                return new HttpStatusCodeResult(200,e.Message);
            }
            return RedirectToAction(nameof(Index));
        }

        private CategoryDTO MapCategoryViewModelToCategoryDTO(CategoryEditViewModel model)
        {
            return new CategoryDTO()
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        private CategoryEditViewModel MapCategoryDTOToCategoryViewModel(CategoryDTO model)
        {
            return new CategoryEditViewModel()
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }

}