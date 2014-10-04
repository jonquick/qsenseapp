using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;


namespace qsenseapp.Models
{

     public enum ExpenseType 
    {
        EmployeeTimeRecording, EmployeeExpense, Purchase
    }

    public class JobExpense
    {
        public int JobExpenseID { get; set; }
        public int JobID { get; set; }
        public ExpenseType ExpenseType { get; set; }
        [DisplayName("Detail")]
        public String Name { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? StartOn { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime? FinishOn { get; set; }
        public String DurationFormatted { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Double? Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Double? PriceNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Double? ActualNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Double? AdjustNet { get; set; }
        [DisplayFormat(DataFormatString = "{0:N}", ApplyFormatInEditMode = true)]
        public Double? ValueNet { get; set; }
        

        
        public virtual Job Job { get; set; }
       
    }
}