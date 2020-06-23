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
    public class OgrTestTakipsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: OgrTestTakips
        public ActionResult Index()
        {
            var ogrTestTakips = db.OgrTestTakips.Include(o => o.Ogrenci).Include(o => o.TestGroup).Include(o => o.Testler);
            return View(ogrTestTakips.ToList());
        }

        // GET: OgrTestTakips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrTestTakip ogrTestTakip = db.OgrTestTakips.Find(id);
            if (ogrTestTakip == null)
            {
                return HttpNotFound();
            }
            return View(ogrTestTakip);
        }

        // GET: OgrTestTakips/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName");
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name");
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name");
            return View();
        }

        // POST: OgrTestTakips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OgrenciId,TestGroupId,TestId,AtanmaTarihi,TamamlanmaTarihi,CevapList")] OgrTestTakip ogrTestTakip)
        {
            if (ModelState.IsValid)
            {
                db.OgrTestTakips.Add(ogrTestTakip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrTestTakip.OgrenciId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrTestTakip.TestGroupId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", ogrTestTakip.TestId);
            return View(ogrTestTakip);
        }

        // GET: OgrTestTakips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrTestTakip ogrTestTakip = db.OgrTestTakips.Find(id);
            if (ogrTestTakip == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrTestTakip.OgrenciId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrTestTakip.TestGroupId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", ogrTestTakip.TestId);
            return View(ogrTestTakip);
        }

        // POST: OgrTestTakips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OgrenciId,TestGroupId,TestId,AtanmaTarihi,TamamlanmaTarihi,CevapList")] OgrTestTakip ogrTestTakip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrTestTakip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrTestTakip.OgrenciId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrTestTakip.TestGroupId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", ogrTestTakip.TestId);
            return View(ogrTestTakip);
        }

        // GET: OgrTestTakips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrTestTakip ogrTestTakip = db.OgrTestTakips.Find(id);
            if (ogrTestTakip == null)
            {
                return HttpNotFound();
            }
            return View(ogrTestTakip);
        }

        // POST: OgrTestTakips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgrTestTakip ogrTestTakip = db.OgrTestTakips.Find(id);
            db.OgrTestTakips.Remove(ogrTestTakip);
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
