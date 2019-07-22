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
    public class ProductsPricesController : Controller
    {
        private Entities db = new Entities();

        // GET: ProductsPrices
        public ActionResult Index()
        {
            var productsPrices = db.ProductsPrices.Include(p => p.Currency).Include(p => p.Product);
            return View(productsPrices.ToList());
        }

        // GET: ProductsPrices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsPrice productsPrice = db.ProductsPrices.Find(id);
            if (productsPrice == null)
            {
                return HttpNotFound();
            }
            return View(productsPrice);
        }

        // GET: ProductsPrices/Create
        public ActionResult Create()
        {
            ViewBag.currency_id = new SelectList(db.Currencies, "id", "name");
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1");
            return View();
        }

        // POST: ProductsPrices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,product_id,date_from,date_to,currency_id,gsv,pc,cc")] ProductsPrice productsPrice)
        {
            if (ModelState.IsValid)
            {
                db.ProductsPrices.Add(productsPrice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.currency_id = new SelectList(db.Currencies, "id", "name", productsPrice.currency_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", productsPrice.product_id);
            return View(productsPrice);
        }

        // GET: ProductsPrices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsPrice productsPrice = db.ProductsPrices.Find(id);
            if (productsPrice == null)
            {
                return HttpNotFound();
            }
            ViewBag.currency_id = new SelectList(db.Currencies, "id", "name", productsPrice.currency_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", productsPrice.product_id);
            return View(productsPrice);
        }

        // POST: ProductsPrices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,product_id,date_from,date_to,currency_id,gsv,pc,cc")] ProductsPrice productsPrice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productsPrice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.currency_id = new SelectList(db.Currencies, "id", "name", productsPrice.currency_id);
            ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", productsPrice.product_id);
            return View(productsPrice);
        }

        // GET: ProductsPrices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductsPrice productsPrice = db.ProductsPrices.Find(id);
            if (productsPrice == null)
            {
                return HttpNotFound();
            }
            return View(productsPrice);
        }

        // POST: ProductsPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductsPrice productsPrice = db.ProductsPrices.Find(id);
            db.ProductsPrices.Remove(productsPrice);
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
