using System.Collections.Generic;

namespace Notes.DAL.Interfaces
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetItemById(int id);
        void Save(T item);
        void Delete(int id);
    }
}
