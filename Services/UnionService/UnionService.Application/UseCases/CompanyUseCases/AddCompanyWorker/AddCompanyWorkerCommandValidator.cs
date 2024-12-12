using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class AddCompanyWorkerCommandValidator : AbstractValidator<AddCompanyWorkerCommand>
    {
        public AddCompanyWorkerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.memberUsername)
                .NotEmpty().WithMessage("Member username should not be empty.");

            RuleFor(x => x.role)
                .IsInEnum().WithMessage("Invalid role. Allowed roles: Owner, Manager, Performer.");
        }
    }
}
