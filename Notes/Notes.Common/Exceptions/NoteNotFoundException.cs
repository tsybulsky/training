using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Common.Exceptions
{
    public class NoteNotFoundException: NoteCustomException
    {
        public NoteNotFoundException(string message) : base(message) { }
    }
}
