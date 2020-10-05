using IdentityServer.Stores;
using IdentityServer.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using IdentityServer.Models.ViewModels;
using IdentityModel;

namespace IdentityServer.Controllers
{
    public class ClientsController: Controller
    {
        private const string GENERIC_ERROR = "There was an error creating the Client record.";
        private const string GENERIC_SUCCESS = "The Client record was saved.";

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

                    return RedirectToAction(nameof(Index)).WithSuccess(GENERIC_SUCCESS);
                }

                return View(model).WithError(GENERIC_ERROR);
            }
            catch
            {
                return RedirectToAction(nameof(Index)).WithError(GENERIC_ERROR);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction(nameof(Index)).WithError(GENERIC_ERROR);

                var client = await _clientRepository.GetAsync(id);

                if (client == null)
                    return RedirectToAction(nameof(Index)).WithError(GENERIC_ERROR);

                var viewModel = new ClientViewModel(client);
                return View(viewModel).WithSuccess(GENERIC_SUCCESS);
            }
            catch
            {
                return RedirectToAction(nameof(Index)).WithError(GENERIC_ERROR);
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
                return RedirectToAction(nameof(Index)).WithError(GENERIC_ERROR);
            }
        }
    }
}
