using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class FacultyLoginByIdRequest
    {
        [Required]
        public string FP_FacultyCode { get; set; }
        [Required]
        public string FP_Password { get; set; }
    }
}