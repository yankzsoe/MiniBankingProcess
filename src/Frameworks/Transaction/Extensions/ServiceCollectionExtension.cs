using AutoMapper;
using Identity.Framework.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Transaction.Framework.Data;
using Transaction.Framework.Data.Interface;
using Transaction.Framework.Data.Repositories;
using Transaction.Framework.Mappers;
using Transaction.Framework.Services;
using Transaction.Framework.Services.Interface;

namespace Transaction.Framework.Extensions {
    public static class ServiceCollectionExtension {
        public static IServiceCollection AddTransactionFramework(this IServiceCollection services, IConfiguration configuration) {
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            // Service
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IReceiveMessageService, ReceiveMessageService>();

            // Repository
            services.AddScoped<IAccountSummaryRepository, AccountSummaryRepository>();
            services.AddScoped<IAccountTransactionRepository, AccountTransactionRepository>();

            // Mappers
            services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));

            // Connection String
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),
                    m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    )
                );

            return services;
        }
    }
}
