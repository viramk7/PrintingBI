using Microsoft.AspNetCore.Http;
using PrintingBI.Data.Repositories.Departments;
using PrintingBI.Services.Helper;
using System.Threading.Tasks;

namespace PrintingBI.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFilterDeptListToEntityHelper _filterDeptListToEntityHelper;

        public DepartmentService(IDepartmentRepository departmentRepository,
                                 IFilterDeptListToEntityHelper filterDeptListToEntityHelper)
        {
            _departmentRepository = departmentRepository;
            _filterDeptListToEntityHelper = filterDeptListToEntityHelper;
        }

        public async Task<(bool, string)> Insert(string connectionString, IFormFile file)
        {
            var (isValidFile, departments) = _filterDeptListToEntityHelper.CreateDepartmentHierarchy(file);

            if (!isValidFile)
                return (false, "File is not valid.");

            await _departmentRepository.Insert(connectionString, departments);

            return (true, "Success");
        }
    }
}
