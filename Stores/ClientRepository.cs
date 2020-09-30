using IdentityServer.Data;
using IdentityServer.Models.IdentityServer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Stores
{
    public class ClientRepository: IClientRepository
    {
        readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task ActivateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(ClientEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task DeactivateAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ClientEntity>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public Task<ClientEntity> GetAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string id, ClientEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
