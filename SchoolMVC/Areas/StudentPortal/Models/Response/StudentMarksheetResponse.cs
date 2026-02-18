using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Response
{
    public class StudentMarksheetResponse
    {
        public StudentMarksheetResponse()
        {
            StudentMarksheetHeadList = new List<StudentMarksheetHeadList>();
        }

        public string STUDENT_ID { get; set; }
        public string STUDENT_NAME { get; set; }
        public string CLASS_NAME { get; set; }
        public long CLASSID { get; set; }
        public string ROLLNO { get; set; }
        public string REGISTRATION_NO { get; set; }
        public string SESSIONNAME { get; set; }
        public long SESSIONID { get; set; }
        public string SCHOOLNAME { get; set; }        
        public string SUBJECT_GROUP_NAME { get; set; }
        public string TERM_NAME { get; set; }
        public List<StudentMarksheetHeadList> StudentMarksheetHeadList { get; set; }
    }

    public class StudentMarksheetHeadList
    {
        public string SUBJECT_NAME { get; set; }
        public string MARKS_OBTAINED { get; set; }
        public string MARKS_OBTAINEDP { get; set; }
        public string MARKS_OBTAINED_ORAL { get; set; }
        public string GRADE { get; set; }
        public string FULL_MARKS { get; set; }
        public string PASS_MARKS { get; set; }
        public string FULL_MARKSP { get; set; }
        public string PASS_MARKSP { get; set; }
        public string FULL_MARKS_ORAL { get; set; }
        public string PASS_MARKS_ORAL { get; set; }
        public string IS_PRACTICAL { get; set; }
        public string IS_ORAL { get; set; }
        public string IS_ABSENTW { get; set; }
        public string IS_ABSENTP { get; set; }
        public string SUJECT_MAINGROUP { get; set; }
        public string IS_OPTIONAL { get; set; }

    }
}