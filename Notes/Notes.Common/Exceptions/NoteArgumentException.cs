using System;

namespace Notes.Common.Exceptions
{
    public class NoteArgumentException: Exception
    {
        public const string ARGUMENT_MESSAGE = "Неверные параметры вызова";
        public NoteArgumentException() : base(ARGUMENT_MESSAGE) { }
        public NoteArgumentException(string message): base(message) { }
        
    }
}
