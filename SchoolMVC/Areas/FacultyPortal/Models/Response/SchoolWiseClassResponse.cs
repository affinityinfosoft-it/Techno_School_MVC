using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class SchoolWiseClassResponse
    {
        public string CM_CLASSNAME { get; set; }
        public long? CM_SCM_SCHOOLID { get; set; }
        public long? CM_CLASSID { get; set; }
    }
}