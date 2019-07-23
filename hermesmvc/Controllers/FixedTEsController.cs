using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using hermesmvc.Models;
using hermesmvc.ViewModels;

namespace hermesmvc.Controllers
{
    public class FixedTEsController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index()
        {
            //test comment
            var getcustomerlist = db.Customers.ToList();
            getcustomerlist.Insert(0, new Customer { name = "All", id = 0 });
            SelectList list_customer = new SelectList(getcustomerlist, "id", "name");              
            ViewBag.CustomerListName = list_customer;

            var getsegmentlist = db.Segments.ToList();
            getsegmentlist.Insert(0, new Segment { name = "All", id = 0 });
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;

            List<FixedTEViewModel> FixedTElist = new List<FixedTEViewModel>();

            var fixedTEs = db.FixedTEs
                        .OrderBy(m => m.segment_id)
                        .GroupBy(m => new {  m.year,  m.Segment, m.Customer }) //m.FixedTE_details.FixedTE_types,
                        .Select(g => new {
                            segment_id = g.Key.Segment.id,
                            segment_name = g.Key.Segment.name,
                            customer_id = g.Key.Customer.id,
                            customer_name = g.Key.Customer.name,
                            year = g.Key.year,
                            value = g.Sum(x => x.value),
                            on_invoice = g.Sum(x => x.FixedTE_details.FixedTE_types.id == 1 ? x.value : 0),
                            off_invoice = g.Sum(x => x.FixedTE_details.FixedTE_types.id == 2 ? x.value : 0)
                        });

            foreach (var item in fixedTEs)
            {
                FixedTEViewModel FixedTEvm = new FixedTEViewModel();
                FixedTEvm.customer_id = item.customer_id;
                FixedTEvm.customer_name = item.customer_name;
                FixedTEvm.segment_id = item.segment_id;
                FixedTEvm.segment_name = item.segment_name;
                FixedTEvm.year = item.year;
                FixedTEvm.On_invoice = item.on_invoice;
                FixedTEvm.Off_invoice = item.off_invoice;
                FixedTElist.Add(FixedTEvm);

            }

            return View(FixedTElist);
            

        }

