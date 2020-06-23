using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class OgrenciAvatar
    {
        public int Id { get; set; }
        public string OgrName { get; set; }
        public string OgrEmail { get; set; }
        public string Kurum { get; set; }
        public string MevcutSinif { get; set; }

        public virtual ICollection<OgrenciTestGroupBind> TestGroups { get; set; }

    }
}