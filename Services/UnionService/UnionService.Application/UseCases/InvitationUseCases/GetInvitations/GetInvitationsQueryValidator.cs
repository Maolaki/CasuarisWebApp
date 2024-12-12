using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class GetInvitationsQueryValidator : AbstractValidator<GetInvitationsQuery>
    {
        public GetInvitationsQueryValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.pageNumber)
                .GreaterThan(0).WithMessage("PageNumber must be greater than 0");

            RuleFor(x => x.pageSize)
                .GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }
}
