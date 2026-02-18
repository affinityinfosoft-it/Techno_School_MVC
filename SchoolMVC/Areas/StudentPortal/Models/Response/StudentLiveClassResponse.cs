using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentLiveClassResponse
    {
        public long? CWLS_ID { get; set; }
        public string CWLS_SBM_SubjectName { get; set; }
        public string CWLS_Title { get; set; }
        public string CWLS_Link { get; set; }

        public long? CWLS_Class_ID { get; set; }
        public DateTime CWLS_ClassDate { get; set; }
        public DateTime? CWLS_ClassTime { get; set; }
        public long? CWLS_Section_ID { get; set; }
        public long? CWLS_SubGr_ID { get; set; }
        public long? CWLS_Sub_ID { get; set; }


    }
}