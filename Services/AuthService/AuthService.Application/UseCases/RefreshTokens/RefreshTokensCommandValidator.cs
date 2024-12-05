using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class RefreshTokensCommandValidator : AbstractValidator<RefreshTokensCommand>
    {
        public RefreshTokensCommandValidator()
        {
            RuleFor(x => x.accessToken)
                .NotEmpty()
                .WithMessage("Access token is required.");

            RuleFor(x => x.refreshToken)
                .NotEmpty()
                .WithMessage("Refresh token is required.");
        }
    }
}
