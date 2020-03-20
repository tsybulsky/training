using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.BLL.DTOModels;

namespace Notes.BLL.Services
{
    public interface INoteService
    {
        IEnumerable<NoteDTO> GetList(int pageNo=0, int pageSize=0);
        NoteDTO GetItemById(int id);
        void Delete(int id);
        void Update(NoteDTO user);
        void Create(NoteDTO user);
        IEnumerable<NoteDTO> GetNotesByCategoryId(int Id, int pageNo=0, int pageSize=0);
        IEnumerable<NoteDTO> SearchByName(string name, int pageNo = 0, int pageSize = 0);
        IEnumerable<NoteDTO> SearchbyDate(DateTime date, int pageNo = 0, int pageSize = 0);
    }
}
