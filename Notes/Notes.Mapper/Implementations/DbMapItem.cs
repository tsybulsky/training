using System.Reflection;

namespace Notes.NoteMappers
{
    class DbMapItem
    {
        public PropertyInfo ModelProperty { get; set; }
        public int ColumnIndex { get; set; }
        public bool AllowNull { get; set; }
    }
}
