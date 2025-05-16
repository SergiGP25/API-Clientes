using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdentificationType { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string PhoneNumber { get; set; }
    }
}
