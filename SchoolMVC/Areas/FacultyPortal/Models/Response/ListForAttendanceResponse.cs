using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class ListForAttendanceResponse
    {
        //public string fromDate { get; set; }
        //public string toDate { get; set; }
        public string StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public int? SAM_Id { get; set; }
        public int? SAM_ClassId { get; set; }
        public int? SAM_SectionId { get; set; }
        public long? SAM_SchoolId { get; set; }
        public long? SAM_SessionId { get; set; }
        public DateTime? SAM_Date { get; set; }
        public string SAM_Date_S { get; set; }
        //public int? Userid { get; set; }
        public string Edit { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        //public int? ABSEND { get; set; }
        //public int? PRESENT { get; set; }
        //public int? MonthId { get; set; }
        //public string MonthName { get; set; }
        //public int? Workingdays { get; set; }
        //public decimal? Percentage { get; set; }
        //public DateTime? fDate { get; set; }
        //public DateTime? tDate { get; set; }
        public Boolean? IsAbsent { get; set; }
    }
}