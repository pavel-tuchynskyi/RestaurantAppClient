using Microsoft.Extensions.Configuration;
using RestaurantAppClient.Common;
using RestaurantAppClient.Common.Interfaces;

namespace RestaurantAppClient.Services
{
    public class ClientFactory : IClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private ApiClient _apiClient;

        public ClientFactory(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public ApiClient CreateClient()
        {
            if(_apiClient != null)
            {
                return _apiClient;
            }

            var backEndUrl = _configuration["BackendApi"];

            if(backEndUrl is null)
            {
                throw new Exception("Backend url is not configured");
            }

            return _apiClient = new ApiClient(backEndUrl, _tokenService);
        }
    }
}
