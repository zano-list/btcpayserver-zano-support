using System.Text.Json.Serialization;


namespace Zano.RPC.Models
{
    public class ZanoRpcWrapper
    {
        public string Id { get; set; }
        public string Jsonrpc { get; set; }
        public GetHeightResponse Result { get; set; }
    }
    public class GetHeightResponse
    {
        // Note: Use [JsonPropertyName] for System.Text.Json
        // [JsonProperty] is for Newtonsoft.Json
        [JsonPropertyName("current_height")]
        public long CurrentHeight { get; set; }

        [JsonPropertyName("is_whatch_only")]
        public bool IsWatchOnly { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }
    }

}