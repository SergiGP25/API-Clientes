using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Enums;
using FluentValidation;
namespace Application.Validators
{
    public class ClientCreateDtoValidator : AbstractValidator<ClientCreateDto>
    {
        public ClientCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100);

            RuleFor(x => x.IdentificationNumber)
                .NotEmpty()
                .Matches("^[0-9]+$")
                .WithMessage("El número de identificación solo debe contener dígitos.");

            RuleFor(x => x.IdentificationType)
                .Must(value => Enum.IsDefined(typeof(IdentificationType), value))
                .WithMessage("El tipo de identificación no es válido.");


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Matches(@"^[\w\.\-]+@([\w\-]+\.)+(com|net|org|edu|co|es)$")
                .WithMessage("El correo electrónico debe tener un formato válido y una extensión aceptada.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("La fecha de nacimiento debe ser anterior a hoy.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^[0-9+\-\s\(\)]+$")
                .WithMessage("El teléfono solo debe contener números, espacios o signos (+, -, etc).");
        }
    }
}
