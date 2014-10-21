using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace qsenseapp.Models
{

    public enum JobType 
    {
        Project= 0, 
        Support= 1, 
        Quotation=2
    }
    public enum ChargeType
    {
        Fixed, Variable
    }

    public class Job
    {
        public int JobID { get; set; }
        [DisplayName("Business Partner")]
        public int BusinessPartnerID { get; set; }
        [DisplayName("Job")]
        public String Name { get; set; }
        [DisplayName("Area")]
        public JobType? JobType { get; set; }
        [DisplayName("Charge")]
        public ChargeType? ChargeType { get; set; }
        [DisplayName("Rate (hr)")]
        public Decimal ChargeRateHourNet { get; set; }
        [DisplayName("Received On")]
        public DateTime? ReceivedOn { get; set;}
        [DisplayName("Recieved By")]
        public String ReceivedBy { get; set; }
         [DisplayName("Due On")]
        public DateTime? DueOn { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? FinishNn { get; set; }

        [DisplayName("Budget Hours")]
        public Decimal BudgetHours { get; set; }
       

        public DateTime CreatedOn { get; set; }
        public String CreatedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public String ModifiedBy { get; set; }


        public virtual BusinessPartner BusinessPartner { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
    }

    public class JobEditViewModel
    {

        public int JobID { get; set; }
        [DisplayName("Business Partner")]
        public int BusinessPartnerID { get; set; }
        [DisplayName("Job Name")]
        public String Name { get; set; }
        [DisplayName("Job Type")]
        public JobType? JobType { get; set; }
        [DisplayName("Charge Type")]
        public ChargeType? ChargeType { get; set; }
        [DisplayName("Price (hour)")]
        [Required(ErrorMessage = "Hourly Rate is required")]
        [Range(25, 500, ErrorMessage = "Price must be between 25 and 500")]
        public Decimal ChargeRateHourNet { get; set; }
        [DisplayName("Due On")]
        public DateTime? DueOn { get; set; }
        public DateTime? StartOn { get; set; }
        public DateTime? FinishOn { get; set; }

        [DisplayName("Budget Hours")]
        public Decimal BudgetHours { get; set; }

        public DateTime ModifiedOn { get; set; }
        public String ModifiedBy { get; set; }

        public virtual BusinessPartner BusinessPartner { get; set; }
    }

    

}