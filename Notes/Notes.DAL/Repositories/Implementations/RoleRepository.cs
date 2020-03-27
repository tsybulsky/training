using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using Notes.DAL.Repositories.Implementations;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Notes.DAL.Repositories.Implementations
{
    public class RoleRepository : CustomRepository<Role>
    {
        public RoleRepository(IDbConnection db): base(db)
        {
            _viewName = "GetRoles";
            _saveProcedureName = "SaveRole";
            _deleteProcedureName = "DeleteRole";
        }

        protected override Role MapFromReader(IDataReader reader)
        {
            if (reader != null)
            {
                try
                {
                    return new Role()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        Name = reader.GetString(reader.GetOrdinal("Name")),
                        LocalizedName = reader.GetString(reader.GetOrdinal("LocalizedName"))
                    };
                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }

            }
            else
                throw new NoteArgumentException("Invalid argument value");            
        }

        protected override void MapToParameters(Role value, IDataParameterCollection parameters)
        {
            if ((value != null) && (parameters != null))
            {
                try
                {
                    parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id });
                    parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = value.Name });
                    parameters.Add(new SqlParameter("@LocalizedName", SqlDbType.NVarChar,50) { Value = value.LocalizedName });                    
                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }
            }
            else
                throw new NoteArgumentException("Invalid parameter value");
        }
    }
}
