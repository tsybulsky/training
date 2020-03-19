using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes.BLL.DTOModels;

namespace Notes.App.ViewModels.Note
{
    public class NoteIndexViewModel
    {
        public string Title { get; set; }
        public IEnumerable<NoteDTO> Notes { get; set; }
        public int CurrentPage { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<CategoryDTO> Categories { get; set; }
    }
}