﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FunTranslator2.Models
{
    public class Translation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string  Translated { get; set; }
        public string TranslationType { get; set; }

    }
}