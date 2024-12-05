using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveInvitationCommandValidator : AbstractValidator<RemoveInvitationCommand>
    {
        public RemoveInvitationCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.invitationId)
                .GreaterThan(0).WithMessage("InvitationId must be greater than 0.");

            RuleFor(x => x.answer)
                .NotNull().WithMessage("Answer should not be null.");
        }
    }
}
