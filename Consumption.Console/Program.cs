using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

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
            
            var token = await AuthenticateAsync(authority,clientId,secret,resourceId);
            System.Console.WriteLine("Logged in with token - " + token.PasswordOrToken);
            
            //token aquired - issue command to work with consumption API
            
            
            System.Console.Read();
        }
        
        private static async Task<AuthData> AuthenticateAsync(string authority, string clientId,string secret,string resourceId)
        {
            var authContext = new AuthenticationContext(authority);
            var clientCred = new ClientCredential(clientId, secret);
            var result = await authContext.AcquireTokenAsync(resourceId, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            var authData = new AuthData {Scheme = AuthScheme.BEARER, PasswordOrToken = result.AccessToken};
            return authData;
        }
    }
    
    
}