using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class AttendanceListResponse
    {
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SAM_Date_S { get; set; }
        public DateTime? SAM_Date { get; set; }
        public int? SAM_Id { get; set; }
        public int? SAM_ClassId { get; set; }
        public int? SAM_SectionId { get; set; }
        public long? SAM_SchoolId { get; set; }
        public long? SAM_SessionId { get; set; }
        public string TotalStudent { get; set; }
        public string PresentStudent { get; set; }
        public string AbsentStudent { get; set; }
    }
}