        [HttpPost]
        public ActionResult Index(FixedTE FTE, FormCollection form)
        {
            var getcustomerlist = db.Customers.ToList();
            getcustomerlist.Insert(0, new Customer { name = "All", id = 0 });
            SelectList list_customer = new SelectList(getcustomerlist, "id", "name");
            ViewBag.CustomerListName = list_customer;

            var getsegmentlist = db.Segments.ToList();
            getsegmentlist.Insert(0, new Segment { name = "All", id = 0 });
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;


            //var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details);
            //var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.FixedTE_details);

            //if (form["CustomerList"] != null && form["SegmentList"] != null)
            //if (form["CustomerList"] != null)
            //{
            //    int intDDL_Customer = Convert.ToInt32(form["CustomerList"].ToString());
            //int intDDL_Segment = Convert.ToInt32(form["SegmentList"].ToString());
            //fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details).Where(f => f.customer_id == intDDL_Customer).Where(f => f.segment_id == intDDL_Segment);
            //    fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.FixedTE_details).Where(f => f.customer_id == intDDL_Customer);
            //}

            //return View(fixedTEs.ToList());

            List<FixedTEViewModel> FixedTElist = new List<FixedTEViewModel>();

            int intDDL_Customer = Convert.ToInt32(form["CustomerList"].ToString());
            int intDDL_Segment = Convert.ToInt32(form["SegmentList"].ToString());

            var fixedTEs = db.FixedTEs
                        .OrderBy(m => m.segment_id)
                        .GroupBy(m => new { m.year, m.Segment, m.Customer }) //m.FixedTE_details.FixedTE_types,
                        .Select(g => new {
                            segment_id = g.Key.Segment.id,
                            segment_name = g.Key.Segment.name,
                            customer_id = g.Key.Customer.id,
                            customer_name = g.Key.Customer.name,
                            year = g.Key.year,
                            value = g.Sum(x => x.value),
                            on_invoice = g.Sum(x => x.FixedTE_details.FixedTE_types.id == 1 ? x.value : 0),
                            off_invoice = g.Sum(x => x.FixedTE_details.FixedTE_types.id == 2 ? x.value : 0)
                        });

            if (intDDL_Customer == 0 && intDDL_Segment == 0)
            {
                foreach (var item in fixedTEs)
                {
                    FixedTEViewModel FixedTEvm = new FixedTEViewModel();
                    FixedTEvm.customer_id = item.customer_id;
                    FixedTEvm.customer_name = item.customer_name;
                    FixedTEvm.segment_id = item.segment_id;
                    FixedTEvm.segment_name = item.segment_name;
                    FixedTEvm.year = item.year;
                    FixedTEvm.On_invoice = item.on_invoice;
                    FixedTEvm.Off_invoice = item.off_invoice;
                    FixedTElist.Add(FixedTEvm);

                }

                return View(FixedTElist);
            }  //If customer and segment was selected "all"
            else if (intDDL_Customer == 0)
            {
                foreach (var item in fixedTEs)
                {
                    if (item.segment_id == intDDL_Segment)
                    {
                        FixedTEViewModel FixedTEvm = new FixedTEViewModel();
                        FixedTEvm.customer_id = item.customer_id;
                        FixedTEvm.customer_name = item.customer_name;
                        FixedTEvm.segment_id = item.segment_id;
                        FixedTEvm.segment_name = item.segment_name;
                        FixedTEvm.year = item.year;
                        FixedTEvm.On_invoice = item.on_invoice;
                        FixedTEvm.Off_invoice = item.off_invoice;
                        FixedTElist.Add(FixedTEvm);
                    }
                }
            }
            else if (intDDL_Segment == 0)
            {
                foreach (var item in fixedTEs)
                {
                    if (item.customer_id == intDDL_Customer)
                    {
                        FixedTEViewModel FixedTEvm = new FixedTEViewModel();
                        FixedTEvm.customer_id = item.customer_id;
                        FixedTEvm.customer_name = item.customer_name;
                        FixedTEvm.segment_id = item.segment_id;
                        FixedTEvm.segment_name = item.segment_name;
                        FixedTEvm.year = item.year;
                        FixedTEvm.On_invoice = item.on_invoice;
                        FixedTEvm.Off_invoice = item.off_invoice;
                        FixedTElist.Add(FixedTEvm);
                    }
                }
            }
            else
            {
                foreach (var item in fixedTEs)
                {
                    if (item.customer_id == intDDL_Customer && item.segment_id == intDDL_Segment)
                    {
                        FixedTEViewModel FixedTEvm = new FixedTEViewModel();
                        FixedTEvm.customer_id = item.customer_id;
                        FixedTEvm.customer_name = item.customer_name;
                        FixedTEvm.segment_id = item.segment_id;
                        FixedTEvm.segment_name = item.segment_name;
                        FixedTEvm.year = item.year;
                        FixedTEvm.On_invoice = item.on_invoice;
                        FixedTEvm.Off_invoice = item.off_invoice;
                        FixedTElist.Add(FixedTEvm);
                    }
                }
            }


            

            return View(FixedTElist);

        }

        public ActionResult Test2()
        {

            List<FixedTEViewModel> FixedTElist = new List<FixedTEViewModel>();

            /* works
            var fixedTEs = db.FixedTEs
                                    .OrderBy(m => m.Segment.name);
            */


            /* does not work
            var fixedTEs = db.FixedTEs
                                    .OrderBy(m => m.segment_id)
                                    .GroupBy(m => new { m.segment_id, m.customer_id, m.year })
                                    .Select(g => new {
                                        segment_id = g.Key.customer_id,
                                        customer_id = g.Key.customer_id,
                                        year = g.Key.year,
                                        value = g.Sum(x => x.value)

                                                        } );
            */


            var fixedTEs = db.FixedTEs
                                    .OrderBy(m => m.segment_id)
                                    .GroupBy(m => new { m.segment_id, m.customer_id, m.year })
                                    .Select(g => new {
                                        segment_id = g.Key.customer_id,
                                        customer_id = g.Key.customer_id,
                                        year = g.Key.year,
                                        value = g.Sum(x => x.value)

                                    });


            /* 
             * works
            var fixedTEs = from p in db.FixedTEs
                           orderby  p.Segment.name
                           select p;
            */



            return View(fixedTEs.ToList());


        }

