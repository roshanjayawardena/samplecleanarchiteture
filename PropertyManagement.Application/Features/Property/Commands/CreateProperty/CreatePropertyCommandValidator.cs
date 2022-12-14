using FluentValidation;

namespace PropertyManagement.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommandValidator : AbstractValidator<CreatePropertyCommand>
    {
        public CreatePropertyCommandValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{Name} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{Name} must not exceed 50 characters.");

            RuleFor(p => p.Address)
               .NotEmpty().WithMessage("{Address} is required.");

            RuleFor(p => p.ContactNo)
                .NotEmpty().WithMessage("{ContactNo} is required.");

        }

    }
}
