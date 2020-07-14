using kimyatesti.Data;
using kimyatesti.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kimyatesti.Controllers
{
    public class UtilityController : Controller
    {
        kimyatestiDataContext db = new kimyatestiDataContext();
        // GET: Utility
        public ActionResult Index()
        {
            return View();
        }   
        
        [HttpGet]
        public ActionResult AkordiyonTest(int Id, int Gr)
        {
            ViewBag.data1 = db.Tests.First(i => i.Id == Id).Name;
            var sorular = db.Sorus.Where(i => i.SoruTestBind.Any(k => k.TestId == Id)).ToList();

            return View(sorular);
        }        

        [HttpPost]
        public ActionResult AkordiyonTest(int Id, int Gr, string UserAns)
        {
            var currentUser = db.OgrenciAvatars.First(i => i.OgrEmail == User.Identity.Name);
            var currentUserTestTakip = db.OgrTestTakips.Where(i => i.OgrenciId == currentUser.Id).First(k => k.TestId == Id);
            currentUserTestTakip.CevapList = UserAns;
            currentUserTestTakip.TamamlanmaTarihi = DateTime.Now;
            db.SaveChanges();

            var sorular = db.Sorus.Where(i => i.SoruTestBind.Any(k => k.TestId == Id)).ToList();
            var dogruCevaplar = "";
            foreach (var i in sorular)
            {
                dogruCevaplar += i.Cevap;
            }

            for (int i = 0; i < UserAns.Length; i++)
            {
                var cevapStatus = 0;

                if (UserAns[i] == dogruCevaplar[i])
                {
                    cevapStatus = 1;
                }
                else if (UserAns == "0")
                {
                    cevapStatus = 0;
                }
                else
                {
                    cevapStatus = -1;
                }

                SoruHistory soruHistoryEntry = new SoruHistory();

                soruHistoryEntry.OgrenciId = currentUser.Id;
                soruHistoryEntry.SoruId = sorular[i].Id;
                soruHistoryEntry.TestId = Id;
                soruHistoryEntry.TestGroupId = Gr;
                soruHistoryEntry.YBD = cevapStatus;
                soruHistoryEntry.OgrencininCevabi = UserAns[i].ToString();
                db.SoruHistories.Add(soruHistoryEntry);
                db.SaveChanges();

            }

            //return View();
            return RedirectToAction("AkordiyonTestSonuc", "Utility", new { Id=Id});
        } 

        public ActionResult AkordiyonTestSonuc(int Id)
        {
            var currentUser = db.OgrenciAvatars.First(i => i.OgrEmail == User.Identity.Name);
            var soruHistoryLog = db.SoruHistories.Where(i => i.TestId == Id).ToList().Where(k=>k.OgrenciId == currentUser.Id).ToList();
            var sorular = db.Sorus.Where(i => i.SoruTestBind.Any(k => k.TestId == Id)).ToList();
            var cevapStringFromDb = "";
            var cevapStringCorrect = "";

            foreach (var i in soruHistoryLog)
            {
                cevapStringFromDb += i.OgrencininCevabi;
            }

            foreach (var i in sorular)
            {
                cevapStringCorrect += i.Cevap;
            }

            

            AkordiyonTestSonucViewModel thisModel = new AkordiyonTestSonucViewModel();
            thisModel.Sorus = sorular;
            thisModel.SoruHistoryLog = cevapStringFromDb;
            thisModel.CorrectAnswers = cevapStringCorrect;

            return View(thisModel);
        }
    }
}