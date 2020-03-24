using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.DAL.Entities
{
    public class NoteReference
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int ReferenceId { get; set; }

        public string ReferenceTitle { get; set; }
    }
}
