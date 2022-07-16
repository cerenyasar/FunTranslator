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
            this._service = service;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<JsonResult> GetTranslate(string text)
        {
            var uri = "?text=" + text;
            var response2 = await _service.GetTranslationResult(text);

            InsertTranslation(response2);
            return new JsonResult
            {
                Data = new
                {
                    Status = "1",
                    Response = response2
                }
            };
        }


        [HttpPost]
        public JsonResult GetTranslationRecords()
        {
            var translations = _context.Translations.ToList();
            return new JsonResult
            {
                Data = translations
            };
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
        }
    }
}