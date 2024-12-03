using FluentValidation;
using UnionService.Domain.Enums;

namespace UnionService.Application.UseCases
{
    public class AddInvitationCommandValidator : AbstractValidator<AddInvitationCommand>
    {
        public AddInvitationCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Type must be a valid InvitationType.");

            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("TeamId must be greater than 0.")
                .When(x => x.Type == InvitationType.Team);
        }
    }
}
