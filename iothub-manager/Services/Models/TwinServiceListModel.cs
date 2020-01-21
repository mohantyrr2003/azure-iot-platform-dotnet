using System.Collections.Generic;

namespace Mmm.Platform.IoT.IoTHubManager.Services.Models
{
    public class TwinServiceListModel
    {
        public TwinServiceListModel(IEnumerable<TwinServiceModel> twins, string continuationToken = null)
        {
            this.ContinuationToken = continuationToken;
            this.Items = new List<TwinServiceModel>(twins);
        }

        public string ContinuationToken { get; set; }

        public List<TwinServiceModel> Items { get; set; }
    }
}