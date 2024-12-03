using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddAccessPerformerCommandValidator : AbstractValidator<AddAccessPerformerCommand>
    {
        public AddAccessPerformerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(command => command.AccessId)
                .GreaterThan(0)
                .WithMessage("AccessId must be a positive integer.");

            RuleFor(command => command.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be a positive integer.");
        }
    }
}
