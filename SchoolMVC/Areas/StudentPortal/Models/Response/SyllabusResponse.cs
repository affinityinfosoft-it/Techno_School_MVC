using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class SyllabusResponse
    {
        public DateTime SM_CREATEDATE { get; set; }
        public long SM_CLASSID { get; set; }
        public string SM_SYLLABUSNAME { get; set; }
        public string SM_UPLOADFILE { get; set; }
    }
}