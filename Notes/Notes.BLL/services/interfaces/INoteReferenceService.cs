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
        void Delete(int id);
        void Update(NoteReferenceDTO category);
        void Create(NoteReferenceDTO category);
        IEnumerable<NoteReferenceDTO> GetReferencesTo(int id);
        IEnumerable<NoteReferenceDTO> GetReferencedFrom(int id);
    }
}
