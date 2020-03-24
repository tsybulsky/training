using AutoMapper;
using Notes.BLL.DTOModels;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using System;
using System.Collections.Generic;
using Notes.Common.Exceptions;

namespace Notes.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        protected readonly INotesDbContext _db;
        public CategoryService(INotesDbContext db)
        {
            _db = db;
        }
        public bool Create(CategoryDTO category)
        {
            if (category != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<CategoryDTO, Category>()).CreateMapper();
                    Category categoryEntity = mapper.Map<CategoryDTO, Category>(category);
                    _db.Categories.Save(categoryEntity);
                    return true;
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
            {
                throw new NoteArgumentException("Неверные параметры вызова");
            }
        }

        public bool Delete(int id)
        {
            if (id >= 0)
            {
                try
                {
                    _db.Categories.Delete(id);
                    return true;
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                return false;
        }

        public CategoryDTO GetItemById(int id)
        {
            if (id > 0)
            {
                try
                {
                    Category category = _db.Categories.GetItemById(id);
                    if (category != null)
                    {
                        IMapper mapper = new MapperConfiguration(c => c.CreateMap<Category, CategoryDTO>()).CreateMapper();
                        return mapper.Map<Category, CategoryDTO>(category);
                    }
                    else
                        throw new NoteNotFoundException("Категория не найдена");
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException("Неверные параметры вызова");
        }

        public IEnumerable<CategoryDTO> GetList()
        {
            try
            {
                List<Category> categories = _db.Categories.GetAll();
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Category, CategoryDTO>()).CreateMapper();
                return mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public bool Update(CategoryDTO category)
        {
            if (category != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<CategoryDTO, Category>()).CreateMapper();
                    Category categoryDb = mapper.Map<CategoryDTO, Category>(category);
                    _db.Categories.Save(categoryDb);
                    return true;
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
            {
                throw new NoteArgumentException("Неверное значение параметра");
            }
        }
    }
}