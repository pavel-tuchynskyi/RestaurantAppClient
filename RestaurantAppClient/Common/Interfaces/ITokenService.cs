namespace RestaurantAppClient.Common.Interfaces
{
    public interface ITokenService
    {
        Task SetAccessToken(string token);
        bool CheckTokenValidity(string token);
        Task<string> GetAccessToken();
        bool IsTokenExpired(string token);
        Task<string> GetClaim(string claimType, string token = null);
        Task<Guid> GetUserId();
    }
}
