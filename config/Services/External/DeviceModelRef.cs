using Newtonsoft.Json;

namespace Mmm.Platform.IoT.Config.Services.External
{
    public class DeviceModelRef
    {
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "Count")]
        public int Count { get; set; }
    }
}