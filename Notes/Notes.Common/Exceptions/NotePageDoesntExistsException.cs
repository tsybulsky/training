using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Common.Exceptions
{
    public class NotePageDoesntExistsException: Exception
    {
        public NotePageDoesntExistsException(int pageNo):base($"Страница {pageNo} не существует")
        {

        }
    }
}
