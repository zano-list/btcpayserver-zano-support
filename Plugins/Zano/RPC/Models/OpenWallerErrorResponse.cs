using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class OpenWalletErrorResponse
    {
        [JsonProperty("code")]
        public int Code { get; set; }
        
        [JsonProperty("message")]
        public string Message { get; set; }
    }
}