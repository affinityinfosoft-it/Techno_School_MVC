using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentPaidReceiptRequest
    {
     
        [Required]
        public string FEESCOLLECTIONID { get; set; }
    }
}