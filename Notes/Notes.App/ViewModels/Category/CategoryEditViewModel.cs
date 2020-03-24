using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes.BLL.DTOModels;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        [HiddenInput(DisplayValue=false)]
        [Required]
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(AllowEmptyStrings =false)]        
        public string Name { get; set; }
        
        public string Error { get; set; }
    }
}