using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class GetCompanyRoleQueryValidator : AbstractValidator<GetCompanyRoleQuery>
    {
        public GetCompanyRoleQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");
        }
    }
}
