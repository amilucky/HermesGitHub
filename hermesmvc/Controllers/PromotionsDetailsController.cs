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
    public class PromotionsDetailsController : Controller
    {
        private Entities db = new Entities();

        //-----------------
        public ActionResult TestCreate()
        {
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1");
            ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name");
            return View();
        }

        public ActionResult Test()
        {
            var promotionsDetails = db.PromotionsDetails.Include(p => p.Product).Include(p => p.Promotion).Where(p => p.Promotion.id == 1);

            return View(promotionsDetails.ToList());
        }

        public ActionResult TestDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            if (promotionsDetail == null)
            {
                return HttpNotFound();
            }
            return View(promotionsDetail);
        }
        //------------------



        // GET: PromotionsDetails
        public ActionResult Index()
        {
            var promotionsDetails = db.PromotionsDetails.Include(p => p.Product).Include(p => p.Promotion);
            return View(promotionsDetails.ToList());
        }

        // GET: PromotionsDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            if (promotionsDetail == null)
            {
                return HttpNotFound();
            }
            return View(promotionsDetail);
        }

        // GET: PromotionsDetails/Create
        public ActionResult Create()
        {
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1");
            ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name");
            return View();
        }

        // POST: PromotionsDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,promotion_id,volume,discount_on,discount_off")] PromotionsDetail promotionsDetail)
        {
            if (ModelState.IsValid)
            {
                db.PromotionsDetails.Add(promotionsDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", promotionsDetail.product_id);
            ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name", promotionsDetail.promotion_id);
            return View(promotionsDetail);
        }

        // GET: PromotionsDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            if (promotionsDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", promotionsDetail.product_id);
            ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name", promotionsDetail.promotion_id);
            return View(promotionsDetail);
        }

        // POST: PromotionsDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,promotion_id,volume,discount_on,discount_off")] PromotionsDetail promotionsDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotionsDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", promotionsDetail.product_id);
            ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name", promotionsDetail.promotion_id);
            return View(promotionsDetail);
        }

        // GET: PromotionsDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            if (promotionsDetail == null)
            {
                return HttpNotFound();
            }
            return View(promotionsDetail);
        }

        // POST: PromotionsDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            db.PromotionsDetails.Remove(promotionsDetail);
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
