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
    public class TestGroupBindsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: TestGroupBinds
        public ActionResult Index()
        {
            var testGroupBinds = db.TestGroupBinds.Include(t => t.Test).Include(t => t.TestGroup);
            return View(testGroupBinds.ToList());
        }

        // GET: TestGroupBinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroupBind testGroupBind = db.TestGroupBinds.Find(id);
            if (testGroupBind == null)
            {
                return HttpNotFound();
            }
            return View(testGroupBind);
        }

        // GET: TestGroupBinds/Create
        public ActionResult Create()
        {
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name");
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name");
            return View();
        }

        // POST: TestGroupBinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestId,TestGroupId")] TestGroupBind testGroupBind)
        {
            if (ModelState.IsValid)
            {
                db.TestGroupBinds.Add(testGroupBind);
                db.SaveChanges();

                var atanacakTestGrubu = testGroupBind.TestGroupId;
                var atanacakOgrenciler = db.OgrenciAvatars.Where(k => k.TestGroups.Any(c => c.TestGroupId == atanacakTestGrubu)).ToList();

                foreach (var ogrenci in atanacakOgrenciler)
                {
                    OgrTestTakip ogrTestTakip = new OgrTestTakip();
                    ogrTestTakip.OgrenciId = ogrenci.Id;
                    ogrTestTakip.TestGroupId = testGroupBind.TestGroupId;
                    ogrTestTakip.TestId = testGroupBind.TestId;
                    ogrTestTakip.AtanmaTarihi = DateTime.Now;
                    db.OgrTestTakips.Add(ogrTestTakip);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", testGroupBind.TestId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", testGroupBind.TestGroupId);
            return View(testGroupBind);
        }

        // GET: TestGroupBinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroupBind testGroupBind = db.TestGroupBinds.Find(id);
            if (testGroupBind == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", testGroupBind.TestId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", testGroupBind.TestGroupId);
            return View(testGroupBind);
        }

        // POST: TestGroupBinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestId,TestGroupId")] TestGroupBind testGroupBind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testGroupBind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", testGroupBind.TestId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", testGroupBind.TestGroupId);
            return View(testGroupBind);
        }

        // GET: TestGroupBinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroupBind testGroupBind = db.TestGroupBinds.Find(id);
            if (testGroupBind == null)
            {
                return HttpNotFound();
            }
            return View(testGroupBind);
        }

        // POST: TestGroupBinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestGroupBind testGroupBind = db.TestGroupBinds.Find(id);
            db.TestGroupBinds.Remove(testGroupBind);
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
