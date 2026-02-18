using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentMarksheetRequest
    {
        [Required]
        public string SD_StudentId { get; set; }
        [Required]
        public string SD_ClassId { get; set; }
        [Required]
        public string SD_CurrentSessionId { get; set; }

    }
}