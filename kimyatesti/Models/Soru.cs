using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class Soru
    {
        public int Id { get; set; }
        public string ImgPath { get; set; }
        public string Cevap { get; set; }
        public string Tuyo { get; set; }
        public float? Zorluk { get; set; }

        public int KonuId { get; set; }

        public virtual Konu Konu { get; set; }
        public virtual ICollection<SoruTestBind> SoruTestBind { get; set; }
    }
}