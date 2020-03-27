using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Category
{
    public class CategoryCreateViewModel
    {
        [Display(Name = "Название")]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
    }
}