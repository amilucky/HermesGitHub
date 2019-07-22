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
    public class SegmentsController : Controller
    {
        private Entities db = new Entities();

        // GET: Segments
        public ActionResult Index()
        {
            return View(db.Segments.ToList());
        }

        // GET: Segments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = db.Segments.Find(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // GET: Segments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Segments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                db.Segments.Add(segment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(segment);
        }

        // GET: Segments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = db.Segments.Find(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // POST: Segments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Segment segment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(segment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(segment);
        }

        // GET: Segments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Segment segment = db.Segments.Find(id);
            if (segment == null)
            {
                return HttpNotFound();
            }
            return View(segment);
        }

        // POST: Segments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Segment segment = db.Segments.Find(id);
            db.Segments.Remove(segment);
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
