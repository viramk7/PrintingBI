using PrintingBI.Common.Configurations;
using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.AssignReportsToall;
using PrintingBI.Data.Repositories.AssignReportsToUser;
using PrintingBI.Data.Repositories.Common;
using PrintingBI.Data.Repositories.Departments;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.Login;
using PrintingBI.Data.Repositories.ProvisionPowerBITenants;
using PrintingBI.Data.Repositories.ReportMaster;
using PrintingBI.Data.Repositories.UserMaster;
using PrintingBI.Data.Repositories.Users;
using PrintingBI.Services.AdminTenantService;
using PrintingBI.Services.AssignReportsToUser;
using PrintingBI.Services.AssignToAllService;
using PrintingBI.Services.Common;
using PrintingBI.Services.Departments;
using PrintingBI.Services.Entities;
using PrintingBI.Services.Helper;
using PrintingBI.Services.HttpClientHelpers;
using PrintingBI.Services.LoginService;
using PrintingBI.Services.Notification;
using PrintingBI.Services.PowerBIService;
using PrintingBI.Services.ProvisionPowerBITenants;
using PrintingBI.Services.ReportsService;
using PrintingBI.Services.UserMaster;
using PrintingBI.Services.Users;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Infrastructure
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICustomerDbInfo, CustomerDbInfo>();
            services.AddScoped<ICustomerDbContext, CustomerDbContext>();
            services.AddScoped(typeof(IHttpClientHelper<>), typeof(HttpClientHelper<>));

            // Repositories
            services.AddTransient<IProvisionPowerBITenantsRepository, ProvisionPowerBITenantsRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ICommonRepository, CommonRepository>();
            services.AddTransient<IUserMasterRepository, UserMasterRepository>();
            services.AddTransient<IReportMasterRepository, ReportMasterRepository>();
            services.AddTransient<IAssignReportsToAllRepository, AssignReportsToAllRepository>();
            services.AddTransient<IAssignReportsToUserRepository, AssignReportsToUserRepository>();

            // Services
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddTransient<IProvisionPowerBITenantsService, ProvisionPowerBITenantsService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IFilterDeptListToEntityHelper, FilterDeptListToEntityHelper>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAdminTenantService, AdminTenantService>();
            services.AddTransient<IEmailNotificationService, EmailNotificationService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICommonService, CommonService>();
            services.AddTransient<IUserMasterService, UserMasterService>();
            services.AddTransient<IPowerBIService, PowerBIService>();
            services.AddTransient<IReportMasterService, ReportMasterService>();
            services.AddTransient<IAssignToAllService, AssignToAllService>();
            services.AddTransient<IAssignReportsToUserService, AssignReportsToUserService>();

            // Helpers
            services.AddTransient<IExtractDeptDataFromExcel, ExtractDeptDataFromExcel>();
            services.AddTransient<IExtractUserDataFromExcel, ExtractUserDataFromExcel>();
            services.AddTransient<IFilterUsertListToEntityHelper, FilterUsertListToEntityHelper>();


            return services;
        }
    }
}
