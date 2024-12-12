using FluentValidation;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public class AddInvitationCommandValidator : AbstractValidator<AddInvitationCommand>
    {
        public AddInvitationCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.memberUsername)
                .NotEmpty().WithMessage("Member username should not be empty.");

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => x.type)
                .IsInEnum().WithMessage("Type must be a valid InvitationType.");

            RuleFor(x => x.role)
                .IsInEnum().WithMessage("Role must be enum.")
                .When(x => x.type == InvitationType.company);

            RuleFor(x => x.teamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.")
                .When(x => x.type == InvitationType.team);
        }
    }
}
