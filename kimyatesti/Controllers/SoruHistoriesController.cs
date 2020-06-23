using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using kimyatesti.Data;
using kimyatesti.Models;

namespace kimyatesti.Controllers
{
    public class SoruHistoriesController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: SoruHistories
        public ActionResult Index()
        {
            var soruHistories = db.SoruHistories.Include(s => s.Ogrenci).Include(s => s.Soru).Include(s => s.TestGroup);
            return View(soruHistories.ToList());
        }

        // GET: SoruHistories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruHistory soruHistory = db.SoruHistories.Find(id);
            if (soruHistory == null)
            {
                return HttpNotFound();
            }
            return View(soruHistory);
        }

        // GET: SoruHistories/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName");
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath");
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name");
            return View();
        }

        // POST: SoruHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestGroupId,SoruId,OgrenciId,YBD,OgrencininCevabi")] SoruHistory soruHistory)
        {
            if (ModelState.IsValid)
            {
                db.SoruHistories.Add(soruHistory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", soruHistory.OgrenciId);
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruHistory.SoruId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", soruHistory.TestGroupId);
            return View(soruHistory);
        }

        // GET: SoruHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruHistory soruHistory = db.SoruHistories.Find(id);
            if (soruHistory == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", soruHistory.OgrenciId);
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruHistory.SoruId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", soruHistory.TestGroupId);
            return View(soruHistory);
        }

        // POST: SoruHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestGroupId,SoruId,OgrenciId,YBD,OgrencininCevabi")] SoruHistory soruHistory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soruHistory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", soruHistory.OgrenciId);
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruHistory.SoruId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", soruHistory.TestGroupId);
            return View(soruHistory);
        }

        // GET: SoruHistories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruHistory soruHistory = db.SoruHistories.Find(id);
            if (soruHistory == null)
            {
                return HttpNotFound();
            }
            return View(soruHistory);
        }

        // POST: SoruHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SoruHistory soruHistory = db.SoruHistories.Find(id);
            db.SoruHistories.Remove(soruHistory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
