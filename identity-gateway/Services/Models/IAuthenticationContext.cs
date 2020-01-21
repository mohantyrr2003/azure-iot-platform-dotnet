using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Mmm.Platform.IoT.IdentityGateway.Services.Models
{
    public interface IAuthenticationContext
    {
        Task<AuthenticationResult> AcquireTokenAsync(string resource, ClientCredential clientCredential);
    }
}