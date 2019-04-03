using CaloriesTracker.Domain.User;
using CaloriesTracker.Infrastructure.Configuration;
using CaloriesTracker.Infrastructure.User;
using Newtonsoft.Json;
using Refit;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CaloriesTracker.Infrastructure.Rest
{
    public class AuthTokenProvider : IAuthTokenProvider
    {
        private readonly HttpClient _httpClient;

        public AuthTokenProvider(INativeHttpClientService nativeHttpClientService)
        {
            _httpClient = nativeHttpClientService.Get();
        }

        /// <summary>
        /// Returns valid auth token (refreshed, if got expired). 
        /// Otherwise returns Empty.
        /// </summary>
        /// <returns></returns>
        public async Task<AuthToken> GetTokenAsync()
        {
            try
            {
                var currentToken = Settings.CurrentToken;
                AuthToken token = AuthToken.Empty;

                if (currentToken != null)
                {
                    token = AuthToken.Create(currentToken.AccessToken, currentToken.RefreshToken, currentToken.ExpiresIn);
                }

                if (token != AuthToken.Empty && !token.IsValid)
                {
                    var requestContent = new StringContent(JsonConvert.SerializeObject(new { token.RefreshToken, Settings.DeviceId }), Encoding.UTF8, "application/json");
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.RefreshToken);

                    using (HttpResponseMessage response = await _httpClient.PostAsync($"{Settings.RestEndpointUrl}/token/refresh", requestContent))
                    using (HttpContent responseContent = response.Content)
                    {
                        string result = await responseContent.ReadAsStringAsync();

                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var jsonResponse = JsonConvert.DeserializeObject<JsonResponse<AuthTokenDto>>(result);

                            if (jsonResponse.Success)
                            {
                                currentToken = Settings.CurrentToken = jsonResponse.Content;
                            }
                            else
                            {
                                currentToken = Settings.CurrentToken = null;
                            }
                        }
                        else
                        {
                            currentToken = Settings.CurrentToken = null;
                            response.StatusCode = System.Net.HttpStatusCode.Unauthorized;
                            throw await ApiException.Create(new HttpRequestMessage(), HttpMethod.Post, response);
                        }

                        token = currentToken != null ? AuthToken.Create(currentToken.AccessToken, currentToken.RefreshToken, currentToken.ExpiresIn) : AuthToken.Empty;
                    }
                }

                return token;
            }
            catch (Exception e)
            {
                return AuthToken.Empty;
            }
        }
    }
}
