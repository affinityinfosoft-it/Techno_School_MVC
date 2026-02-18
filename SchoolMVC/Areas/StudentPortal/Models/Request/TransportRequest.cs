using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class TransportDetailsRequest
        {
            public long? SchoolId { get; set; }
            public long? ClassId { get; set; }
            public long? SectionId { get; set; }
            public long? SessionId { get; set; }
            public string StudentId { get; set; }
        }
}
