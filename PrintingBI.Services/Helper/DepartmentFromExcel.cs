namespace PrintingBI.Services.Helper
{
    public class DepartmentFromExcel
    {
        public DepartmentFromExcel()
        {

        }

        public DepartmentFromExcel(string departmentName, string parentDepartmentName)
        {
            DepartmentName = departmentName;
            ParentDepartmentName = parentDepartmentName;
        }

        public string DepartmentName { get; set; }
        public string ParentDepartmentName { get; set; }
    }
}
