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
    public class SinifsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: Sinifs
        public ActionResult Index()
        {
            return View(db.Sinifs.ToList());
        }

        // GET: Sinifs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinif sinif = db.Sinifs.Find(id);
            if (sinif == null)
            {
                return HttpNotFound();
            }
            return View(sinif);
        }

        // GET: Sinifs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sinifs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Sinif sinif)
        {
            if (ModelState.IsValid)
            {
                db.Sinifs.Add(sinif);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sinif);
        }

        // GET: Sinifs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinif sinif = db.Sinifs.Find(id);
            if (sinif == null)
            {
                return HttpNotFound();
            }
            return View(sinif);
        }

        // POST: Sinifs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Sinif sinif)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sinif).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sinif);
        }

        // GET: Sinifs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sinif sinif = db.Sinifs.Find(id);
            if (sinif == null)
            {
                return HttpNotFound();
            }
            return View(sinif);
        }

        // POST: Sinifs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sinif sinif = db.Sinifs.Find(id);
            db.Sinifs.Remove(sinif);
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
