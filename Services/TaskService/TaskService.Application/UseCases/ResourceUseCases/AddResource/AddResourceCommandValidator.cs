using FluentValidation;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public class AddResourceCommandValidator : AbstractValidator<AddResourceCommand>
    {
        public AddResourceCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.TaskInfoId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");

            RuleFor(x => x.ResourceData)
                .NotEmpty().WithMessage("Resource data must not be empty.")
                .When(x => x.Type == ResourceType.text);

            RuleFor(x => x.ResourceDataBytes)
                .NotNull().WithMessage("Resource data bytes must not be null.")
                .When(x => x.Type == ResourceType.image || x.Type == ResourceType.video);

            RuleFor(x => x.ContentType)
                .NotNull().WithMessage("Content type must not by empty.")
                .When(x => x.Type == ResourceType.image || x.Type == ResourceType.video);

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Resource type is invalid.");
        }
    }
}
