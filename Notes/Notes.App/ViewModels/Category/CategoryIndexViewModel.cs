using Notes.BLL.DTOModels;
using System.Collections.Generic;

namespace Notes.App.ViewModels.Category
{
    public class CategoryIndexViewModel
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public int TotalCount { get; set; }
        public int CurrentPage { get; set; }
    }
}