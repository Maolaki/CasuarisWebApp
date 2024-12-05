using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.teamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

            RuleFor(x => x.name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.name));

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => new { x.name, x.description })
                .Must(x => !string.IsNullOrEmpty(x.name) || !string.IsNullOrEmpty(x.description))
                .WithMessage("Either Name or Description must be provided.");
        }
    }
}
