using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.Name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.Name));

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ImageFile)
                .Must(file => file == null || file.Length <= 5000000)
                .WithMessage("If provided, image size must not exceed 5 MB.")
                .Must(file => file == null ||
                              file.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                              file.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase))
                .WithMessage("If provided, image must be of type JPEG or PNG.");

            RuleFor(x => new { x.Name, x.Description, x.ImageFile })
                .Must(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Description) || x.ImageFile != null)
                .WithMessage("At least one of Name, Description, or LogoData must be provided.");
        }
    }
}
