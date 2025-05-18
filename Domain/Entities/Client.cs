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
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string IdentificationNumber { get; private set; }
        public IdentificationType IdentificationType { get; private set; }
        public string Email { get; private set; }
        public DateTime BirthDate { get; private set; }
        public string Phone { get; private set; }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }

        public int Age => DateTime.Today.Year - BirthDate.Year -
                         (DateTime.Today.DayOfYear < BirthDate.DayOfYear ? 1 : 0);

        public Client(string name, string identificationNumber, IdentificationType identificationType, string email, DateTime birthDate, string phone)
        {
            Name = name;
            IdentificationNumber = identificationNumber;
            IdentificationType = identificationType;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            IsDeleted = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Update(string name, string email, DateTime birthDate, string phone)
        {
            Name = name;
            Email = email;
            BirthDate = birthDate;
            Phone = phone;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
