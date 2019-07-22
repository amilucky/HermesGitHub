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
    public class PromotionsStatusController : Controller
    {
        private Entities db = new Entities();

        // GET: PromotionsStatus
        public ActionResult Index()
        {
            return View(db.PromotionsStatuses.ToList());
        }

        // GET: PromotionsStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsStatus promotionsStatus = db.PromotionsStatuses.Find(id);
            if (promotionsStatus == null)
            {
                return HttpNotFound();
            }
            return View(promotionsStatus);
        }

        // GET: PromotionsStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PromotionsStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] PromotionsStatus promotionsStatus)
        {
            if (ModelState.IsValid)
            {
                db.PromotionsStatuses.Add(promotionsStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(promotionsStatus);
        }

        // GET: PromotionsStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsStatus promotionsStatus = db.PromotionsStatuses.Find(id);
            if (promotionsStatus == null)
            {
                return HttpNotFound();
            }
            return View(promotionsStatus);
        }

        // POST: PromotionsStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] PromotionsStatus promotionsStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionsStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(promotionsStatus);
        }

        // GET: PromotionsStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsStatus promotionsStatus = db.PromotionsStatuses.Find(id);
            if (promotionsStatus == null)
            {
                return HttpNotFound();
            }
            return View(promotionsStatus);
        }

        // POST: PromotionsStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionsStatus promotionsStatus = db.PromotionsStatuses.Find(id);
            db.PromotionsStatuses.Remove(promotionsStatus);
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
