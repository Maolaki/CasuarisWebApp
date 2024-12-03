using FluentValidation;

namespace UnionService.Application.UseCases
{
    public class RemoveCompanyWorkerCommandValidator : AbstractValidator<RemoveCompanyWorkerCommand>
    {
        public RemoveCompanyWorkerCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.Role)
                .NotEmpty().WithMessage("Role is required.")
                .Must(BeAValidRole).WithMessage("Invalid role. Allowed roles: Owner, Manager, Performer.");
        }

        private bool BeAValidRole(string role)
        {
            var allowedRoles = new[] { "Owner", "Manager", "Performer" };
            return allowedRoles.Contains(role);
        }
    }
}
