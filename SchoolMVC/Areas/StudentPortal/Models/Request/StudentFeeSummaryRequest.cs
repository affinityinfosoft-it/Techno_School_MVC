using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentFeeSummaryRequest
    {
        [Required]
        public string SD_StudentId { get; set; }
        //[Required]
        //public string SESSIONID { get; set; }
        //[Required]
        //public string CLASSID { get; set; }
    }
}