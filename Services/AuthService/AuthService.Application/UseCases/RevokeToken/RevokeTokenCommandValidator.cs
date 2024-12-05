using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
    {
        public RevokeTokenCommandValidator()
        {
            RuleFor(x => x.refreshToken)
                .NotEmpty()
                .WithMessage("RefreshToken cannot be null or empty.");

            RuleFor(x => x.user)
                .NotNull()
                .WithMessage("User cannot be null.");
        }
    }
}
