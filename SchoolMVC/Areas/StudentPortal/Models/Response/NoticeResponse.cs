using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class NoticeResponse
    {
        public DateTime NM_ENTRYDATE { get; set; }
        public DateTime NM_EXPDATE { get; set; }

        public string NM_FACULTYNAME { get; set; }
        public string NM_TITLE { get; set; }
        public string NM_NOTICE { get; set; }
        public string NM_UPLOADFILE { get; set; }
        public string NM_Link { get; set; }
    }
}