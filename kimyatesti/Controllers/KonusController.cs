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
    public class KonusController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: Konus
        public ActionResult Index()
        {
            var konus = db.Konus.Include(k => k.Unite);
            return View(konus.ToList());
        }

        // GET: Konus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Konu konu = db.Konus.Find(id);
            if (konu == null)
            {
                return HttpNotFound();
            }
            return View(konu);
        }

        // GET: Konus/Create
        public ActionResult Create()
        {
            ViewBag.UniteId = new SelectList(db.Unites, "Id", "Name");
            return View();
        }

        // POST: Konus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UniteId")] Konu konu)
        {
            if (ModelState.IsValid)
            {
                db.Konus.Add(konu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UniteId = new SelectList(db.Unites, "Id", "Name", konu.UniteId);
            return View(konu);
        }

        // GET: Konus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Konu konu = db.Konus.Find(id);
            if (konu == null)
            {
                return HttpNotFound();
            }
            ViewBag.UniteId = new SelectList(db.Unites, "Id", "Name", konu.UniteId);
            return View(konu);
        }

        // POST: Konus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,UniteId")] Konu konu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(konu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UniteId = new SelectList(db.Unites, "Id", "Name", konu.UniteId);
            return View(konu);
        }

        // GET: Konus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Konu konu = db.Konus.Find(id);
            if (konu == null)
            {
                return HttpNotFound();
            }
            return View(konu);
        }

        // POST: Konus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Konu konu = db.Konus.Find(id);
            db.Konus.Remove(konu);
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
