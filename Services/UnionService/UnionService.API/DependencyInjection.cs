using FluentValidation;
using MediatR;
using UnionService.API.Filters;
using UnionService.Application.Behaviors;
using UnionService.Application.Profiles;
using UnionService.Application.Services;
using UnionService.Application.UseCases;
using UnionService.Domain.Interfaces;
using UnionService.Infrastructure;

namespace UnionService
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AddCompanyHandler).Assembly));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccessService, AccessService>();

            services.AddAutoMapper(typeof(TeamToTeamDTOProfile).Assembly);

            services.AddValidatorsFromAssembly(typeof(AddCompanyCommandValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<EnsureAuthenticatedUserFilter>();
        }
    }
}