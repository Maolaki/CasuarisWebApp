using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
    {
        public UpdateCompanyCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.name)
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.name));

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => x.imageFile)
                .Must(file => file == null || file.Length <= 5000000)
                .WithMessage("If provided, image size must not exceed 5 MB.")
                .Must(file => file == null ||
                              file.ContentType.Equals("image/jpeg", StringComparison.OrdinalIgnoreCase) ||
                              file.ContentType.Equals("image/png", StringComparison.OrdinalIgnoreCase))
                .WithMessage("If provided, image must be of type JPEG or PNG.");

            RuleFor(x => new { x.name, x.description, x.imageFile })
                .Must(x => !string.IsNullOrEmpty(x.name) || !string.IsNullOrEmpty(x.description) || x.imageFile != null)
                .WithMessage("At least one of Name, Description, or LogoData must be provided.");
        }
    }
}
