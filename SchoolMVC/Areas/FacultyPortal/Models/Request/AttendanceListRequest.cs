using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Request
{
    public class AttendanceListRequest
    {
        public int? SAM_ClassId { get; set; }
        public int? SAM_SectionId { get; set; }
        public long? SAM_SchoolId { get; set; }
        public long? SAM_SessionId { get; set; }
        public DateTime? SAM_Date { get; set; }
        public string SAM_Date_S { get; set; }
        public long? TotalStudent { get; set; }
        public long? PresentStudent { get; set; }
        public long? AbsentStudent { get; set; }
    }



}