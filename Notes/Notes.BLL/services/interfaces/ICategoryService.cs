using Notes.BLL.DTOModels;
using System.Collections.Generic;

namespace Notes.BLL.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetList();
        
        CategoryDTO GetItemById(int id);
        void Delete(int id);
        void Update(CategoryDTO category);
        void Create(CategoryDTO category);
    }
}
