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
        public ActionResult AkordiyonTest(int?Id)
        {
            ViewBag.data1 = db.Tests.First(i => i.Id == Id).Name;
            var sorular = db.Sorus.Where(i => i.SoruTestBind.Any(k => k.TestId == Id)).ToList();

            return View(sorular);
        }        

        [HttpPost]
        public ActionResult AkordiyonTest(int?Id, string UserAns)
        {
            var currentUser = db.OgrenciAvatars.First(i => i.OgrEmail == User.Identity.Name);
            db.OgrTestTakips.Where(i => i.OgrenciId == currentUser.Id).First(k => k.TestId == Id).CevapList = UserAns;
            db.SaveChanges();

            //return View();
            return RedirectToAction("index", "dashboard");
        }

        [HttpGet]
        public PartialViewResult Accordion5(int?Id)
        {
            var sorular = db.Sorus.Where(i => i.SoruTestBind.Any(k => k.TestId == Id)).ToList();
            return PartialView(sorular);
        }        
    }
}