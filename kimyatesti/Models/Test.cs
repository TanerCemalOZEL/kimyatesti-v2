using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Aciklama { get; set; }

        public virtual ICollection<SoruTestBind> TestSorulari { get; set; }
        public virtual ICollection<TestGroupBind> TestGroups { get; set; }
        public virtual ICollection<OgrTestTakip> testTakip { get; set; }
    }
}