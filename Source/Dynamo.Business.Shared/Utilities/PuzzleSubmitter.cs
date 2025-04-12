using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Dynamo.Business.Shared.Utilities
{
    public class PuzzleSubmitter
    {
        private readonly HttpClient _httpClient;
        public PuzzleSubmitter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response> SubmitAnswerAsync(Submission submission)
        {
            string apiEndpoint = "https://yv9w75zly1.execute-api.us-east-1.amazonaws.com/dev/TMYC_Solution_Checker";

            // Construct the object that matches the required JSON structure
            var payload = new
            {
                operation = "submit",
                payload = new
                {
                    Item = new
                    {
                        id = submission.EmailAddress,
                        question = submission.Question,
                        submission = submission.Answer
                    }
                }
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync(apiEndpoint, content);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();

            var wrapper = JsonSerializer.Deserialize<ResponseWrapper>(responseJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var responseItem = wrapper?.Item;

            if (responseItem == null)
                throw new NullReferenceException("response cannot be null");
            else
                return responseItem;
        }
    }

    public class Submission
    {
        public string EmailAddress { get; set; }
        public string Question { get; set; }
        public double Answer { get; set; }
    }

    public class ResponseWrapper
    {
        public Response Item { get; set; }
    }

    public class Response
    {
        [JsonPropertyName("id")]
        public string EmailAddress { get; set; }
        [JsonPropertyName("question")]
        public string Question { get; set; }
        [JsonPropertyName("submission")]
        public double Answer { get; set; }
        [JsonPropertyName("result")]
        public string Result { get; set; }
        [JsonPropertyName("time")]
        public string RawDateTime { get; set; }
        [JsonPropertyName("id-time")]
        public string IdTime { get; set; }

        [JsonIgnore]
        public DateTime DateTime => DateTime.Parse(RawDateTime);
    }
}
