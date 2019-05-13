using Microsoft.Extensions.DependencyInjection.Extensions;
using PrintingBI.Data.Repositories.Author;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.Provisioning;
using PrintingBI.Data.Repositories.User;
using PrintingBI.Services.Author;
using PrintingBI.Services.Entities;
using PrintingBI.Services.Provisioning;
using PrintingBI.Services.User;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AppServiceCollectionExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            // Infrastructure
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


            // Repositories
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            //services.TryAddEnumerable(new[]
            //{
            //    ServiceDescriptor.Transient<IProvisionTable,ProvisionUserTable>(),
            //    ServiceDescriptor.Transient<IProvisionTable,ProvisionDepartmentTable>(),
            //});

            services.Scan(scan => scan
                .FromAssemblyOf<IProvisionTable>()
                .AddClasses(c => c.AssignableTo<IProvisionTable>())
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.AddTransient<IProvisioningRepository, ProvisioningRepository>();

            // Services
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProvisioningService, ProvisioningService>();

            return services;
        }
    }
}
