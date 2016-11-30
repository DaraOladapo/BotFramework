using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace AssistantBot.CognitiveServices
{
    [Serializable]
    public class CognitiveClient
    {
        [NonSerialized]
        HttpClient _httpClient;

        public CognitiveClient()
        {
            _httpClient = new HttpClient();
            // Request headers:
            var apiToken = ConfigurationManager.AppSettings["CognitiveServicesApiToken"];
            _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", apiToken);
        }
        //Send sentence to Cognitive Services and get response:
        public async Task<CheckSpellingResponse> CheckSpelling(string sentence)
        {
            StringContent content = new StringContent("text=" + sentence);
            var uri = "https://api.cognitive.microsoft.com/bing/v5.0/spellcheck/?mode=spell&text=" + sentence;
            var response = await _httpClient.GetStringAsync(uri);
            var checkSpellingResponse = JsonConvert.DeserializeObject<CheckSpellingResponse>(response);
            return checkSpellingResponse;
        }
    }
}