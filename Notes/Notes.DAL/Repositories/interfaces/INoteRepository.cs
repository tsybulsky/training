using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.Entities;

namespace Notes.DAL.Repositories.Interfaces
{
    public interface INoteRepository
    {
        IEnumerable<Note> SearchByName(string searchText, int pageNo=1, int pageSize=0);
        IEnumerable<Note> SearchByCategoryName(string categoryName, int pageNo=1, int pageSize=0);
        IEnumerable<Note> SearchByDate(DateTime date, int pageNo=1, int pageSize=0);
    }
}
