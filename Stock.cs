﻿using System.Text.Json.Serialization;

namespace StockAPI_Testx
{
    public class Stock
    {
        [JsonPropertyName("afterHours")]
        public decimal AfterHours { get; set; }

        [JsonPropertyName("afterHours")]
        public decimal Open { get; set; }

        [JsonPropertyName("close")]
        public decimal Close { get; set; }

        [JsonPropertyName("preMarket")]
        public decimal PreMarket { get; set; }

        [JsonPropertyName("high")]
        public decimal High { get; set; }

        [JsonPropertyName("low")]
        public decimal Low { get; set; }
    }
}
