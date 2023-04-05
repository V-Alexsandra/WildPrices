using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WildPrices.Data.Contexts.Contracts;
using WildPrices.Data.Contexts.Implementation;
using WildPrices.Data.Entities;
using WildPrices.Data.Repositories.Contracts;
using WildPrices.Data.Repositories.Implementation;

namespace WildPrices.WebApi.IoC
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection ConfigureIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
