using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace kimyatesti.Data
{
    public class kimyatestiDataContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        //public kimyatestiDataContext() : base("name=db17_kimyatesti")
        public kimyatestiDataContext() : base("name=kimyatestiDataContext")
        {
        }

        public System.Data.Entity.DbSet<kimyatesti.Models.Sinif> Sinifs { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.Unite> Unites { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.Konu> Konus { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.Soru> Sorus { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.Test> Tests { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.SoruTestBind> SoruTestBinds { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.TestGroup> TestGroups { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.TestGroupBind> TestGroupBinds { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.OgrenciAvatar> OgrenciAvatars { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.OgrenciTestGroupBind> OgrenciTestGroupBinds { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.OgrTestTakip> OgrTestTakips { get; set; }

        public System.Data.Entity.DbSet<kimyatesti.Models.SoruHistory> SoruHistories { get; set; }
    }
}
