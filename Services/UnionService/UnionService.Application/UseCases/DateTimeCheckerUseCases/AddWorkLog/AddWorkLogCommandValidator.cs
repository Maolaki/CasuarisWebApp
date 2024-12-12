using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddWorkLogCommandValidator : AbstractValidator<AddWorkLogCommand>
    {
        public AddWorkLogCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty()
                .WithMessage("Username is required.");

            RuleFor(x => x.companyId)
                .GreaterThan(0)
                .WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.workDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("WorkDate cannot be in the future.");

            RuleFor(x => x.hoursWorked)
                .GreaterThan(TimeSpan.Zero)
                .WithMessage("HoursWorked must be greater than zero.");
        }
    }
}
