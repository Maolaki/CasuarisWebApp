using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class GetCompanyQueryValidator : AbstractValidator<GetCompanyQuery>
    {
        public GetCompanyQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0");
        }
    }
}
