using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Request
{
    public class ClassWiseSectionRequest
    {
        public long? SCM_SCHOOLID { get; set; }
        public long? CM_CLASSID { get; set; }

    }
}