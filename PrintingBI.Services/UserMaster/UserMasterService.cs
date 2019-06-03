using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.UserMaster;
using PrintingBI.Services.Entities;
using System.Threading.Tasks;

namespace PrintingBI.Services.UserMaster
{
    public class UserMasterService : EntityService<PrinterBIUser>, IUserMasterService
    {
        private readonly IUserMasterRepository _userRepo;
        private readonly IMapper _mapper;

        public UserMasterService(IUserMasterRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _userRepo = repository;
            _mapper = mapper;
        }

        public async Task<string> InsertUser<CreateUserDto>(CreateUserDto dto)
        {
            var entity = _mapper.Map<PrinterBIUser>(dto);
            var result = await _userRepo.InsertUser(entity);
            return result;
        }

        //public async Task<string> InsertUser(PrinterBIUser entity)
        //{
        //    var result = await _userRepo.InsertUser(entity);
        //    return result;
        //}
    }
}
