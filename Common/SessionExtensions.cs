using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Newtonsoft.Json;  
using System;

namespace IdentityServer.Common
{
    public static class SessionExtensions
    {
        private  const string KEY = "alerts";

        public static List<ApplicationAlert> GetAlerts(this HttpContext context)
        {
            var exAlerts = context.Session.GetString(KEY);

            if (!string.IsNullOrEmpty(exAlerts))
            {
                var alerts = JsonConvert.DeserializeObject<List<ApplicationAlert>>(exAlerts);
                context.Session.SetString(KEY, string.Empty);

                return alerts;
            }

            return new List<ApplicationAlert>();
        }

        public static void SetAlerts(this HttpContext context, List<ApplicationAlert> alerts)
        {
            if (alerts == null)
                throw new ArgumentNullException(nameof(alerts));

            context.Session.SetString(KEY, JsonConvert.SerializeObject(alerts));
        }
    }
}