using FluentValidation;
using EchoApi.Models;

namespace EchoApi.Validators;

public class EchoRequestValidator : AbstractValidator<EchoRequest>
{
    public EchoRequestValidator()
    {
        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text cannot be empty")
            .MaximumLength(500)
            .WithMessage("Text cannot exceed 500 characters");
    }
}