using System;

namespace Notes.Common.Exceptions
{
    public class NoteCustomException: Exception
    {
        public NoteCustomException(string message) : base(message) { }
    }
}
