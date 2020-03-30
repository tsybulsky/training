using Notes.BLL.DTOModels;
using System.Collections.Generic;

namespace Notes.App.ViewModels.Note
{
    public class NoteSearchByNameViewModel: NoteIndexViewModel
    {
        public string SearchText { get; set; }
    }
}