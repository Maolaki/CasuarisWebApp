using FluentValidation;
using MediatR;
using StatisticsService.API.Filters;
using StatisticsService.Application.Behaviors;
using StatisticsService.Application.Services;
using StatisticsService.Application.UseCases;
using StatisticsService.Domain.Interfaces;
using StatisticsService.Infrastructure;

namespace StatisticsService
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjectionServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetCompanyStatisticsHandler).Assembly));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccessService, AccessService>();

            services.AddValidatorsFromAssembly(typeof(GetCompanyStatisticsQueryValidator).Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddScoped<EnsureAuthenticatedUserFilter>();
        }
    }
}