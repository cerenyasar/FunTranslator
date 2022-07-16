using FunTranslator2.Dtos;
using FunTranslator2.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace FunTranslator2.UnitTests
{
    [TestClass]
    public class FunTranslatorServiceTests
    {
        [TestMethod]
        public async Task GetTranslationResultTest()

        {

            var expectedReponse = new ResponseDTO
            {
                contents = new ContentDTO
                {
                    Text = "Hello",
                    Translated = "]-[3llO",
                    Translation = "leetspeak"
                }
            };

            var jss = new JavaScriptSerializer();
            var json = jss.Serialize(expectedReponse);
            string url = "https://api.funtranslations.com/translate/leetspeak.json";

            HttpResponseMessage httpResponse = new HttpResponseMessage();
            httpResponse.StatusCode = System.Net.HttpStatusCode.OK;
            httpResponse.Content = new StringContent(json, Encoding.UTF8, "application/json");

            Mock<HttpMessageHandler> mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get && r.RequestUri.ToString().StartsWith(url)),
                ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            HttpClient httpClient = new HttpClient(mockHandler.Object);

            IFunTranslatorService service = new FunTranslatorService(httpClient);

            ResponseDTO actualReponse = await service.GetTranslationResult("Hello");



            //assert
            CollectionAssert.Equals(expectedReponse, actualReponse);
        }
    
    }
}
