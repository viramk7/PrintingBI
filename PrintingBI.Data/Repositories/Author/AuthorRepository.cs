using PrintingBI.Data.Repositories.Generic;

namespace PrintingBI.Data.Repositories.Author
{
    public class AuthorRepository : EfRepository<Entities.Author>, IAuthorRepository
    {
        public AuthorRepository(PrintingBIDbContext context) : base(context)
        {

        }
    }
}
