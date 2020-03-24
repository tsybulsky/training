using Notes.BLL.DTOModels;
using System.Collections.Generic;

namespace Notes.BLL.Services
{
    public interface ICategoryService
    {
        IEnumerable<CategoryDTO> GetList();
        
        CategoryDTO GetItemById(int id);
        bool Delete(int id);
        bool Update(CategoryDTO category);
        bool Create(CategoryDTO category);
    }
}
