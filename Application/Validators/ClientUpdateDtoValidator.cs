using Application.DTOs;
using FluentValidation;


public class ClientUpdateDtoValidator : AbstractValidator<ClientUpdateDto>
{
    public ClientUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name is required.")
            .MaximumLength(100).WithMessage("The name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The e-mail address is required.")
            .EmailAddress().WithMessage("The e-mail format is not valid.")
            .Matches(@"^[\w\.\-]+@([\w\-]+\.)+(com|net|org|edu|co|es)$")
            .WithMessage("The email must have a valid extension (com, net, org, etc).");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today)
            .WithMessage("The date of birth must be before today.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("The telephone number is required.")
            .Matches(@"^[0-9+\-\s\(\)]+$")
            .WithMessage("The phone should only contain numbers, spaces or signs (+, -, (), etc).");
    }
}
