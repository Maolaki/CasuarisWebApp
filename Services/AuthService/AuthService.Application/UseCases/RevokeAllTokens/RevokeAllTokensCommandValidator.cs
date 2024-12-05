using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class RevokeAllTokensCommandValidator : AbstractValidator<RevokeAllTokensCommand>
    {
        public RevokeAllTokensCommandValidator()
        {
            RuleFor(x => x.user)
                .NotNull()
                .WithMessage("User cannot be null.");
        }
    }
}
