using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Services.Entities;

namespace PrintingBI.Services.Author
{
    public class AuthorService : EntityService<Data.Entities.Author>, IAuthorService
    {
        public AuthorService(IRepository<Data.Entities.Author> repository) : base(repository)
        {

        }
    }
}
