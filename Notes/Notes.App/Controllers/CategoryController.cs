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
        
        [Authorize]
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
                        noteModel.Owner = (user != null) ? (user.NameOrLogin) : "не найден";
                        model.Notes.Add(noteModel);
                        if (++counter > 9)
                            break;
                    }
                    return PartialView(model);
                }
                else
                {
                    return PartialView();
                }
            }
            catch (NoteCustomException e)
            {
                return new HttpNotFoundResult(e.Message);                
            }            
        }

        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult DoCreate(string name)
        {
            JsonResult result = new JsonResult();
            if (!String.IsNullOrWhiteSpace(name))
            {
                try
                {
                    CategoryDTO category = new CategoryDTO() { Name = name };
                    _bl.Categories.Create(category);
                    result.Data = new { Code = 0, Message = "OK" };

                }
                catch (NoteCustomException e)
                {
                    result.Data = new { Code = -1, Message = e.Message, Source = e.GetType().Name };
                }
                catch (Exception e)
                {
                    result.Data = new { Code = -2, Message = e.Message };
                }
            }
            else
                result.Data = new { Code = 3, Message = "Неверные значения параметров" };
            return result;
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
        public ActionResult DoEdit(CategoryEditViewModel model)
        {
            JsonResult result = new JsonResult();
            if ((model != null)&&(ModelState.IsValid))
            {
                CategoryDTO category = MapCategoryViewModelToCategoryDTO(model);
                try
                {
                    _bl.Categories.Update(category);
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
            }
            else
            {
                result.Data = new { Code = -3, Message = "Ошибка проверки данных" };
            }
            return result;
        }

        [HttpPost]
        public ActionResult DoDelete(int id)
        {
            JsonResult result = new JsonResult();
            try
            {
                _bl.Categories.Delete(id);
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