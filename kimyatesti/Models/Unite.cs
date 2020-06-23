using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class Unite
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int SinifId { get; set; }

        public virtual Sinif Sinif { get; set; }
        public virtual ICollection<Konu> Konular { get; set; }
    }
}