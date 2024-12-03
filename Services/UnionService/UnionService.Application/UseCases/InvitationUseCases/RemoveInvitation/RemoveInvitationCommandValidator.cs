using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveInvitationCommandValidator : AbstractValidator<RemoveInvitationCommand>
    {
        public RemoveInvitationCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.InvitationId)
                .GreaterThan(0).WithMessage("InvitationId must be greater than 0.");
        }
    }
}
