using System.Text.Json.Serialization;


namespace Dynamo.Business.Shared.Cyber.LogAnalyzer
{
    public class LogEntry
    {
        [JsonPropertyName("timestamp")]
        //public DateTime TimeStamp { get; set; }
        //[JsonPropertyName("timestamp")]
        public string TimeStampString { get; set; }
        [JsonPropertyName("ip_address")]
        public string IpAddress { get; set; }
        [JsonPropertyName("request_method")]
        public string RequestMethod { get; set; }
        [JsonPropertyName("endpoint")]
        public string Endpoint { get; set; }
        [JsonPropertyName("status_code")]
        public int StatusCode { get; set; }
        [JsonPropertyName("resonse_time")]
        public int ResponseTime { get; set; }
        [JsonPropertyName("user_agent")]
        public string UserAgent { get; set; }
    }
}
