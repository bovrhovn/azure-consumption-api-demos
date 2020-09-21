using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;

namespace Consumption.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Console.WriteLine("Authenticating against Azure...");

            //authenticate against Azure
            //how to register application, is available here https://docs.microsoft.com/en-us/azure/active-directory/develop/howto-create-service-principal-portal
            string authority = "https://login.windows.net/" + Environment.GetEnvironmentVariable("TenantId");
            string clientId = Environment.GetEnvironmentVariable("ClientId");
            string secret = Environment.GetEnvironmentVariable("Secret");
            string resourceId = "https://management.azure.com/";

            var token = await AuthenticateAsync(authority, clientId, secret, resourceId);
            System.Console.WriteLine("Logged in with token - " + token.PasswordOrToken);

            //token aquired - issue command to work with consumption API
            string uri =
                $"https://management.azure.com/subscriptions/{Environment.GetEnvironmentVariable("SubscriptionId")}/providers/Microsoft.Consumption/usageDetails?api-version=2018-03-31&$expand=properties/additionalProperties";
            var (content, statusCode) = await GetAsync(uri, token.PasswordOrToken);
            if (statusCode == HttpStatusCode.OK)
            {
                //convert to models
                var billingModel = JsonConvert.DeserializeObject<BillingModel>(content);
                System.Console.WriteLine($"Details: {Environment.NewLine}");
                foreach (var usageResource in billingModel.UsageList)
                {
                    System.Console.WriteLine(
                        $"{usageResource.Name} used in {usageResource.Properties.ConsumedService} " +
                        $"{usageResource.Properties.UsageQuantity} {usageResource.Properties.Currency} " + "" +
                        $"in {usageResource.Properties.SubscriptionName}");
                }
            }
            else
            {
                System.Console.WriteLine("No data available!");
            }

            System.Console.Read();
        }

        private static async Task<AuthData> AuthenticateAsync(string authority, string clientId, string secret,
            string resourceId)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(clientId, secret);
            var result = await authContext.AcquireTokenAsync(resourceId, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            var authData = new AuthData {Scheme = AuthScheme.BEARER, PasswordOrToken = result.AccessToken};
            return authData;
        }

        private static async Task<(string Content, HttpStatusCode StatusCode)> GetAsync(string uri,
            string authorizationHeader,
            List<KeyValuePair<string, string>> additionalHeaders = null)
        {
            using var httpClient = new HttpClient();
            HttpResponseMessage response = null;

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", authorizationHeader);

            // All Cosmos Db calls expect the current UTC time in RFC1123 format and the API version in the header variables
            if (uri.Contains("documents.azure.com"))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(authorizationHeader);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(uri)
            };

            if (additionalHeaders != null)
            {
                foreach (var h in additionalHeaders)
                {
                    request.Headers.Add(h.Key, h.Value);
                }
            }

            var content = "";

            try
            {
                response = await httpClient.SendAsync(request);
                content = await response.Content.ReadAsStringAsync();
                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                content += "\n\n" + ex.Message;
            }

            return (Content: content, StatusCode: response?.StatusCode ?? HttpStatusCode.InternalServerError);
        }
    }
}