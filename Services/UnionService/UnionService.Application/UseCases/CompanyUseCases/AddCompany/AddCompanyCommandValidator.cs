using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Name must have minimum 5 characters.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.description)
                .NotNull().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => x.imageFile)
                .Must(file => file == null || file.Length <= 5000000)
                .WithMessage("If provided, image size must not exceed 5 MB.")
                .Must(file => file == null ||
                              file.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                              file.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase))
                .WithMessage("If provided, image must be of type JPEG or PNG.");
        }
    }
}
