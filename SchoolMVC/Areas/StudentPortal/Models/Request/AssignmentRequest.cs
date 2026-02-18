using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolMVC.Areas.StudentPortal.Models.Request

{
    public class StudentAssignmentsListRequest
    {
        public long? ASM_ID { get; set; }
        public long? ASM_FP_Id { get; set; }
        public long? ASM_SubGr_ID { get; set; }
        public long? ASM_Sub_ID { get; set; }
        public long? ASM_School_ID { get; set; }
        public long? ASM_Class_ID { get; set; }
        public long? ASM_Section_ID { get; set; }
        public long? ASM_Session_ID { get; set; }
        public string ASM_Title { get; set; }
        public string ASM_Desc { get; set; }
        public int? CM_ID { get; set; }
        public DateTime ASM_StartDate { get; set; }
        public DateTime ASM_ExpDate { get; set; }
        public DateTime Created_Date { get; set; }
        public string ASM_SGM_SubjectGroupName { get; set; }
        public string ASM_SBM_SubjectName { get; set; }
        public string ASM_UploadDoc { get; set; }
        public string ASM_CM_CLASSNAME { get; set; }
        public string ASM_SECM_SECTIONNAME { get; set; }
        public string ASM_StartDateS { get; set; }
        public string ASM_ExpDateS { get; set; }
        public long? Userid { get; set; }
        public string AST_StudentId { get; set; }
        public string AST_UploadDoc { get; set; }
    }
    public class StudentAssignmentDtlsRequest
    {
        public int? asmId { get; set; }
    }
    public class StudentAssignmentMaster
    {

        public long? ASM_ID { get; set; }
        public long? ASM_FP_Id { get; set; }
        public long? ASM_SubGr_ID { get; set; }
        public string AST_StudentId { get; set; }
        public long? ASM_Sub_ID { get; set; }
        public long? ASM_School_ID { get; set; }
        public long? ASM_Class_ID { get; set; }
        public long? ASM_Section_ID { get; set; }
        public long? ASM_Session_ID { get; set; }
        public string ASM_Title { get; set; }
        public string ASM_Desc { get; set; }
        public int? CM_ID { get; set; }
        public DateTime? ASM_StartDate { get; set; }
        public DateTime? ASM_ExpDate { get; set; }
        public DateTime Created_Date { get; set; }
        public string ASM_SGM_SubjectGroupName { get; set; }
        public string ASM_SBM_SubjectName { get; set; }
        public string ASM_UploadDoc { get; set; }
        public string ASM_CM_CLASSNAME { get; set; }
        public string ASM_SECM_SECTIONNAME { get; set; }
        public string SBM_SubjectName { get; set; }
        public long? Userid { get; set; }

    }
    public class StudentWiseFacultyRequest
    {
        public long? SchoolId { get; set; }
        public long? ClassId { get; set; }
        public long? SectionId { get; set; }
        public long? SessionId { get; set; }
    }
    public class StudentWiseFacultyResponse
    {
        public long? FacultyId { get; set; }
        public string FacultyName { get; set; }
        public long? SubjectId { get; set; }
        public string SubjectCode { get; set; }
        public string SubjectName { get; set; }
        public string IsClassTeacherYN { get; set; }
    }
}
