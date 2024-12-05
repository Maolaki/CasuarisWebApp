using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class RemoveResourceCommandValidator : AbstractValidator<RemoveResourceCommand>
    {
        public RemoveResourceCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.resourceId)
                .GreaterThan(0).WithMessage("ResourceId must be greater than 0.");
        }
    }
}
