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
    public class UnitesController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: Unites
        public ActionResult Index()
        {
            var unites = db.Unites.Include(u => u.Sinif);
            return View(unites.ToList());
        }

        // GET: Unites/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unites.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            return View(unite);
        }

        // GET: Unites/Create
        public ActionResult Create()
        {
            ViewBag.SinifId = new SelectList(db.Sinifs, "Id", "Name");
            return View();
        }

        // POST: Unites/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,SinifId")] Unite unite)
        {
            if (ModelState.IsValid)
            {
                db.Unites.Add(unite);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SinifId = new SelectList(db.Sinifs, "Id", "Name", unite.SinifId);
            return View(unite);
        }

        // GET: Unites/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unites.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            ViewBag.SinifId = new SelectList(db.Sinifs, "Id", "Name", unite.SinifId);
            return View(unite);
        }

        // POST: Unites/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,SinifId")] Unite unite)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unite).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SinifId = new SelectList(db.Sinifs, "Id", "Name", unite.SinifId);
            return View(unite);
        }

        // GET: Unites/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Unite unite = db.Unites.Find(id);
            if (unite == null)
            {
                return HttpNotFound();
            }
            return View(unite);
        }

        // POST: Unites/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Unite unite = db.Unites.Find(id);
            db.Unites.Remove(unite);
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
