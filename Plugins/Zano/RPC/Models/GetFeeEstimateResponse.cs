using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class GetFeeEstimateResponse
    {
        [JsonProperty("default_fee")]
        public long DefaultFee { get; set; }
    }
}