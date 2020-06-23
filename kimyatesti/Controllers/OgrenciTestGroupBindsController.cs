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
    public class OgrenciTestGroupBindsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: OgrenciTestGroupBinds
        public ActionResult Index()
        {
            var ogrenciTestGroupBinds = db.OgrenciTestGroupBinds.Include(o => o.Ogrenci).Include(o => o.TestGroups);
            return View(ogrenciTestGroupBinds.ToList());
        }

        // GET: OgrenciTestGroupBinds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciTestGroupBind ogrenciTestGroupBind = db.OgrenciTestGroupBinds.Find(id);
            if (ogrenciTestGroupBind == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciTestGroupBind);
        }

        // GET: OgrenciTestGroupBinds/Create
        public ActionResult Create()
        {
            ViewBag.OgrenciAvatarId = new SelectList(db.OgrenciAvatars, "Id", "OgrName");
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name");
            return View();
        }

        // POST: OgrenciTestGroupBinds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TestGroupId,OgrenciAvatarId")] OgrenciTestGroupBind ogrenciTestGroupBind)
        {
            if (ModelState.IsValid)
            {
                db.OgrenciTestGroupBinds.Add(ogrenciTestGroupBind);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OgrenciAvatarId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrenciTestGroupBind.OgrenciAvatarId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrenciTestGroupBind.TestGroupId);
            return View(ogrenciTestGroupBind);
        }

        // GET: OgrenciTestGroupBinds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciTestGroupBind ogrenciTestGroupBind = db.OgrenciTestGroupBinds.Find(id);
            if (ogrenciTestGroupBind == null)
            {
                return HttpNotFound();
            }
            ViewBag.OgrenciAvatarId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrenciTestGroupBind.OgrenciAvatarId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrenciTestGroupBind.TestGroupId);
            return View(ogrenciTestGroupBind);
        }

        // POST: OgrenciTestGroupBinds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TestGroupId,OgrenciAvatarId")] OgrenciTestGroupBind ogrenciTestGroupBind)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenciTestGroupBind).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OgrenciAvatarId = new SelectList(db.OgrenciAvatars, "Id", "OgrName", ogrenciTestGroupBind.OgrenciAvatarId);
            ViewBag.TestGroupId = new SelectList(db.TestGroups, "Id", "Name", ogrenciTestGroupBind.TestGroupId);
            return View(ogrenciTestGroupBind);
        }

        // GET: OgrenciTestGroupBinds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciTestGroupBind ogrenciTestGroupBind = db.OgrenciTestGroupBinds.Find(id);
            if (ogrenciTestGroupBind == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciTestGroupBind);
        }

        // POST: OgrenciTestGroupBinds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgrenciTestGroupBind ogrenciTestGroupBind = db.OgrenciTestGroupBinds.Find(id);
            db.OgrenciTestGroupBinds.Remove(ogrenciTestGroupBind);
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
