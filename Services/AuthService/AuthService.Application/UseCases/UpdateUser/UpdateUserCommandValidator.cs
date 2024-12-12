using FluentValidation;

namespace AuthService.Application.UseCases
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Current username should not be empty.");

            RuleFor(x => x.newUsername)
                .MaximumLength(20).WithMessage("New username cannot exceed 20 characters.")
                .MinimumLength(3).WithMessage("New username must be at least 3 characters long.")
                .When(x => !string.IsNullOrEmpty(x.newUsername))
                .WithMessage("New username must be provided if specified.");

            RuleFor(x => x.newEmail)
                .EmailAddress().WithMessage("New email is not in a valid format.")
                .When(x => !string.IsNullOrEmpty(x.newEmail));

            RuleFor(x => new { x.newUsername, x.newEmail })
                .Must(x => !string.IsNullOrEmpty(x.newUsername) || !string.IsNullOrEmpty(x.newEmail))
                .WithMessage("At least one of newUsername or newEmail must be provided.");
        }
    }
}
