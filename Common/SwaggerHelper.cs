using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace IdentityServer.Common
{
    public static class SwaggerHelper
    {
        internal static IConfiguration Configuration { get; set; }

        internal static void ConfigureSwaggerGen(SwaggerGenOptions swaggerGenOptions)
        {
            var webApiAssembly = Assembly.GetEntryAssembly();

            AddSwaggerDocPerVersion(swaggerGenOptions, webApiAssembly);
            ApplyDocInclusions(swaggerGenOptions);
            IncludeXmlComments(swaggerGenOptions, webApiAssembly);

            //TODO need authentication here
        }

        private static void IncludeXmlComments(SwaggerGenOptions swaggerGenOptions, Assembly webApiAssembly)
        {
            var xmlFile = $"{webApiAssembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            swaggerGenOptions.IncludeXmlComments(xmlPath);
        }

        private static void ApplyDocInclusions(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.DocInclusionPredicate((docName, apiDesc) =>
            {
                var versions = apiDesc.CustomAttributes()
                            .OfType<ApiVersionAttribute>()
                            .SelectMany(a => a.Versions);

                return versions.Any(v => $"v{v.ToString()}" == docName);
            });
        }

        private static void AddSwaggerDocPerVersion(SwaggerGenOptions swaggerGenOptions, Assembly webApiAssembly)
        {
            var apiVersions = GetApiVersions(webApiAssembly);

            foreach(var apiVersion in apiVersions)
            {
                swaggerGenOptions.SwaggerDoc($"v{apiVersion}", new Microsoft.OpenApi.Models.OpenApiInfo
                { 
                       Version = $"v{apiVersion}",
                       Title = "Identity Server",
                       Description ="RESTful Services for Identity"
                });
            };
        }

        private static IEnumerable<string> GetApiVersions(Assembly webApiAssembly)
        {
            var apiVersions = webApiAssembly.DefinedTypes
                .Where(x => (x.IsSubclassOf(typeof(ControllerBase)) &&
                    x.GetCustomAttributes<ApiVersionAttribute>().Any()))
                .Select(x => x.GetCustomAttribute<ApiVersionAttribute>())
                .SelectMany(x => x.Versions)
                .Distinct()
                .OrderBy(x => x);

            return apiVersions.Select(x => x.ToString());
        }

        internal static void ConfigureSwaggerUI(SwaggerUIOptions swaggerUIOptions)
        {
            var webApiAssembly = Assembly.GetEntryAssembly();
            var apiVersions = GetApiVersions(webApiAssembly);

            foreach (var apiVersion in apiVersions)
            {
                swaggerUIOptions.SwaggerEndpoint($"/swagger/v{apiVersion}/swagger.json", $"{apiVersion} Docs");
            };

            //TODO do the authentication here...

            swaggerUIOptions.RoutePrefix = "swagger";
        }
    }
}
