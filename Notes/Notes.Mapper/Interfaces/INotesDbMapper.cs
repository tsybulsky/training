using System.Data;
namespace Notes.NoteMappers
{
    public interface INotesDbMapper
    {
        T Map<T>(IDataReader reader) where T: class, new();
    }
}
