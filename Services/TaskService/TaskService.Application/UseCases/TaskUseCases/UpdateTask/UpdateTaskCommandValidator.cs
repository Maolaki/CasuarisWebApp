using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.taskId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");

            RuleFor(x => x.name)
                .MinimumLength(3).WithMessage("Name must have at least 3 characters.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.description));

            RuleFor(x => x.budget)
                .GreaterThanOrEqualTo(0).WithMessage("Budget must be 0 or greater.")
                .When(x => x.budget.HasValue);

            RuleFor(x => x.status)
                .IsInEnum().WithMessage("Invalid TaskStatus value.")
                .When(x => x.status.HasValue);

            RuleFor(x => new { x.name, x.description, x.budget, x.status })
                .Must(x => !string.IsNullOrEmpty(x.name) || !string.IsNullOrEmpty(x.description) || x.budget != null || x.status != null)
                .WithMessage("At least one of Name, Description, Budget or Status must be provided.");
        }
    }
}
