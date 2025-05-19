
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces;

namespace Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> CreateAsync(ClientCreateDto dto)
        {
            var client = new Client(
                dto.Name,
                dto.IdentificationNumber,
                (IdentificationType)dto.IdentificationType,
                dto.Email,
                dto.BirthDate,
                dto.Phone
            );

            await _repository.AddAsync(client);
            return client.Id;
        }

        public async Task UpdateAsync(int id, ClientUpdateDto dto)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) throw new DomainException("Cliente no encontrado.");

            client.Update(dto.Name, dto.Email, dto.BirthDate, dto.Phone);
            await _repository.UpdateAsync(client);
        }

        public async Task DeleteAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) throw new DomainException("Cliente no encontrado.");

            await _repository.DeleteAsync(client);
        }

        public async Task<ClientDto?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            return ToDto(client);
        }

        public async Task<IEnumerable<ClientDto?>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            return clients.Select(ToDto).ToList();
        }

        private static ClientDto? ToDto(Client? client)
        {
            if (client == null)
            {
                return null;
            }

            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                IdentificationNumber = client.IdentificationNumber,
                IdentificationType = client.IdentificationType.ToString(),
                Email = client.Email,
                Age = client.Age,
                Phone = client.Phone,
                CreatedAt = client.CreatedAt,
                UpdatedAt = client.UpdatedAt
            };
        }
    }
}
