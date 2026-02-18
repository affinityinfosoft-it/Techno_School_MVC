using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Request
{
    public class StudentListForAttendanceRequest
    {


        public long? SAM_SchoolId { get; set; }
        public long? SAM_SessionId { get; set; }
        public long? SAM_ClassId { get; set; }
        public long? SAM_SectionId { get; set; }
        public DateTime? SAM_Date { get; set; }
    }
}