using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            client.MarkAsDeleted();
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Cliente con ID {id} no encontrado.");
            }
            return client;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.AsNoTracking().ToListAsync();
        }
    }
}
