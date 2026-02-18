
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentDetailsByIdRequest
    {
        [Required]
        public string SD_StudentId { get; set; }
    }
}