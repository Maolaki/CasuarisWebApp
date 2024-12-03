using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class RemoveTaskCommandValidator : AbstractValidator<RemoveTaskCommand>
    {
        public RemoveTaskCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TaskId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");
        }
    }
}
