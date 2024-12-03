using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddCompanyCommandValidator : AbstractValidator<AddCompanyCommand>
    {
        public AddCompanyCommandValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(5).WithMessage("Name must have minimum 5 characters.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.LogoContentType)
                .Matches(@"^image/(jpeg|png)$").WithMessage("LogoContentType must be a valid image MIME type (e.g., image/jpeg, image/png).")
                .When(x => !string.IsNullOrEmpty(x.LogoContentType));

            //RuleFor(x => x.LogoData)
            //    .NotNull().WithMessage("LogoData is required when LogoContentType is provided.")
            //    .When(x => !string.IsNullOrEmpty(x.LogoContentType));
        }
    }
}
