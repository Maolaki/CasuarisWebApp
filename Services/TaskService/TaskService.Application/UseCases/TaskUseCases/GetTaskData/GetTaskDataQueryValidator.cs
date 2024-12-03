using FluentValidation;

namespace TaskService.Application.UseCases
{
    public class GetTaskDataQueryValidator : AbstractValidator<GetTaskDataQuery>
    {
        public GetTaskDataQueryValidator()
        {
            RuleFor(query => query.username)
                .NotEmpty()
                .WithMessage("Username cannot be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TaskId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");
        }
    }
}
