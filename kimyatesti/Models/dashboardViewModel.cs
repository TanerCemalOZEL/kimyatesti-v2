using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class dashboardViewModel
    {
        public int testId { get; set; }
        public string testAciklamasi { get; set; }
        public string testAdi { get; set; }
        public int testGroupId { get; set; }
        public string testGroup { get; set; }
        public DateTime tarih { get; set; }
    }
}