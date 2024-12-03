using FluentValidation;
using TaskService.Domain.Enums;

namespace TaskService.Application.UseCases
{
    public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
    {
        public UpdateResourceCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username should not be empty.");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.ResourceId)
                .GreaterThan(0).WithMessage("ResourceId must be greater than 0.");

            RuleFor(x => x.Type)
                .IsInEnum().WithMessage("Invalid Resource Type.");

            RuleFor(x => x.ContentType)
                .NotNull().WithMessage("Content type must not by empty.")
                .When(x => x.Type == ResourceType.image || x.Type == ResourceType.video);

            RuleFor(x => x.Data)
                .NotEmpty().WithMessage("Data must be provided for text resource.")
                .When(x => x.Type == ResourceType.text);

            RuleFor(x => x.DataBytes)
                .NotNull().WithMessage("DataBytes must be provided for image or video resource.")
                .When(x => x.Type == ResourceType.image || x.Type == ResourceType.video);
        }
    }
}
