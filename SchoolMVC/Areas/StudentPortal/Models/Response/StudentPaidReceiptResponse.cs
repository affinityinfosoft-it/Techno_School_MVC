using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentPaidReceiptResponse
    {
        public StudentPaidReceiptResponse()
        {
            StudentPaidReceiptFeesHeadList = new List<StudentPaidReceiptFeesHeadList>();
        }
      
        public long FEESCOLLECTIONID { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string RECIPTNO { get; set; }
        public string ADMISSIONID { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string STUDENT_NAME { get; set; }
        public string SEXNAME { get; set; }
        public string INSTALLMENT { get; set; }
        public string FEESDATE { get; set; }


      
        
        public string PAYMODE { get; set; }
        public string BANKNAME { get; set; }
        public string CHQNO { get; set; }
        public string CHQDATE { get; set; }
        public decimal PAIDAMOUNT { get; set; }


        public decimal? DUEAMT { get; set; }
        public decimal? TOTALFEESDUEAMOUNT { get; set; }
        public string DISCOUNTEDBY { get; set; }
       
        public List<StudentPaidReceiptFeesHeadList> StudentPaidReceiptFeesHeadList { get; set; }


    }

    public class StudentPaidReceiptFeesHeadList
     {
        public string FEM_FEESNAME { get; set; }
        public long? INSTALMENTNO { get; set; }
        public decimal? INSTALMENTAMOUNT { get; set; }
        public decimal? PYMENTAMOUNT { get; set; }

    }
}