using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddTeamMemberCommandValidator : AbstractValidator<AddTeamMemberCommand>
    {
        public AddTeamMemberCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");
        }
    }
}
