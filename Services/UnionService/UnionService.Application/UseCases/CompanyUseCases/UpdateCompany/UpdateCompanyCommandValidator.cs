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

            RuleFor(x => x.LogoContentType)
                .Matches(@"^image/(jpeg|png|gif)$").WithMessage("LogoContentType must be a valid image MIME type (e.g., image/jpeg, image/png, image/gif).")
                .When(x => !string.IsNullOrEmpty(x.LogoContentType));

            RuleFor(x => x.LogoData)
                .NotNull().WithMessage("LogoData is required when LogoContentType is provided.")
                .When(x => !string.IsNullOrEmpty(x.LogoContentType));

            RuleFor(x => new { x.Name, x.Description, x.LogoData })
                .Must(x => !string.IsNullOrEmpty(x.Name) || !string.IsNullOrEmpty(x.Description) || x.LogoData != null)
                .WithMessage("At least one of Name, Description, or LogoData must be provided.");
        }
    }
}
