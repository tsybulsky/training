using System;

namespace Notes.Common.Exceptions
{
    public class NoteArgumentException: Exception
    {
        public NoteArgumentException(string message): base(message) { }
    }
}
