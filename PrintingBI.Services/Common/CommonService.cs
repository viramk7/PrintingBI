using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Common;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.Common
{
    public class CommonService : EntityService<PrinterBIDepartment>, ICommonService
    {
        public CommonService(ICommonRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
