using AutoMapper;
using PrintingBI.API.Models;
using PrintingBI.Data.Entities;

namespace PrintingBI.API
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Author, AuthorsDto>()
                .ForMember(d => d.Name, s => s.MapFrom(m => $"{m.FirstName} {m.LastName}"));

            CreateMap<AuthorCreateDto, Author>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}
