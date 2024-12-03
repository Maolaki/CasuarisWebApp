using FluentValidation;
using TaskService.Application.UseCases;

public class GetAllTasksInfoQueryValidator : AbstractValidator<GetAllTasksInfoQuery>
{
    public GetAllTasksInfoQueryValidator()
    {
        RuleFor(query => query.username)
            .NotEmpty()
            .WithMessage("Username cannot be empty.");

        RuleFor(query => query.CompanyId)
            .GreaterThan(0)
            .WithMessage("CompanyId must be greater than 0.");
    }
}
