using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentLiveClassRequest
    {
        [Required]
        public long? SD_CurrentClassId { get; set; }
        [Required]
        public long? SD_CurrentSectionId { get; set; }
        [Required]
        public long? SD_CurrentSessionId { get; set; }
    }
}