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
    public class PromotionsController : Controller
    {
        private Entities db = new Entities();

        //------

        public ActionResult DeleteItem(int id, int promoid)
        {
            PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            db.PromotionsDetails.Remove(promotionsDetail);
            db.SaveChanges();
            return RedirectToAction("Edit", new { id = promoid});
        }

        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                id = 1;
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }


            return View(promotion);
        }

        // GET: Promotions/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            Promotion promotion = db.Promotions.Find(id);
            //Promotion promotion = db.Promotions.Include(u => u.PromotionsDetails).SingleOrDefault(u => u.promotype_id == id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            //PromotionsDetail promotionsDetail = db.PromotionsDetails.Find(id);
            //if (promotionsDetail == null)
            //{
            //    return HttpNotFound();
            //}



            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", promotion.customer_id);
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name", promotion.promostatus_id);
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name", promotion.promotype_id);
            //ViewBag.product_id = new SelectList(db.Products, "id", "internal_code_1", promotionsDetail.product_id);
            //ViewBag.promotion_id = new SelectList(db.Promotions, "id", "promo_name", promotionsDetail.promotion_id);
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,promostatus_id,shipment_from,shipment_to,active_from,active_to,promo_name,customer_id,edit_date,note,week1,week2,week3,week4,leaflet_fee,secondaryplacement_fee,promotype_id,PromotionsDetails")] Promotion promotion) //, ICollection<PromotionsDetail>  promotionsDetail
        {
            if (ModelState.IsValid)
            {
                //db.Entry(promotion).State = EntityState.Modified;
                foreach (var item in promotion.PromotionsDetails)
                {
                    item.Product.id = item.product_id;
                    db.Entry(item).State = EntityState.Modified;
                }
                db.SaveChanges();
                db.Entry(promotion).State = EntityState.Modified;
                //promotion.PromotionsDetails.ForEach(p => db.Entry(p).State = EntityState.Modified);
                //db.Entry(promotion.PromotionsDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", promotion.customer_id);
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name", promotion.promostatus_id);
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name", promotion.promotype_id);

            return View(promotion);
        }
        //------
        [HttpGet]
        public PartialViewResult AddProducts(int? id, FormCollection form)
        {            
            var getsegmentlist = db.Segments.ToList();
            getsegmentlist.Insert(0, new Segment { name = "All", id = 0 });
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;
            ViewBag.Id = id;

            int intDDL_Segment = 0;
            if (form["SegmentList"] != null)
            {
                intDDL_Segment = Convert.ToInt32(form["SegmentList"].ToString());
            }
            

            
            var promotion = db.Promotions.Find(id);
            var pds = new List<PromotionsDetail>();

            if (intDDL_Segment == 0)
            {
                foreach (var item in db.Products)
                {
                    PromotionsDetail pd = new PromotionsDetail();
                    pd.product_id = item.id;
                    pd.Product = item;
                    pd.Promotion = promotion;
                    pd.promotion_id = promotion.id;
                    pds.Add(pd);
                }
            }
            else
            {
                foreach (var item in db.Products)
                {
                    if (item.PromoGroup.segment_id == intDDL_Segment)
                    {
                        PromotionsDetail pd = new PromotionsDetail();
                        pd.product_id = item.id;
                        pd.Product = item;
                        pd.Promotion = promotion;
                        pd.promotion_id = promotion.id;
                        pds.Add(pd);
                    }                    
                }
            }
            

            return PartialView(pds.ToList());
        }

        
        public PartialViewResult SearchProducts(int promotion_id, FormCollection form)
        {
            
            var getsegmentlist = db.Segments.ToList();
            getsegmentlist.Insert(0, new Segment { name = "All", id = 0 });
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;
            ViewBag.Id = promotion_id;

            //int intDDL_Segment = segmentId;
            int intDDL_Segment = 0;
            if (form["SegmentList"] != null)
            {
                intDDL_Segment = Convert.ToInt32(form["SegmentList"].ToString());
            }


            //int intDDL_Segment = segmentId;

            var promotion = db.Promotions.Find(promotion_id);
            var pds = new List<PromotionsDetail>();

            if (intDDL_Segment == 0)
            {
                foreach (var item in db.Products)
                {
                    PromotionsDetail pd = new PromotionsDetail();
                    pd.product_id = item.id;
                    pd.Product = item;
                    pd.Promotion = promotion;
                    pd.promotion_id = promotion.id;
                    pds.Add(pd);
                }
            }
            else
            {
                foreach (var item in db.Products.Where(p => p.PromoGroup.segment_id == intDDL_Segment))
                {
                    
                        PromotionsDetail pd = new PromotionsDetail();
                        pd.product_id = item.id;
                        pd.Product = item;
                        pd.Promotion = promotion;
                        pd.promotion_id = promotion.id;
                        pds.Add(pd);
                    
                }
            }            

            return PartialView("AddProducts", pds.ToList());
        }

        //public class ProductRequest
        //{
        //    public string[] Params { get; set; }
        //    public int Id { get; set; }
        //}

        
        public ActionResult AddProduct(int? id, int pid)
        {
            
            //foreach (var item in checkbox)
            //{
            //    products.Add(db.Products.Find(item));
            //}
            var promotion = db.Promotions.Find(id);
            PromotionsDetail detail = new PromotionsDetail();
            Product product = db.Products.Find(pid);

            detail.Product = product;
            detail.product_id = product.id;
            detail.promotion_id = promotion.id;
            db.PromotionsDetails.Add(detail);
            promotion.PromotionsDetails.Add(detail);
            id = promotion.id;
            db.SaveChanges();
            return RedirectToAction("Edit", "Promotions", new { id });
            
            
        }


        public ActionResult ProductsPartial(Promotion promotion)
        {
            //var products = new List<Product>();
            //foreach (var item in promotion.PromotionsDetails)
            //{
            //    products.Add(item.Product);
            //}
            return PartialView(promotion);
        }

        // GET: Promotions
        public ActionResult Index()
        {
            var promotions = db.Promotions.Include(p => p.Customer).Include(p => p.PromotionsStatus).Include(p => p.PromotionsType);
            return View(promotions.ToList());
        }

        // GET: Promotions/Details/5
        public ActionResult Details_init(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // GET: Promotions/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name");
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name");
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name");
            return View();
        }

        // POST: Promotions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,promostatus_id,shipment_from,shipment_to,active_from,active_to,promo_name,customer_id,edit_date,note,week1,week2,week3,week4,leaflet_fee,secondaryplacement_fee,promotype_id")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(promotion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", promotion.customer_id);
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name", promotion.promostatus_id);
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name", promotion.promotype_id);
            return View(promotion);
        }

        // GET: Promotions/Edit/5
        public ActionResult Edit_init(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", promotion.customer_id);
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name", promotion.promostatus_id);
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name", promotion.promotype_id);
            return View(promotion);
        }

        // POST: Promotions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit_init([Bind(Include = "id,promostatus_id,shipment_from,shipment_to,active_from,active_to,promo_name,customer_id,edit_date,note,week1,week2,week3,week4,leaflet_fee,secondaryplacement_fee,promotype_id")] Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", promotion.customer_id);
            ViewBag.promostatus_id = new SelectList(db.PromotionsStatuses, "id", "name", promotion.promostatus_id);
            ViewBag.promotype_id = new SelectList(db.PromotionsTypes, "id", "name", promotion.promotype_id);
            return View(promotion);
        }

        // GET: Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotion promotion = db.Promotions.Find(id);
            if (promotion == null)
            {
                return HttpNotFound();
            }
            return View(promotion);
        }

        // POST: Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotion promotion = db.Promotions.Find(id);
            db.Promotions.Remove(promotion);
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
