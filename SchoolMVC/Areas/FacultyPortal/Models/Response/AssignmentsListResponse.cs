using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.FacultyPortal.Models.Response
{
    public class AssignmentsListResponse
    {
        public long? ASM_ID { get; set; }
        //public long? ASM_SubGr_ID { get; set; }
        //public long? ASM_Sub_ID { get; set; }
        public long? ASM_School_ID { get; set; }
        public long? ASM_Class_ID { get; set; }
        public long? ASM_Section_ID { get; set; }
        public long? ASM_Session_ID { get; set; }
        public long? ASM_FP_Id { get; set; }
        public long? ASM_SubGr_ID { get; set; }
        public long? ASM_Sub_ID { get; set; }
        public string ASM_Title { get; set; }
        public string ASM_Desc { get; set; }
        //public DateTime? ASM_StartDate { get; set; }
        //public DateTime? ASM_ExpDate { get; set; }
        //public DateTime Created_Date { get; set; }
        public string ASM_SGM_SubjectGroupName { get; set; }
        public string ASM_SBM_SubjectName { get; set; }
        public string ASM_UploadDoc { get; set; }
        public string ASM_CM_CLASSNAME { get; set; }
        public string ASM_SECM_SECTIONNAME { get; set; }
        public string ASM_StartDateS { get; set; }
        public string ASM_ExpDateS { get; set; }
        public long? Userid { get; set; }
        public string FP_Name { get; set; }

        public List<AssignmentStudentResponse> Students { get; set; }
    }


    public class AssignmentStudentResponse
    {
        public long? AST_ID { get; set; }
        public long? AST_ASM_ID { get; set; }
        public long? AST_StudentId { get; set; }

        public string SD_StudentSId { get; set; }
        public string SD_StudentName { get; set; }

        public string AST_UploadDoc { get; set; }
        public bool IsAbsent { get; set; }
    }

    public class UploadedStudentListResponse
    {
        public long? AST_ID { get; set; }
        public long? AST_ASM_ID { get; set; }
        public long? AST_StudentId { get; set; }

        public string SD_StudentSId { get; set; }
        public string SD_StudentName { get; set; }

        public string AST_UploadDoc { get; set; }
        public string Rollno { get; set; }
        public string SubmitteddateS { get; set; }
        public string Obtainedmarks { get; set; }
        public string AST_TotalMarks { get; set; }
        public string Total_Student { get; set; }
       
        public bool IsAbsent { get; set; }
    }

}