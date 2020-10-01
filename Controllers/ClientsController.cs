using IdentityServer.Stores;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityServer.Models.ViewModels;
using IdentityModel;

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

        public IActionResult New()
        {
            //TODO need to get Resources
            var viewModel = new ClientViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> New(ClientViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.Secret = model.Secret.ToSha256();
                    await _clientRepository.AddAsync(model.MapToModel());
                    //TODO need to add extension .WithSuccess
                    return RedirectToAction(nameof(Index));
                }

                //TODO need to add .WithError
                return View(model);
            }
            catch
            {
                return RedirectToAction(nameof(Index)); 
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction(nameof(Index));

                var client = await _clientRepository.GetAsync(id);

                if (client == null)
                    return RedirectToAction(nameof(Index));

                var viewModel = new ClientViewModel(client);
                return View(viewModel);
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> UniqueClientName(string ClientName, string ClientId)
        {
            try{
                if (string.IsNullOrEmpty(ClientId))
                {
                    var result = await _clientRepository.GetAsync(c => c.ClientName.ToLower() == ClientName.ToLower());
                    return Json(result == null);
                }

                return Json(true);
            }
            catch {
                //TODO need to add .WithError extension
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
