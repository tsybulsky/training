using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Notes.NoteMappers;

namespace Notes.DAL.Repositories.Implementations
{
    public class NoteRepository : CustomRepository<Note>
    {
        public NoteRepository(IDbConnection db): base (db)
        {
            _viewName = "GetNotes";
            _saveProcedureName = "SaveNote";
            _deleteProcedureName = "DeleteNote";
        }

        protected override Note MapFromReader(IDataReader reader)
        {
            if (reader != null)
            {
                try
                {
                    int imageFieldIndex = reader.GetOrdinal("Picture");
                    var note = new Note()
                    {
                        Id = reader.GetInt32(reader.GetOrdinal("Id")),
                        CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                        Title = reader.GetString(reader.GetOrdinal("Title")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        CreationDate = reader.GetDateTime(reader.GetOrdinal("CreationDate")),
                        //ActualTill = reader.GetDateTime(reader.GetOrdinal("ActualTill")),
                        OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId"))
                    };
                    int actualTillIndex = reader.GetOrdinal("ActualTill");
                    if (reader[actualTillIndex] is DBNull)
                        note.ActualTill = null;
                    else
                        note.ActualTill = reader.GetDateTime(actualTillIndex);
                    if (!(reader[imageFieldIndex] is DBNull))
                    {
                        long fieldSize = reader.GetBytes(imageFieldIndex, 0, null, 0, 0);
                        note.Image = new byte[fieldSize];
                        reader.GetBytes(imageFieldIndex, 0, note.Image, 0, (int)fieldSize);
                    }
                    return note;
                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }
            }
            else
            {
                throw new NoteArgumentException("Reader cannot be null");
            }
        }

        protected override void MapToParameters(Note value, IDataParameterCollection parameters)
        {
            if ((value != null) && (parameters != null))
            {
                try
                {
                    parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id, Direction = ParameterDirection.InputOutput });
                    parameters.Add(new SqlParameter("@Title", SqlDbType.NVarChar, 1000) { Value = value.Title });
                    parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar) { Value = value.Description });
                    parameters.Add(new SqlParameter("@CategoryId", SqlDbType.Int) { Value = value.CategoryId });
                    parameters.Add(new SqlParameter("@OwnerId", SqlDbType.Int) { Value = value.OwnerId });
                    parameters.Add(new SqlParameter("@Picture", SqlDbType.VarBinary) { Value = value.Image });
                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }
            }
            else
                throw new NoteArgumentException("Invalid parameter value");
        }

        public int GetTotalCount()
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT COUNT(*) FROM {_viewName}";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return reader.GetInt32(0);
                        else
                            return 0;
                    }
                }
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public IEnumerable<Note> GetAll(int pageNo, int pageSize=0)
        {
            try
            {
                int count = GetTotalCount();
                if ((pageSize > 0)&&((pageNo-1) * pageSize > count))
                {
                    throw new NotePageDoesntExistsException(pageNo);
                }
                
                using (IDbCommand command = _db.CreateCommand())
                {

                    command.CommandText = $"SELECT * FROM {_viewName} ORDER BY Id";
                    if (pageSize > 0)
                    {
                        command.CommandText += $" OFFSET {(pageNo - 1) * pageSize} ROWS FETCH FIRST {pageSize} ROWS ONLY ";
                    }
                    List<Note> items = new List<Note>();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = reader.GetSchemaTable();
                        INotesDbMapper mapper = new NotesDbMapper(typeof(Note), table);
                        while (reader.Read())
                        {
                            items.Add(mapper.Map<Note>(reader));
                        }
                    }
                    return items;
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        public IEnumerable<Note> GetItemsByCategoryId(int categoryId, int pageNo=1, int pageSize=0)
        {
            try
            {
                int count = GetTotalCount();
                if ((pageNo - 1) * pageSize > count)
                {
                    throw new NotePageDoesntExistsException(pageNo);
                }
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {_viewName} WHERE CategoryId = {categoryId} ORDER BY Id";
                    if (pageSize > 0)
                    {
                        command.CommandText += $" OFFSET {(pageNo - 1) * pageSize} ROWS FETCH FIRST {pageSize} ROWS ONLY ";
                    }                    
                    List<Note> items = new List<Note>();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = reader.GetSchemaTable();
                        INotesDbMapper mapper = new NotesDbMapper(typeof(Note), table);
                        while (reader.Read())
                        {
                            items.Add(mapper.Map<Note>(reader));
                        }
                    }
                    return items;
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }
    }
}
