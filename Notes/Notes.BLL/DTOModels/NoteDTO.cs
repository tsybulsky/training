using System;
using System.Collections.Generic;

namespace Notes.BLL.DTOModels
{
    public class NoteDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public List<NoteDTO> References { get; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public int OwnerId { get; set; }
        public DateTime? ActualTill { get; set; }
        public string Owner { get; set; }
        public byte[] Image { get; set; }
    }
}
