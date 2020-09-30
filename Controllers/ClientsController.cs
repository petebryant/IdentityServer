using IdentityServer.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class ClientsController: Controller
    {
        readonly IClientRepository _clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAllAsync();
            return View(clients);
        }
    }
}
