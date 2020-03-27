using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.Entities;
using System.Data.SqlClient;
using Notes.DAL.Repositories.Interfaces;
using Notes.Common.Exceptions;

namespace Notes.DAL.Repositories.Implementations
{
    public class UserRoleRepository : CustomRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDbConnection db) : base(db)
        {
            _viewName = "dbo.GetUserRoles";
            _saveProcedureName = "dbo.SaveUserRole";
            _deleteProcedureName = "dbo.DeleteUserRole";
        }

        public IEnumerable<UserRole> GetListByUser(int userId)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM {_viewName} WHERE UserId = {userId}";
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        List<UserRole> userRoles = new List<UserRole>();
                        while (reader.Read())
                        {
                            userRoles.Add(MapFromReader(reader));
                        }
                        return userRoles;
                    }
                }
            }
            catch(Exception e)
            {
                throw new NoteDataException(e.Message);
            }
        }

        protected override UserRole MapFromReader(IDataReader reader)
        {
            return new UserRole
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                LocalizedRoleName = GetAsString(reader,"LocalizedRoleName")
            };
        }

        protected override void MapToParameters(UserRole value, IDataParameterCollection parameters)
        {
            if (value != null)
            {
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id });
                parameters.Add(new SqlParameter("@UserId", SqlDbType.Int) { Value = value.UserId });
                parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = value.RoleId });
            }
        }
    }
}
