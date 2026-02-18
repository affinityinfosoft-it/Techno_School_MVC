using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentDueFeeResponse
    {
        public string STUDENTID { get; set; }
        public string CLASSNAME { get; set; }
        public string FEES_HEAD { get; set; }
        public string INSTALLMENTNO { get; set; }
        public decimal INSTALMENT_AMOUNT { get; set; }
        public decimal PAYABLE_AMOUNT { get; set; }
        public DateTime DUE_DATE { get; set; }
        public decimal DUE_AMOUNT { get; set; }

        public decimal TOTAL_DUEAMOUNT { get; set; }
    }
}