using Application.DTOs;
using FluentValidation;


public class ClientUpdateDtoValidator : AbstractValidator<ClientUpdateDto>
{
    public ClientUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("El formato del correo electrónico no es válido.")
            .Matches(@"^[\w\.\-]+@([\w\-]+\.)+(com|net|org|edu|co|es)$")
            .WithMessage("El correo debe tener una extensión válida (com, net, org, etc).");

        RuleFor(x => x.BirthDate)
            .LessThan(DateTime.Today)
            .WithMessage("La fecha de nacimiento debe ser anterior a hoy.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^[0-9+\-\s\(\)]+$")
            .WithMessage("El teléfono solo debe contener números, espacios o signos (+, -, (), etc).");
    }
}
