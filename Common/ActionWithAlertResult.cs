using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Common
{
    public class ActionWithAlertResult : IActionResult
    {
        private readonly IActionResult _internalResult;
        private readonly AlertType _type;
        private readonly string _message;

        public ActionWithAlertResult(IActionResult internalResult, AlertType type, string message)
        {
            _internalResult = internalResult;
            _type = type;
            _message = message;
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            await _internalResult.ExecuteResultAsync(context);

            var alerts = new List<ApplicationAlert>(); 
            alerts.Add(new ApplicationAlert(_type, _message));

            context.HttpContext.SetAlerts(alerts);
        }
    }
}