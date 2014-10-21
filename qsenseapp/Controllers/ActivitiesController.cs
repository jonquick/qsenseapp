using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using qsenseapp.Models;
using Microsoft.Exchange.WebServices.Data;

namespace qsenseapp.Controllers
{
    public class ActivitiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Activities
        public ActionResult Index()
        {
            return View(db.Activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Activities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityID,JobID,HasAppointment,AppointmentID,StartOn,FinishOn,Subject")] Activity activity)
        {
            if (ModelState.IsValid)
            {

                db.Activities.Add(activity);
                db.SaveChanges();

                //Activity newAct = db.Activities.Find(activity.ActivityID).

                Activity newAct = db.Activities
                        .Where(a => a.ActivityID == activity.ActivityID)
                        .Include("Job")
                        .FirstOrDefault();


                //Job newJob = 

                //context.Players.Include(player => player.Team).
                if (activity.HasAppointment)
                {
                    //connect to Hosted exchange and create the appointment

                    // Create the binding.
                    ExchangeService service = new ExchangeService();
                    // Set the credentials for the on-premises server.
                    service.Credentials = new WebCredentials("jon.quick@qsense.co.uk", "Adm1rals38");

                    service.Url = new Uri("https://pod51047.outlook.com/ews/exchange.asmx");


                    // Set the URL.
                    //service.AutodiscoverUrl("jon.quick@qsense.co.uk", (a) => { return true; });

                    Appointment appointment = new Appointment(service);


                    Activity act = db.Activities.Find(activity.ActivityID);

                    // Set the properties on the appointment object to create the appointment.
                    appointment.Subject = newAct.Job.BusinessPartner.Name + " - " + newAct.Subject;
                    appointment.Body = newAct.Job.BusinessPartner.Name + Environment.NewLine + 
                                       newAct.Job.Name + Environment.NewLine +
                                       newAct.Job.DueOn
                        ;
                    appointment.Start = activity.StartOn;
                    appointment.End = activity.FinishOn;
                    appointment.Location = "Somewhere";
                    appointment.ReminderDueBy = activity.StartOn.AddDays(-1);

                    ExtendedPropertyDefinition extendedPropertyJobID = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "JobID", MapiPropertyType.Integer);
                    appointment.SetExtendedProperty(extendedPropertyJobID, activity.JobID);

                    ExtendedPropertyDefinition extendedPropertyActivityDesc = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "ActivityDesc", MapiPropertyType.String);
                    appointment.SetExtendedProperty(extendedPropertyActivityDesc, activity.Subject);

                    ExtendedPropertyDefinition extendedPropertyBusinessPartner = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "BusinessPartner", MapiPropertyType.String);
                     appointment.SetExtendedProperty(extendedPropertyBusinessPartner, act.Job.BusinessPartner.Name);

                    ExtendedPropertyDefinition extendedPropertyJobName = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "JobName", MapiPropertyType.String);
                    appointment.SetExtendedProperty(extendedPropertyJobName, act.Job.Name);

                    ExtendedPropertyDefinition extendedPropertyJobBudget = new ExtendedPropertyDefinition(DefaultExtendedPropertySet.PublicStrings, "JobBudget", MapiPropertyType.String);
                    appointment.SetExtendedProperty(extendedPropertyJobBudget, act.Job.BudgetHours);

                    // Save the appointment to your calendar.
                    appointment.Save(SendInvitationsMode.SendToNone);

                    // Verify that the appointment was created by using the appointment's item ID.
                    Item item = Item.Bind(service, appointment.Id, new PropertySet(ItemSchema.Subject));
                    Console.WriteLine("\nAppointment created: " + item.Subject + "\n");

                    activity.AppointmentID = appointment.Id.ToString();

            



               
                    

                }



                return RedirectToAction("Index");
            }

            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityID,JobID,HasAppointment,AppointmentID,StartOn,FinishOn,Subject")] Activity activity)
        {
            if (ModelState.IsValid)
            {


                // Create the binding.
                ExchangeService service = new ExchangeService();
                // Set the credentials for the on-premises server.
                service.Credentials = new WebCredentials("jon.quick@qsense.co.uk", "Adm1rals38");

                service.Url = new Uri("https://pod51047.outlook.com/ews/exchange.asmx");

                // Set the URL.
                //service.AutodiscoverUrl("jon.quick@qsense.co.uk", (a) => { return true; });

                ItemId id = new ItemId(activity.AppointmentID);

              
                // Instantiate an appointment object by binding to it by using the ItemId.
                // As a best practice, limit the properties returned to only the ones you need.
                Appointment appointment = Appointment.Bind(service,id, new PropertySet(AppointmentSchema.Subject, AppointmentSchema.Start, AppointmentSchema.End));

                string oldSubject = appointment.Subject;

                // Update properties on the appointment with a new subject, start time, and end time.
                appointment.Subject = activity.Subject;
                appointment.Start = activity.StartOn;
                appointment.End = activity.FinishOn;

            

                // Unless explicitly specified, the default is to use SendToAllAndSaveCopy.
                // This can convert an appointment into a meeting. To avoid this,
                // explicitly set SendToNone on non-meetings.

               // SendInvitationsOrCancellationsMode mode = appointment.IsMeeting ?
               //     SendInvitationsOrCancellationsMode.SendToAllAndSaveCopy : SendInvitationsOrCancellationsMode.SendToNone;

                // Send the update request to the Exchange server.

                appointment.Update(ConflictResolutionMode.AlwaysOverwrite);

                // Verify the update.
                Console.WriteLine("Subject for the appointment was \"" + oldSubject + "\". The new subject is \"" + appointment.Subject + "\"");






                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
