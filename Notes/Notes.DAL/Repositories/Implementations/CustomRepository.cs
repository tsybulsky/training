using Notes.Common.Exceptions;
using Notes.DAL.Interfaces;
using Notes.NoteMappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;


namespace Notes.DAL.Repositories.Implementations
{
    public abstract class CustomRepository<T> : IRepository<T> where T: class, new()
    {
        protected readonly IDbConnection _db;
        protected string _saveProcedureName;
        protected string _deleteProcedureName;
        protected string _viewName;       
        public CustomRepository(IDbConnection db)
        {
            _db = db;
            if (_db.State != ConnectionState.Open)
            {
                _db.Open();
            }
        }
        protected string GetTableName(Type type)
        {
            try
            {
                IList<CustomAttributeData> attributesData = type.GetCustomAttributesData().Where(a => a.AttributeType == typeof(TableAttribute)).ToList();
                if (attributesData.Count == 0)
                {
                    string tableName = type.Name;
                    if (tableName.EndsWith("s"))
                    {
                        return tableName + "es";
                    }
                    else if (tableName.EndsWith("y"))
                    {
                        return tableName.Substring(tableName.Length - 1, 1) + "ies";
                    }
                    else
                        return tableName;
                }
                else
                {
                    var attribute = attributesData.First<CustomAttributeData>();
                    var tableName = attribute.NamedArguments.First<CustomAttributeNamedArgument>(a => a.MemberName == "TableName");
                    return (string)tableName.TypedValue.Value;
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        protected string GetIdColumnName(Type type)
        {
            try
            {
                var properties = type.GetProperties(BindingFlags.Public).Where(p => (p.Name.ToLower() == "id") || (p.Name.ToLower() == type.Name + "id"));
                if (properties.Count() == 0)
                {
                    return "";
                }
                else
                {
                    PropertyInfo idProperty = properties.First();
                    CustomAttributeData attributeData = idProperty.GetCustomAttributesData()
                            .Where(a => a.AttributeType == typeof(ColumnAttribute))
                            .ToList().First();
                    if (attributeData == null)
                    {
                        return idProperty.Name;
                    }
                    else
                    {
                        CustomAttributeNamedArgument argument = attributeData.NamedArguments.Where(a => a.MemberName == "Name").First();
                        if (argument != null)
                            return (string)(argument.TypedValue.Value);
                        else
                            return idProperty.Name;
                    }
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        public void Delete(int id)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = _deleteProcedureName;
                    command.Parameters.Add(new SqlParameter { ParameterName = "@Id", Value = id });
                    int procedureResult = command.ExecuteNonQuery();
                    if (procedureResult != 1)
                    {
                        throw new NoteDataException($"Database error 0x{procedureResult:X,-8}");
                    }
                }
            }
            catch (Exception E)
            {
                throw new NoteDataException(E.Message);
            }
        }

        protected abstract T MapFromReader(IDataReader reader);

        protected abstract void MapToParameters(T value, IDataParameterCollection parameters);

        public List<T> GetAll()
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {_viewName}";
                    List<T> items = new List<T>();
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        DataTable table = reader.GetSchemaTable();
                        INotesDbMapper mapper = new NotesDbMapper(typeof(T), table);
                        while (reader.Read())
                        {
                            items.Add(mapper.Map<T>(reader));
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

        public void Save(T item)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = _saveProcedureName;
                    MapToParameters(item, command.Parameters);
                    int result = command.ExecuteNonQuery();
                    if (result != 1)
                        throw new NoteCustomException("Ошибка сохранения данных");
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        public T GetItemById(int id)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {_viewName} WHERE Id = {id}";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapFromReader(reader);
                        }
                        else
                            return null;
                    }
                }
            }
            catch (NoteDataException )
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }
    }
}
