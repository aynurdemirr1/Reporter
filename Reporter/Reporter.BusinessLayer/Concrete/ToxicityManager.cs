using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Reporter.BusinessLayer.Abstract;

namespace Reporter.BusinessLayer.Concrete
{
    public class ToxicityManager : IToxicityDetectionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _huggingFaceApiToken;
        private readonly string _huggingFaceModelUrl;

        public ToxicityManager(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _huggingFaceApiToken = configuration["HuggingFace:ApiToken"];
            _huggingFaceModelUrl = configuration["HuggingFace:ModelUrl"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _huggingFaceApiToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ToxicityDetectionResult> DetectToxicityAsync(string commentText)
        {
            var requestBody = new { inputs = commentText };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_huggingFaceModelUrl, jsonContent);

            // ✅ Log: HTTP yanıt durumu
            Console.WriteLine($"TOXICITY API STATUS: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"TOXICITY API ERROR BODY: {error}");
                return new ToxicityDetectionResult { IsToxic = false, Score = 0, DetectedLabel = "undetected" };
            }

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine("TOXICITY RAW RESPONSE: " + responseString); // ✅ Log yanıt

            var modelResponse = JsonConvert.DeserializeObject<List<List<ModelPrediction>>>(responseString);

            if (modelResponse == null || modelResponse.Count == 0 || modelResponse[0].Count == 0)
            {
                Console.WriteLine("TOXICITY RESPONSE FORMAT ERROR");
                return new ToxicityDetectionResult { IsToxic = false, Score = 0, DetectedLabel = "undetected" };
            }

            var topPrediction = modelResponse[0].OrderByDescending(p => p.Score).FirstOrDefault();

            Console.WriteLine($"LABEL: {topPrediction?.Label} | SCORE: {topPrediction?.Score}");

            if (topPrediction != null && topPrediction.Label.ToLower().Contains("toxic"))
            {
                return new ToxicityDetectionResult
                {
                    IsToxic = topPrediction.Score > 0.5,
                    Score = topPrediction.Score,
                    DetectedLabel = topPrediction.Label
                };
            }

            return new ToxicityDetectionResult
            {
                IsToxic = false,
                Score = topPrediction?.Score ?? 0,
                DetectedLabel = topPrediction?.Label ?? "none"
            };
        }


        private class ModelPrediction
        {
            public string Label { get; set; }
            public double Score { get; set; }
        }
    }



}