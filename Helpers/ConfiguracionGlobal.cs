using System;
using Microsoft.AspNetCore.Http;

namespace EchoBotWithCounter.Helpers
{
    public class ConfiguracionGlobal : IConfiguracionGlobal
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ConfiguracionGlobal(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;

            UriBuilder uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Path = request.Path.ToString(),
                Port = request.Host.Port.Value,
                Query = request.QueryString.ToString(),
            };
            return uriBuilder.Uri;
        }
    }
}
