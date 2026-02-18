using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class ClassWiseSectionResponse
    {
        public string SECM_SECTIONNAME { get; set; }
        public long? SECM_SECTIONID { get; set; }
        public long? SECM_CM_CLASSID { get; set; }
        public long? SECM_SCM_SCHOOLID { get; set; }
        public Int32? SECM_OCCUPANCY { get; set; }
        public long? SECM_CREATEDUID { get; set; }
        public bool SECM_ISACTIVE { get; set; }
    }
}