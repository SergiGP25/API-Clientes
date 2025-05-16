using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;

namespace Application.Interfaces
{
    public interface IClientService
    {
        Task<Guid> CreateAsync(ClientCreateDto dto);
        Task UpdateAsync(Guid id, ClientUpdateDto dto);
        Task DeleteAsync(Guid id);
        Task<ClientDto> GetByIdAsync(Guid id);
        Task<IEnumerable<ClientDto>> GetAllAsync();
    }
}
