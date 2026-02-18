using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentPaidDetailsResponse
    {
        public string RECIPTNO { get; set; }
        public string FEESCOLLECTIONID { get; set; }
        public decimal PAIDAMOUNT { get; set; }
        public string PAYMODE { get; set; }
        public string FEESDATE { get; set; }
        public string FROMMONTH { get; set; }
        public string TOMONTH { get; set; }
        



    }
}