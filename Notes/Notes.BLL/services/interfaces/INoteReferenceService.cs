using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.BLL.DTOModels;

namespace Notes.BLL.Services
{
    public interface INoteReferenceService
    {
        IEnumerable<NoteReferenceDTO> GetList();
        NoteReferenceDTO GetItemById(int id);
        bool Delete(int id);
        bool Update(NoteReferenceDTO category);
        bool Create(NoteReferenceDTO category);
        IEnumerable<NoteReferenceDTO> GetReferencesTo(int id);
        IEnumerable<NoteReferenceDTO> GetReferencedFrom(int id);
    }
}
