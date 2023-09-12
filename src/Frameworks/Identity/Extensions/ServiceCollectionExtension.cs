using Identity.Framework.Data;
using Identity.Framework.Data.Interface;
using Identity.Framework.Data.Repositories;
using Identity.Framework.Helpers;
using Identity.Framework.Mappers;
using Identity.Framework.Services;
using Identity.Framework.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Identity.Framework.Extensions {
    public static class ServiceCollectionExtension {
        public static IServiceCollection AddIdentityFramework(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // Services
            services.AddScoped<IUserCredentialService, UserCredentialService>();
            services.AddScoped<ISendMessageQueueService, SendMessageQueueService>();

            // Repository
            services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();

            // Mappers
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    )
                );

            return services;
        }
    }
}
