using Course.Web.Models;
using Microsoft.Extensions.Options;

namespace Course.Web.Helpers
{
    public  class PhotoHelper
    {
        readonly ServiceApiSettings _serviceApiSettings;

        public PhotoHelper(IOptions<ServiceApiSettings> serviceApiSettings)
        {
            _serviceApiSettings = serviceApiSettings.Value;
        }
        public string GetPhotoUrl(string photoUrl)
            => $"{_serviceApiSettings.PhotoStockUri}/photos/{photoUrl}";
    }
}
