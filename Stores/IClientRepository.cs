using IdentityServer.Models.IdentityServer;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Stores
{
    public interface IClientRepository
    {
        Task AddAsync(ClientEntity entity);
        Task ActivateAsync(string id);
        Task DeactivateAsync(string id);
        Task UpdateAsync(string id, ClientEntity entity);
        Task<IEnumerable<ClientEntity>> GetAllAsync();
        Task<ClientEntity> GetAsync(string id);
        Task<ClientEntity> GetAsync(Func<ClientEntity, bool> predicate);
        Task RemoveAsync(string id);
    }
}
