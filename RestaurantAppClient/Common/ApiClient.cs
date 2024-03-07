using Microsoft.AspNetCore.WebUtilities;
using RestaurantAppClient.Common.Interfaces;
using RestaurantAppClient.Models;
using RestaurantAppClient.Models.Account;
using RestaurantAppClient.Models.Orders;
using RestaurantAppClient.Models.Shared;
using RestaurantAppClient.Pages;
using RestaurantAppClient.Services;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MenuItem = RestaurantAppClient.Models.Menu.MenuItem;

namespace RestaurantAppClient.Common
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly JsonSerializerOptions _serializerOptions;
        private readonly ITokenService _tokenService;

        public ApiClient(string url, ITokenService tokenService)
        {
            _httpClient.BaseAddress = new Uri(url);
            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _tokenService = tokenService;
        }

        public async Task<Response<AccessToken>> LoginUserAsync(UserLogin user)
        {
            Response<AccessToken> requestResponse = null;
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var responseMsg = await _httpClient.PostAsync("api/Account/Login", content))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response<AccessToken>>(responseContent, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        public async Task<Response> RegisterUserAsync(UserRegister user)
        {
            Response requestResponse = null;
            try
            {
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var responseMsg = await _httpClient.PostAsync("api/Account/Register", content))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response>(responseContent, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        public async Task<Response> ConfirmEmail(string id, string token)
        {
            Response requestResponse = null;
            try
            {
                var parameters = new Dictionary<string, string>()
                {
                    { "id", id },
                    { "token", token }
                };

                using (var responseMsg = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"api/Account/ConfirmEmail", parameters)))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response>(responseContent, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        private async Task AddBearerToken()
        {
            var token = await _tokenService.GetAccessToken();

            if(!_tokenService.CheckTokenValidity(token))
            {
                await Shell.Current?.GoToAsync($"///{nameof(MainPage)}");
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<Response<PagedList<T>>> GetMenuItems<T>(
            string menu,
            string category,
            Search search,
            OrderBy orderBy,
            Paging paging)
            where T : MenuItem
        {
            Response<PagedList<T>> requestResponse = null;
            try
            {
                await AddBearerToken();

                var parameters = new QueryParameters(search, orderBy, paging);
                var json = JsonSerializer.Serialize(parameters);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                category = category.Replace("-", "");

                using (var responseMsg = await _httpClient.PostAsync($"api/Menu/{menu}/GetAll/{category}", content))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response<PagedList<T>>>(responseContent, _serializerOptions);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        public async Task<Response<T>> GetMenuItemDetail<T>(
            string menu,
            string category,
            Guid itemId)
            where T : MenuItem
        {
            Response<T> requestResponse = null;
            try
            {
                await AddBearerToken();

                var parameters = new Dictionary<string, string>()
                {
                    { "id", itemId.ToString() }
                };
                category = category.Replace("-", "");

                using (var responseMsg = await _httpClient.GetAsync(QueryHelpers.AddQueryString($"api/Menu/{menu}/Get/{category}", parameters)))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response<T>>(responseContent, _serializerOptions);
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        public async Task<Response> CreateOrder(OrderCreate order)
        {
            Response requestResponse = null;
            try
            {
                await AddBearerToken();

                var json = JsonSerializer.Serialize(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var responseMsg = await _httpClient.PostAsync("api/Orders/Create", content))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response>(responseContent, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }

        public async Task<Response<PagedList<Order>>> GetUserOrders(OrderBy orderBy, Paging paging)
        {
            Response<PagedList<Order>> requestResponse = null;

            try
            {
                await AddBearerToken();

                var userId = await _tokenService.GetUserId();

                var order = new OrderGetAll(userId, orderBy, paging);
                var json = JsonSerializer.Serialize(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                using (var responseMsg = await _httpClient.PostAsync("api/Orders/GetAll", content))
                {
                    var responseContent = await responseMsg.Content.ReadAsStringAsync();
                    requestResponse = JsonSerializer.Deserialize<Response<PagedList<Order>>>(responseContent, _serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("ERROR: {0}", ex.Message);
            }

            return requestResponse;
        }
    }
}
