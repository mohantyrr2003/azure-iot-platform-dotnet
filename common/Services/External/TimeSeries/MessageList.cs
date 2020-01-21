using System.Collections.Generic;

namespace Mmm.Platform.IoT.Common.Services.External.TimeSeries
{
    public class MessageList
    {
        public MessageList()
        {
            this.Messages = new List<Message>();
            this.Properties = new List<string>();
        }

        public MessageList(
            List<Message> messages,
            List<string> properties)
        {
            if (messages != null)
            {
                this.Messages = messages;
            }

            if (properties != null)
            {
                this.Properties = properties;
            }
        }

        public List<Message> Messages { get; set; }

        public List<string> Properties { get; set; }
    }
}