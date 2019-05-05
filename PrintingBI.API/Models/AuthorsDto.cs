namespace PrintingBI.API.Models
{
    /// <summary>
    /// An author with id and name
    /// </summary>
    public class AuthorsDto
    {
        /// <summary>
        /// The unique id of the author
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the author
        /// </summary>
        public string Name { get; set; }
    }
}
