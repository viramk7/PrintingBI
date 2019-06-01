using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Common;
using PrintingBI.Services.Entities;

namespace PrintingBI.Services.Common
{
    public class CommonService : EntityService<PrinterBIDepartment>, ICommonService
    {
        public CommonService(ICommonRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
