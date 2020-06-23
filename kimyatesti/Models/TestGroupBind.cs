using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kimyatesti.Models
{
    public class TestGroupBind
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public int TestGroupId { get; set; }

        public virtual Test Test { get; set; }
        public virtual TestGroup TestGroup { get; set; }
    }
}