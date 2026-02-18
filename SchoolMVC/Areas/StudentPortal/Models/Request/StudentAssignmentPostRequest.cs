using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request
{
    public class StudentAssignmentPostRequest
    {
        [Required]
        public long? AST_ASM_ID { get; set; }
        [Required]
        public string AST_StudentId { get; set; }
        [Required]
        public string AST_UploadDoc { get; set; }
        [Required]
        public bool? IsAbsent { get; set; }
    }
}