using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class GetBalanceResponse
    {
        [JsonProperty("balance")]
        public long Balance { get; set; }
        
        [JsonProperty("unlocked_balance")]
        public long UnlockedBalance { get; set; }
    }
}