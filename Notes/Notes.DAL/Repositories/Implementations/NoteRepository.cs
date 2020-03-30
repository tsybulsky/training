using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Notes.NoteMappers;
using Notes.DAL.Repositories.Interfaces;

namespace Notes.DAL.Repositories.Implementations
{
    public class NoteRepository : CustomRepository<Note>, INoteRepository
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
                        ActualTill = GetAsDateTime(reader,"ActualTill"),
                        OwnerId = reader.GetInt32(reader.GetOrdinal("OwnerId")),
                        Image = GetAsBytes(reader,"Picture"),
                        PictureMimeType = GetAsString(reader,"PictureMimeType")
                    };
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
                    parameters.Add(new SqlParameter("@PictureMimeType", SqlDbType.VarChar, 100) { Value = value.PictureMimeType });
                }
                catch (Exception e)
                {
                    throw new NoteDataException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
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

        public IEnumerable<Note> SearchByName(string searchText, int pageNo = 1, int pageSize = 0)
        {
            searchText = searchText.Replace("%", "").Replace("_", "");
            try
            {
                int count = GetTotalCount();
                if ((pageNo - 1) * pageSize > count)
                {
                    throw new NotePageDoesntExistsException(pageNo);
                }
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {_viewName} WHERE Title LIKE '%{searchText}%' ORDER BY Id";
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

        public IEnumerable<Note> SearchByCategoryName(string categoryName, int pageNo = 1, int pageSize = 0)
        {
            categoryName = categoryName.Replace("%", "").Replace("_", "");
            try
            {
                int count = GetTotalCount();
                if ((pageNo - 1) * pageSize > count)
                {
                    throw new NotePageDoesntExistsException(pageNo);
                }
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT A.* FROM {_viewName} A INNER JOIN dbo.GetCategories C ON c.Id = A.CategoryId WHERE C.Name LIKE '%{categoryName}%' ORDER BY Id";
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

        public IEnumerable<Note> SearchByDate(DateTime date, int pageNo = 1, int pageSize = 0)
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
                    command.CommandText = $"SELECT * FROM {_viewName} WHERE CreationDate BETWEEN '{date}' AND '{date.AddDays(1).AddSeconds(-1)}' ORDER BY Id";
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
