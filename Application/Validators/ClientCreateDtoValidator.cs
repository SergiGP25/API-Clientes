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
                .NotEmpty().WithMessage("The name is required.")
                .MaximumLength(100);

            RuleFor(x => x.IdentificationNumber)
                .NotEmpty()
                .Matches("^[0-9]+$")
                .WithMessage("The identification number must contain only numbers.");

            RuleFor(x => x.IdentificationType)
                .Must(value => Enum.IsDefined(typeof(IdentificationType), value))
                .WithMessage("The type of identification is not valid.");


            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .Matches(@"^[\w\.\-]+@([\w\-]+\.)+(com|net|org|edu|co|es)$")
                .WithMessage("The e-mail must have a valid format and an accepted extension.");

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.Today)
                .WithMessage("The date of birth must be before today.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .Matches(@"^[0-9+\-\s\(\)]+$")
                .WithMessage("The phone should only contain numbers, spaces or signs (+, -, etc).");
        }
    }
}
