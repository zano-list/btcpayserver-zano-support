using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public partial class CreateAccountRequest
    {
        [JsonProperty("label")]
        public string Label { get; set; }
    }
}