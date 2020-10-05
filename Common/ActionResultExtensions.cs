using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Common
{
    public static class ActionResultExtensions
    {
        public static IActionResult WithError(this IActionResult inner, string message)
        {
            return new ActionWithAlertResult(inner, AlertType.Error, message);
        }

        public static IActionResult WithSuccess(this IActionResult inner, string message)
        {
            return new ActionWithAlertResult(inner, AlertType.Success, message);
        }

        public static IActionResult WithWarning(this IActionResult inner, string message)
        {
            return new ActionWithAlertResult(inner, AlertType.Warning, message);
        }

        public static IActionResult WithInfo(this IActionResult inner, string message)
        {
            return new ActionWithAlertResult(inner, AlertType.Info, message);
        }
    }
}