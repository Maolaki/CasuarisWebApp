using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
    {
        public UpdateTeamCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => new { x.Name, x.Description })
                .Must(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Description))
                .WithMessage("Either Name or Description must be provided.");
        }
    }
}
