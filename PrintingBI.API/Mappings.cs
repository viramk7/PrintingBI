using AutoMapper;
using PrintingBI.API.Models;
using PrintingBI.Data.Entities;

namespace PrintingBI.API
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<UserDto, PrinterBIUser>();
            CreateMap<CreateUserDto, PrinterBIUser>();
            CreateMap<UpdateUserDto, PrinterBIUser>();
            CreateMap<DepartmnetDto, PrinterBIDepartment>();

        }
    }
}
