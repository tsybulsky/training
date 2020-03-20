using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Notes.BLL.DTOModels;
namespace Notes.App.ViewModels.Home
{
    public class IndexViewModel
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public bool IsListView { get; set; }
        public IEnumerable<NoteDTO> Notes { get; set; }
    }
}