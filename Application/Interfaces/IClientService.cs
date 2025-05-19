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
        Task<int> CreateAsync(ClientCreateDto dto);
        Task UpdateAsync(int id, ClientUpdateDto dto);
        Task DeleteAsync(int id);
        Task<ClientDto?> GetByIdAsync(int id);
        Task<IEnumerable<ClientDto?>> GetAllAsync();
    }
}
