namespace PrintingBI.API.Models
{
    public class AuthorsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public AuthorsDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
