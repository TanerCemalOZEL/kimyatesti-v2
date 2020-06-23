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
    public class SoruTestBindsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: SoruTestBinds
        public ActionResult Index()
        {
            var soruTestBinds = db.SoruTestBinds.Include(s => s.Soru).Include(s => s.Test);
            return View(soruTestBinds.ToList());
        }

        // GET: SoruTestBinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruTestBind soruTestBind = db.SoruTestBinds.Find(id);
            if (soruTestBind == null)
            {
                return HttpNotFound();
            }
            return View(soruTestBind);
        }

        // GET: SoruTestBinds/Create
        public ActionResult Create()
        {
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath");
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name");
            return View();
        }

        // POST: SoruTestBinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,SoruId,TestId")] SoruTestBind soruTestBind)
        {
            if (ModelState.IsValid)
            {
                db.SoruTestBinds.Add(soruTestBind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruTestBind.SoruId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", soruTestBind.TestId);
            return View(soruTestBind);
        }

        // GET: SoruTestBinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruTestBind soruTestBind = db.SoruTestBinds.Find(id);
            if (soruTestBind == null)
            {
                return HttpNotFound();
            }
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruTestBind.SoruId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", soruTestBind.TestId);
            return View(soruTestBind);
        }

        // POST: SoruTestBinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,SoruId,TestId")] SoruTestBind soruTestBind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(soruTestBind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SoruId = new SelectList(db.Sorus, "Id", "ImgPath", soruTestBind.SoruId);
            ViewBag.TestId = new SelectList(db.Tests, "Id", "Name", soruTestBind.TestId);
            return View(soruTestBind);
        }

        // GET: SoruTestBinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SoruTestBind soruTestBind = db.SoruTestBinds.Find(id);
            if (soruTestBind == null)
            {
                return HttpNotFound();
            }
            return View(soruTestBind);
        }

        // POST: SoruTestBinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SoruTestBind soruTestBind = db.SoruTestBinds.Find(id);
            db.SoruTestBinds.Remove(soruTestBind);
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
