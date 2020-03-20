namespace Notes.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsEditor { get; set; }
    }
}
