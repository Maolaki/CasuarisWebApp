using FluentValidation;

namespace StatisticsService.Application.UseCases
{
    public class GetCompanyStatisticsQueryValidator : AbstractValidator<GetCompanyStatisticsQuery>
    {
        public GetCompanyStatisticsQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.startDate)
                .LessThan(x => x.endDate).WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.endDate)
                .Must((x, endDate) => !endDate.HasValue || (x.startDate.HasValue && endDate >= x.startDate.Value.AddDays(7)))
                .WithMessage("EndDate must be at least one week after StartDate.");
        }
    }
}
