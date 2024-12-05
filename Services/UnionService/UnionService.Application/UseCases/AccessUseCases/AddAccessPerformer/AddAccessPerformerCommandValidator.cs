using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddAccessPerformerCommandValidator : AbstractValidator<AddAccessPerformerCommand>
    {
        public AddAccessPerformerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .NotNull().WithMessage("CompanyId cannot be null.")
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.accessId)
                .NotNull().WithMessage("AccessId cannot be null.")
                .GreaterThan(0)
                .WithMessage("AccessId must be a positive integer.");

            RuleFor(x => x.userId)
                .NotNull().WithMessage("UserId cannot be null.")
                .GreaterThan(0)
                .WithMessage("UserId must be a positive integer.");
        }
    }
}
