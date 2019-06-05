using AutoMapper;
using Microsoft.PowerBI.Api.V2.Models;
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

            CreateMap<Report ,PrinterBIReportMaster>()
                .ForMember(dest => dest.Id , opt => opt.MapFrom(src =>src.Id))
                .ForMember(dest => dest.ReportName, opt => opt.MapFrom(src => src.Name));

        }
    }
}
