using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class ClassWiseBirthdayResponse
    {
        public string STUDENTID { get; set; }
        public string STUDENTNAME { get; set; }
        public string PHOTO { get; set; }
        public string DOB { get; set; }
        public string BIRTHDAY_STATUS { get; set; }
        public string CLASSID { get; set; }
        public string CLASSNAME { get; set; }
        public string SECTIONNAME { get; set; }
        public string AGE { get; set; }
    }
}