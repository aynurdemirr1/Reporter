using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Reporter.BusinessLayer.Abstract;
using Newtonsoft.Json;

namespace Reporter.BusinessLayer.Concrete
{
    public class TranslationManager : ITranslationService
    {
        private readonly HttpClient _httpClient;
        private readonly string _huggingFaceModelUrl;
        private readonly string _huggingFaceApiToken;

        public TranslationManager(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _huggingFaceModelUrl = configuration["HuggingFaceTranslate:ModelUrl"];
            _huggingFaceApiToken = configuration["HuggingFaceTranslate:ApiToken"];

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _huggingFaceApiToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<string> TranslateToEnglishAsync(string turkishText)
        {
            var requestBody = new { inputs = turkishText };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_huggingFaceModelUrl, jsonContent);

            if (!response.IsSuccessStatusCode)
                return null;

            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<TranslationResponse>>(responseString);
            return result?.FirstOrDefault()?.translation_text;
        }

        private class TranslationResponse
        {
            public string translation_text { get; set; }
        }
    }
}
