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
    public class OgrenciAvatarsController : Controller
    {
        private kimyatestiDataContext db = new kimyatestiDataContext();

        // GET: OgrenciAvatars
        public ActionResult Index()
        {
            return View(db.OgrenciAvatars.ToList());
        }

        // GET: OgrenciAvatars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciAvatar ogrenciAvatar = db.OgrenciAvatars.Find(id);
            if (ogrenciAvatar == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciAvatar);
        }

        // GET: OgrenciAvatars/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OgrenciAvatars/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OgrName,OgrEmail,Kurum,MevcutSinif")] OgrenciAvatar ogrenciAvatar)
        {
            if (ModelState.IsValid)
            {
                db.OgrenciAvatars.Add(ogrenciAvatar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ogrenciAvatar);
        }

        // GET: OgrenciAvatars/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciAvatar ogrenciAvatar = db.OgrenciAvatars.Find(id);
            if (ogrenciAvatar == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciAvatar);
        }

        // POST: OgrenciAvatars/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OgrName,OgrEmail,Kurum,MevcutSinif")] OgrenciAvatar ogrenciAvatar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ogrenciAvatar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ogrenciAvatar);
        }

        // GET: OgrenciAvatars/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OgrenciAvatar ogrenciAvatar = db.OgrenciAvatars.Find(id);
            if (ogrenciAvatar == null)
            {
                return HttpNotFound();
            }
            return View(ogrenciAvatar);
        }

        // POST: OgrenciAvatars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OgrenciAvatar ogrenciAvatar = db.OgrenciAvatars.Find(id);
            db.OgrenciAvatars.Remove(ogrenciAvatar);
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
