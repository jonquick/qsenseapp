using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace qsenseapp.Models
{
    public class Activity
    {
        public enum ActivityType
        {
            Meeting,
            Call,
            Email,
            InstantMessage,
            Work,
            Travel
        }

        public enum ActivityDirection
        {
            In,
            Out
        }

        public int ActivityID { get; set; }
        public int JobID { get; set; }

        [DisplayName("Has Appointment")]
        public Boolean HasAppointment { get; set; }
        public string AppointmentID { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime StartOn { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime FinishOn { get; set; }
        public String Subject { get; set; }

        public virtual Job Job { get; set; }
        public virtual ApplicationUser User { get; set; }


    }
}