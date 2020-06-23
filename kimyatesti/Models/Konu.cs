using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class Konu
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int UniteId { get; set; }

        public virtual Unite Unite { get; set; }
        public virtual ICollection<Soru> Sorular { get; set; }

    }
}