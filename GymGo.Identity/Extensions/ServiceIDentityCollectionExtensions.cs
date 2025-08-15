using GymGo.Application.Contracts.Identity;
using GymGo.Identity.Contexts;
using GymGo.Identity.Models;
using GymGo.Identity.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymGo.Identity.Extensions
{
    public static class ServiceIDentityCollectionExtensions
    {
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication(IdentityConstants.ApplicationScheme).AddApplicationCookie();

            //services.AddAuthorizationBuilder();

            //services.AddDbContext<IdentityDbContext>(options =>
            //{
            //    options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"));
            //});
            services.AddDbContextFactory<IdentityDbContext>((sp, options) =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"));
            }, ServiceLifetime.Scoped); // <- clave

            //services.AddIdentityCore<ApplicationUser>()
            //    .AddEntityFrameworkStores<IdentityDbContext>()
            //    .AddApiEndpoints();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IUnitOfWorkIdentityFactory, UnitOfWorkIdentityFactory>();
            services.AddScoped<IIdentityUnitOfWork>(sp => sp.GetRequiredService<IUnitOfWorkIdentityFactory>().Create());
        }
    }
}
