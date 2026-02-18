using AccountManagementSystem.Models;
using SchoolMVC.Areas.FacultyPortal.Models.Request;
using SchoolMVC.Areas.FacultyPortal.Models.Response;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolMVC.Repositories
{
    public class FacultyRepository : IFacultyRepository
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;
        }
        #region API integration faculty Portal
        #region GetFacultyLogin
        public FacultyProfileMasters_FPM GetFacultyLogin(FacultyProfileMasters_FPM obj)
        {
            List<FacultyProfileMasters_FPM> ListObject = new List<FacultyProfileMasters_FPM>();
            FacultyProfileMasters_FPM DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FP_FacultyCode", obj.FP_FacultyCode));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetFacultyLogin", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new FacultyProfileMasters_FPM();
                    DataObject.FP_Id = Convert.ToInt64(rdr["FP_Id"]);
                    DataObject.FP_FacultyCode = Convert.ToString(rdr["FP_FacultyCode"]);
                    DataObject.FP_UserType = Convert.ToString(rdr["FP_UserType"]);
                    DataObject.FP_Password = Convert.ToString(rdr["FP_Password"]);
                    DataObject.FP_SchoolId = Convert.ToInt64(rdr["FP_SchoolId"]);
                    DataObject.FP_SessionId = Convert.ToInt64(rdr["FP_SessionId"]);
                    DataObject.FP_DesignationId = Convert.ToInt64(rdr["FP_DesignationId"]);
                    DataObject.FP_Phone = Convert.ToString(rdr["FP_Phone"]);
                    DataObject.FP_Name = Convert.ToString(rdr["FP_Name"]);
                    DataObject.SCM_SCHOOLNAME = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    DataObject.SM_SESSIONNAME = Convert.ToString(rdr["SM_SESSIONNAME"]);
                    DataObject.DM_Name = Convert.ToString(rdr["DM_Name"]);
                    //ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return DataObject;
        }
        #endregion
        #region Daily Attendance
        #region GetAttendanceList
        public List<AttendanceListResponse> GetAttendanceList(AttendanceListRequest obj)
        {
            List<AttendanceListResponse> ListObject = new List<AttendanceListResponse>();
            AttendanceListResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.SAM_ClassId == 0)
            {
                obj.SAM_ClassId = null;
            }
            if (obj.SAM_SectionId == 0)
            {
                obj.SAM_SectionId = null;
            }
            arrParams.Add(new SqlParameter("@TransType", "SELECT_LIST"));
            arrParams.Add(new SqlParameter("@SAM_SchoolId", obj.SAM_SchoolId));
            arrParams.Add(new SqlParameter("@SAM_SessionId", obj.SAM_SessionId));
            arrParams.Add(new SqlParameter("@SAM_ClassId", obj.SAM_ClassId));
            arrParams.Add(new SqlParameter("@SAM_SectionId", obj.SAM_SectionId));
            arrParams.Add(new SqlParameter("@SAM_Date_S", obj.SAM_Date_S));
            arrParams.Add(new SqlParameter("@TotalStudent", obj.TotalStudent));
            arrParams.Add(new SqlParameter("@PresentStudent", obj.PresentStudent));
            arrParams.Add(new SqlParameter("@AbsentStudent", obj.AbsentStudent));



            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_DailyAttendance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new AttendanceListResponse();
                    DataObject.SAM_ClassId = Convert.ToInt32(rdr["SAM_ClassId"]);
                    DataObject.SAM_SectionId = Convert.ToInt32(rdr["SAM_SectionId"]);
                    DataObject.SAM_Id = Convert.ToInt32(rdr["SAM_Id"]);
                    // DataObject.SAM_Id = rdr["SAM_Id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_Id"]);

                    DataObject.SAM_SchoolId = Convert.ToInt32(rdr["SAM_SchoolId"]);
                    DataObject.SAM_SessionId = Convert.ToInt32(rdr["SAM_SessionId"]);
                    DataObject.ClassName = Convert.ToString(rdr["ClassName"]);
                    DataObject.SectionName = rdr["SectionName"] == DBNull.Value ? "" : Convert.ToString(rdr["SectionName"]);
                    DataObject.SAM_Date_S = Convert.ToString(rdr["SAM_Date_S"]);
                    DataObject.SAM_Date = Convert.ToDateTime(rdr["SAM_Date"]);
                    DataObject.TotalStudent = Convert.ToString(rdr["TotalStudent"]);
                    DataObject.PresentStudent = Convert.ToString(rdr["PresentStudent"]);
                    DataObject.AbsentStudent = Convert.ToString(rdr["AbsentStudent"]);

                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetListForAttendance
        public List<ListForAttendanceResponse> GetListForAttendance(ListForAttendanceRequest obj)
        {
            List<ListForAttendanceResponse> ListObject = new List<ListForAttendanceResponse>();
            ListForAttendanceResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.SAM_SectionId == 0)
            {
                obj.SAM_SectionId = null;
            }
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@SAM_SchoolId", obj.SAM_SchoolId));
            arrParams.Add(new SqlParameter("@SAM_SessionId", obj.SAM_SessionId));
            arrParams.Add(new SqlParameter("@SAM_Date", obj.SAM_Date));
            arrParams.Add(new SqlParameter("@SAM_ClassId", obj.SAM_ClassId));
            arrParams.Add(new SqlParameter("@SAM_SectionId", obj.SAM_SectionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_DailyAttendance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ListForAttendanceResponse();
                    DataObject.StudentName = Convert.ToString(rdr["StudentName"]);
                    DataObject.StudentId = Convert.ToString(rdr["StudentId"]);
                    DataObject.ClassName = rdr["ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["ClassName"]);
                    DataObject.SAM_ClassId = rdr["SAM_ClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_ClassId"]);
                    DataObject.SAM_SectionId = rdr["SAM_SectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_SectionId"]);
                    DataObject.Roll = rdr["Roll"] == DBNull.Value ? "" : Convert.ToString(rdr["Roll"]);
                    DataObject.SectionName = rdr["SectionName"] == DBNull.Value ? "" : Convert.ToString(rdr["SectionName"]);
                    DataObject.IsAbsent = rdr["IsAbsent"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsAbsent"]);
                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion


        #endregion
        #region Live ClassDelete
        public string DeleteClasswiseLiveclass(int? id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));
            arrParams.Add(new SqlParameter("@CWLS_ID", id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClasswiseLiveclass", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region GetAssignmentList
        public List<AssignmentsListResponse> GetAssignmentList(AssignmentsListRequest obj)
        {
            List<AssignmentsListResponse> list = new List<AssignmentsListResponse>();
            Dictionary<int, AssignmentsListResponse> map = new Dictionary<int, AssignmentsListResponse>();

            List<SqlParameter> arrParams = new List<SqlParameter>
        {
        new SqlParameter("@TransType", "SELECT_LIST"),
        new SqlParameter("@ASM_School_ID", obj.ASM_School_ID),
        new SqlParameter("@ASM_Session_ID", obj.ASM_Session_ID),
        new SqlParameter("@ASM_FP_Id", (object)obj.ASM_FP_Id ?? DBNull.Value),
        new SqlParameter("@ASM_Class_ID", (object)obj.ASM_Class_ID ?? DBNull.Value),
        new SqlParameter("@ASM_Section_ID", (object)obj.ASM_Section_ID ?? DBNull.Value),
        new SqlParameter("@ASM_SubGr_ID", (object)obj.ASM_SubGr_ID ?? DBNull.Value),
        new SqlParameter("@ASM_Sub_ID", (object)obj.ASM_Sub_ID ?? DBNull.Value),
        new SqlParameter("@ASM_StartDateS", (object)obj.ASM_StartDateS ?? DBNull.Value),
        new SqlParameter("@ASM_ExpDateS", (object)obj.ASM_ExpDateS ?? DBNull.Value),
        new SqlParameter("@ASM_Title", (object)obj.ASM_Title ?? DBNull.Value),
        new SqlParameter("@ASM_Desc", (object)obj.ASM_Desc ?? DBNull.Value),
        new SqlParameter("@ASM_UploadDoc", (object)obj.ASM_UploadDoc ?? DBNull.Value)
        };

            SqlParameter outPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(outPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_AssignmentMaster",
                arrParams.ToArray()))
            {
                // ================= FIRST RESULT SET (Assignment Master) =================
                while (rdr.Read())
                {
                    var asm = new AssignmentsListResponse
                    {
                        ASM_ID = Convert.ToInt64(rdr["ASM_ID"]),
                        ASM_FP_Id = Convert.ToInt64(rdr["ASM_FP_Id"]),
                        ASM_Sub_ID = Convert.ToInt64(rdr["ASM_Sub_ID"]),
                        ASM_SubGr_ID = Convert.ToInt64(rdr["ASM_SubGr_ID"]),
                        ASM_Class_ID = Convert.ToInt64(rdr["ASM_Class_ID"]),
                        ASM_Section_ID = Convert.ToInt64(rdr["ASM_Section_ID"]),
                        ASM_School_ID = Convert.ToInt64(rdr["ASM_School_ID"]),
                        ASM_Session_ID = Convert.ToInt64(rdr["ASM_Session_ID"]),

                        ASM_SBM_SubjectName = Convert.ToString(rdr["ASM_SBM_SubjectName"]),
                        ASM_CM_CLASSNAME = Convert.ToString(rdr["ASM_CM_CLASSNAME"]),
                        ASM_SECM_SECTIONNAME = Convert.ToString(rdr["ASM_SECM_SECTIONNAME"]),
                        FP_Name = Convert.ToString(rdr["FP_Name"]),

                        ASM_Title = Convert.ToString(rdr["ASM_Title"]),
                        ASM_Desc = Convert.ToString(rdr["ASM_Desc"]),
                        ASM_StartDateS = Convert.ToString(rdr["ASM_StartDateS"]),
                        ASM_ExpDateS = Convert.ToString(rdr["ASM_ExpDateS"]),
                        ASM_UploadDoc = Convert.ToString(rdr["ASM_UploadDoc"]),

                        Students = new List<AssignmentStudentResponse>()
                    };

                    list.Add(asm);
                    map.Add((int)asm.ASM_ID, asm);
                }

                // ================= SECOND RESULT SET (Students / Transactions) =================
                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        long? asmId = Convert.ToInt32(rdr["AST_ASM_ID"]);

                        if (map.ContainsKey((int)asmId))
                        {
                            map[(int)asmId].Students.Add(new AssignmentStudentResponse
                            {
                                AST_ID = Convert.ToInt64(rdr["AST_ID"]),
                                AST_ASM_ID = asmId,
                                AST_StudentId = Convert.ToInt64(rdr["AST_StudentId"]),
                                SD_StudentSId = Convert.ToString(rdr["SD_StudentSId"]),
                                SD_StudentName = Convert.ToString(rdr["SD_StudentName"]),
                                AST_UploadDoc = Convert.ToString(rdr["AST_UploadDoc"]),
                                IsAbsent = Convert.ToBoolean(rdr["IsAbsent"])
                            });
                        }
                    }
                }
            }

            return list;
        }

        public List<UploadedStudentListResponse> GetUploadedStudentList(AssignmentsListRequest obj)
        {
            List<UploadedStudentListResponse> list = new List<UploadedStudentListResponse>();

            List<SqlParameter> arrParams = new List<SqlParameter>
        {
        new SqlParameter("@TransType", "UPLOAD_STUDENT_LIST"),
        new SqlParameter("@ASM_ID", obj.ASM_ID),
        };

            SqlParameter outPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(outPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_AssignmentMaster",
                arrParams.ToArray()))
            {
                while (rdr.Read())
                {
                    var asm = new UploadedStudentListResponse
                    {
                        AST_ID = Convert.ToInt64(rdr["AST_ID"]),
                        AST_ASM_ID = Convert.ToInt64(rdr["AST_ASM_ID"]),
                        AST_StudentId = Convert.ToInt64(rdr["AST_StudentId"]),
                        SD_StudentSId = Convert.ToString(rdr["SD_StudentSId"]),
                        SD_StudentName = Convert.ToString(rdr["SD_StudentName"]),
                        AST_UploadDoc = Convert.ToString(rdr["AST_UploadDoc"]),
                        IsAbsent = Convert.ToBoolean(rdr["IsAbsent"]),
                        Rollno = Convert.ToString(rdr["Rollno"]),
                        SubmitteddateS = Convert.ToString(rdr["SubmitteddateS"]),
                        Obtainedmarks = Convert.ToString(rdr["obtainedmarks"]),
                        AST_TotalMarks = Convert.ToString(rdr["AST_TotalMarks"]),
                        Total_Student = Convert.ToString(rdr["Total_Student"]),

                    };

                    list.Add(asm);
                }

            }

            return list;
        }
        public AssignmentsListResponse GetAssignmentById(int asmId)
        {
            AssignmentsListResponse DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>
            {
                new SqlParameter("@TransType", "SELECT_ONE"),
                new SqlParameter("@ASM_ID", asmId)
            };

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_AssignmentMaster",
                arrParams.ToArray()))
            {
                // ========== FIRST RESULT SET (Assignment Master) ==========
                if (rdr.Read())
                {
                    DataObject = new AssignmentsListResponse
                    {
                        ASM_ID = Convert.ToInt32(rdr["ASM_ID"]),
                        ASM_FP_Id = Convert.ToInt32(rdr["ASM_FP_Id"]),
                        ASM_Sub_ID = Convert.ToInt32(rdr["ASM_Sub_ID"]),
                        ASM_SubGr_ID = Convert.ToInt32(rdr["ASM_SubGr_ID"]),
                        ASM_Class_ID = Convert.ToInt32(rdr["ASM_Class_ID"]),
                        ASM_Section_ID = Convert.ToInt32(rdr["ASM_Section_ID"]),
                        ASM_School_ID = Convert.ToInt32(rdr["ASM_School_ID"]),
                        ASM_Session_ID = Convert.ToInt32(rdr["ASM_Session_ID"]),

                        ASM_CM_CLASSNAME = rdr["ASM_CM_CLASSNAME"].ToString(),
                        ASM_SECM_SECTIONNAME = Convert.ToString(rdr["ASM_SECM_SECTIONNAME"]),
                        ASM_SBM_SubjectName = Convert.ToString(rdr["ASM_SBM_SubjectName"]),
                        FP_Name = Convert.ToString(rdr["FP_Name"]),

                        ASM_Title = rdr["ASM_Title"].ToString(),
                        ASM_Desc = rdr["ASM_Desc"].ToString(),
                        ASM_StartDateS = rdr["ASM_StartDateS"].ToString(),
                        ASM_ExpDateS = rdr["ASM_ExpDateS"].ToString(),
                        ASM_UploadDoc = rdr["ASM_UploadDoc"].ToString(),

                        Students = new List<AssignmentStudentResponse>()
                    };
                }

                // ========== SECOND RESULT SET (Students) ==========
                if (DataObject != null && rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        DataObject.Students.Add(new AssignmentStudentResponse
                        {
                            AST_ID = Convert.ToInt32(rdr["AST_ID"]),
                            AST_ASM_ID = Convert.ToInt32(rdr["AST_ASM_ID"]),
                            AST_StudentId = Convert.ToInt32(rdr["AST_StudentId"]),
                            SD_StudentSId = Convert.ToString(rdr["SD_StudentSId"]),
                            SD_StudentName = Convert.ToString(rdr["SD_StudentName"]),
                            AST_UploadDoc = Convert.ToString(rdr["AST_UploadDoc"]),
                            IsAbsent = Convert.ToBoolean(rdr["IsAbsent"])
                        });
                    }
                }
            }

            return DataObject;
        }
        public string DeleteAssignment(int? id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));
            arrParams.Add(new SqlParameter("@Att_Id", id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AssignmentMaster", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }

        #endregion

        #endregion
       


    }
}