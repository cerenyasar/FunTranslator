using FunTranslator2.Dtos;
using FunTranslator2.Models;
using FunTranslator2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace FunTranslator2.Controllers
{
    public class TranslationController : Controller
    {
        private ApplicationDbContext _context;       
        private readonly IFunTranslatorService _service;

        public TranslationController(IFunTranslatorService service, ApplicationDbContext context)
        {
            this._context = context;
            //this._context = new ApplicationDbContext();
            this._service = service;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: Index
        public ActionResult Index()
        {
            //var result = GetTranslate();
            //var translations =_context.Translations.ToList<Translation>();
            return View();
        }

        //Hosted web API REST Service base url
        
        //string Baseurl = "https://api.funtranslations.com/translate/leetspeak.json";
        [HttpPost]
        public async Task<JsonResult> GetTranslate(string text)
        {
            var uri = "?text=" + text;
            var response2 = await _service.GetTranslationX(text);

            InsertTranslation(response2);
            return new JsonResult
            {
                Data = new
                {
                    Status = "1",
                    Response = response2
                }
            };

            //return null;

            //var response = _service.GetTranslationResult(uri);

            //InsertTranslation(response);
            //return new JsonResult
            //{
            //    Data = new
            //    {
            //        Status = "1",
            //        Response = response
            //    }
            //};


            //OLD CODE


            //_httpClient.BaseAddress = new Uri(Baseurl);

            //var responseTask = _httpClient.GetAsync("?text=" + text);
            //responseTask.Wait();

            //var result = responseTask.Result;
            //if (result.IsSuccessStatusCode)
            //{
            //    var readTask = result.Content.ReadAsStringAsync();
            //    readTask.Wait();

            //    var response = readTask.Result;

            //    var jss = new JavaScriptSerializer();
            //    var translatedData = jss.Deserialize<ResponseDTO>(response);
            //    InsertTranslation(translatedData);
            //    return new JsonResult
            //    {
            //        Data = new
            //        {
            //            Status = "1",
            //            Response = translatedData.contents.Translated
            //        }
            //    };

            //}
            //else
            //{
            //    throw new Exception("Translation failed!");
            //}


            //using (var client = new HttpClient())
            //{
            //    //Passing service base url
            //    client.BaseAddress = new Uri(Baseurl);

            //    var responseTask = client.GetAsync("?text="+text);
            //    responseTask.Wait();

            //    var result = responseTask.Result;
            //    if (result.IsSuccessStatusCode)
            //    {
            //        var readTask = result.Content.ReadAsStringAsync();
            //        readTask.Wait();

            //        var response = readTask.Result; 

            //        var jss = new JavaScriptSerializer();
            //        var translatedData = jss.Deserialize<ResponseDTO>(response);
            //        InsertTranslation(translatedData);
            //        return new JsonResult
            //        {
            //            Data = new
            //            {
            //                Status = "1",
            //                Response = translatedData
            //            }
            //        };

            //    }
            //    else
            //    {
            //        throw new Exception("Translation failed!");
            //    }   
            //}
        }


        [HttpPost]
        public JsonResult GetTranslationRecords()
        {
            var translations = _context.Translations.ToList();
            return new JsonResult
            {
                Data = translations
            };
            //var list = new List<Translation> {
            //    new Translation { Id = 1, Text = "Hello", Translated = "]-[3l10",TranslationType = "leetspeak"}
            //};
            //var viewModel = new TranslationListViewModel();
            //viewModel.Translations = list;
            //return View(viewModel);
        }

        public void InsertTranslation(ResponseDTO data)
        {
            var record = new Translation
            {
                Text = data.contents.Text,
                Translated = data.contents.Translated,
                TranslationType = data.contents.Translation
            };

            _context.Translations.Add(record);
            _context.SaveChanges();

            //   var customer = Mapper.Map<CustomerDto, Customer>(data);
            //_context.Customers.Add(customer);
            //_context.SaveChanges();

            //customerDto.Id = customer.Id;

            //return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
        }
    }
}