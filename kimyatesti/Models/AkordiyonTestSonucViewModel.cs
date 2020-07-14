using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class AkordiyonTestSonucViewModel
    {
        public List<Soru> Sorus { get; set; }
        public string SoruHistoryLog { get; set; }
        public string CorrectAnswers { get; set; }
    }
}