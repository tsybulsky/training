using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Notes.App.ViewModels.Note
{
    public class NoteSearchByDateViewModel: NoteIndexViewModel
    {
        public DateTime SearchDate { get; set; }
    }
}