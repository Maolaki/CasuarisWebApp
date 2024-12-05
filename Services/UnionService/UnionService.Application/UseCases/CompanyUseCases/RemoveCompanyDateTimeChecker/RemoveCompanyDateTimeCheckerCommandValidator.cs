using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyDateTimeCheckerCommandValidator : AbstractValidator<RemoveCompanyDateTimeCheckerCommand>
    {
        public RemoveCompanyDateTimeCheckerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.dateTimeCheckerId)
                .GreaterThan(0).WithMessage("DateTimeCheckerId must be greater than 0.");
        }
    }
}
