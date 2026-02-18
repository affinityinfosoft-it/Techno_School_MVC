using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentFeeSummaryResponse
    {


        public string FEESNAME { get; set; }
        public string INSTALMENTNO { get; set; }
        public long? FEESID { get; set; }
        public decimal FEES_AMOUNT { get; set; }
        public decimal INSTALMENT_AMOUNT { get; set; }
        public decimal PAID_AMOUNT { get; set; }
        public DateTime DUE_MONTH { get; set; }


        //public string STUDENT_ID { get; set; }
        //public string SESSIONID { get; set; }
        //public string FEE_HEAD { get; set; }




    }
}
