using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Notes.App.ViewModels.Note
{
    public class NoteReferenceViewModel
    {
        [HiddenInput]
        public int Id { get; set; }
        [HiddenInput]
        public int NoteId { get; set; }
        [HiddenInput]
        public int ReferenceId { get; set; }
        [Display(Name = "Название")]
        public string ReferenceTitle { get; set; }
    }
}