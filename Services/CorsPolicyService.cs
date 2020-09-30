using IdentityServer.Models.IdentityServer;
using IdentityServer.Stores;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Services
{
    public class CorsPolicyService : ICorsPolicyService
    {
        IClientRepository _clientRepository;
        ILogger<CorsPolicyService> _logger;
        public CorsPolicyService(IClientRepository clientRepository, ILogger<CorsPolicyService> logger)
        {
            _clientRepository = clientRepository;
            _logger = logger;
        }
        public async Task<bool> IsOriginAllowedAsync(string origin)
        {
            var clients = await _clientRepository.GetAllAsync();
            var result = clients.Any(c => c.AllowedCorsOrigins.Contains(origin, StringComparer.OrdinalIgnoreCase));

            var allowText = result ? "Allowing" : "Not allowing";
            _logger.LogInformation("CorsPolicyService {0} origin: {1}", allowText, origin);

            return result;
        }
    }
}
