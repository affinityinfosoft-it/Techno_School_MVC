using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class FacultyDetailsByIdResponse
    {

        public long? FP_Id { get; set; }
        public string FP_FacultyCode { get; set; }
        public string FP_UserType { get; set; }
        public long? FP_SchoolId { get; set; }
        public long? FP_SessionId { get; set; }
        public long? FP_DesignationId { get; set; }
        public string FP_Phone { get; set; }
        public string FP_Name { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string DM_Name { get; set; }
    }
}