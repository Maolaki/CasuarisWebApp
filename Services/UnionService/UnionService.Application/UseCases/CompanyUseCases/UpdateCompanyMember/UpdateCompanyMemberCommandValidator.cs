using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class UpdateCompanyMemberCommandValidator : AbstractValidator<UpdateCompanyMemberCommand>
    {
        public UpdateCompanyMemberCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .NotNull().WithMessage("CompanyId must not be null.")
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.memberId)
                .NotNull().WithMessage("MemberId must not be null.")
                .GreaterThan(0).WithMessage("MemberId must be greater than 0.");

            RuleFor(x => x.salary)
                .GreaterThanOrEqualTo(0).WithMessage("Salary must not be negative.")
                .When(x => x.salary.HasValue);

            RuleFor(x => x.workHours)
                .InclusiveBetween(0, 24).WithMessage("WorkHours must be between 0 and 24.")
                .When(x => x.workHours.HasValue);

            RuleFor(x => x.workDays)
                .InclusiveBetween(0, 7).WithMessage("WorkDays must be between 0 and 7.")
                .When(x => x.workDays.HasValue);

            RuleFor(x => new { x.salary, x.workHours, x.workDays })
                .Must(values => values.salary.HasValue || values.workHours.HasValue || values.workDays.HasValue)
                .WithMessage("At least one of CompanyRole, Salary, WorkHours, or WorkDays must be provided.");
        }
    }
}
