using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Notes.DAL.Repositories.Implementations
{
    public class CategoryRepository : CustomRepository<Category>
    {
        public CategoryRepository(IDbConnection db):base(db)
        {
            _viewName = "dbo.GetCategories";
            _saveProcedureName = "dbo.SaveGetegory";
            _deleteProcedureName = "dbo.deleteCategory";
        }
        protected override Category MapFromReader(IDataReader reader)
        {
            try

            {
                return new Category()
                {
                    Name = reader.GetString(reader.GetOrdinal("Name"))
                };
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        protected override void MapToParameters(Category value, IDataParameterCollection parameters)
        {
            try
            {
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id });
                parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar) { Value = value.Name });
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }
    }
}
