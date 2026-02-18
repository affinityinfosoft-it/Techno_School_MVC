using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class SchoolWiseSubjectGroupResponse
    {
        public string SGM_SubjectGroupName { get; set; }
        public long? SGM_SCHOOLID { get; set; }
        public long? SGM_SubjectGroupID { get; set; }
    }
}