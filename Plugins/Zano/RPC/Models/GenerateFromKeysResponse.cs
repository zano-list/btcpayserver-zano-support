﻿using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class GenerateFromKeysResponse
    {
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("jsonrpc")] public string Jsonrpc { get; set; }
        [JsonProperty("result")] public GenerateFromKeysResult Result { get; set; }
        [JsonProperty("error")] public ErrorResponse Error { get; set; }
    }
}
