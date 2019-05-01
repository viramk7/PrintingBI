using Microsoft.Extensions.DependencyInjection;
using PrintingBI.Data;
using PrintingBI.Data.Repositories.Author;
using PrintingBI.Data.Repositories.Generic;
using PrintingBI.Services.Author;
using PrintingBI.Services.Entities;

namespace PrintingBI.API
{
    public static  class DependencyRegistrar
    {
        public static void Resolve(IServiceCollection services)
        {
            // Infrastructure
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            //services.AddScoped<IDbContext, PrintingBIDbContext>();

            // Repositories
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            
            // Services
            services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));
            services.AddScoped<IAuthorService, AuthorService>();

        }
    }
}
