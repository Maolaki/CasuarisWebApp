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

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.taskInfoId)
                .GreaterThan(0).WithMessage("TaskId must be greater than 0.");

            RuleFor(x => x.resourceData)
                .NotNull().WithMessage("Resource data must not be null.")
                .When(x => x.type == ResourceType.text);

            RuleFor(x => x.imageFile)
                .NotEmpty().WithMessage("ImageFile bytes must not be empty.")
                .When(x => x.type == ResourceType.image || x.type == ResourceType.video);

            RuleFor(x => x.type)
                .IsInEnum().WithMessage("Resource type is invalid.");
        }
    }
}
