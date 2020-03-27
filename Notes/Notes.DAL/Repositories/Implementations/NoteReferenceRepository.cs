using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Notes.DAL.Repositories.Implementations
{
    public class NoteReferenceRepository : CustomRepository<NoteReference>
    {        
        public NoteReferenceRepository(IDbConnection db) : base(db)
        {
            _viewName = "GetNotes";
            _saveProcedureName = "SaveNote";
            _deleteProcedureName = "DeleteNote";
        }
        protected override NoteReference MapFromReader(IDataReader reader)
        {
            if (reader != null)
            {
                try
                {
                    return new NoteReference()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        NoteId = reader.GetInt32(reader.GetOrdinal("NoteId")),
                        ReferenceId = reader.GetInt32(reader.GetOrdinal("ReferenceId"))
                    };

                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }
            }
            else
                throw new NoteArgumentException("Неверное значение параметра");
        }

        protected override void MapToParameters(NoteReference value, IDataParameterCollection parameters)
        {
            parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id, Direction = ParameterDirection.InputOutput });
            parameters.Add(new SqlParameter("@NoteId", SqlDbType.Int) { Value = value.NoteId });
            parameters.Add(new SqlParameter("@ReferenceId", SqlDbType.Int) { Value = value.ReferenceId });            
        }
    }
}
