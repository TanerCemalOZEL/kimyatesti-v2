using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class SoruTestBind
    {
        public int Id { get; set; }
        public int SoruId { get; set; }
        public int TestId { get; set; }

        public virtual Soru Soru { get; set; }
        public virtual Test Test { get; set; }
    }
}