using System;

namespace Mmm.Platform.IoT.Common.TestHelpers
{
    public class WebServiceHost
    {
        public static string GetBaseAddress()
        {
            int port = new Random().Next(40000, 60000);
            string baseAddress = "http://127.0.0.1:" + port;
            return baseAddress;
        }
    }
}
