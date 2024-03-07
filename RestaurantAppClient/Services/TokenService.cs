using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models.Account;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace RestaurantAppClient.Services
{
    public class TokenService : ITokenService
    {
        private readonly IAlertService _alertService;

        public TokenService(IAlertService alertService)
        {
            _alertService = alertService;
        }

        public bool CheckTokenValidity(string token)
        {
            if(string.IsNullOrWhiteSpace(token))
            {
                return false;
            }

            return !IsTokenExpired(token);
        }

        public async Task SetAccessToken(string token)
        {
            await SecureStorage.SetAsync(nameof(AccessToken), token);
        }

        public async Task<string> GetAccessToken()
        {
            return await SecureStorage.GetAsync(nameof(AccessToken));
        }

        public bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadJwtToken(token);

            return securityToken.ValidTo < DateTime.UtcNow;
        }

        public async Task<string> GetClaim(string claimType, string token = null)
        {
            token ??= await GetAccessToken();

            if (string.IsNullOrWhiteSpace(token))
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.ReadJwtToken(token);

            var claim = securityToken.Claims.SingleOrDefault(x => x.Type == claimType);

            return claim.Value;
        }

        public async Task<Guid> GetUserId()
        {
            var userId = await GetClaim(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return default;
            }

            return new Guid(userId);
        }
    }
}
