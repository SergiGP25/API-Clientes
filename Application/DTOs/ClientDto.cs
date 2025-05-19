using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationType { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ClientDto()
        {
            Name = string.Empty;
            IdentificationNumber = string.Empty;
            IdentificationType = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }
}
