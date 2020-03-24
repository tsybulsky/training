using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Notes.BLL.DTOModels;

namespace Notes.App.ViewModels.Note
{
    public class NoteDetailViewModel
    {
        [HiddenInput(DisplayValue=false)]
        public int Id { get; set; }
        [Display(Name = "Заголовок")]
        [MaxLength(1000)]
        [Required]
        public string Title { get; set; }
        [Display(Name="Создано")]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }
        [Display(Name="Описание")]
        public string Description { get; set; }
        public List<NoteReferenceViewModel> References { get; set; }
        [HiddenInput]
        public int CategoryId { get; set; }
        [Display(Name="Категория")]
        public string Category { get; set; }
        [HiddenInput]
        public int OwnerId { get; set; }
        [Display(Name="Владелец")]
        public string Owner { get; set; }
        [Display(Name="Актуальна до")]
        public DateTime? ActualTill { get; set; }
        public byte[] Image { get; set; }
    }
}