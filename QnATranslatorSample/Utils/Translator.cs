using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Headers;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace QnATranslatorSample.Utils
{
    public static class Translator
    {
        public static string originLanguage { get; set; }
        public static async Task<string> TranslateText(string inputText, string language, string accessToken)
        {
            string url = "http://api.microsofttranslator.com/v2/Http.svc/Translate";
            string query = $"?text={System.Net.WebUtility.UrlEncode(inputText)}&to={language}&contentType=text/plain";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(url + query).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                    return "Hata: " + result;
                var translatedText = XElement.Parse(result).Value;
                return translatedText;
            }
        }

        public static async Task<string> GetAuthenticationToken(string key)
        {
            string endpoint = "https://api.cognitive.microsoft.com/sts/v1.0/issueToken";

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", key);
                var response = await client.PostAsync(endpoint, null).ConfigureAwait(false);
                var token = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return token;
            }
        }

        public static async Task<string> DetectLanguage(string inputText, string accessToken)
        {
            string url = "https://api.microsofttranslator.com/V2/Http.svc/Detect";
            string query = $"?text={System.Net.WebUtility.UrlEncode(inputText)}&contentType=text/plain";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync(url + query).ConfigureAwait(false);
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (!response.IsSuccessStatusCode)
                    return "Hata: " + result;
                var language = XElement.Parse(result).Value;
                return language;
            }
        }
    }
}