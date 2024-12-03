using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyCommandValidator : AbstractValidator<RemoveCompanyCommand>
    {
        public RemoveCompanyCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");
        }
    }
}
