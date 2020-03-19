using Notes.Common.Exceptions;
using Notes.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Notes.DAL.Repositories.Interfaces;
using Notes.Common;

namespace Notes.DAL.Repositories.Implementations
{
    public class UserRepository : CustomRepository<User>, IUserRepository
    {
        
        public UserRepository(IDbConnection db):base(db)
        {
            _viewName = "dbo.GetUsers";
            _saveProcedureName = "dbo.SaveUser";
            _deleteProcedureName = "dbo.DeleteUser";
        }

        public User Validate(string login, string password)
        {
            string hash = PasswordHash.HashString(password);
            IDbCommand command = _db.CreateCommand();
            command.CommandText = $"SELECT * FROM dbo.GetUsers WHERE (UserName = '{login}')and(password = '{hash}')";
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return MapFromReader(reader);
                }
                else
                {
                    return null;
                }
            }
        }

        protected override User MapFromReader(IDataReader reader)
        {
            try
            {
                return new User()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    RoleId = reader.GetInt32(reader.GetOrdinal("RoleId")),
                    RoleName = reader.GetString(reader.GetOrdinal("RoleName")),
                    UserName = reader.GetString(reader.GetOrdinal("Username")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    Status = reader.GetInt32(reader.GetOrdinal("Status")),
                    CreatedOnDate = reader.GetDateTime(reader.GetOrdinal("CreatedOnDate"))
                };                
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            }            
        }

        protected override void MapToParameters(User value, IDataParameterCollection parameters)
        {        
            try
            {
                parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = value.Id });
                parameters.Add(new SqlParameter("@RoleId", SqlDbType.Int) { Value = value.RoleId });
                parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar, 50) { Value = value.UserName });
                parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = value.Email });
                parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 40) { Value = value.Password });
                parameters.Add(new SqlParameter("@Status", SqlDbType.Int) { Value = value.Status });
            }
            catch (Exception e)
            {
                throw new NoteDataException(e.Message);
            };
        }

        public void UpdatePassword(int id, string password)
        {
            if ((id > 0) && (password != null))
            {
                IDbCommand command = _db.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "dbo.UserUpdatePassword";
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                command.Parameters.Add(new SqlParameter("@Passowrd", SqlDbType.NVarChar, 40) { Value = PasswordHash.HashString(password) });
                int resultCode = command.ExecuteNonQuery();
                if (resultCode <= 0)
                {
                    throw new NoteNotFoundException("User not found");
                }
                else if (resultCode > 1)
                {
                    throw new NoteDataException("Database integrity error. Please, call database's administrator");
                }
            }
            else
                throw new NoteArgumentException("Invalid parameter value");
        }
    }
}
