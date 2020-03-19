using Notes.DAL.Repositories.Implementations;
using System.Data;
using System.Data.SqlClient;

namespace Notes.DAL.DbContext
{
    public class NotesDbContext: INotesDbContext
    {
        private readonly IDbConnection db;
        public UserRepository Users { get; set; }
        public RoleRepository Roles { get; set; }
        public CategoryRepository Categories { get; set; }
        public NoteRepository Notes { get; set; }

        public NotesDbContext(string connectionString)
        {
            db = new SqlConnection(connectionString);
            Users = new UserRepository(db);
            Roles = new RoleRepository(db);
            Categories = new CategoryRepository(db);
            Notes = new NoteRepository(db);            
        }
    }
}
