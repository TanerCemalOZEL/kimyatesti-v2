using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class TestGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Aciklama { get; set; }

        public virtual ICollection<TestGroupBind> Testler { get; set; }
        public virtual ICollection<OgrenciTestGroupBind> OgrenciAvatars { get; set; }
        public virtual ICollection<OgrTestTakip> testTakip { get; set; }

    }
}