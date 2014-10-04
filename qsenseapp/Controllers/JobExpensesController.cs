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
    public class JobExpensesController : Controller
    {
        private qSenseDbContext db = new qSenseDbContext();

        // GET: JobExpenses
        public ActionResult Index()
        {
            var jobExpenses = db.JobExpenses.Include(j => j.Job);
            return View(jobExpenses.ToList());
        }

        // GET: JobExpenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobExpense jobExpense = db.JobExpenses.Find(id);
            if (jobExpense == null)
            {
                return HttpNotFound();
            }
            return View(jobExpense);
        }

        // GET: JobExpenses/Create
        public ActionResult Create()
        {
            ViewBag.BusinessPartnerLookup = new SelectList(db.BusinessPartners, "ID", "Name");
            ViewBag.JobLookup = new SelectList(db.Jobs, "JobID", "Name");
            //ViewBag.JobLookup = new SelectList(null,null,null);
            return View();
        }

        public JsonResult GetJobList(int id)
        {

            var jobs = db.Jobs.Where(bp => bp.BusinessPartnerID == id).ToList().Select(j => new SelectListItem
            {
                Text = j.Name,
                Value = j.JobID.ToString()
            });
  
            return Json(jobs);

        }

        public JsonResult GetJobPriceNet(int id)
        {

            Job job = db.Jobs.Find(id);

            return Json(job.ChargeRateHourNet);
        }

        // POST: JobExpenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobExpenseID,BusinessPartnerID,JobID,ExpenseType,Name,StartOn,FinishOn,PriceNet")] JobExpense jobExpense)
        {

            TimeSpan diff;
            String durationFormatted = "";

            if (ModelState.IsValid)
            {

                diff = jobExpense.FinishOn.Value- jobExpense.StartOn.Value;
            
                jobExpense.Quantity = diff.TotalHours;
                jobExpense.ActualNet = diff.TotalHours * jobExpense.PriceNet;
                if (diff.Hours > 0)
                {

                    durationFormatted = diff.Hours.ToString();

                    if (diff.Hours > 1)
                    {
                        durationFormatted += " hrs";
                    }
                    else
                    {
                        durationFormatted += " hr";
                    }

                }
                if (diff.Minutes > 0)
                {

                    durationFormatted += diff.Minutes.ToString();

                    if (diff.Minutes > 1)
                    {
                        durationFormatted += " mins";
                    }
                    else
                    {
                        durationFormatted += " min";
                    }

                }
                jobExpense.DurationFormatted = durationFormatted;

              

                db.JobExpenses.Add(jobExpense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BusinessPartnerID = new SelectList(db.BusinessPartners, "BusinessPartnerID", "Name");
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "Name", jobExpense.JobID);
            return View(jobExpense);
        }

        // GET: JobExpenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobExpense jobExpense = db.JobExpenses.Find(id);
            if (jobExpense == null)
            {
                return HttpNotFound();
            }
            ViewBag.BusinessPartnerID = new SelectList(db.BusinessPartners, "BusinessPartnerID", "Name");
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "Name", jobExpense.JobID);
            return View(jobExpense);
        }

        // POST: JobExpenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobExpenseID,BusinessPartnerID,JobID,ExpenseType,Name,StartOn,FinishOn,PriceNet")] JobExpense jobExpense)
        {
            TimeSpan diff;
            String durationFormatted = "";

            if (ModelState.IsValid)
            {

                diff = jobExpense.FinishOn.Value - jobExpense.StartOn.Value;
              
                jobExpense.Quantity = diff.TotalHours;
                jobExpense.ActualNet = diff.TotalHours * jobExpense.PriceNet;

                if (diff.Hours > 0)
                {

                    durationFormatted = diff.Hours.ToString();

                    if (diff.Hours > 1)
                    {
                        durationFormatted += " hrs";
                    }
                    else
                    {
                        durationFormatted += " hr";    
                    }
                    
                }
                if (diff.Minutes > 0)
                {

                    durationFormatted += " " + diff.Minutes.ToString();

                    if (diff.Minutes > 1)
                    {
                        durationFormatted += " mins";
                    }
                    else
                    {
                        durationFormatted += " min";
                    }

                }
                jobExpense.DurationFormatted = durationFormatted;
              

                db.Entry(jobExpense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BusinessPartnerID = new SelectList(db.BusinessPartners, "BusinessPartnerID", "Name");
            ViewBag.JobID = new SelectList(db.Jobs, "JobID", "Name", jobExpense.JobID);
            return View(jobExpense);
        }

        // GET: JobExpenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobExpense jobExpense = db.JobExpenses.Find(id);
            if (jobExpense == null)
            {
                return HttpNotFound();
            }
            return View(jobExpense);
        }

        // POST: JobExpenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            JobExpense jobExpense = db.JobExpenses.Find(id);
            db.JobExpenses.Remove(jobExpense);
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
