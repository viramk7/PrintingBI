using System.Collections.Generic;
using System.Threading.Tasks;
using PrintingBI.Data.Entities;

namespace PrintingBI.Data.Repositories.Departments
{
    public interface IDepartmentRepository
    {
        Task Insert(string connectionString, IEnumerable<Department> departments);
        Task<List<Department>> GetDepartmentList(string connectionString);
    }
}