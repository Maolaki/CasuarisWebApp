using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class GetUserInfoQueryValidator : AbstractValidator<GetUserInfoQuery>
    {
        public GetUserInfoQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters long.")
                .MaximumLength(20)
                .WithMessage("Username cannot exceed 20 characters.");
        }
    }
}
