using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes.BLL.DTOModels;
using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Category
{
    public class CategoryDetailViewModel
    {
        [Display(Name="Id")]        
        public int Id { get; set; }
        [Display(Name="Название")]
        public string Name { get; set; }
        public List<CategoryNoteViewModel> Notes { get; set; }
    }
}