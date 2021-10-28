using Newtonsoft.Json;

namespace NewBrainfieldNetCore.Common.Requests
{
    public class PaymentRequest
    {
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Head
    {
        [JsonProperty("responseTimestamp")]
        public string ResponseTimestamp { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }
    }

    public class ResultInfo
    {
        [JsonProperty("resultStatus")]
        public string ResultStatus { get; set; }

        [JsonProperty("resultCode")]
        public string ResultCode { get; set; }

        [JsonProperty("resultMsg")]
        public string ResultMsg { get; set; }
    }

    public class Body
    {
        [JsonProperty("resultInfo")]
        public ResultInfo ResultInfo { get; set; }

        [JsonProperty("txnToken")]
        public string TxnToken { get; set; }

        [JsonProperty("isPromoCodeValid")]
        public bool IsPromoCodeValid { get; set; }

        [JsonProperty("authenticated")]
        public bool Authenticated { get; set; }
    }

    public class Root
    {
        [JsonProperty("head")]
        public Head Head { get; set; }

        [JsonProperty("body")]
        public Body Body { get; set; }

        public string OrderId { get; set; }
        public string Amount { get; set; }
    }
}
