using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class Peer
    {
        [JsonProperty("info")]
        public Info Info { get; set; }
    }
}