using CaloriesTracker.Infrastructure.User;
using CaloriesTracker.Infrastructure.Rest;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using System;
using System.Net.Http;

namespace CaloriesTracker.Infrastructure.Mock.Rest
{
    public class HttpClientFactory
    {
        private const string ApiEndpoint = "http://localhost:44331";

        public static HttpClient Create()
        {
            var mockHttp = new MockHttpMessageHandler();

            #region Pin code token

            mockHttp.When($"{ApiEndpoint}/account/registration?email=vtomazov@gmail.com")
                .Respond("application/json", JsonConvert.SerializeObject(
                        new JsonResponse<string>
                        {
                            Success = true,
                            Content = "Registration OK and blah-blah"
                        }
                    ));

            mockHttp.When($"{ApiEndpoint}/account/registration?email=harold.vt@mail.ru")
                .Respond("application/json", JsonConvert.SerializeObject(
                        new JsonResponse<string>
                        {
                            Success = false,
                            Content = "Email already exists"
                        }
                    ));

            #endregion

            #region Auth token

            mockHttp//.When($"{Settings.LocalhostUrl}/account/pin-code/token?Username=dcslAdmin&Password=%212345Qwert&DeviceId=unknown")
               .When($"{ApiEndpoint}/account/token?Username=vtomazov@gmail.com&Password=5432!Qwert")
               .Respond("application/json", JsonConvert.SerializeObject(new AuthTokenDto
               {
                   AccessToken = "Test-Access-Token",
                   RefreshToken = "Test-Refresh-Token",
                   ExpiresIn = DateTimeOffset.Now.AddYears(20)
               }));

            #endregion           

            return new HttpClient(mockHttp) { BaseAddress = new Uri(ApiEndpoint) };
        }
    }
}
