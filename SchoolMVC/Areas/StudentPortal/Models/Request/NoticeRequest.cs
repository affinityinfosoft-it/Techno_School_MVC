
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class NoticeRequest
    {
        [Required]
        public string SD_StudentId { get; set; }
         [Required]
        public long? SD_CurrentSessionId { get; set; }

         [Required]
        public long? NT_ID { get; set; }

    }
}