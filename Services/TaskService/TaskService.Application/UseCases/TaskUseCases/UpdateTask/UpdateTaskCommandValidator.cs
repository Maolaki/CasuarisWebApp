using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TaskId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");

            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name must have at least 3 characters.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Budget)
                .GreaterThanOrEqualTo(0).WithMessage("Budget must be 0 or greater.")
                .When(x => x.Budget.HasValue);

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid TaskStatus value.")
                .When(x => x.Status.HasValue);
        }
    }
}
