using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class ChangeResourcePositionCommandValidator : AbstractValidator<ChangeResourcePositionCommand>
    {
        public ChangeResourcePositionCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.taskInfoId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");

            RuleFor(x => x.resourceId)
                .GreaterThan(0).WithMessage("ResourceId must be greater than 0.");

            RuleFor(x => x.newPosition)
                .GreaterThanOrEqualTo(0).WithMessage("New position must be a valid index.");
        }
    }
}
