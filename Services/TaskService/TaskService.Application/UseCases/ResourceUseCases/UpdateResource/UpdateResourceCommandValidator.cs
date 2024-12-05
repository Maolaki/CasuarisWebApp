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

            RuleFor(x => x.companyId)
                .GreaterThan(0).WithMessage("CompanyId must be greater than 0.");

            RuleFor(x => x.resourceId)
                .GreaterThan(0).WithMessage("ResourceId must be greater than 0.");

            RuleFor(x => x.type)
                .IsInEnum().WithMessage("Invalid Resource Type.");

            RuleFor(x => x.data)
                .NotEmpty().WithMessage("Data must be provided for text resource.")
                .When(x => x.type == ResourceType.text);

            RuleFor(x => x.imageFile)
                .NotEmpty().WithMessage("ImageFile should not be empty.")
                .When(x => x.type == ResourceType.image || x.type == ResourceType.video);
        }
    }
}
