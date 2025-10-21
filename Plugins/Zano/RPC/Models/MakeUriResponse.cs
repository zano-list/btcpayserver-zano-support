using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class MakeUriResponse
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}