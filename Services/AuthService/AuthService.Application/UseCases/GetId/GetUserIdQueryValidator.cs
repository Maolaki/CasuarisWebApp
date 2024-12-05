using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class GetUserIdQueryValidator : AbstractValidator<GetUserIdQuery>
    {
        public GetUserIdQueryValidator()
        {
            RuleFor(x => x.claimsPrincipalIdentity)
                .NotNull()
                .WithMessage("ClaimsPrincipalIdentity cannot be null.");
        }
    }
}
