using Notes.BLL.DTOModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.DAL.DbContext;
using Notes.DAL.Entities;
using AutoMapper;
using Notes.Common.Exceptions;

namespace Notes.BLL.Services
{
    public class NoteReferenceService : INoteReferenceService
    {
        private readonly INotesDbContext _db;

        public NoteReferenceService(INotesDbContext db)
        {
            _db = db;
        }

        public void Create(NoteReferenceDTO noteReference)
        {
            if (noteReference != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReferenceDTO, NoteReference>()).CreateMapper();
                    NoteReference noteReferenceEntity = mapper.Map<NoteReferenceDTO, NoteReference>(noteReference);
                    _db.NoteReferences.Save(noteReferenceEntity);                    
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
            {
                throw new NoteArgumentException();
            }
        }

        public void Delete(int id)
        {
            if (id >= 0)
            {
                try
                {
                    _db.NoteReferences.Delete(id);
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }

        public NoteReferenceDTO GetItemById(int id)
        {
            if (id > 0)
            {
                try
                {
                    NoteReference noteReference = _db.NoteReferences.GetItemById(id);
                    if (noteReference != null)
                    {
                        IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReference, NoteReferenceDTO>()).CreateMapper();
                        NoteReferenceDTO noteReferenceDto = mapper.Map<NoteReference, NoteReferenceDTO>(noteReference);
                        noteReferenceDto.Reference = _db.Notes.GetItemById(noteReference.ReferenceId)?.Title;
                        return noteReferenceDto;
                    }
                    else
                        throw new NoteNotFoundException("Ссылка заметки не найдена");
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
                throw new NoteArgumentException();
        }

        public IEnumerable<NoteReferenceDTO> GetList()
        {
            try
            {
                List<NoteReference> categories = _db.NoteReferences.GetAll();
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReference, NoteReferenceDTO>()).CreateMapper();
                return mapper.Map<List<NoteReference>, List<NoteReferenceDTO>>(categories);
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public IEnumerable<NoteReferenceDTO> GetReferencedFrom(int id)
        {
            try
            {
                List<NoteReference> categories = _db.NoteReferences.GetAll().Where(nr=>nr.NoteId == id).ToList();
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReference, NoteReferenceDTO>()).CreateMapper();
                return mapper.Map<List<NoteReference>, List<NoteReferenceDTO>>(categories);
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public IEnumerable<NoteReferenceDTO> GetReferencesTo(int id)
        {
            try
            {
                List<NoteReference> categories = _db.NoteReferences.GetAll().Where(nr => nr.ReferenceId == id).ToList();
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReference, NoteReferenceDTO>()).CreateMapper();
                return mapper.Map<List<NoteReference>, List<NoteReferenceDTO>>(categories);
            }
            catch (NoteCustomException)
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public void Update(NoteReferenceDTO reference)
        {
            if (reference != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteReferenceDTO, NoteReference>()).CreateMapper();
                    NoteReference noteReference = mapper.Map<NoteReferenceDTO, NoteReference>(reference);
                    _db.NoteReferences.Save(noteReference);
                }
                catch (NoteCustomException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);
                }
            }
            else
            {
                throw new NoteArgumentException();
            }
        }
    }
}
