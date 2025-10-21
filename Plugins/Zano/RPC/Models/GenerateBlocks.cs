﻿using Newtonsoft.Json;

namespace Zano.RPC.Models
{
    public class GenerateBlocks
    {
        [JsonProperty("wallet_address")]
        public string WalletAddress { get; set; }
        
        [JsonProperty("amount_of_blocks")]
        public int AmountOfBlocks { get; set; }
    }
}