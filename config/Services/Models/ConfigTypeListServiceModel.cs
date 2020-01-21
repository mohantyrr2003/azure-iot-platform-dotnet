using System;
using System.Collections.Generic;
using System.Linq;
using Mmm.Platform.IoT.Config.Services.Models;
using Newtonsoft.Json;

namespace Mmm.Platform.IoT.Config.Services.External
{
    public class ConfigTypeListServiceModel
    {
        private HashSet<string> configTypes = new HashSet<string>();

        [JsonProperty("configtypes")]
        public string[] ConfigTypes
        {
            get
            {
                return configTypes.ToArray<string>();
            }
            set
            {
                Array.ForEach<string>(value, c => configTypes.Add(c));
            }
        }

        internal void add(string customConfig)
        {
            configTypes.Add(customConfig.Trim());
        }

    }
}
