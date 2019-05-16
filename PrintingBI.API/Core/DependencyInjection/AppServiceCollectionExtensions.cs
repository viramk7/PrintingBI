using Microsoft.Extensions.DependencyInjection.Extensions;
using PrintingBI.Authentication.Configuration;
using PrintingBI.Data.Infrastructure;
using PrintingBI.Data.Repositories.Author;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.Provisioning;
using PrintingBI.Data.Repositories.ProvisionPowerBITenants;
using PrintingBI.Data.Repositories.User;
using PrintingBI.Services.Author;
using PrintingBI.Services.Entities;
using PrintingBI.Services.Provisioning;
using PrintingBI.Services.ProvisionPowerBITenants;
using PrintingBI.Services.User;

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

            // Repositories
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IProvisionPowerBITenantsRepository, ProvisionPowerBITenantsRepository>();
            services.AddTransient<IProvisioningRepository, ProvisioningRepository>();

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
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProvisioningService, ProvisioningService>();
            services.AddTransient<IProvisionPowerBITenantsService, ProvisionPowerBITenantsService>();
            
            return services;
        }
    }
}
