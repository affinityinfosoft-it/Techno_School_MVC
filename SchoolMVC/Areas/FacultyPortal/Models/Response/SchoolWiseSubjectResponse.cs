using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class SchoolWiseSubjectResponse
    {
        public string SBM_SubjectName { get; set; }
        public long? SBM_SchoolId { get; set; }
        public long? SBM_Id { get; set; }
        public string SGM_SubjectGroupName { get; set; }
        public long? SGM_SubjectGroupID { get; set; }

    }
}