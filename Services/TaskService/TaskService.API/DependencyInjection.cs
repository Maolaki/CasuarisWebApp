using FluentValidation;
using MediatR;
using TaskService.API.Filters;
using TaskService.Application.Behaviors;
using TaskService.Application.Profiles;
using TaskService.Application.Services;
using TaskService.Application.UseCases;
using TaskService.Domain.Interfaces;
using TaskService.Infrastructure;

namespace TaskService
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddResourceCommand).Assembly));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccessService, AccessService>();

            services.AddAutoMapper(typeof(ResourceToResourceDTOProfile).Assembly);

            services.AddValidatorsFromAssembly(typeof(AddResourceCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<EnsureAuthenticatedUserFilter>();
        }
    }
}