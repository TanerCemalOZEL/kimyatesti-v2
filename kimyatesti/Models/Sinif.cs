using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class Sinif
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Unite> Uniteler { get; set; }

    }
}