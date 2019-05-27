using AutoMapper;
using PrintingBI.Data.Entities;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.UserMaster;
using PrintingBI.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrintingBI.Services.UserMaster
{
    public class UserMasterService : EntityService<PrinterBIUser>, IUserMasterService
    {
        public UserMasterService(IUserMasterRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }
    }
}