        public ActionResult Index_test(int? id)
        {
            //----
            var getcustomerlist = db.Customers.ToList();
            SelectList list_customer = new SelectList(getcustomerlist, "id", "name");
            ViewBag.CustomerListName = list_customer;

            var getsegmentlist = db.Segments.ToList();
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;




            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details);
            //----------
            //var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details).Where(f => f.customer_id.Equals(1));
            return View(fixedTEs.ToList());
        }

        /* */
        [HttpPost]
        public ActionResult Index_test(FixedTE FTE, FormCollection form)
        {
            var getcustomerlist = db.Customers.ToList();
            SelectList list_customer = new SelectList(getcustomerlist, "id", "name");
            ViewBag.CustomerListName = list_customer;

            var getsegmentlist = db.Segments.ToList();
            SelectList list_segment = new SelectList(getsegmentlist, "id", "name");
            ViewBag.SegmentListName = list_segment;
            

            var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details);

            if (form["CustomerList"] != null && form["SegmentList"] != null)
            {
                int intDDL_Customer = Convert.ToInt32(form["CustomerList"].ToString());
                int intDDL_Segment = Convert.ToInt32(form["SegmentList"].ToString());
                fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details).Where(f => f.customer_id == intDDL_Customer).Where(f => f.segment_id == intDDL_Segment);
            }

            return View(fixedTEs.ToList());
        }

        // ---------------------

        // GET: FixedTEs
        public ActionResult Index_init()
        {
            var fixedTEs = db.FixedTEs.Include(f => f.Customer).Include(f => f.Segment).Include(f => f.FixedTE_details);
            return View(fixedTEs.ToList());
        }

        // GET: FixedTEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE fixedTE = db.FixedTEs.Find(id);
            if (fixedTE == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE);
        }

        // GET: FixedTEs/Create
        public ActionResult Create()
        {
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name");
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name");
            ViewBag.fixedte_item_id = new SelectList(db.FixedTE_details, "id", "name");
            return View();
        }

        // POST: FixedTEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,customer_id,year,segment_id,fixedte_item_id,value")] FixedTE fixedTE)
        {
            if (ModelState.IsValid)
            {
                db.FixedTEs.Add(fixedTE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", fixedTE.customer_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", fixedTE.segment_id);
            ViewBag.fixedte_item_id = new SelectList(db.FixedTE_details, "id", "name", fixedTE.fixedte_item_id);
            return View(fixedTE);
        }

        // GET: FixedTEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE fixedTE = db.FixedTEs.Find(id);
            if (fixedTE == null)
            {
                return HttpNotFound();
            }
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", fixedTE.customer_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", fixedTE.segment_id);
            ViewBag.fixedte_item_id = new SelectList(db.FixedTE_details, "id", "name", fixedTE.fixedte_item_id);
            return View(fixedTE);
        }

        // POST: FixedTEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,customer_id,year,segment_id,fixedte_item_id,value")] FixedTE fixedTE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fixedTE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.customer_id = new SelectList(db.Customers, "id", "name", fixedTE.customer_id);
            ViewBag.segment_id = new SelectList(db.Segments, "id", "name", fixedTE.segment_id);
            ViewBag.fixedte_item_id = new SelectList(db.FixedTE_details, "id", "name", fixedTE.fixedte_item_id);
            return View(fixedTE);
        }

        // GET: FixedTEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FixedTE fixedTE = db.FixedTEs.Find(id);
            if (fixedTE == null)
            {
                return HttpNotFound();
            }
            return View(fixedTE);
        }

        // POST: FixedTEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FixedTE fixedTE = db.FixedTEs.Find(id);
            db.FixedTEs.Remove(fixedTE);
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
