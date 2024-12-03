using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class GetTeamsQueryValidator : AbstractValidator<GetTeamsQuery>
    {
        public GetTeamsQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username cannot be empty");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0");

            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }
}
