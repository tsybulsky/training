using System;
using System.Collections.Generic;

namespace Notes.DAL.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public string Description { get; set; }
        public List<Note> References { get; }
        public int CategoryId { get; set; }
        public DateTime? ActualTill { get; set; }
        public int OwnerId { get; set; }
        public byte[] Image { get; set; }
        public string PictureMimeType { get; set; }
    }
}
