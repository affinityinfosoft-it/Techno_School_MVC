using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.MarkSheet.Models
{
    public class MarkSheetModel
    {
        public List<clsStudentMarksEntry> GetStudentMarksList { get; set; }
        //public MarkSheetModel()
        //{
        //    GetStudentMarksList = new List<clsStudentMarksEntry>();
        //}
    }
}