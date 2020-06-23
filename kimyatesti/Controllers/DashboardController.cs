using kimyatesti.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kimyatesti.Controllers
{
    public class DashboardController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();
        // GET: Dashboard
        public ActionResult Index()
        {
            var currentUser = db.OgrenciAvatars.First(i => i.OgrEmail == User.Identity.Name);

            var testler = db.Tests.Where(i => i.testTakip.Any(k => k.Ogrenci.OgrEmail == currentUser.OgrEmail));



            var x1 = db.OgrTestTakips.Where(i => i.Ogrenci.OgrEmail == currentUser.OgrEmail).ToList();
            foreach (var item in x1)
            {
                var x2 = item.Testler.Name;
                var x3 = item.Testler.Aciklama;
                var x4 = item.Testler.Id;
                var x5 = item.TestGroup;
            }

            //return View(testler);
            return View(testler);
        }
    }
}