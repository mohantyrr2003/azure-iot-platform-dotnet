using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Mmm.Platform.IoT.Common.Services.Config;
using Mmm.Platform.IoT.Common.Services.Exceptions;
using Mmm.Platform.IoT.Common.Services.External;
using Mmm.Platform.IoT.Common.Services.Http;

namespace Mmm.Platform.IoT.Config.Services.External
{
    public class AzureResourceManagerClient : IAzureResourceManagerClient
    {
        private readonly IHttpClient httpClient;
        private readonly IUserManagementClient userManagementClient;
        private readonly AppConfig config;

        public AzureResourceManagerClient(
            IHttpClient httpClient,
            AppConfig config,
            IUserManagementClient userManagementClient)
        {
            this.httpClient = httpClient;
            this.userManagementClient = userManagementClient;
            this.config = config;
        }

        public async Task<bool> IsOffice365EnabledAsync()
        {
            if (string.IsNullOrEmpty(config.ConfigService.ConfigServiceActions.SubscriptionId) ||
                string.IsNullOrEmpty(config.ConfigService.ConfigServiceActions.SolutionName) ||
                string.IsNullOrEmpty(config.ConfigService.ConfigServiceActions.ArmEndpointUrl))
            {
                throw new InvalidConfigurationException("Subscription Id, Resource Group, and Arm Endpoint Url must be specified" +
                                                        "in the environment variable configuration for this " +
                                                        "solution in order to use this API.");
            }

            var logicAppTestConnectionUri = config.ConfigService.ConfigServiceActions.ArmEndpointUrl +
                                               $"/subscriptions/{config.ConfigService.ConfigServiceActions.SubscriptionId}/" +
                                               $"resourceGroups/{config.ConfigService.ConfigServiceActions.SolutionName}/" +
                                               "providers/Microsoft.Web/connections/" +
                                               "office365-connector/extensions/proxy/testconnection?" +
                                               $"api-version={config.ConfigService.ConfigServiceActions.ManagementApiVersion}";

            var request = await this.CreateRequest(logicAppTestConnectionUri);

            var response = await this.httpClient.GetAsync(request);

            if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new NotAuthorizedException("The application is not authorized and has not been " +
                                                 "assigned contributor permissions for the subscription. Go to the Azure portal and " +
                                                 "assign the application as a contributor in order to retrieve the token.");
            }

            return response.IsSuccessStatusCode;
        }

        private async Task<HttpRequest> CreateRequest(string uri, IEnumerable<string> content = null)
        {
            var request = new HttpRequest();
            request.SetUriFromString(uri);
            if (uri.ToLowerInvariant().StartsWith("https:"))
            {
                request.Options.AllowInsecureSSLServer = true;
            }

            if (content != null)
            {
                request.SetContent(content);
            }

            var token = await this.userManagementClient.GetTokenAsync();
            request.Headers.Add("Authorization", "Bearer " + token);

            return request;
        }
    }
}
