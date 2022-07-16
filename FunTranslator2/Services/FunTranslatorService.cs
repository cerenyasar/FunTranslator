using FunTranslator2.Dtos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace FunTranslator2.Services
{
    public class FunTranslatorService : IFunTranslatorService
    {

        private readonly HttpClient _httpClient;
        private readonly UriBuilder GetDivisionsUri;

        public FunTranslatorService(HttpClient httpClient)
        {

            this._httpClient = httpClient;
            //GetDivisionsUri = new UriBuilder($"{url}");
            GetDivisionsUri = new UriBuilder($"https://api.funtranslations.com/translate/leetspeak.json");
        }


        public ResponseDTO GetTranslationResult(string requestUri)
        {
            try
            {
                //var requestUri = "http://api.songs.com/songs";
                _httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiUrl"]);
                var response = _httpClient.GetAsync(requestUri);
               // response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var responseData = response.Result.Content.ReadAsStringAsync();
                    responseData.Wait();
                    var jss = new JavaScriptSerializer();
                    var translatedData = jss.Deserialize<ResponseDTO>(responseData.Result);
                    return translatedData;
                }
                else
                {
                    throw new Exception("Translation failed!");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public async Task<ResponseDTO> GetTranslationX(string requestUri)
        {
            //ConfigurationManager.AppSettings["ApiUrl"]
            GetDivisionsUri.Query = $"text={requestUri}";

            var response = await _httpClient.GetAsync(GetDivisionsUri.ToString());
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDTO>(json);
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}