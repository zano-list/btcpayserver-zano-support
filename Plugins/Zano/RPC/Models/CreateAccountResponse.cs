using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class CreateAccountResponse
    {
        [JsonProperty("account_index")]
        public long AccountIndex { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}