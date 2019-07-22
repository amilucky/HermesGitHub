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
    public class FixedTE_typesController : Controller
    {
        private Entities db = new Entities();

        // GET: FixedTE_types
        public ActionResult Index()
        {
            return View(db.FixedTE_types.ToList());
        }

        // GET: FixedTE_types/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_types fixedTE_types = db.FixedTE_types.Find(id);
            if (fixedTE_types == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE_types);
        }

        // GET: FixedTE_types/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FixedTE_types/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] FixedTE_types fixedTE_types)
        {
            if (ModelState.IsValid)
            {
                db.FixedTE_types.Add(fixedTE_types);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(fixedTE_types);
        }

        // GET: FixedTE_types/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_types fixedTE_types = db.FixedTE_types.Find(id);
            if (fixedTE_types == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE_types);
        }

        // POST: FixedTE_types/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] FixedTE_types fixedTE_types)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixedTE_types).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fixedTE_types);
        }

        // GET: FixedTE_types/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE_types fixedTE_types = db.FixedTE_types.Find(id);
            if (fixedTE_types == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE_types);
        }

        // POST: FixedTE_types/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedTE_types fixedTE_types = db.FixedTE_types.Find(id);
            db.FixedTE_types.Remove(fixedTE_types);
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
