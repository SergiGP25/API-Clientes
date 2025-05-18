using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientCreateDto
    {
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public int IdentificationType { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public ClientCreateDto()
        {
            Name = string.Empty;
            IdentificationNumber = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }
    }

}
