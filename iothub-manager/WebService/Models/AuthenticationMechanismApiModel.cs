using Mmm.Platform.IoT.IoTHubManager.Services.Models;
using Newtonsoft.Json;

namespace Mmm.Platform.IoT.IoTHubManager.WebService.Models
{
    public class AuthenticationMechanismApiModel
    {
        public AuthenticationMechanismApiModel()
        {
        }

        public AuthenticationMechanismApiModel(AuthenticationMechanismServiceModel model)
        {
            this.AuthenticationType = model.AuthenticationType;
            this.PrimaryKey = model.PrimaryKey;
            this.SecondaryKey = model.SecondaryKey;
            this.PrimaryThumbprint = model.PrimaryThumbprint;
            this.SecondaryThumbprint = model.SecondaryThumbprint;
        }

        [JsonProperty(PropertyName = "PrimaryKey", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryKey { get; set; }

        [JsonProperty(PropertyName = "SecondaryKey", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryKey { get; set; }

        [JsonProperty(PropertyName = "PrimaryThumbprint", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryThumbprint { get; set; }

        [JsonProperty(PropertyName = "SecondaryThumbprint", NullValueHandling = NullValueHandling.Ignore)]
        public string SecondaryThumbprint { get; set; }

        [JsonProperty(PropertyName = "AuthenticationType", NullValueHandling = NullValueHandling.Ignore)]
        public AuthenticationType AuthenticationType { get; set; }

        public AuthenticationMechanismServiceModel ToServiceModel()
        {
            return new AuthenticationMechanismServiceModel()
            {
                AuthenticationType = this.AuthenticationType,
                PrimaryKey = this.PrimaryKey,
                SecondaryKey = this.SecondaryKey,
                PrimaryThumbprint = this.PrimaryThumbprint,
                SecondaryThumbprint = this.SecondaryThumbprint,
            };
        }
    }
}