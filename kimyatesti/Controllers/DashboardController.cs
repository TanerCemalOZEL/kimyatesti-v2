using kimyatesti.Data;
using kimyatesti.Models;
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

            //var testler = db.Tests.Where(i => i.testTakip.Any(k => k.Ogrenci.OgrEmail == currentUser.OgrEmail));


            List<dashboardViewModel> testList = new List<dashboardViewModel>();

            var x1 = db.OgrTestTakips.Where(i => i.Ogrenci.OgrEmail == currentUser.OgrEmail).ToList().Where(k => k.TamamlanmaTarihi == null).ToList();
            foreach (var item in x1)
            {
                dashboardViewModel testX = new dashboardViewModel();
                testX.testId = item.Testler.Id;
                testX.testAdi = item.Testler.Name;
                testX.testAciklamasi = item.Testler.Aciklama;
                testX.testGroup = item.TestGroup.Name;
                testX.testGroupId = item.TestGroupId;
                testX.tarih = item.AtanmaTarihi;
                testList.Add(testX);
            }

            if (x1.Count == 0)
            {
                dashboardViewModel testX = new dashboardViewModel();
                testX.testAdi = "";
                testX.testAciklamasi = "Bu grupta çözülmemiş testiniz bulunmuyor.";
                testList.Add(testX);
            }

            SummaryViewModel summary = new SummaryViewModel();
            summary.SoruCozdun = db.SoruHistories.Where(i => i.OgrenciId == currentUser.Id).Count();
            summary.TestCozdun = db.OgrTestTakips.Where(i => i.OgrenciId == currentUser.Id).ToList().Where(k => k.TamamlanmaTarihi != null).Count();
            summary.TestBekliyor = db.OgrTestTakips.Where(i => i.OgrenciId == currentUser.Id).ToList().Where(k => k.TamamlanmaTarihi == null).Count();

            ViewBag.Summary = summary;

            return View(testList);
        }
    }
}