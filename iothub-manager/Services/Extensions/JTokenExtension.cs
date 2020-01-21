using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Mmm.Platform.IoT.IoTHubManager.Services.Extensions
{
    public static class JTokenExtension
    {
        public static IEnumerable<string> GetAllLeavesPath(this JToken root)
        {
            if (root is JValue)
            {
                yield return root.Path;
            }
            else
            {
                foreach (var child in root.Values())
                {
                    foreach (var name in child.GetAllLeavesPath())
                    {
                        yield return name;
                    }
                }
            }
        }
    }
}