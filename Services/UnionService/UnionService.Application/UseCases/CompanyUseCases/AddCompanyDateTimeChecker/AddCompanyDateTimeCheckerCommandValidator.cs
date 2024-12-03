using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddCompanyDateTimeCheckerCommandValidator : AbstractValidator<AddCompanyDateTimeCheckerCommand>
    {
        public AddCompanyDateTimeCheckerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Type must be a valid DateTimeCheckerType.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");
        }
    }
}
