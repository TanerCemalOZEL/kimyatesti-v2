using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class OgrTestTakip
    {
        // testGroup içerisine bir test tanımlandığı anda bağlı tüm öğrenciler
        // için TamamlanmaTarihi ve CevapList dışındaki sütunlar doldurulur.
        // Testi tamamlayarak kaydeden öğrencinin cevapları ve teslim tarihi kaydedilir.
        public int Id { get; set; }
        public int OgrenciId { get; set; }
        public int TestGroupId { get; set; }
        public int TestId { get; set; }
        public DateTime AtanmaTarihi { get; set; }
        public DateTime? TamamlanmaTarihi { get; set; }
        public string CevapList { get; set; }

        public virtual OgrenciAvatar Ogrenci { get; set; }
        public virtual TestGroup TestGroup { get; set; }
        public virtual Test Testler { get; set; }

    }
}