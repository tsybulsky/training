using Notes.BLL.DTOModels;
using System.Collections.Generic;
namespace Notes.BLL.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetList();
        UserDTO GetItemById(int id);        
        void Delete(int id);
        void Update(UserDTO user);
        void Create(UserDTO user);
        LoggedUser Login(string login, string password);
        void Logout();
        void ChangePassword(int id, string oldPassword, string newPassword);
    }
}
