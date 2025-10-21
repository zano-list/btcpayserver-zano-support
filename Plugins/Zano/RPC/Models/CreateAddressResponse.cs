using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class CreateAddressResponse
    {
        [JsonProperty("integrated_address")]
        public string Address { get; set; }
        
        [JsonProperty("payment_id")]
        public string PaymentId { get; set; }
    }
}