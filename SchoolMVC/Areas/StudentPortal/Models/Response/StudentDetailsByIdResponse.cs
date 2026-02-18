using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentDetailsByIdResponse
    {
        public long? SD_Id { get; set; }
        public string SD_StudentId { get; set; }
        public long? SD_SchoolId { get; set; }
        public long? SD_CurrentClassId { get; set; }
        public long? SD_CurrentSectionId { get; set; }
        public long? SD_CurrentSessionId { get; set; }
        public int? SD_CurrentRoll { get; set; }
        public string SD_StudentName { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SD_CM_CLASSNAME { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public string SM_SESSIONNAME { get; set; }

    }
}