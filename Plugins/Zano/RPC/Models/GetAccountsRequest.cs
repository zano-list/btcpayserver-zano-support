using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class GetAccountsRequest
    {
        [JsonProperty("tag")]
        public string Tag { get; set; }
    }
}