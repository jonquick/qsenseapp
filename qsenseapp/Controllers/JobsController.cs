using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qsenseapp.Models;
using AutoMapper;
using Microsoft.AspNet.Identity;

namespace qsenseapp.Controllers
{
   
    public class JobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public static IEnumerable<SelectListItem> GetEnumSelectList<T>()
        {
            return (Enum.GetValues(typeof(T)).Cast<int>().Select(e => new SelectListItem() { Text = Enum.GetName(typeof(T), e), Value = e.ToString() })).ToList();
        }

        // GET: Jobs
        public ActionResult Index(int? JobTypeLookup)
        {

            var jobs = from j in db.Jobs select j;

            if (!object.Equals(JobTypeLookup,null))
            {
                var JobTypeName = Enum.GetName(typeof(JobType), JobTypeLookup);
                jobs = jobs.Where(j => j.JobType.Value.ToString().Equals(JobTypeName));
                                       
            }


            ViewBag.JobTypeLookup = new SelectList(GetEnumSelectList<JobType>(),"Value","Text");
                
            return View(jobs.ToList());
        }

        // GET: Jobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // GET: Jobs/Create
        public ActionResult Create()
        {
            ViewBag.BusinessPartnerLookup = new SelectList(db.BusinessPartners, "ID", "Name");
            return View();
        }

        public JsonResult GetBusinessPartnerPriceNet(int id)
        {

            BusinessPartner bp = db.BusinessPartners.Find(id);

            return Json(bp.ChargeRateHourNet);
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobID,BusinessPartnerID,Name,JobType,ChargeType,ChargeRateHourNet, DueOn, BudgetHours")] Job job)
        {
            if (ModelState.IsValid)
            {
                job.ReceivedOn = DateTime.Now;

                var uid = User.Identity.GetUserId();




                job.ReceivedBy = uid;
                job.CreatedOn = DateTime.Now;
                job.CreatedBy = uid;
                job.ModifiedOn = DateTime.Now;
                job.ModifiedBy = uid;

                db.Jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        // GET: Jobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Mapper.CreateMap<Job, JobEditViewModel>();

            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            var jobView = Mapper.Map<Job, JobEditViewModel>(job);
            ViewBag.BusinessPartnerLookup = new SelectList(db.BusinessPartners, "ID", "Name", job.BusinessPartnerID);
            return View(jobView);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobID,BusinessPartnerID,Name,JobType,ChargeType,ChargeRateHourNet, DueOn, BudgetHours")] JobEditViewModel jobEdit)
        {
            

            if (ModelState.IsValid)
            {
                var dbJob = db.Jobs.Find(jobEdit.JobID);
                {

                    var uid = User.Identity.GetUserId();

                    dbJob.BusinessPartnerID = jobEdit.BusinessPartnerID;
                    dbJob.Name = jobEdit.Name;
                    dbJob.ChargeRateHourNet = jobEdit.ChargeRateHourNet;
                    dbJob.ChargeType = jobEdit.ChargeType;
                    dbJob.DueOn = jobEdit.DueOn;
                    dbJob.FinishNn = jobEdit.FinishOn;
                    dbJob.JobType = jobEdit.JobType;
                    dbJob.StartOn = jobEdit.StartOn;
                    dbJob.BusinessPartner = jobEdit.BusinessPartner;
                    dbJob.ModifiedOn = DateTime.Now;
                    dbJob.ModifiedBy = uid;
                    dbJob.BudgetHours = jobEdit.BudgetHours;
                }
                
                db.Entry(dbJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(jobEdit);
        }

        // GET: Jobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Job job = db.Jobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Job job = db.Jobs.Find(id);
            db.Jobs.Remove(job);
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
