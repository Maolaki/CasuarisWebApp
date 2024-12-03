using FluentValidation;
using MediatR;
using AuthService.Application.Interfaces;
using AuthService.Application.Profiles;
using AuthService.Application.UseCases;
using AuthService.Application.Behaviors;
using AuthService.API.Filters;
using AuthService.Domain.Interfaces;
using AuthService.Infrastructure;
using AuthService.Infrastructure.Entities;
using AuthService.Infrastructure.Services;

namespace AuthService
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AuthenticateUserHandler).Assembly));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddValidatorsFromAssembly(typeof(AuthenticateUserQueryValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<EnsureAuthenticatedUserFilter>();
        }
    }
}