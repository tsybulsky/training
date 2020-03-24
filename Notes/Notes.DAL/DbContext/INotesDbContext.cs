using Notes.DAL.Repositories.Implementations;

namespace Notes.DAL.DbContext
{
    public interface INotesDbContext
    {
        UserRepository Users {get;set;}
        RoleRepository Roles { get; set; }
        CategoryRepository Categories { get; set; }
        NoteRepository Notes { get; set; }        
        NoteReferenceRepository NoteReferences { get; set; }
    }
}
