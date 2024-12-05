using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveAccessPerformerCommandValidator : AbstractValidator<RemoveAccessPerformerCommand>
    {
        public RemoveAccessPerformerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .NotNull().WithMessage("CompanyId cannot be null.")
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(command => command.accessId)
                .NotNull().WithMessage("AccessId cannot be null.")
                .GreaterThan(0)
                .WithMessage("AccessId must be a positive integer.");

            RuleFor(command => command.userId)
                .NotNull().WithMessage("UserId cannot be null.")
                .GreaterThan(0)
                .WithMessage("UserId must be a positive integer.");
        }
    }
}
