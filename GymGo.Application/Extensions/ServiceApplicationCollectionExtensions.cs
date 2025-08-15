using FluentValidation;
using GymGo.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;


namespace GymGo.Application.Extensions
{
    public static class ServiceApplicationCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IMyService, MyService>();
            services.AddAutoMapper(config =>
                config.AddMaps(AppDomain.CurrentDomain.GetAssemblies())
            );
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(config =>
            {
                //services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
                config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
                services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkTransactionBehavior<,>));
            });
            
            

            return services;
        }
    }
}
