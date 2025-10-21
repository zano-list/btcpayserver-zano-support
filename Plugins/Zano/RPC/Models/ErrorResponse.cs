using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class ErrorResponse
    {
        [JsonProperty("code")] public int Code { get; set; }
        [JsonProperty("message")] public string Message { get; set; }
    }
}
