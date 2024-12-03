using FluentValidation;

namespace StatisticsService.Application.UseCases
{
    public class GetCompanyStatisticsQueryValidator : AbstractValidator<GetCompanyStatisticsQuery>
    {
        public GetCompanyStatisticsQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("StartDate must be before EndDate.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(x => x.StartDate.AddDays(7)).WithMessage("EndDate must be at least one week after StartDate.");
        }
    }
}
