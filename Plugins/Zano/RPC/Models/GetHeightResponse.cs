using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class GetHeightResponse
    {
        [JsonProperty("height")]
        public long Height { get; set; }
    }
}