using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddTeamCommandValidator : AbstractValidator<AddTeamCommand>
    {
        public AddTeamCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.description)
                .NotNull().WithMessage("Description should not be empty.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");
        }
    }
}
