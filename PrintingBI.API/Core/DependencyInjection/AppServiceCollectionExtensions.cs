using PrintingBI.Data.Repositories.Author;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Data.Repositories.User;
using PrintingBI.Services.Author;
using PrintingBI.Services.Entities;
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
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
