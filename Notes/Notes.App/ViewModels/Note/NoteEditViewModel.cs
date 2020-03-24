using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Notes.BLL.DTOModels;

namespace Notes.App.ViewModels.Note
{
    public class NoteEditViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        [MaxLength(1000)]
        [Required]
        public string Title { get; set; }
        [Display(Name = "Создано")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [Display(Name = "Описание")]
        public string Description { get; set; }        
        [HiddenInput]
        [Display(Name = "Категория")]
        public int CategoryId { get; set; }        
        public IEnumerable<CategoryDTO> Categories { get; set; }        
        [Editable(false)]
        public int OwnerId { get; set; }
        [Editable(false)]
        [Display(Name = "Владелец")]
        public string Owner { get; set; }
        
        [Display(Name="Актуальна до")]
        public DateTime? ActualTill { get; set; }        
        public byte[] Image { get; set; }
    }
}