using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class SoruHistory
    {
        public int Id { get; set; }
        public int TestGroupId { get; set; }
        public int SoruId { get; set; }
        public int OgrenciId { get; set; }
        public int YBD { get; set; }
        public int OgrencininCevabi { get; set; }

        public virtual TestGroup TestGroup { get; set; }
        public virtual Soru Soru { get; set; }
        public virtual OgrenciAvatar Ogrenci { get; set; }
    }
}