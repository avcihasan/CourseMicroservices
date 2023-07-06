using Course.Web.Models;
using IdentityModel.Client;
using System.Net.Http;

namespace Course.Web.Helpers
{
    public static class HelperMethods
    {
        public static async Task<DiscoveryDocumentResponse> GetDiscoveryDocumentAsync(HttpClient httpClient, ServiceApiSettings serviceApiSettings)
        {
            var disco = await httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy() { RequireHttps = false }
            });

            if (disco.IsError)
                throw disco.Exception;
            return disco;
        }
    }
}
