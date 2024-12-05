using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddCompanyDateTimeCheckerCommandValidator : AbstractValidator<AddCompanyDateTimeCheckerCommand>
    {
        public AddCompanyDateTimeCheckerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .NotNull().WithMessage("CompanyId should not be null.")
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.type)
                .NotNull().WithMessage("Type should not be null.")
                .IsInEnum().WithMessage("Type must be a valid DateTimeCheckerType.");

            RuleFor(x => x.address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address must not exceed 200 characters.");

            RuleFor(x => x.model)
                .NotEmpty().WithMessage("Model is required.")
                .MaximumLength(50).WithMessage("Model must not exceed 50 characters.");
        }
    }
}
