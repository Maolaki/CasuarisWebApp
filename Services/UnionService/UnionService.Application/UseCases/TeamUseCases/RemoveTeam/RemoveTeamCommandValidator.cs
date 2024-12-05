using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveTeamCommandValidator : AbstractValidator<RemoveTeamCommand>
    {
        public RemoveTeamCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.teamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.");
        }
    }
}
