using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class RoutineResponse
    {

        public string CWTR_TITLE { get; set; }
        public string CWTR_DESCRIPTION { get; set; }
        public string CWTR_UPLOADFILE { get; set; }
        public DateTime CWTR_CREATEDATE { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
       
    }
}