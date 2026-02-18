using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
   
    public class TransportDetailsResponse
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public long? ClassId { get; set; }
        public string ClassName { get; set; }
        public long? SectionId { get; set; }
        public string SectionName { get; set; }
        public string BusName { get; set; }
        public string PickupStop { get; set; }
        public string DropLocation { get; set; }
        public List<MonthlyPaymentStatus> MonthlyPaymentStatus { get; set; }
    }

    public class MonthlyPaymentStatus
    {
        public string TransactionNo { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public decimal? PaymentAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMode { get; set; }
    }


}