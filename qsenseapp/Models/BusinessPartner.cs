using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace qsenseapp.Models
{
    public class BusinessPartner
    {

        public int ID { get; set; }
        public String Name { get; set; }
        public Decimal ChargeRateHourNet { get; set; }
        [Url]
        public String Website { get; set; }

        public virtual ICollection<Job> Jobs { get; set; }

    }
}