using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class AttendenceResponse
    {
        public List<string> Present { get; set; }
        public List<string> HalfDay { get; set; }
        public List<string> Absent { get; set; }
        public List<string> Leave { get; set; }
        public Dictionary<string, string> Holiday { get; set; }
        public List<string> NoExam { get; set; }
    }
}