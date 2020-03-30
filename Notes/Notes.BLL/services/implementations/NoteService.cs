using AutoMapper;
using Notes.BLL.DTOModels;
using Notes.DAL.DbContext;
using System;
using System.Collections.Generic;
using Notes.DAL.Entities;
using Notes.Common.Exceptions;

namespace Notes.BLL.Services
{
    public class NoteService : INoteService
    {
        private readonly INotesDbContext _db;

        public NoteService(INotesDbContext db)
        {
            _db = db;            
        }

        public void Create(NoteDTO note)
        {
            try
            {
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteDTO, Note>()).CreateMapper();
                Note dbNote = mapper.Map<NoteDTO, Note>(note);
                _db.Notes.Save(dbNote);
            }
            catch (NoteCustomException )
            {
                throw;
            }
            catch (Exception e)
            {
                throw new NoteCustomException(e.Message);
            }
        }

        public void Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    _db.Notes.Delete(id);
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
            throw new NoteArgumentException("Неверное значение параметра");
        }

        public NoteDTO GetItemById(int id)
        {
            if (id > 0)
            {
                try
                {
                    Note note = _db.Notes.GetItemById(id);
                    if (note != null)
                    {
                        IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                        return mapper.Map<Note, NoteDTO>(note);
                    }
                    throw new NoteNotFoundException("Запись не найдена");
                }
                catch (NoteCustomException)
                {
                    throw;
                }
            }
            throw new NoteArgumentException("Неверное значение параметра");
        }            
        
        public IEnumerable<NoteDTO> GetList(int pageNo = 0, int pageSize = 0)
        {
            try
            {
                IEnumerable<Note> notes;
                if (pageSize == 0)
                {
                    notes = _db.Notes.GetAll(pageNo, pageSize);                    
                }
                else
                {
                    notes = _db.Notes.GetAll();
                }
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
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

        public IEnumerable<NoteDTO> GetNotesByCategoryId(int id, int pageNo = 1, int pageSize = 0)
        {
            try
            {
                IEnumerable<Note> notes;
                if (pageSize < 0)
                    pageSize = 0;
                notes = _db.Notes.GetItemsByCategoryId(id,pageNo, pageSize);
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
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
        
        public IEnumerable<NoteDTO> SearchByDate(DateTime date, int pageNo = 1, int pageSize = 0)
        {
            try
            {
                IEnumerable<Note> notes;
                if (pageSize < 0)
                    pageSize = 0;
                notes = _db.Notes.SearchByDate(date, pageNo, pageSize);
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
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

        public IEnumerable<NoteDTO> SearchByName(string name, int pageNo = 1, int pageSize = 0)
        {
            try
            {
                IEnumerable<Note> notes;
                if (pageSize < 0)
                    pageSize = 0;
                notes = _db.Notes.SearchByName(name, pageNo, pageSize);
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
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
        public IEnumerable<NoteDTO> SearchByCategoryName(string categoryName, int pageNo = 1, int pageSize=0)
        {
            try
            {
                IEnumerable<Note> notes;
                if (pageSize < 0)
                    pageSize = 0;
                notes = _db.Notes.SearchByCategoryName(categoryName, pageNo, pageSize);
                IMapper mapper = new MapperConfiguration(c => c.CreateMap<Note, NoteDTO>()).CreateMapper();
                return mapper.Map<IEnumerable<Note>, IEnumerable<NoteDTO>>(notes);
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
        public void Update(NoteDTO note)
        {
            if (note != null)
            {
                try
                {
                    IMapper mapper = new MapperConfiguration(c => c.CreateMap<NoteDTO, Note>()).CreateMapper();
                    Note noteEntity = mapper.Map<NoteDTO, Note>(note);
                    _db.Notes.Save(noteEntity);
                }
                catch (Exception e)
                {
                    throw new NoteCustomException(e.Message);

                }
            }
            else
                throw new NoteArgumentException();
        }
    }   
}
