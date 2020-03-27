using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Notes.App.ViewModels.Category
{
    public class CategoryNoteViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Название")]        
        public string Title { get; set; }
        [Display(Name = "Актуально до")]
        public DateTime? ActualTill { get; set; }
        public int OwnerId { get; set; }
        [Display(Name = "Владелец")]
        public string Owner { get; set; }
    }
}