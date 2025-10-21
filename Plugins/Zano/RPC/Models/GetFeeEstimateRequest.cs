using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class GetFeeEstimateRequest
    {
        [JsonProperty("grace_blocks")]
        public int? GraceBlocks { get; set; }
    }
}