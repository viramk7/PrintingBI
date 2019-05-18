using Microsoft.Extensions.DependencyInjection.Extensions;
using PrintingBI.Authentication.Configuration;
using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Author;
using PrintingBI.Data.Repositories.Departments;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.Login;
using PrintingBI.Data.Repositories.Provisioning;
using PrintingBI.Data.Repositories.ProvisionPowerBITenants;
using PrintingBI.Services.AdminTenantService;
using PrintingBI.Services.Author;
using PrintingBI.Services.Departments;
using PrintingBI.Services.Entities;
using PrintingBI.Services.HttpClientHelpers;
using PrintingBI.Services.LoginService;
using PrintingBI.Services.Provisioning;
using PrintingBI.Services.ProvisionPowerBITenants;

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
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IProvisionPowerBITenantsRepository, ProvisionPowerBITenantsRepository>();
            services.AddTransient<IProvisioningRepository, ProvisioningRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();

            //services.TryAddEnumerable(new[]
            //{
            //    ServiceDescriptor.Transient<IProvisionTable,ProvisionUserTable>(),
            //    ServiceDescriptor.Transient<IProvisionTable,ProvisionDepartmentTable>(),
            //});

            services.Scan(scan => scan
                .FromAssemblyOf<IProvision>()
                .AddClasses(c => c.AssignableTo<IProvision>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );
            
            // Services
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IProvisioningService, ProvisioningService>();
            services.AddTransient<IProvisionPowerBITenantsService, ProvisionPowerBITenantsService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IFilterDeptListToEntityHelper, FilterDeptListToEntityHelper>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAdminTenantService, AdminTenantService>();

            // Helpers
            services.AddTransient<IExtractDeptDataFromExcel, ExtractDeptDataFromExcel>();
            

            return services;
        }
    }
}
