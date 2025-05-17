using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Exceptions;

namespace Domain.Entities
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private set; } 

        public string FullName { get; private set; }
        public string IdentificationNumber { get; private set; }
        public IdentificationType IdentificationType { get; private set; }
        public string Email { get; private set; }
        public int Age { get; private set; }
        public string PhoneNumber { get; private set; }

        public Client(string fullName, string identificationNumber, IdentificationType identificationType, string email, int age, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new DomainException("El nombre es requerido.");
            if (string.IsNullOrWhiteSpace(identificationNumber)) throw new DomainException("El número de identificación es requerido.");
            if (string.IsNullOrWhiteSpace(email)) throw new DomainException("El correo electrónico es requerido.");
            if (age < 0 || age > 99) throw new DomainException("La edad no es válida.");
            if (string.IsNullOrWhiteSpace(phoneNumber)) throw new DomainException("El número de teléfono es requerido.");

            FullName = fullName;
            IdentificationNumber = identificationNumber;
            IdentificationType = identificationType;
            Email = email;
            Age = age;
            PhoneNumber = phoneNumber;
        }

        public void Update(string fullName, string email, int age, string phoneNumber)
        {
            if (!string.IsNullOrWhiteSpace(fullName)) FullName = fullName;
            if (!string.IsNullOrWhiteSpace(email)) Email = email;
            if (age > 0 && age < 99) Age = age;
            if (!string.IsNullOrWhiteSpace(phoneNumber)) PhoneNumber = phoneNumber;
        }
    }
}
