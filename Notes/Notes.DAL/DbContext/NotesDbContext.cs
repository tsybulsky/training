using Notes.DAL.Repositories.Implementations;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Notes.DAL.DbContext
{
    public class NotesDbContext: INotesDbContext
    {
        private readonly IDbConnection _db;
        public UserRepository Users { get; set; }
        public RoleRepository Roles { get; set; }

        public UserRoleRepository UserRoles { get; set; }
        public CategoryRepository Categories { get; set; }
        public NoteRepository Notes { get; set; }
        public NoteReferenceRepository NoteReferences { get; set; }

        public NotesDbContext(string connectionString)
        {
            _db = new SqlConnection(connectionString);
            Users = new UserRepository(_db);
            Roles = new RoleRepository(_db);
            UserRoles = new UserRoleRepository(_db);
            Categories = new CategoryRepository(_db);
            Notes = new NoteRepository(_db);
            NoteReferences = new NoteReferenceRepository(_db);
        }

        public string GetDbError(int code)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.getErrorMessage";
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Code", Value = code });
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Message", Direction = ParameterDirection.Output });
                    int procedureResult = command.ExecuteNonQuery();
                    if (procedureResult == 0)
                    {
                        return command.Parameters["@Message"].ToString();
                    }
                    else
                        return $"Ошибка {procedureResult}";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
