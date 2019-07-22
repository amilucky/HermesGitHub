using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hermesmvc.Models;

namespace hermesmvc.Controllers
{
    public class PromoGroupsController : Controller
    {
        private Entities db = new Entities();

        // GET: PromoGroups
        public ActionResult Index()
        {
            var promoGroups = db.PromoGroups.Include(p => p.Brand).Include(p => p.Segment).Include(p => p.Technology);
            return View(promoGroups.ToList());
        }

        // GET: PromoGroups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoGroup promoGroup = db.PromoGroups.Find(id);
            if (promoGroup == null)
            {
                return HttpNotFound();
            }
            return View(promoGroup);
        }

        // GET: PromoGroups/Create
        public ActionResult Create()
        {
            ViewBag.brand_id = new SelectList(db.Brands, "id", "name");
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name");
            ViewBag.technology_id = new SelectList(db.Technologies, "id", "name");
            return View();
        }

        // POST: PromoGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,segment_id,brand_id,technology_id,cc,dph")] PromoGroup promoGroup)
        {
            if (ModelState.IsValid)
            {
                db.PromoGroups.Add(promoGroup);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.brand_id = new SelectList(db.Brands, "id", "name", promoGroup.brand_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", promoGroup.segment_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "id", "name", promoGroup.technology_id);
            return View(promoGroup);
        }

        // GET: PromoGroups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoGroup promoGroup = db.PromoGroups.Find(id);
            if (promoGroup == null)
            {
                return HttpNotFound();
            }
            ViewBag.brand_id = new SelectList(db.Brands, "id", "name", promoGroup.brand_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", promoGroup.segment_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "id", "name", promoGroup.technology_id);
            return View(promoGroup);
        }

        // POST: PromoGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,segment_id,brand_id,technology_id,cc,dph")] PromoGroup promoGroup)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promoGroup).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.brand_id = new SelectList(db.Brands, "id", "name", promoGroup.brand_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", promoGroup.segment_id);
            ViewBag.technology_id = new SelectList(db.Technologies, "id", "name", promoGroup.technology_id);
            return View(promoGroup);
        }

        // GET: PromoGroups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromoGroup promoGroup = db.PromoGroups.Find(id);
            if (promoGroup == null)
            {
                return HttpNotFound();
            }
            return View(promoGroup);
        }

        // POST: PromoGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromoGroup promoGroup = db.PromoGroups.Find(id);
            db.PromoGroups.Remove(promoGroup);
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
