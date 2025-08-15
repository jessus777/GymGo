using GymGo.Identity.Contexts;
using GymGo.Identity.Models;
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
            services.AddAuthentication(IdentityConstants.ApplicationScheme).AddApplicationCookie();

            services.AddAuthorizationBuilder();

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("IdentityConnectionString"));
            });

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddApiEndpoints();
        }
    }
}
