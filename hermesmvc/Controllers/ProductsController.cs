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
    public class ProductsController : Controller
    {
        private Entities db = new Entities();

        public ActionResult AddToPromo2(int id)
        {

                if (ModelState.IsValid)
                {
                    PromotionsDetail promotionsDetail = new PromotionsDetail();
                    promotionsDetail.discount_off = 0;
                    promotionsDetail.discount_on = 0;
                    promotionsDetail.edlp = 0;
                    promotionsDetail.product_id = id;
                    promotionsDetail.promotion_id = 1;
                    promotionsDetail.te_off = 0;
                    promotionsDetail.te_on = 0;
                    promotionsDetail.volume = 0;

                    db.PromotionsDetails.Add(promotionsDetail);
                    db.SaveChanges();
                }



            return RedirectToAction("AddToPromo");
            
            


        }

        public ActionResult AddToPromo()
        {
             // 1 version
            var products = db.Products.Include(p => p.PromoGroup);
            return View(products.ToList());
            

            /* //2 version
            
            if (id == null)
            {

            }
            else
            {
                if (ModelState.IsValid)
                {
                    PromotionsDetail promotionsDetail = new PromotionsDetail();
                    promotionsDetail.discount_off = 0;
                    promotionsDetail.discount_on = 0;
                    promotionsDetail.edlp = 0;
                    promotionsDetail.product_id = id;
                    promotionsDetail.promotion_id = 1;
                    promotionsDetail.te_off = 0;
                    promotionsDetail.te_on = 0;
                    promotionsDetail.volume = 0;

                    db.PromotionsDetails.Add(promotionsDetail);
                    db.SaveChanges();
                }

            }

            return RedirectToAction("AddToPromo");
            
            */


        }


        [HttpPost]
        public ActionResult AddToPromo([Bind(Include = "id, internal_code_1")] Product product)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //int id = 1;
            int id_product = product.id;

            if (ModelState.IsValid)
            {
                PromotionsDetail promotionsDetail = new PromotionsDetail();
                promotionsDetail.discount_off = 0;
                promotionsDetail.discount_on = 0;
                promotionsDetail.edlp = 0;
                promotionsDetail.product_id = 1;
                promotionsDetail.promotion_id = 1;
                promotionsDetail.te_off = 0;
                promotionsDetail.te_on = 0;
                promotionsDetail.volume = 0;

                db.PromotionsDetails.Add(promotionsDetail);
                db.SaveChanges();
                //return RedirectToAction("Index");
            }

            return RedirectToAction("AddToPromo");

            //ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", promotionsDetail.product_id);
            //ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name", promotionsDetail.promotion_id);
            //return View(promotionsDetail);
            //var products = db.Products.Include(p => p.PromoGroup);
            //return View(products.ToList());
        }


        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.PromoGroup);
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.promogroup_id = new SelectList(db.PromoGroups, "id", "name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,internal_code_1,internal_code_2,ean,deactive,promogroup_id,size,name,casecount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.promogroup_id = new SelectList(db.PromoGroups, "id", "name", product.promogroup_id);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.promogroup_id = new SelectList(db.PromoGroups, "id", "name", product.promogroup_id);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,internal_code_1,internal_code_2,ean,deactive,promogroup_id,size,name,casecount")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.promogroup_id = new SelectList(db.PromoGroups, "id", "name", product.promogroup_id);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
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
