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
    public class TestGroupsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: TestGroups
        public ActionResult Index()
        {
            return View(db.TestGroups.ToList());
        }

        // GET: TestGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroup testGroup = db.TestGroups.Find(id);
            if (testGroup == null)
            {
                return HttpNotFound();
            }
            return View(testGroup);
        }

        // GET: TestGroups/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Aciklama")] TestGroup testGroup)
        {
            if (ModelState.IsValid)
            {
                db.TestGroups.Add(testGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testGroup);
        }

        // GET: TestGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroup testGroup = db.TestGroups.Find(id);
            if (testGroup == null)
            {
                return HttpNotFound();
            }
            return View(testGroup);
        }

        // POST: TestGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Aciklama")] TestGroup testGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testGroup);
        }

        // GET: TestGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestGroup testGroup = db.TestGroups.Find(id);
            if (testGroup == null)
            {
                return HttpNotFound();
            }
            return View(testGroup);
        }

        // POST: TestGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestGroup testGroup = db.TestGroups.Find(id);
            db.TestGroups.Remove(testGroup);
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
