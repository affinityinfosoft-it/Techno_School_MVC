using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class AttendenceRequest
    {
        [Required]
        public string SD_StudentId { get; set; }
        [Required]
        public int Year { get; set; }
        [Required]
        public int Month { get; set; }
    }
}