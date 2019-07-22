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
    public class FixedTE_detailsController : Controller
    {
        private Entities db = new Entities();

        // GET: FixedTE_details
        public ActionResult Index()
        {
            var fixedTE_details = db.FixedTE_details.Include(f => f.FixedTE_types);
            return View(fixedTE_details.ToList());
        }

        // GET: FixedTE_details/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_details fixedTE_details = db.FixedTE_details.Find(id);
            if (fixedTE_details == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE_details);
        }

        // GET: FixedTE_details/Create
        public ActionResult Create()
        {
            ViewBag.type_id = new SelectList(db.FixedTE_types, "id", "name");
            return View();
        }

        // POST: FixedTE_details/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,type_id")] FixedTE_details fixedTE_details)
        {
            if (ModelState.IsValid)
            {
                db.FixedTE_details.Add(fixedTE_details);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.type_id = new SelectList(db.FixedTE_types, "id", "name", fixedTE_details.type_id);
            return View(fixedTE_details);
        }

        // GET: FixedTE_details/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_details fixedTE_details = db.FixedTE_details.Find(id);
            if (fixedTE_details == null)
            {
                return HttpNotFound();
            }
            ViewBag.type_id = new SelectList(db.FixedTE_types, "id", "name", fixedTE_details.type_id);
            return View(fixedTE_details);
        }

        // POST: FixedTE_details/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,type_id")] FixedTE_details fixedTE_details)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixedTE_details).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.type_id = new SelectList(db.FixedTE_types, "id", "name", fixedTE_details.type_id);
            return View(fixedTE_details);
        }

        // GET: FixedTE_details/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_details fixedTE_details = db.FixedTE_details.Find(id);
            if (fixedTE_details == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE_details);
        }

        // POST: FixedTE_details/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedTE_details fixedTE_details = db.FixedTE_details.Find(id);
            db.FixedTE_details.Remove(fixedTE_details);
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
