using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunTranslator2.Dtos
{
    public class ContentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Translated { get; set; }
        public string Translation { get; set; }
    }
}