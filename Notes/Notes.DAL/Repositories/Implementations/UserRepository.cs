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

        private void UpdateLoginTime(int userId)
        {
            try
            {
                using (IDbCommand command = _db.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "dbo.UpdateLoginTime";
                    command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = userId });
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw new NoteDataException("Ошибка обновления данных пользователя\r\n"+e.Message);
            }
        }

        public User Validate(string login, string password)
        {
            string hash = PasswordHash.HashString(password);
            IDbCommand command = _db.CreateCommand();
            command.CommandText = $"SELECT * FROM dbo.GetUsers WHERE (Login = '{login}')and(password = '{hash}')";
            User user;
            using (IDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    user = MapFromReader(reader);

                }
                else
                {
                    return null;
                }
            }
            UpdateLoginTime(user.Id);
            return user;
        }

        protected override User MapFromReader(IDataReader reader)
        {
            try
            {
                return new User()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Login = reader.GetString(reader.GetOrdinal("Login")),
                    Password = reader.GetString(reader.GetOrdinal("Password")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Name = GetAsString(reader, "Name"),
                    Status = reader.GetInt32(reader.GetOrdinal("Status")),
                    CreatedOnDate = reader.GetDateTime(reader.GetOrdinal("CreatedOnDate")),
                    LastLogin = GetAsDateTime(reader, "LastLogin"),
                    NameOrLogin = GetAsString(reader, "NameOrLogin")
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
                parameters.Add(new SqlParameter("@Login", SqlDbType.NVarChar, 50) { Value = value.Login });
                parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 40) { Value = value.Password });
                parameters.Add(new SqlParameter("@EMail", SqlDbType.NVarChar, 50) { Value = value.Email });
                parameters.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = value.Name });
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
                command.CommandText = "dbo.UpdatePasswordUser";
                command.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int) { Value = id });
                command.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar, 40) { Value = PasswordHash.HashString(password) });
                int resultCode = command.ExecuteNonQuery();
                if (resultCode <= 0)
                {
                    throw new NoteNotFoundException("Пользователь не найден");
                }
                else if (resultCode > 1)
                {
                    throw new NoteDataException("Ошибка целостности базы данных");
                }
            }
            else
                throw new NoteArgumentException();
        }
    }
}
