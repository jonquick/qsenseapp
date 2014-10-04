using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qsenseapp.Models;

namespace qsenseapp.Controllers
{
    public class BusinessPartnersController : Controller
    {
        private qSenseDbContext db = new qSenseDbContext();

        // GET: BusinessPartners
        public ActionResult Index()
        {
            return View(db.BusinessPartners.ToList());
        }

        // GET: BusinessPartners/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = db.BusinessPartners.Find(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // GET: BusinessPartners/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BusinessPartners/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Website,ChargeRateHourNet")] BusinessPartner businessPartner)
        {
            if (ModelState.IsValid)
            {
                db.BusinessPartners.Add(businessPartner);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(businessPartner);
        }

        // GET: BusinessPartners/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = db.BusinessPartners.Find(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // POST: BusinessPartners/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Website,ChargeRateHourNet")] BusinessPartner businessPartner)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessPartner).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(businessPartner);
        }

        // GET: BusinessPartners/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusinessPartner businessPartner = db.BusinessPartners.Find(id);
            if (businessPartner == null)
            {
                return HttpNotFound();
            }
            return View(businessPartner);
        }

        // POST: BusinessPartners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BusinessPartner businessPartner = db.BusinessPartners.Find(id);
            db.BusinessPartners.Remove(businessPartner);
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
