using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class RoutineRequest
    {
        [Required]
        public string CWTR_Class { get; set; }
        //[Required]
        //public long? SD_CurrentSessionId { get; set; }
    }
}