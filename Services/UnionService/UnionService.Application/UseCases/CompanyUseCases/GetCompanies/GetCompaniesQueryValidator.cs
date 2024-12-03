using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class GetCompaniesQueryValidator : AbstractValidator<GetCompaniesQuery>
    {
        public GetCompaniesQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }
}
