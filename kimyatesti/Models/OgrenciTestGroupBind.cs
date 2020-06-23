using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class OgrenciTestGroupBind
    {
        public int Id { get; set; }
        public int TestGroupId { get; set; }
        public int OgrenciAvatarId { get; set; }

        public virtual TestGroup TestGroups { get; set; }
        public virtual OgrenciAvatar Ogrenci { get; set; }

    }
}