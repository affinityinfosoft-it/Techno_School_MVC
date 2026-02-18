using AccountManagementSystem.Models;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
namespace SchoolMVC.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;
        }

        #region Enquery
        //public string InsertUpdateEnquery(StudentEnquery_ENQ obj)
        //{
        //    try
        //    {
        //        List<SqlParameter> arrParams = new List<SqlParameter>();
        //        if (obj.ENQ_Id == null)
        //        {

        //            arrParams.Add(new SqlParameter("@TRANSTYPE", "INSERT"));
        //        }
        //        if (obj.ENQ_Id != null)
        //        {
        //            arrParams.Add(new SqlParameter("@ENQ_Id", obj.ENQ_Id));
        //            arrParams.Add(new SqlParameter("@TRANSTYPE", "UPDATE"));
        //        }
        //        arrParams.Add(new SqlParameter("@ENQ_SexId", obj.ENQ_SexId));
        //        arrParams.Add(new SqlParameter("@ENQ_DOB", obj.ENQ_DOB));
        //        arrParams.Add(new SqlParameter("@ENQ_Age", obj.ENQ_Age));
        //        arrParams.Add(new SqlParameter("@ENQ_ClassId", obj.ENQ_ClassId));
        //        arrParams.Add(new SqlParameter("@ENQ_CreatedBy", obj.ENQ_CreatedBy));
        //        arrParams.Add(new SqlParameter("@ENQ_Date", obj.ENQ_Date));
        //        arrParams.Add(new SqlParameter("@ENQ_GuardianName", obj.ENQ_GuardianName));
        //        arrParams.Add(new SqlParameter("@ENQ_MobNo", obj.ENQ_MobNo));
        //        arrParams.Add(new SqlParameter("@ENQ_SessionId", obj.ENQ_SessionId));
        //        arrParams.Add(new SqlParameter("@ENQ_StudentName", obj.ENQ_StudentName));
        //        arrParams.Add(new SqlParameter("@ENQ_SchoolId", obj.ENQ_SchoolId));
        //        // added on 01/08/2025 by uttaran
        //        arrParams.Add(new SqlParameter("@ENQ_IsTest", obj.ENQ_IsTest));
        //        arrParams.Add(new SqlParameter("@ENQ_DocFile", obj.ENQ_DocFile));
        //        arrParams.Add(new SqlParameter("@ENQ_ObMarks", obj.ENQ_ObMarks));
        //        arrParams.Add(new SqlParameter("@ENQ_Attributes", obj.ENQ_Attributes));
        //        // added on 10/12/2018 by Mousumi
        //        arrParams.Add(new SqlParameter("@ENQ_IsForm", obj.ENQ_IsForm));
        //        arrParams.Add(new SqlParameter("@ENQ_FormNo", obj.ENQ_FormNo));
        //        arrParams.Add(new SqlParameter("@ENQ_Type", obj.ENQ_Type));
        //        arrParams.Add(new SqlParameter("@ENQ_AlternativeMobNo", obj.ENQ_AlternativeMobNo));
        //        // added on 24/01/2019 by7 Mousumi 
        //        arrParams.Add(new SqlParameter("@ENQ_FormAmount", obj.ENQ_FormAmount));
        //        arrParams.Add(new SqlParameter("@Paymode", obj.Paymode));
        //        arrParams.Add(new SqlParameter("@BankName", obj.BankName));
        //        arrParams.Add(new SqlParameter("@BranchName", obj.BranchName));
        //        arrParams.Add(new SqlParameter("@CheqDDNo", obj.CheqDDNo));
        //        arrParams.Add(new SqlParameter("@CheqDDDate", obj.CheqDDDate));
        //        arrParams.Add(new SqlParameter("@Card_TrnsRefNo", obj.Card_TrnsRefNo));
        //        SqlParameter OutPutEnquiryNo = new SqlParameter("@OutPutEnquiryNo", SqlDbType.VarChar, 100);
        //        OutPutEnquiryNo.Direction = ParameterDirection.Output;
        //        arrParams.Add(OutPutEnquiryNo);
        //        SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentEnquery", arrParams.ToArray());
        //        var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
        //        return val;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}



        public Tuple<string, string> InsertUpdateEnquery(StudentEnquery_ENQ obj)
        {
            try
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();
                SqlParameter OutPutEnquiryNo = new SqlParameter("@OutPutEnquiryNo", SqlDbType.VarChar, 100);
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutEnquiryNo.Direction = ParameterDirection.Output;
                OutPutId.Direction = ParameterDirection.Output;

                if (obj.ENQ_Id == null)
                {
                    arrParams.Add(new SqlParameter("@TRANSTYPE", "INSERT"));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@ENQ_Id", obj.ENQ_Id));
                    arrParams.Add(new SqlParameter("@TRANSTYPE", "UPDATE"));
                }

                // Add other parameters
                arrParams.Add(new SqlParameter("@ENQ_SexId", obj.ENQ_SexId));
                arrParams.Add(new SqlParameter("@ENQ_DOB", obj.ENQ_DOB));
                arrParams.Add(new SqlParameter("@ENQ_Age", obj.ENQ_Age));
                arrParams.Add(new SqlParameter("@ENQ_ClassId", obj.ENQ_ClassId));
                arrParams.Add(new SqlParameter("@ENQ_CreatedBy", obj.ENQ_CreatedBy));
                arrParams.Add(new SqlParameter("@ENQ_Date", obj.ENQ_Date));
                arrParams.Add(new SqlParameter("@ENQ_GuardianName", obj.ENQ_GuardianName));
                arrParams.Add(new SqlParameter("@ENQ_MobNo", obj.ENQ_MobNo));
                arrParams.Add(new SqlParameter("@ENQ_SessionId", obj.ENQ_SessionId));
                arrParams.Add(new SqlParameter("@ENQ_StudentName", obj.ENQ_StudentName));
                arrParams.Add(new SqlParameter("@ENQ_SchoolId", obj.ENQ_SchoolId));
                // added on 01/08/2025 by uttaran
                arrParams.Add(new SqlParameter("@ENQ_IsTest", obj.ENQ_IsTest));
                arrParams.Add(new SqlParameter("@ENQ_DocFile", obj.ENQ_DocFile));
                arrParams.Add(new SqlParameter("@ENQ_ObMarks", obj.ENQ_ObMarks));
                arrParams.Add(new SqlParameter("@ENQ_Attributes", obj.ENQ_Attributes));
                // added on 10/12/2018 by Mousumi
                arrParams.Add(new SqlParameter("@ENQ_IsForm", obj.ENQ_IsForm));
                arrParams.Add(new SqlParameter("@ENQ_FormNo", obj.ENQ_FormNo));
                arrParams.Add(new SqlParameter("@ENQ_Type", obj.ENQ_Type));
                arrParams.Add(new SqlParameter("@ENQ_AlternativeMobNo", obj.ENQ_AlternativeMobNo));
                // added on 24/01/2019 by7 Mousumi 
                arrParams.Add(new SqlParameter("@ENQ_FormAmount", obj.ENQ_FormAmount));
                arrParams.Add(new SqlParameter("@Paymode", obj.Paymode));
                arrParams.Add(new SqlParameter("@BankName", obj.BankName));
                arrParams.Add(new SqlParameter("@BranchName", obj.BranchName));
                arrParams.Add(new SqlParameter("@CheqDDNo", obj.CheqDDNo));
                arrParams.Add(new SqlParameter("@CheqDDDate", obj.CheqDDDate));
                arrParams.Add(new SqlParameter("@Card_TrnsRefNo", obj.Card_TrnsRefNo));

                // Other parameters...
                arrParams.Add(OutPutEnquiryNo);
                arrParams.Add(OutPutId);

                // Execute the stored procedure
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentEnquery", arrParams.ToArray());

                // Return the OutputId and EnquiryNo
                return new Tuple<string, string>(Convert.ToString(OutPutId.Value), Convert.ToString(OutPutEnquiryNo.Value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<StudentEnquery_ENQ> GetEnqueryList(StudentEnquery_ENQ obj)
        {
            List<StudentEnquery_ENQ> ListObject = new List<StudentEnquery_ENQ>();
            StudentEnquery_ENQ DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TRANSTYPE", "SELECT"));
            arrParams.Add(new SqlParameter("@ENQ_SchoolId", obj.ENQ_SchoolId));
            arrParams.Add(new SqlParameter("@ENQ_SessionId", obj.ENQ_SessionId));
            if (obj.ENQ_ClassId != null) arrParams.Add(new SqlParameter("@ENQ_ClassId", obj.ENQ_ClassId));
            if (obj.ENQ_EnqueryNo != null) arrParams.Add(new SqlParameter("@ENQ_EnqueryNo", obj.ENQ_EnqueryNo));
            if (obj.ENQ_StudentName != null) arrParams.Add(new SqlParameter("@ENQ_StudentName", obj.ENQ_StudentName));
            if (obj.FromDate != null)
                arrParams.Add(new SqlParameter("@FromDate", obj.FromDate));

            if (obj.ToDate != null)
                arrParams.Add(new SqlParameter("@ToDate", obj.ToDate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentEnquery", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentEnquery_ENQ();
                    DataObject.ENQ_Id = Convert.ToInt64(rdr["ENQ_Id"]);
                    DataObject.ENQ_EnqueryNo = Convert.ToString(rdr["ENQ_EnqueryNo"]);
                    DataObject.ENQ_StudentName = Convert.ToString(rdr["ENQ_StudentName"]);
                    DataObject.ENQ_CM_CLASSNAME = Convert.ToString(rdr["ENQ_CM_CLASSNAME"]);
                    DataObject.ENQ_Date = Convert.ToDateTime(rdr["ENQ_Date"]);
                    DataObject.ENQ_MobNo = Convert.ToString(rdr["ENQ_MobNo"]);
                    DataObject.ENQ_IsAdmitted = Convert.ToBoolean(rdr["ENQ_IsAdmitted"]);
                    DataObject.ENQ_Type = Convert.ToString(rdr["ENQ_Type"]);
                    DataObject.ENQ_GuardianName = Convert.ToString(rdr["ENQ_GuardianName"]);

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region Admission Details
        public string InsertUpdateAdmissionDetails(StudetDetails_SD obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.SD_Id == null)
            {

                arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            }
            if (obj.SD_Id != null)
            {
                arrParams.Add(new SqlParameter("@SD_Id", obj.SD_Id));
                arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
            }
            arrParams.Add(new SqlParameter("@SD_OldStudentId", obj.SD_OldStudentId));
            arrParams.Add(new SqlParameter("@SD_FormNo", obj.SD_FormNo));
            arrParams.Add(new SqlParameter("@SD_AppliactionDate", obj.SD_AppliactionDate));
            arrParams.Add(new SqlParameter("@SD_ClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@Sd_SexId", obj.Sd_SexId));
            arrParams.Add(new SqlParameter("@SD_DOB", obj.SD_DOB));
            arrParams.Add(new SqlParameter("@SD_CasteId", obj.SD_CasteId));
            arrParams.Add(new SqlParameter("@SD_FatherName", obj.SD_FatherName));
            arrParams.Add(new SqlParameter("@SD_MotherName", obj.SD_MotherName));
            arrParams.Add(new SqlParameter("@SD_PresentAddress", obj.SD_PresentAddress));
            arrParams.Add(new SqlParameter("@SD_StateId", obj.SD_StateId));
            arrParams.Add(new SqlParameter("@SD_DistrictId", obj.SD_DistrictId));
            arrParams.Add(new SqlParameter("@SD_PinCode", obj.SD_PinCode));
            arrParams.Add(new SqlParameter("@SD_ContactNo1", obj.SD_ContactNo1));
            arrParams.Add(new SqlParameter("@SD_ContactNo2", obj.SD_ContactNo2));
            arrParams.Add(new SqlParameter("@SD_EmailId", obj.SD_EmailId));
            arrParams.Add(new SqlParameter("@SD_LastSchoolName", obj.SD_LastSchoolName));
            arrParams.Add(new SqlParameter("@SD_TCDate", obj.SD_TCDate));
            arrParams.Add(new SqlParameter("@SD_StudentCategoryId", obj.SD_StudentCategoryId));
            arrParams.Add(new SqlParameter("@SD_TCNo", obj.SD_TCNo));
            arrParams.Add(new SqlParameter("@SD_Photo", obj.SD_Photo));
            arrParams.Add(new SqlParameter("@SD_MotherTongueId", obj.SD_MotherTongueId));
            arrParams.Add(new SqlParameter("@SD_CreatedBy", obj.SD_CreatedBy));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_BloodGroupId", obj.SD_BloodGroupId));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SecondLanguageId", obj.SD_SecondLanguageId));
            arrParams.Add(new SqlParameter("@SD_ThirdLanguageId", obj.SD_ThirdLanguageId));
            arrParams.Add(new SqlParameter("@SD_AadharNo", obj.SD_AadharNo));
            arrParams.Add(new SqlParameter("@SD_ReligionId", obj.SD_ReligionId));
            arrParams.Add(new SqlParameter("@SD_TCType", obj.SD_TCType));
            arrParams.Add(new SqlParameter("@SD_HouseId", obj.SD_HouseId));
            arrParams.Add(new SqlParameter("@SD_PermanentAddress", obj.SD_PermanentAddress));
            arrParams.Add(new SqlParameter("@SD_PermanentStateId", obj.SD_PermanentStateId));
            arrParams.Add(new SqlParameter("@SD_PermanentDistrictId", obj.SD_PermanentDistrictId));
            arrParams.Add(new SqlParameter("@SD_PermanentPin", obj.SD_PermanentPin));
            arrParams.Add(new SqlParameter("@ENQ_Id", obj.ENQ_Id));
            arrParams.Add(new SqlParameter("@SD_IsSingleParent", obj.SD_IsSingleParent));
            arrParams.Add(new SqlParameter("@SD_PermanentPoliceStation", obj.SD_PermanentPoliceStation));
            arrParams.Add(new SqlParameter("@SD_PoliceStation", obj.SD_PoliceStation));
            arrParams.Add(new SqlParameter("@SD_NationalityId", obj.SD_NationalityId));
            arrParams.Add(new SqlParameter("@SD_LocalGuardianName", obj.SD_LocalGuardianName));
            arrParams.Add(new SqlParameter("@SD_LocalGuardianPhoneNo", obj.SD_LocalGuardianPhoneNo));
            arrParams.Add(new SqlParameter("@SD_IsTransport", obj.SD_IsTransport));
            arrParams.Add(new SqlParameter("@SD_TransportTypeId", obj.SD_TransportTypeId));
            arrParams.Add(new SqlParameter("@SD_TransportFare", obj.SD_TransportFare));
            arrParams.Add(new SqlParameter("@SD_RouteId", obj.SD_RouteId));
            arrParams.Add(new SqlParameter("@SD_PickLocationId", obj.SD_PickLocationId));
            arrParams.Add(new SqlParameter("@SD_DropLocationId", obj.SD_DropLocationId));
            arrParams.Add(new SqlParameter("@SD_TransportMonthId", obj.SD_TransportMonthId));
            arrParams.Add(new SqlParameter("@SD_GQualification", obj.SD_GQualification));
            arrParams.Add(new SqlParameter("@SD_GOccupation", obj.SD_GOccupation));
            arrParams.Add(new SqlParameter("@SD_GDesignation", obj.SD_GDesignation));
            arrParams.Add(new SqlParameter("@SD_GIncome", obj.SD_GIncome));

            SqlParameter OutPutAdmissionNo = new SqlParameter("@OutPutAdmissionNo", SqlDbType.VarChar, 100);
            OutPutAdmissionNo.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutAdmissionNo);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AdmissionDetails", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<StudetDetails_SD> GetAdmissionStudentList(StudetDetails_SD obj)
        {
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            StudetDetails_SD DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SD_SessionId));
            if (obj.SD_ClassId != null) arrParams.Add(new SqlParameter("@SD_ClassId", obj.SD_ClassId));
            if (obj.SD_AppliactionNo != null) arrParams.Add(new SqlParameter("@SD_AppliactionNo", obj.SD_AppliactionNo));
            if (obj.SD_StudentName != null) arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            if (obj.FromDate != null)
                arrParams.Add(new SqlParameter("@FromDate", obj.FromDate));

            if (obj.ToDate != null)
                arrParams.Add(new SqlParameter("@ToDate", obj.ToDate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AdmissionDetails", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudetDetails_SD();
                    DataObject.SD_Id = Convert.ToInt64(rdr["SD_Id"]);
                    DataObject.SD_AppliactionNo = Convert.ToString(rdr["SD_AppliactionNo"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_FatherName = Convert.ToString(rdr["SD_FatherName"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_AppliactionDate = Convert.ToDateTime(rdr["SD_AppliactionDate"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        #endregion
        #region Exam Attendance
        public List<StudentExamAttandanceMaster_SEA> GetStudentListForExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            if (obj.SEA_SubjectId == 0)
            {
                obj.SEA_SubjectId = null;
            }
            if (obj.SEA_SecId == 0)
            {
                obj.SEA_SecId = null;
            }
            List<StudentExamAttandanceMaster_SEA> ListObject = new List<StudentExamAttandanceMaster_SEA>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@SEA_SchoolId", obj.SEA_SchoolId));
            arrParams.Add(new SqlParameter("@SEA_SessionId", obj.SEA_SessionId));
            arrParams.Add(new SqlParameter("@SEA_Attn_Date", obj.SEA_Attn_Date));
            arrParams.Add(new SqlParameter("@SEA_ClassId", obj.SEA_ClassId));
            arrParams.Add(new SqlParameter("@SEA_SecId", obj.SEA_SecId));
            arrParams.Add(new SqlParameter("@SEA_SubjectId", obj.SEA_SubjectId));
            arrParams.Add(new SqlParameter("@SEA_TermId", obj.SEA_TermId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_ExamAttendance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudentExamAttandanceMaster_SEA DataObject = new StudentExamAttandanceMaster_SEA();
                    DataObject.SEA_StudentName = Convert.ToString(rdr["SEA_StudentName"]);
                    DataObject.SEA_StudentId = Convert.ToString(rdr["SEA_StudentId"]);
                    DataObject.SEA_ClassName = rdr["SEA_ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["SEA_ClassName"]);
                    DataObject.SEA_ClassId = rdr["SEA_ClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SEA_ClassId"]);
                    DataObject.SEA_SecId = rdr["SEA_SecId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SEA_SecId"]);
                    DataObject.SEA_RollNo = rdr["SEA_RollNo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SEA_RollNo"]);
                    DataObject.SEA_SectionName = rdr["SEA_SectionName"] == DBNull.Value ? "" : Convert.ToString(rdr["SEA_SectionName"]);
                    DataObject.SEA_isAbsent = rdr["SEA_isAbsent"] == DBNull.Value ? false : Convert.ToBoolean(rdr["SEA_isAbsent"]);
                    ListObject.Add(DataObject);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string InsertUpdateExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SEAD_StudentId", typeof(string));
            dt.Columns.Add("SEAD_IsAbsent", typeof(bool));
            foreach (var data in obj.ExamAttendanceList)
            {
                DataRow dr = dt.NewRow();
                dr["SEAD_StudentId"] = data.SEAD_StudentId;
                dr["SEAD_IsAbsent"] = data.SEAD_IsAbsent;

                dt.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            arrParams.Add(new SqlParameter("@SEA_SchoolId", obj.SEA_SchoolId));
            arrParams.Add(new SqlParameter("@SEA_SessionId", obj.SEA_SessionId));
            arrParams.Add(new SqlParameter("@SEA_Attn_Date", obj.SEA_Attn_Date));
            arrParams.Add(new SqlParameter("@SEA_ClassId", obj.SEA_ClassId));
            arrParams.Add(new SqlParameter("@SEA_SecId", obj.SEA_SecId));
            arrParams.Add(new SqlParameter("@SEA_SubjectId", obj.SEA_SubjectId));
            arrParams.Add(new SqlParameter("@SEA_TermId", obj.SEA_TermId));
            arrParams.Add(new SqlParameter("@ExamAttendanceType", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ExamAttendance", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<StudentExamAttandanceMaster_SEA> ExamAttendanceList(StudentExamAttandanceMaster_SEA obj)
        {
            List<StudentExamAttandanceMaster_SEA> ListObject = new List<StudentExamAttandanceMaster_SEA>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.SEA_ClassId == 0)
            {
                obj.SEA_ClassId = null;
            }
            if (obj.SEA_SecId == 0)
            {
                obj.SEA_SecId = null;
            }
            if (obj.SEA_SubjectId == 0)
            {
                obj.SEA_SubjectId = null;
            }
            if (obj.SEA_TermId == 0)
            {
                obj.SEA_TermId = null;
            }
            if (obj.FromDate != null)
                arrParams.Add(new SqlParameter("@FromDate", obj.FromDate));

            if (obj.ToDate != null)
                arrParams.Add(new SqlParameter("@ToDate", obj.ToDate));

            arrParams.Add(new SqlParameter("@TransType", "SELECT_LIST"));
            arrParams.Add(new SqlParameter("@SEA_SchoolId", obj.SEA_SchoolId));
            arrParams.Add(new SqlParameter("@SEA_SessionId", obj.SEA_SessionId));
            arrParams.Add(new SqlParameter("@SEA_SubjectId", obj.SEA_SubjectId));
            arrParams.Add(new SqlParameter("@SEA_ClassId", obj.SEA_ClassId));
            arrParams.Add(new SqlParameter("@SEA_TermId", obj.SEA_TermId));
            arrParams.Add(new SqlParameter("@SEA_SecId", obj.SEA_SecId));
            //arrParams.Add(new SqlParameter("@SEA_Attn_Date_S", obj.SEA_Attn_Date_S));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_ExamAttendance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudentExamAttandanceMaster_SEA DataObject = new StudentExamAttandanceMaster_SEA();
                    DataObject.SEA_SchoolId = Convert.ToInt32(rdr["SEA_SchoolId"]);
                    DataObject.SEA_ClassId = Convert.ToInt32(rdr["SEA_ClassId"]);
                    DataObject.SEA_SecId = Convert.ToInt32(rdr["SEA_SecId"]);
                    DataObject.SEA_SubjectId = Convert.ToInt32(rdr["SEA_SubjectId"]);
                    DataObject.SEA_TermId = Convert.ToInt32(rdr["SEA_TermId"]);
                    DataObject.SEA_TermName = Convert.ToString(rdr["TM_TermName"]);
                    DataObject.SEA_SubjectName = Convert.ToString(rdr["SBM_SubjectName"]);
                    DataObject.SEA_ClassName = rdr["CM_CLASSNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SEA_Attn_Id = Convert.ToInt64(rdr["SEA_Attn_Id"]);
                    DataObject.SEA_Attn_Date = Convert.ToDateTime(rdr["SEA_Attn_Date"]);
                    DataObject.SEA_Attn_Date_S = Convert.ToString(rdr["SEA_Attn_Date_S"]);
                    DataObject.SEA_SecId = rdr["SEA_SecId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SEA_SecId"]);
                    DataObject.SEA_SectionName = rdr["SECM_SECTIONNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["SECM_SECTIONNAME"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string DeleteExamAttendance(int? id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));
            arrParams.Add(new SqlParameter("@Att_Id", id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ExamAttendance", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region Daily Attendance
        public List<StudentAttendenceMaster_SAM> GetStudentListForAttendance(StudentAttendenceMaster_SAM obj)
        {
            if (obj.SAM_SectionId == 0)
            {
                obj.SAM_SectionId = null;
            }
            List<StudentAttendenceMaster_SAM> ListObject = new List<StudentAttendenceMaster_SAM>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
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
                    StudentAttendenceMaster_SAM DataObject = new StudentAttendenceMaster_SAM();
                    DataObject.StudentName = Convert.ToString(rdr["StudentName"]);
                    DataObject.StudentId = Convert.ToString(rdr["StudentId"]);
                    DataObject.ClassName = rdr["ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["ClassName"]);
                    DataObject.SAM_ClassId = rdr["SAM_ClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_ClassId"]);
                    DataObject.SAM_SectionId = rdr["SAM_SectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_SectionId"]);
                    DataObject.Roll = rdr["Roll"] == DBNull.Value ? "" : Convert.ToString(rdr["Roll"]);
                    DataObject.SectionName = rdr["SectionName"] == DBNull.Value ? "" : Convert.ToString(rdr["SectionName"]);
                    DataObject.IsAbsent = rdr["IsAbsent"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsAbsent"]);
                    DataObject.IsHalfDay = rdr["IsHalfDay"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsHalfDay"]);
                    DataObject.IsLateComing = rdr["IsLateComing"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsLateComing"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string InsertUpdateAttendance(StudentAttendenceMaster_SAM obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SEAD_StudentId", typeof(string));
            dt.Columns.Add("SEAD_IsAbsent", typeof(bool));
            dt.Columns.Add("SEAD_IsHalfDay", typeof(bool));
            dt.Columns.Add("SEAD_IsLateComing", typeof(bool));
            foreach (var data in obj.attendnceList)
            {
                if ((data.IsAbsent ?? false)
  || (data.IsHalfDay ?? false)
  || (data.IsLateComing ?? false))
                {
                    DataRow dr = dt.NewRow();
                    dr["SEAD_StudentId"] = data.StudentId;
                    dr["SEAD_IsAbsent"] = data.IsAbsent ?? false;
                    dr["SEAD_IsHalfDay"] = data.IsHalfDay ?? false;
                    dr["SEAD_IsLateComing"] = data.IsLateComing ?? false;
                    dt.Rows.Add(dr);
                }
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            arrParams.Add(new SqlParameter("@SAM_SchoolId", obj.SAM_SchoolId));
            arrParams.Add(new SqlParameter("@SAM_SessionId", obj.SAM_SessionId));
            arrParams.Add(new SqlParameter("@SAM_Date", obj.SAM_Date));
            arrParams.Add(new SqlParameter("@SAM_ClassId", obj.SAM_ClassId));
            arrParams.Add(new SqlParameter("@SAM_SectionId", obj.SAM_SectionId));
            arrParams.Add(new SqlParameter("@ExamAttendanceType", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_DailyAttendance", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<StudentAttendenceMaster_SAM> AttendanceList(StudentAttendenceMaster_SAM obj)
        {
            List<StudentAttendenceMaster_SAM> ListObject = new List<StudentAttendenceMaster_SAM>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.SAM_ClassId == 0)
            {
                obj.SAM_ClassId = null;
            }
            if (obj.SAM_SectionId == 0)
            {
                obj.SAM_SectionId = null;
            }
            if (obj.FromDateS != null)
                arrParams.Add(new SqlParameter("@FromDateS", obj.FromDateS));

            if (obj.ToDateS != null)
                arrParams.Add(new SqlParameter("@ToDateS", obj.ToDateS));
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
                    StudentAttendenceMaster_SAM DataObject = new StudentAttendenceMaster_SAM();
                    DataObject.SAM_ClassId = Convert.ToInt32(rdr["SAM_ClassId"]);
                    DataObject.SAM_SectionId = Convert.ToInt32(rdr["SAM_SectionId"]);
                    DataObject.SAM_Id = Convert.ToInt32(rdr["SAM_Id"]);
                    // DataObject.SAM_Id = rdr["SAM_Id"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SAM_Id"]);

                    DataObject.SAM_SchoolId = Convert.ToInt32(rdr["SAM_SchoolId"]);
                    DataObject.SAM_SessionId = Convert.ToInt32(rdr["SAM_SessionId"]);
                    DataObject.ClassName = Convert.ToString(rdr["ClassName"]);
                    DataObject.SectionName = rdr["SectionName"] == DBNull.Value ? "" : Convert.ToString(rdr["SectionName"]);
                    DataObject.SAM_Date_S = Convert.ToString(rdr["SAM_Date_S"]);
                    DataObject.SAM_Date = rdr["SAM_Date"] != DBNull.Value ? (DateTime?)rdr["SAM_Date"] : null;

                    //DataObject.SAM_Date = Convert.ToDateTime(rdr["SAM_Date"]);
                    DataObject.TotalStudent = Convert.ToInt32(rdr["TotalStudent"]);
                    DataObject.PresentStudent = Convert.ToInt32(rdr["PresentStudent"]);
                    DataObject.AbsentStudent = Convert.ToInt32(rdr["AbsentStudent"]);
                    DataObject.HalfDayStudent = Convert.ToInt32(rdr["HalfDayStudent"]);
                    DataObject.LateComingStudent = Convert.ToInt32(rdr["LateComingStudent"]);

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string DeleteAttendance(int? id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));
            arrParams.Add(new SqlParameter("@Att_Id", id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_DailyAttendance", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
     
        #endregion
        #region Student Promotion

        public List<clsStudentList> studentsForPromotion(clsStudentList query)
        {
            List<clsStudentList> dbResults = new List<clsStudentList>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SelectStudentsForPromotion"));
            arrParams.Add(new SqlParameter("@SchoolId", query.SchoolId));
            arrParams.Add(new SqlParameter("@SessionId", query.SessionId));
            if (query.ClassId != 0) arrParams.Add(new SqlParameter("@ClassId", query.ClassId));
            if (query.SectionId != 0) arrParams.Add(new SqlParameter("@SectionId", query.SectionId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "StudentsPromotion_SP", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    try
                    {
                        dbResults.Add(new clsStudentList
                        {
                            SD_Id = Convert.ToInt32(rdr["SD_Id"].ToString()),
                            SD_AppliactionNo = Convert.ToString(rdr["SD_AppliactionNo"]),
                            StudentId = Convert.ToString(rdr["SD_StudentId"]),
                            SD_RegistrationNo = Convert.ToString(rdr["SD_RegistrationNo"]),
                            StudentName = Convert.ToString(rdr["SD_StudentName"]),
                            ClassId = Convert.ToInt32(rdr["SD_CurrentClassId"]),
                            ClassName = Convert.ToString(rdr["CM_CLASSNAME"]),
                            Roll = Convert.ToInt32(rdr["SD_CurrentRoll"]),
                            SectionId = Convert.ToInt32(rdr["SD_CurrentSectionId"]),
                            SectionName = Convert.ToString(rdr["SECM_SECTIONNAME"]),
                            SessionId = Convert.ToInt32(rdr["SD_CurrentSessionID"]),
                            SessionName = Convert.ToString(rdr["SM_SESSIONNAME"]),
                        });
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
                rdr.Close();
            }
            rdr.Dispose();
            return dbResults;
        }
        public long UpdateStudentPromotion(clsStudentList obj)
        {
            try
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();
                DataTable dt = new DataTable();
                arrParams.Add(new SqlParameter("@TransactionType", "UpdateStudents"));
                dt.Columns.Add("REGISTRATIONID", typeof(string));
                dt.Columns.Add("RollNo", typeof(Int32));
                dt.Columns.Add("ClassId", typeof(Int64));
                dt.Columns.Add("SectionId", typeof(Int64));
                dt.Columns.Add("StudentPromoteStatus", typeof(string)); // P / F
                foreach (var MD in obj.PromotedStudentList)
                {
                    DataRow dr = dt.NewRow();
                    dr["REGISTRATIONID"] = MD.SD_RegistrationNo;
                    dr["RollNo"] = MD.Roll;
                    dr["ClassId"] = MD.ClassId;
                    dr["SectionId"] = MD.SectionId;
                    dr["StudentPromoteStatus"] = MD.StudentPromoteStatus; // 'P' or 'F'
                    dt.Rows.Add(dr);
                }
                arrParams.Add(new SqlParameter("@PromotedStudentList", dt));
                arrParams.Add(new SqlParameter("@SessionId", obj.SessionId));
                arrParams.Add(new SqlParameter("@SchoolId", obj.SchoolId));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "StudentsPromotion_SP", arrParams.ToArray());
                return Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region Hostel Fees Collection
        public List<HostelMaster_HM> GetHotelFeesCollectionList(HostelMaster_HM obj)
        {
            // get fees list for insertion  
            List<HostelMaster_HM> ListObject = new List<HostelMaster_HM>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select"));
            arrParams.Add(new SqlParameter("@hostel_SchoolId", obj.hostel_SchoolId));
            arrParams.Add(new SqlParameter("@Hostel_StudentId", obj.Hostel_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_HosteFeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    HostelMaster_HM DataObject = new HostelMaster_HM();
                    DataObject.Hostel_Id = Convert.ToInt32(rdr["Hostel_Id"]);
                    DataObject.hostel_SchoolId = Convert.ToInt64(rdr["hostel_SchoolId"]);
                    DataObject.Hostel_Month = rdr["Hostel_Month"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Hostel_Month"]);
                    DataObject.Hostel_StudentId = rdr["Hostel_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["Hostel_StudentId"]);
                    DataObject.Hoset_Fare = rdr["Hoset_Fare"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["Hoset_Fare"]);
                    DataObject.Hostel_FeesName = rdr["Hostel_FeesName"] == DBNull.Value ? "" : Convert.ToString(rdr["Hostel_FeesName"]);
                    DataObject.Hostel_FeesId = rdr["Hostel_FeesId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["Hostel_FeesId"]);

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string InsertHostelFeesCollection(HostelTransactionMaster_HTM obj)
        {
            string Id = "";
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("HTM_FeesHeadId", typeof(Int64));
            dt1.Columns.Add("HTM_InstalmentNo", typeof(Int32));
            dt1.Columns.Add("HTM_StudentId", typeof(string));
            dt1.Columns.Add("HTM_MonthlyFare", typeof(decimal));
            foreach (var data in obj.FeesStructure)
            {
                DataRow dr = dt1.NewRow();
                dr["HTM_FeesHeadId"] = data.HTM_FeesHeadId;
                dr["HTM_InstalmentNo"] = data.HTM_InstalmentNo;
                dr["HTM_StudentId"] = data.HTM_StudentId;
                dr["HTM_MonthlyFare"] = data.HTM_MonthlyFare;
                Id = data.HTM_StudentId;
                dt1.Rows.Add(dr);
            }
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("HTM_FeesHeadId", typeof(Int64));
            dt2.Columns.Add("HTM_InstalmentNo", typeof(Int32));
            dt2.Columns.Add("HTM_StudentId", typeof(string));
            dt2.Columns.Add("HTM_MonthlyFare", typeof(decimal));
            foreach (var data in obj.PaidFeesStructure)
            {
                DataRow dr = dt2.NewRow();
                dr["HTM_FeesHeadId"] = data.HTM_FeesHeadId;
                dr["HTM_InstalmentNo"] = data.HTM_InstalmentNo;
                dr["HTM_StudentId"] = data.HTM_StudentId;
                dr["HTM_MonthlyFare"] = data.HTM_MonthlyFare;
                dt2.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.HTM_TransId == null)
            {
                arrParams.Add(new SqlParameter("@TransType", "Insert"));
            }
            if (obj.HTM_TransId != null)
            {
                arrParams.Add(new SqlParameter("@TransType", "Edit"));
                arrParams.Add(new SqlParameter("@HTM_TransId", obj.HTM_TransId));
            }
            arrParams.Add(new SqlParameter("@hostel_SchoolId", obj.HTM_SchoolId));
            arrParams.Add(new SqlParameter("@hostel_SessionId", obj.HTM_SessionId));
            arrParams.Add(new SqlParameter("@HFD_Paymentmode", obj.HFD_Paymentmode));
            arrParams.Add(new SqlParameter("@HFD_PaidAmount", obj.HFD_PaidAmount));
            arrParams.Add(new SqlParameter("@HFD_FeesCollectionDate", obj.HFD_FeesCollectionDate));
            arrParams.Add(new SqlParameter("@HFD_BankId", obj.HFD_BankId));
            arrParams.Add(new SqlParameter("@HFD_BranchName", obj.HFD_BranchName));
            arrParams.Add(new SqlParameter("@HFD_CheqDDNo", obj.HFD_CheqDDNo));
            arrParams.Add(new SqlParameter("@HFD_CheqDDDate", obj.HFD_CheqDDDate));
            arrParams.Add(new SqlParameter("@HFD_Card_TrnsRefNo", obj.HFD_Card_TrnsRefNo));
            arrParams.Add(new SqlParameter("@HostelFeesTransactionType", dt1));
            arrParams.Add(new SqlParameter("@HostelPaidFeesTransactionType", dt2));
            arrParams.Add(new SqlParameter("@Hostel_StudentId", Id));
            arrParams.Add(new SqlParameter("@CreatedBy", obj.HTM_CreatedBy));
            //SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.VarChar, 50);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HosteFeesCollection", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<HostelTransactionMaster_HTM> LoadHostelFeesCollectionList(HostelTransactionMaster_HTM obj)
        {
            // bindind list for list page
            List<HostelTransactionMaster_HTM> ListObject = new List<HostelTransactionMaster_HTM>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select_List"));
            arrParams.Add(new SqlParameter("@Hostel_StudentId", obj.HTM_StudentId));
            arrParams.Add(new SqlParameter("@ClassId", obj.ClassId.HasValue ? (object)obj.ClassId.Value : DBNull.Value));

            if (obj.fromDate.HasValue && obj.fromDate.Value > DateTime.MinValue)
                arrParams.Add(new SqlParameter("@FromDate", obj.fromDate));
            else
                arrParams.Add(new SqlParameter("@FromDate", DBNull.Value));

            if (obj.toDate.HasValue && obj.toDate.Value > DateTime.MinValue)
                arrParams.Add(new SqlParameter("@ToDate", obj.toDate));
            else
                arrParams.Add(new SqlParameter("@ToDate", DBNull.Value));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_HosteFeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    HostelTransactionMaster_HTM DataObject = new HostelTransactionMaster_HTM();
                    DataObject.HFD_PaidAmount = rdr["HFD_PaidAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["HFD_PaidAmount"]);
                    DataObject.HFD_Paymentmode = rdr["HFD_Paymentmode"] == DBNull.Value ? "" : Convert.ToString(rdr["HFD_Paymentmode"]);
                    DataObject.HTM_StudentName = rdr["HTM_StudentName"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_StudentName"]);
                    DataObject.HTM_ClassName = rdr["HTM_ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_ClassName"]);
                    DataObject.HTM_TransId = rdr["HTM_TransId"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_TransId"]);
                    DataObject.HTM_StudentId = rdr["HTM_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_StudentId"]);
                    DataObject.HFD_FeesCollectionDate = rdr["HFD_FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["HFD_FeesCollectionDate"]);





                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<HostelTransactionMaster_HTM> GetHostelFeesCollectionListForEdit(HostelTransactionMaster_HTM obj)
        {
            // GETTTING DATA FOR EDIT
            List<HostelTransactionMaster_HTM> ListObject = new List<HostelTransactionMaster_HTM>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select_For_Edit"));
            //arrParams.Add(new SqlParameter("@HTM_SchoolId", obj.HTM_SchoolId));
            arrParams.Add(new SqlParameter("@HTM_TransId", obj.HTM_TransId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_HosteFeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    HostelTransactionMaster_HTM DataObject = new HostelTransactionMaster_HTM();
                    DataObject.HTM_Id = Convert.ToInt32(rdr["HTM_Id"]);
                    DataObject.HTM_TransId = Convert.ToString(rdr["HTM_TransId"]);
                    DataObject.HTM_SchoolId = Convert.ToInt64(rdr["HTM_SchoolId"]);
                    DataObject.HTM_InstalmentNo = rdr["HTM_InstalmentNo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["HTM_InstalmentNo"]);
                    DataObject.HTM_StudentId = rdr["HTM_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_StudentId"]);
                    DataObject.HTM_MonthlyFare = rdr["HTM_MonthlyFare"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["HTM_MonthlyFare"]);
                    DataObject.HTM_FeesName = rdr["HTM_FeesName"] == DBNull.Value ? "" : Convert.ToString(rdr["HTM_FeesName"]);
                    DataObject.HTM_FeesHeadId = rdr["HTM_FeesHeadId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["HTM_FeesHeadId"]);
                    DataObject.HFD_BankId = rdr["HFD_BankId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["HFD_BankId"]);
                    DataObject.HFD_BranchName = rdr["HFD_BranchName"] == DBNull.Value ? "" : Convert.ToString(rdr["HFD_BranchName"]);
                    DataObject.HFD_CheqDDNo = rdr["HFD_CheqDDNo"] == DBNull.Value ? "" : Convert.ToString(rdr["HFD_CheqDDNo"]);
                    DataObject.HFD_Card_TrnsRefNo = rdr["HFD_Card_TrnsRefNo"] == DBNull.Value
                        ? ""
                        : Convert.ToString(rdr["HFD_Card_TrnsRefNo"]);
                    DataObject.HFD_Paymentmode = rdr["HFD_Paymentmode"] == DBNull.Value ? "" : Convert.ToString(rdr["HFD_Paymentmode"]);
                    DataObject.HFD_CheqDDDate = rdr["HFD_CheqDDDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["HFD_CheqDDDate"]);
                    DataObject.HFD_FeesCollectionDate = Convert.ToDateTime(rdr["HFD_FeesCollectionDate"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }




        #endregion
        #region Transport Fees Collection
        public List<TransportFeesTransaction_TR> GetTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        { // GETTING DATA FOR INSERTION 
            List<TransportFeesTransaction_TR> ListObject = new List<TransportFeesTransaction_TR>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select"));
            arrParams.Add(new SqlParameter("@TR_SchoolId", obj.TR_SchoolId));
            arrParams.Add(new SqlParameter("@TR_StudentId", obj.TR_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportFeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    TransportFeesTransaction_TR DataObject = new TransportFeesTransaction_TR();
                    DataObject.TR_Id = Convert.ToInt32(rdr["TR_Id"]);
                    DataObject.TR_SchoolId = Convert.ToInt64(rdr["TR_SchoolId"]);
                    DataObject.TR_InstallmentMonth = rdr["TR_InstallmentMonth"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TR_InstallmentMonth"]);
                    DataObject.TR_StudentId = rdr["TR_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_StudentId"]);
                    DataObject.TR_MonthlyFare = rdr["TR_MonthlyFare"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TR_MonthlyFare"]);
                    //DataObject.TR_FeesName = rdr["TR_FeesName"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_FeesName"]);
                    DataObject.TR_FeesName = rdr["TR_FeesName"] == DBNull.Value ? "Transport Fees" : Convert.ToString(rdr["TR_FeesName"]);
                    DataObject.TR_FeesHeadId = rdr["TR_FeesHeadId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TR_FeesHeadId"]);

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<TransportFeesTransaction_TR> GetTransportFeesCollectionListForEdit(TransportFeesTransaction_TR obj)
        {
            // GETTTING DATA FOR EDIT
            List<TransportFeesTransaction_TR> ListObject = new List<TransportFeesTransaction_TR>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select_For_Edit"));
            arrParams.Add(new SqlParameter("@TR_SchoolId", obj.TR_SchoolId));
            arrParams.Add(new SqlParameter("@TR_TransId", obj.TR_TransId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportFeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    TransportFeesTransaction_TR DataObject = new TransportFeesTransaction_TR();
                    DataObject.TR_Id = Convert.ToInt32(rdr["TR_Id"]);
                    DataObject.TR_TransId = Convert.ToString(rdr["TR_TransId"]);
                    DataObject.TR_SchoolId = Convert.ToInt64(rdr["TR_SchoolId"]);
                    DataObject.TR_InstallmentMonth = rdr["TR_InstallmentMonth"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TR_InstallmentMonth"]);
                    DataObject.TR_StudentId = rdr["TR_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_StudentId"]);
                    DataObject.TR_MonthlyFare = rdr["TR_MonthlyFare"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TR_MonthlyFare"]);
                    DataObject.TR_FeesName = rdr["TR_FeesName"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_FeesName"]);
                    DataObject.TR_FeesHeadId = rdr["TR_FeesHeadId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TR_FeesHeadId"]);
                    DataObject.TD_BankId = rdr["TD_BankId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TD_BankId"]);
                    DataObject.TD_BranchName = rdr["TD_BranchName"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_BranchName"]);
                    DataObject.TD_CheqDDNo = rdr["TD_CheqDDNo"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_CheqDDNo"]);
                    DataObject.TD_Card_TrnsRefNo = rdr["TD_Card_TrnsRefNo"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_Card_TrnsRefNo"]);
                    DataObject.TD_Paymentmode = rdr["TD_Paymentmode"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_Paymentmode"]);
                    //if (DataObject.TD_Paymentmode != "Cash")
                    //{
                    //    DataObject.TD_CheqDDDate = Convert.ToDateTime(rdr["TD_CheqDDDate"]);
                    //}
                    DataObject.TD_CheqDDDate = rdr["TD_CheqDDDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["TD_CheqDDDate"]);
                    DataObject.TD_FeesCollectionDate = rdr["TD_FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["TD_FeesCollectionDate"]);


                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public string InsertTransportFeesCollection(TransportFeesTransaction_TR obj)
        {
            string Id = "";
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("TR_FeesHeadId", typeof(Int64));
            dt1.Columns.Add("TR_InstallmentMonth", typeof(Int32));
            dt1.Columns.Add("TR_StudentId", typeof(string));
            dt1.Columns.Add("TR_MonthlyFare", typeof(decimal));
            foreach (var data in obj.FeesStructure)
            {
                DataRow dr = dt1.NewRow();
                dr["TR_FeesHeadId"] = data.TR_FeesHeadId;
                dr["TR_InstallmentMonth"] = data.TR_InstallmentMonth;
                dr["TR_StudentId"] = data.TR_StudentId;
                dr["TR_MonthlyFare"] = data.TR_MonthlyFare;
                Id = data.TR_StudentId;
                dt1.Rows.Add(dr);
            }
            DataTable dt2 = new DataTable();
            dt2.Columns.Add("TR_FeesHeadId", typeof(Int64));
            dt2.Columns.Add("TR_InstallmentMonth", typeof(Int32));
            dt2.Columns.Add("TR_StudentId", typeof(string));
            dt2.Columns.Add("TR_MonthlyFare", typeof(decimal));
            foreach (var data in obj.PaidFeesStructure)
            {
                DataRow dr = dt2.NewRow();
                dr["TR_FeesHeadId"] = data.TR_FeesHeadId;
                dr["TR_InstallmentMonth"] = data.TR_InstallmentMonth;
                dr["TR_StudentId"] = data.TR_StudentId;
                dr["TR_MonthlyFare"] = data.TR_MonthlyFare;
                dt2.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            if (obj.TR_TransId == null)
            {
                arrParams.Add(new SqlParameter("@TransType", "Insert"));
            }
            if (obj.TR_TransId != null)
            {
                arrParams.Add(new SqlParameter("@TransType", "Edit"));
                arrParams.Add(new SqlParameter("@TR_TransId", obj.TR_TransId));
            }
            arrParams.Add(new SqlParameter("@TR_SchoolId", obj.TR_SchoolId));
            arrParams.Add(new SqlParameter("@TR_SessionId", obj.TR_SessionId));
            arrParams.Add(new SqlParameter("@TD_Paymentmode", obj.TD_Paymentmode));
            arrParams.Add(new SqlParameter("@TD_PaidAmount", obj.TD_PaidAmount));
            arrParams.Add(new SqlParameter("@TD_FeesCollectionDate", obj.TD_FeesCollectionDate));
            arrParams.Add(new SqlParameter("@TD_BankId", obj.TD_BankId));
            arrParams.Add(new SqlParameter("@TD_BranchName", obj.TD_BranchName));
            arrParams.Add(new SqlParameter("@TD_CheqDDNo", obj.TD_CheqDDNo));
            arrParams.Add(new SqlParameter("@TD_CheqDDDate", obj.TD_CheqDDDate));
            arrParams.Add(new SqlParameter("@TD_Card_TrnsRefNo", obj.TD_Card_TrnsRefNo));
            arrParams.Add(new SqlParameter("@TransportFeesTransactionType", dt1));
            arrParams.Add(new SqlParameter("@TransportPaidFeesTransactionType", dt2));
            arrParams.Add(new SqlParameter("@TR_StudentId", Id));
            arrParams.Add(new SqlParameter("@CreatedBy", obj.TR_CreatedBy));
            //SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.VarChar, 50);


            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportFeesCollection", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }

        public List<TransportFeesTransaction_TR> LoadTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        {
            List<TransportFeesTransaction_TR> ListObject = new List<TransportFeesTransaction_TR>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select_List"));
            arrParams.Add(new SqlParameter("@TR_SchoolId", obj.TR_SchoolId));
            arrParams.Add(new SqlParameter("@TR_StudentId", string.IsNullOrWhiteSpace(obj.TR_StudentId) ? (object)DBNull.Value : obj.TR_StudentId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.TR_ClassId > 0 ? (object)obj.TR_ClassId : DBNull.Value));
            arrParams.Add(new SqlParameter("@FromDate", obj.fromDate.HasValue ? (object)obj.fromDate.Value : DBNull.Value));
            arrParams.Add(new SqlParameter("@ToDate", obj.toDate.HasValue ? (object)obj.toDate.Value : DBNull.Value));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportFeesCollection", arrParams.ToArray());

            if (rdr != null)
            {
                while (rdr.Read())
                {
                    TransportFeesTransaction_TR DataObject = new TransportFeesTransaction_TR();
                    DataObject.TR_PaidAmount = rdr["TR_PaidAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TR_PaidAmount"]);
                    DataObject.TR_StudentId = rdr["TR_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_StudentId"]);
                    DataObject.TR_StudentName = rdr["TR_StudentName"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_StudentName"]);
                    DataObject.TR_ClassName = rdr["TR_ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["TR_ClassName"]);
                    DataObject.TD_TransId = rdr["TD_TransId"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_TransId"]);
                    DataObject.TD_Paymentmode = rdr["TD_Paymentmode"] == DBNull.Value ? "" : Convert.ToString(rdr["TD_Paymentmode"]);
                    DataObject.TD_FeesCollectionDate = rdr["TD_FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["TD_FeesCollectionDate"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion

        #region TC Master
        public List<TCMaster> GetAllStudent(TCMaster obj)
        {
            List<TCMaster> ListObject = new List<TCMaster>();
            TCMaster DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SM_SESSIONID));
            if (obj.CM_CLASSID > 0)
                arrParams.Add(new SqlParameter("@SD_ClassId", obj.CM_CLASSID));

            if (!string.IsNullOrWhiteSpace(obj.SD_StudentId))
                arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));

            if (!string.IsNullOrWhiteSpace(obj.SD_StudentName))
                arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TCStudent", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new TCMaster();
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);

                    DataObject.SD_FatherName = Convert.ToString(rdr["SD_FatherName"]);
                    DataObject.SD_MotherName = Convert.ToString(rdr["SD_MotherName"]);
                    DataObject.SD_AppliactionNo = Convert.ToString(rdr["SD_AppliactionNo"]);
                    DataObject.SD_AppliactionDate = rdr["SD_AppliactionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SD_AppliactionDate"]);
                    DataObject.SD_DOB = rdr["SD_DOB"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["SD_DOB"]);
                    DataObject.CSM_CASTENAME = Convert.ToString(rdr["CSM_CASTENAME"]);
                    DataObject.NM_NATIONNAME = Convert.ToString(rdr["NM_NATIONNAME"]);

                    DataObject.AdmittedClassName = Convert.ToString(rdr["AdmittedClassName"]);

                    DataObject.SGM_SubjectGroupName = Convert.ToString(rdr["SubjectGroupName"]);

                    DataObject.SubjectNames = Convert.ToString(rdr["SubjectNames"]);









                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public long CancelStudents(TCMaster TCMaster)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Insert"));
            arrParams.Add(new SqlParameter("@UM_USERID", TCMaster.UM_USERID));
            arrParams.Add(new SqlParameter("@TCTypeId", TCMaster.TCTypeId));
            arrParams.Add(new SqlParameter("@SM_SESSIONID", TCMaster.SM_SESSIONID));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLID", TCMaster.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_ClassId", TCMaster.CM_CLASSID));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", TCMaster.SECM_SECTIONID));
            arrParams.Add(new SqlParameter("@SD_StudentId", TCMaster.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_TCNo", TCMaster.SD_TCNo));
            arrParams.Add(new SqlParameter("@TC_Fees", TCMaster.TC_Fees));
            // Newly added TC fields
            arrParams.Add(new SqlParameter("@DOB_Words", TCMaster.DOB_Words ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@DOB_Proof", TCMaster.DOB_Proof ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Last_Exam", TCMaster.Last_Exam ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@FailedStatus", TCMaster.FailedStatus ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Q_Promotion", TCMaster.Q_Promotion ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Last_DuePaid", TCMaster.Last_DuePaid ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Fee_Consession", TCMaster.Fee_Consession ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@No_Wdays", TCMaster.No_Wdays ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@No_WPdays", TCMaster.No_WPdays ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@NCC_Cadet_Scout_Guide", TCMaster.NCC_Cadet_Scout_Guide ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Games_Activities", TCMaster.Games_Activities ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@School_Category", TCMaster.School_Category ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Genral_Conduct", TCMaster.Genral_Conduct ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@AP_Date", TCMaster.AP_Date.HasValue ? (object)TCMaster.AP_Date.Value : DBNull.Value));
            arrParams.Add(new SqlParameter("@Issue_Date", TCMaster.Issue_Date.HasValue ? (object)TCMaster.Issue_Date.Value : DBNull.Value));
            arrParams.Add(new SqlParameter("@Reason_Leave", TCMaster.Reason_Leave ?? (object)DBNull.Value));
            arrParams.Add(new SqlParameter("@Remarks", TCMaster.Remarks ?? (object)DBNull.Value));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TCStudent", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<TCMaster> GetAllTCStudent(TCMaster obj)
        {
            List<TCMaster> ListObject = new List<TCMaster>();
            TCMaster DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECTTC"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SM_SESSIONID));
            if (obj.CM_CLASSID != null) arrParams.Add(new SqlParameter("@SD_ClassId", obj.CM_CLASSID));
            if (obj.SD_StudentId != null) arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            if (obj.SD_StudentName != null) arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TCStudent", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new TCMaster();
                    DataObject.TC_ANumber = Convert.ToString(rdr["TC_ANumber"]);
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);
                    DataObject.SD_TCDate = Convert.ToString(rdr["SD_TCDate"]);
                    DataObject.TC_Fees = Convert.ToString(rdr["TC_Fees"]);

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        /// print tc certificate
        public TCCertificate GetTCCertificate(string studentId)
        {
            TCCertificate tc = null;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_TCStudent", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransType", "PRINTRECEIPT");
                    cmd.Parameters.AddWithValue("@SD_StudentId", studentId);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            tc = new TCCertificate
                            {
                                SD_AppliactionNo = dr["SD_AppliactionNo"].ToString(),
                                SD_StudentId = dr["SD_StudentId"].ToString(),
                                SCM_AFFNO = dr["SCM_AFFNO"].ToString(),
                                SCM_SCHOOLCODE = dr["SCM_SCHOOLCODE"].ToString(),
                                SCM_SCHOOLNAME = dr["SCM_SCHOOLNAME"].ToString(),
                                SCM_FULLNAME = dr["SCM_FULLNAME"].ToString(),
                                SCM_SCHOOLADDRESS1 = dr["SCM_SCHOOLADDRESS1"].ToString(),
                                SCM_SCHOOLADDRESS2 = dr["SCM_SCHOOLADDRESS2"].ToString(),
                                SCM_OFFICEADDRESS = dr["SCM_OFFICEADDRESS"].ToString(),
                                SCM_CONTACTPERSON = dr["SCM_CONTACTPERSON"].ToString(),
                                SCM_PHONENO1 = dr["SCM_PHONENO1"].ToString(),
                                SCM_PHONENO2 = dr["SCM_PHONENO2"].ToString(),
                                SCM_SECRETARYNAME = dr["SCM_SECRETARYNAME"].ToString(),
                                SCM_EMAILID = dr["SCM_EMAILID"].ToString(),
                                SCM_WEBSITE = dr["SCM_WEBSITE"].ToString(),
                                SCM_SCHOOLLOGO = dr["SCM_SCHOOLLOGO"].ToString(),
                                SCM_IMAGENAME = dr["SCM_IMAGENAME"].ToString(),
                                TC_ANumber = dr["TC_ANumber"].ToString(),

                                SD_StudentName = dr["SD_StudentName"].ToString(),
                                SD_FatherName = dr["SD_FatherName"].ToString(),
                                SD_MotherName = dr["SD_MotherName"].ToString(),
                                NM_NATIONNAME = dr["NM_NATIONNAME"].ToString(),
                                CSM_CASTENAME = dr["CSM_CASTENAME"].ToString(),

                                SD_AppliactionDate = dr["SD_AppliactionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SD_AppliactionDate"]),
                                SD_DOB = dr["SD_DOB"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["SD_DOB"]),

                                JoinedClass = dr["JoinedClass"].ToString(),
                                CurrentClass = dr["CurrentClass"].ToString(),
                                SD_CurrentRoll = dr["SD_CurrentRoll"].ToString(),
                                SECM_SECTIONNAME = dr["SECM_SECTIONNAME"].ToString(),
                                SubjectGroupName = dr["SubjectGroupName"].ToString(),
                                SubjectNames = dr["SubjectNames"].ToString(),

                                DOB_Words = dr["DOB_Words"].ToString(),
                                DOB_Proof = dr["DOB_Proof"].ToString(),
                                Last_Exam = dr["Last_Exam"].ToString(),
                                FailedStatus = dr["FailedStatus"].ToString(),
                                Q_Promotion = dr["Q_Promotion"].ToString(),
                                Last_DuePaid = dr["Last_DuePaid"].ToString(),
                                Fee_Consession = dr["Fee_Consession"].ToString(),
                                No_Wdays = dr["No_Wdays"].ToString(),
                                No_WPdays = dr["No_WPdays"].ToString(),
                                NCC_Cadet_Scout_Guide = dr["NCC_Cadet_Scout_Guide"].ToString(),
                                Games_Activities = dr["Games_Activities"].ToString(),
                                School_Category = dr["School_Category"].ToString(),
                                Genral_Conduct = dr["Genral_Conduct"].ToString(),

                                AP_Date = dr["AP_Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["AP_Date"]),
                                Issue_Date = dr["Issue_Date"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr["Issue_Date"]),
                                Reason_Leave = dr["Reason_Leave"].ToString(),
                                Remarks = dr["Remarks"].ToString()
                            };
                        }
                    }
                }
            }

            return tc;
        }
        public string DeleteTC(string SD_StudentId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETETC"));
            arrParams.Add(new SqlParameter("@SD_StudentId", SD_StudentId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                      CommandType.StoredProcedure,
                                      "SP_TCStudent",
                                      arrParams.ToArray());

            return Convert.ToString(arrParams[arrParams.Count - 1].Value);
        }



        #endregion
        #region Discontinue Student
        public List<StudetDetails_SD> GetDisStudentList(StudetDetails_SD obj)
        {
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            StudetDetails_SD DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", obj.TransType));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SD_SessionId));
            if (obj.SD_ClassId != null) arrParams.Add(new SqlParameter("@SD_ClassId", obj.SD_ClassId));
            if (obj.SD_StudentId != null) arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            if (obj.SD_StudentName != null) arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_DiscontinueStudent", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);
                    DataObject.SD_ContactNo2 = Convert.ToString(rdr["SD_ContactNo2"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region DeleteStudentWise Subjest Setting
        public string DeleteStudentWiseSubject(string id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
        new SqlParameter("@TransactionType", "DELETE"),
        new SqlParameter("@SWS_StudentSID", id)
    };

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "InserUpdate_StudentwiseSubject_Sp", arrParams.ToArray());

            return Convert.ToString(OutPutId.Value);
        }

        #endregion

        #region Miscellaneous Collection

        public DbResult InsertUpdateMiscCollection(MiscellaneousTransactionMaster obj)
        {
            DbResult dbResult = new DbResult();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_InsertUpdate_MiscCollection", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        string transType = (obj.MTM_Id == 0 || obj.MTM_Id == null) ? "INSERT" : "UPDATE";
                        cmd.Parameters.AddWithValue("@TransType", transType);

                        cmd.Parameters.AddWithValue("@MTM__TransId", (object)obj.MTM__TransId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MTM_Id", obj.MTM_Id);
                        cmd.Parameters.AddWithValue("@MTM_FeesHeadId", obj.MTM_FeesHeadId);
                        cmd.Parameters.AddWithValue("@MTM_StudentId", obj.MTM_StudentId);
                        cmd.Parameters.AddWithValue("@MTM_Amount", obj.MTM_Amount);
                        cmd.Parameters.AddWithValue("@MTM_Narration", obj.MTM_Narration);
                        cmd.Parameters.AddWithValue("@MTM_SchoolId", obj.MTM_SchoolId);
                        cmd.Parameters.AddWithValue("@MTM_SessionId", obj.MTM_SessionId);
                        cmd.Parameters.AddWithValue("@MTM_CreatedBy", obj.MTM_CreatedBy);
                        cmd.Parameters.AddWithValue("@MFD_Paymentmode", (object)obj.MFD_Paymentmode ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MFD_BankId", (object)obj.MFD_BankId ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MFD_BranchName", (object)obj.MFD_BranchName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MFD_CheqDDNo", (object)obj.MFD_CheqDDNo ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MFD_CheqDDDate", (object)obj.MFD_CheqDDDate ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@MFD_Card_TrnsRefNo", (object)obj.MFD_Card_TrnsRefNo ?? DBNull.Value);

                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                dbResult.Result = rdr["Result"].ToString();
                                dbResult.TransactionId = rdr["TransactionId"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dbResult.Result = "ERROR: " + ex.Message;
                dbResult.TransactionId = null;
            }

            return dbResult;
        }
        public List<MiscellaneousTransactionMaster> MiscFeesCollectionList(MiscellaneousTransactionMaster obj)
        {
            // bindind list for list page
            List<MiscellaneousTransactionMaster> ListObject = new List<MiscellaneousTransactionMaster>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select_List"));
            arrParams.Add(new SqlParameter("@MTM_SchoolId", obj.MTM_SchoolId));
            arrParams.Add(new SqlParameter("@MTM_SessionId", obj.MTM_SessionId));
            arrParams.Add(new SqlParameter("@MTM_StudentId", obj.MTM_StudentId));
            arrParams.Add(new SqlParameter("@FromDate", obj.FromDate));
            arrParams.Add(new SqlParameter("@ToDate", obj.ToDate));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_CurrentClassId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_InsertUpdate_MiscCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    MiscellaneousTransactionMaster DataObject = new MiscellaneousTransactionMaster();
                    DataObject.MTM_Id = rdr["MTM_Id"] != DBNull.Value ? Convert.ToInt64(rdr["MTM_Id"]) : 0;
                    DataObject.MTM__TransId = rdr["MTM__TransId"] == DBNull.Value ? "" : Convert.ToString(rdr["MTM__TransId"]);
                    DataObject.MTM_StudentId = rdr["MTM_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["MTM_StudentId"]);
                    DataObject.SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.MTM_ClassName = rdr["MTM_ClassName"] == DBNull.Value ? "" : Convert.ToString(rdr["MTM_ClassName"]);
                    DataObject.MFD_PaidAmount = rdr["MFD_PaidAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["MFD_PaidAmount"]);
                    DataObject.MFD_Paymentmode = rdr["MFD_Paymentmode"] == DBNull.Value ? "" : Convert.ToString(rdr["MFD_Paymentmode"]);
                    DataObject.MTM_Narration = rdr["MTM_Narration"] == DBNull.Value ? "" : Convert.ToString(rdr["MTM_Narration"]);
                    DataObject.MTM_FeesName = rdr["MTM_FeesName"] == DBNull.Value ? "" : Convert.ToString(rdr["MTM_FeesName"]);
                    DataObject.MFD__FeesCollectionDate = rdr["MFD__FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["MFD__FeesCollectionDate"]);





                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public void DeleteMiscCollection(string id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
        new SqlParameter("@TransType", "DELETE"),
        new SqlParameter("@MTM__TransId", id)
    };

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_InsertUpdate_MiscCollection",
                arrParams.ToArray()
            );
        }
      

        #endregion

        #region Dropout
        public List<DropOut_DOP> GetAllDropOutStudent(DropOut_DOP obj)
        {
            List<DropOut_DOP> ListObject = new List<DropOut_DOP>();
            DropOut_DOP DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECTDropOut"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SM_SESSIONID));
            if (obj.CM_CLASSID != null) arrParams.Add(new SqlParameter("@SD_ClassId", obj.CM_CLASSID));
            if (obj.SD_StudentId != null) arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            if (obj.SD_StudentName != null) arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_DropOut", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new DropOut_DOP();
                    DataObject.DOP_ANumber = Convert.ToString(rdr["DOP_ANumber"]);
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);
                    DataObject.DOP_Date = Convert.ToString(rdr["DOP_Date"]);
                    DataObject.DOP_Reason = Convert.ToString(rdr["DOP_Reason"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<DropOut_DOP> GetAllDStudent(DropOut_DOP obj)
        {
            List<DropOut_DOP> ListObject = new List<DropOut_DOP>();
            DropOut_DOP DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_SessionId", obj.SM_SESSIONID));
            if (obj.CM_CLASSID != null) arrParams.Add(new SqlParameter("@SD_ClassId", obj.CM_CLASSID));
            if (obj.SD_StudentId != null) arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            if (obj.SD_StudentName != null) arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_DropOut", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new DropOut_DOP();
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SD_ContactNo1 = Convert.ToString(rdr["SD_ContactNo1"]);
                    //DataObject.DOP_Reason = Convert.ToString(rdr["DOP_Reason"]);









                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long CancelDStudents(DropOut_DOP DropOut_DOP)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Insert"));
            arrParams.Add(new SqlParameter("@UM_USERID", DropOut_DOP.UM_USERID));
            arrParams.Add(new SqlParameter("@SM_SESSIONID", DropOut_DOP.SM_SESSIONID));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLID", DropOut_DOP.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SD_ClassId", DropOut_DOP.CM_CLASSID));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", DropOut_DOP.SECM_SECTIONID));
            arrParams.Add(new SqlParameter("@SD_StudentId", DropOut_DOP.SD_StudentId));
            arrParams.Add(new SqlParameter("@DOP_Reason", DropOut_DOP.DOP_Reason));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_DropOut", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public string DeleteDropOut(string SD_StudentId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETEDROPOUT"));
            arrParams.Add(new SqlParameter("@SD_StudentId", SD_StudentId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(),
                                      CommandType.StoredProcedure,
                                      "SP_DropOut",
                                      arrParams.ToArray());

            return Convert.ToString(arrParams[arrParams.Count - 1].Value);
        }


        #endregion

        #region StudentAssignmentList

        public long InsertUpdateStudentAssignmentMaster(StudentAssignmentMaster ASM)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = "STUDENT-UPDATE";

            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@ASM_ID", ASM.ASM_ID));
            arrParams.Add(new SqlParameter("@AST_StudentId", ASM.AST_StudentId));
            //arrParams.Add(new SqlParameter("@ASM_SubGr_ID", ASM.ASM_SubGr_ID));
            //arrParams.Add(new SqlParameter("@ASM_Sub_ID", ASM.ASM_Sub_ID));
            //arrParams.Add(new SqlParameter("@ASM_School_ID", ASM.ASM_School_ID));
            //arrParams.Add(new SqlParameter("@ASM_Class_ID", ASM.ASM_Class_ID));
            //arrParams.Add(new SqlParameter("@ddlASM_Section_ID", ASM.ASM_Section_ID));
            //arrParams.Add(new SqlParameter("@ASM_Session_ID", ASM.ASM_Session_ID));
            //arrParams.Add(new SqlParameter("@ASM_Title", ASM.ASM_Title));
            //arrParams.Add(new SqlParameter("@ASM_Desc", ASM.ASM_Desc));
            //arrParams.Add(new SqlParameter("@ASM_StartDate", ASM.ASM_StartDate == null ? (object)DBNull.Value : ASM.ASM_StartDate));
            //arrParams.Add(new SqlParameter("@ASM_ExpDate", ASM.ASM_ExpDate == null ? (object)DBNull.Value : ASM.ASM_ExpDate));
            arrParams.Add(new SqlParameter("@ASM_UploadDoc", ASM.ASM_UploadDoc));
            arrParams.Add(new SqlParameter("@Userid", ASM.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentAssignmenListMaster", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }
        public List<StudentAssignmentsListResponse> GetStudentAssignmentList(StudentAssignmentsListRequest obj)
        {
            List<StudentAssignmentsListResponse> list = new List<StudentAssignmentsListResponse>();
            Dictionary<int, StudentAssignmentsListResponse> map = new Dictionary<int, StudentAssignmentsListResponse>();
            List<SqlParameter> arrParams = new List<SqlParameter>
        {
            new SqlParameter("@TransType", "SELECT_LIST"),
            new SqlParameter("@ASM_School_ID", obj.ASM_School_ID),
            new SqlParameter("@ASM_Session_ID", obj.ASM_Session_ID),
            //new SqlParameter("@ASM_FP_Id", (object)obj.ASM_FP_Id ?? DBNull.Value),
            new SqlParameter("@ASM_Class_ID", (object)obj.ASM_Class_ID ?? DBNull.Value),
            new SqlParameter("@ASM_Section_ID", (object)obj.ASM_Section_ID ?? DBNull.Value),
            new SqlParameter("@ASM_SubGr_ID", (object)obj.ASM_SubGr_ID ?? DBNull.Value),
            new SqlParameter("@ASM_Sub_ID", (object)obj.ASM_Sub_ID ?? DBNull.Value),
            new SqlParameter("@ASM_StartDateS", (object)obj.ASM_StartDateS ?? DBNull.Value),
            new SqlParameter("@ASM_ExpDateS", (object)obj.ASM_ExpDateS ?? DBNull.Value),
            new SqlParameter("@ASM_Title", (object)obj.ASM_Title ?? DBNull.Value),
            new SqlParameter("@ASM_Desc", (object)obj.ASM_Desc ?? DBNull.Value),
            new SqlParameter("@ASM_UploadDoc", (object)obj.ASM_UploadDoc ?? DBNull.Value),
            new SqlParameter("@AST_StudentId", (object)obj.AST_StudentId ?? DBNull.Value),
            // 
        };
            SqlParameter outPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(outPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_StudentAssignmenListMaster",
                arrParams.ToArray()))
            {
                while (rdr.Read())
                {
                    var asm = new StudentAssignmentsListResponse
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
                        ExpiredYN = Convert.ToString(rdr["ExpiredYN"]),

                        Students = new List<UploadAssignmentStudentResponse>()
                    };

                    list.Add(asm);
                    map.Add((int)asm.ASM_ID, asm);
                }
                if (rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        long? asmId = Convert.ToInt32(rdr["AST_ASM_ID"]);

                        if (map.ContainsKey((int)asmId))
                        {
                            map[(int)asmId].Students.Add(new UploadAssignmentStudentResponse
                            {
                                AST_ID = Convert.ToInt64(rdr["AST_ID"]),
                                AST_ASM_ID = asmId,
                                AST_StudentId = Convert.ToInt64(rdr["AST_StudentId"]),
                                SD_StudentSId = Convert.ToString(rdr["SD_StudentSId"]),
                                SD_StudentName = Convert.ToString(rdr["SD_StudentName"]),
                                AST_UploadDoc = Convert.ToString(rdr["AST_UploadDoc"]),
                                IsAbsent = Convert.ToBoolean(rdr["IsAbsent"]),
                                Obtainedmarks = Convert.ToString(rdr["AST_Marks"]),
                                TotalMarks = Convert.ToString(rdr["AST_TotalMarks"])
                            });
                        }
                    }
                }
            }

            return list;
        }
        public List<StudentAttendanceDetailsResponse> GetStudentAttendanceDetails(StudentAttendanceDetailsRequest request)
        {
            List<StudentAttendanceDetailsResponse> list = new List<StudentAttendanceDetailsResponse>();
            Dictionary<int, StudentAssignmentsListResponse> map = new Dictionary<int, StudentAssignmentsListResponse>();
            List<SqlParameter> arrParams = new List<SqlParameter>
            {
            new SqlParameter("@SchoolId", request.SchoolId),
            new SqlParameter("@ClassId", request.ClassId),
            new SqlParameter("@SectionId", request.SectionId),
            new SqlParameter("@SessionId", request.SessionId),
            new SqlParameter("@Date", request.Date)
        };
            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_Get_Sec_Wise_Attendance_Details",
                arrParams.ToArray()))
            {
                while (rdr.Read())
                {
                    var asm = new StudentAttendanceDetailsResponse
                    {
                        StudentId = Convert.ToString(rdr["StudentId"]),
                        StudentName = Convert.ToString(rdr["StudentName"]),
                        ClassId = Convert.ToInt64(rdr["ClassId"]),
                        ClassName = Convert.ToString(rdr["ClassName"]),
                        SectionId = Convert.ToInt64(rdr["SectionId"]),
                        SectionName = Convert.ToString(rdr["SectionName"]),
                        Roll = Convert.ToInt32(rdr["Roll"]),
                        IsAbsent = Convert.ToBoolean(rdr["IsAbsent"]),
                        IsHalfDay = Convert.ToBoolean(rdr["IsHalfDay"]),
                        IsLateComing = Convert.ToBoolean(rdr["IsLateComing"]),
                        IsAttendanceTaken = Convert.ToString(rdr["IsAttendanceTaken"]),
                    };

                    list.Add(asm);
                }
                return list;
            }
        }
        public StudentAssignmentsListResponse GetStudentAssignmentById(StudentAssignmentDtlsRequest studentAssignmentDtlsRequest)
        {
            StudentAssignmentsListResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>
            {
                new SqlParameter("@TransType", "SELECT_ONE"),
                new SqlParameter("@ASM_ID", studentAssignmentDtlsRequest.asmId)
            };

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_StudentAssignmenListMaster",
                arrParams.ToArray()))
            {
                if (rdr.Read())
                {
                    DataObject = new StudentAssignmentsListResponse
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
                        Students = new List<UploadAssignmentStudentResponse>()
                    };
                }

                if (DataObject != null && rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        DataObject.Students.Add(new UploadAssignmentStudentResponse
                        {
                            AST_ID = Convert.ToInt32(rdr["AST_ID"]),
                            AST_ASM_ID = Convert.ToInt32(rdr["AST_ASM_ID"]),
                            AST_StudentId = Convert.ToInt32(rdr["AST_StudentId"]),
                            SD_StudentSId = Convert.ToString(rdr["SD_StudentSId"]),
                            SD_StudentName = Convert.ToString(rdr["SD_StudentName"]),
                            AST_UploadDoc = Convert.ToString(rdr["AST_UploadDoc"]),
                            IsAbsent = Convert.ToBoolean(rdr["IsAbsent"]),
                            Obtainedmarks = Convert.ToString(rdr["AST_Marks"]),
                            TotalMarks = Convert.ToString(rdr["AST_TotalMarks"])
                        });
                    }
                }
            }

            return DataObject;
        }
        public string DeleteStudentAssignment(int? id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));
            arrParams.Add(new SqlParameter("@Att_Id", id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentAssignmenListMaster", arrParams.ToArray());
            var val = Convert.ToString(arrParams[arrParams.Count - 1].Value);
            return val;
        }

        public List<StudentWiseFacultyResponse> GetStudentWiseFacultyList(StudentWiseFacultyRequest obj)
        {
            List<StudentWiseFacultyResponse> list = new List<StudentWiseFacultyResponse>();
            Dictionary<int, StudentWiseFacultyResponse> map = new Dictionary<int, StudentWiseFacultyResponse>();
            List<SqlParameter> arrParams = new List<SqlParameter>
        {
            new SqlParameter("@School_ID",  (object)obj.SchoolId),
            new SqlParameter("@Class_ID", obj.ClassId),
            new SqlParameter("@Section_ID", obj.SectionId),
            new SqlParameter("@Session_ID", obj.SessionId),
        };
            SqlParameter outPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(outPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentWiseFacultyList", arrParams.ToArray()))
            {
                while (rdr.Read())
                {
                    var asm = new StudentWiseFacultyResponse
                    {
                        FacultyId = Convert.ToInt64(rdr["FP_Id"]),
                        FacultyName = Convert.ToString(rdr["FP_Name"]),
                        SubjectId = Convert.ToInt64(rdr["CWF_SubjectId"]),
                        SubjectCode = Convert.ToString(rdr["SBM_SubjectCode"]),
                        SubjectName = Convert.ToString(rdr["SBM_SubjectName"]),
                        IsClassTeacherYN = Convert.ToString(rdr["IsClassTeacherYN"]),
                    };
                    list.Add(asm);
                }
            }
            return list;
        }
        #endregion

        #region StudentPortal API Integration Add By Uttaran
        #region GetStudentLogin
        public StudetDetails_SD GetStudentLogin(StudetDetails_SD obj)
        {
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            StudetDetails_SD DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentLogin", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudetDetails_SD();
                    DataObject.SD_Id = Convert.ToInt64(rdr["SD_Id"]);
                    DataObject.SD_StudentId = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_Password = Convert.ToString(rdr["SD_Password"]);
                    DataObject.SD_SchoolId = Convert.ToInt64(rdr["SD_SchoolId"]);
                    DataObject.SD_CurrentClassId = Convert.ToInt64(rdr["SD_CurrentClassId"]);
                    DataObject.SD_CurrentSectionId = Convert.ToInt64(rdr["SD_CurrentSectionId"]);
                    DataObject.SD_CurrentSessionId = Convert.ToInt64(rdr["SD_CurrentSessionId"]);
                    DataObject.SD_CurrentRoll = Convert.ToInt32(rdr["SD_CurrentRoll"]);
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SCM_SCHOOLNAME = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SECM_SECTIONNAME = Convert.ToString(rdr["SECM_SECTIONNAME"]);
                    DataObject.SM_SESSIONNAME = Convert.ToString(rdr["SM_SESSIONNAME"]);

                    //ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return DataObject;
        }
        #endregion
        #region GetStudentNotice
        public List<NoticeResponse> GetNotice(NoticeRequest obj)
        {
            List<NoticeResponse> ListObject = new List<NoticeResponse>();
            NoticeResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            arrParams.Add(new SqlParameter("@NT_ID", obj.NT_ID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetNotice", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new NoticeResponse();
                    DataObject.NM_ENTRYDATE = Convert.ToDateTime(rdr["NM_EntryDate"]);
                    DataObject.NM_EXPDATE = Convert.ToDateTime(rdr["NM_ExpDate"]);
                    DataObject.NM_TITLE = Convert.ToString(rdr["NM_Title"]);
                    DataObject.NM_NOTICE = Convert.ToString(rdr["NM_Notice"]);
                    DataObject.NM_FACULTYNAME = Convert.ToString(rdr["FP_Name"]);
                    DataObject.NM_UPLOADFILE = Convert.ToString(rdr["NM_UploadFile"]);
                    DataObject.NM_Link = Convert.ToString(rdr["NM_Link"]);
                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentPaidDetails
        public List<StudentPaidDetailsResponse> GetStudentPaidDetails(StudentPaidDetailsRequest obj)
        {
            List<StudentPaidDetailsResponse> ListObject = new List<StudentPaidDetailsResponse>();
            StudentPaidDetailsResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            //arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            //arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_CurrentClassId));

            arrParams.Add(new SqlParameter("@STUDENTID", obj.STUDENTID));
            arrParams.Add(new SqlParameter("@SESSIONID", obj.SESSIONID));
            arrParams.Add(new SqlParameter("@CLASSID", obj.CLASSID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentPaidDetails", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentPaidDetailsResponse();
                    DataObject.RECIPTNO = Convert.ToString(rdr["RECIPTNO"]);
                    DataObject.FEESCOLLECTIONID = Convert.ToString(rdr["FEESCOLLECTIONID"]);
                    DataObject.PAIDAMOUNT = Convert.ToDecimal(rdr["PAIDAMOUNT"]);
                    DataObject.PAYMODE = Convert.ToString(rdr["PAYMODE"]);
                    DataObject.FEESDATE = Convert.ToString(rdr["FEESDATE"]);
                    DataObject.FROMMONTH = Convert.ToString(rdr["FROMMONTH"]);
                    DataObject.TOMONTH = Convert.ToString(rdr["TOMONTH"]);

                    //DataObject.ADMISSIONID = Convert.ToString(rdr["ADMISSIONID"]);
                    //DataObject.CREATEDATE = Convert.ToString(rdr["CREATEDATE"]);
                    //DataObject.CLASSID = Convert.ToString(rdr["CLASSID"]);
                    //DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    //DataObject.Sd_SexId = Convert.ToString(rdr["Sd_SexId"]);
                    //DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);

                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentSyllabus
        public List<SyllabusResponse> GetSyllabus(SyllabusRequest obj)
        {
            List<SyllabusResponse> ListObject = new List<SyllabusResponse>();
            SyllabusResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetSyllabus", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new SyllabusResponse();
                    DataObject.SM_SYLLABUSNAME = Convert.ToString(rdr["SM_SyllabusName"]);
                    DataObject.SM_CLASSID = Convert.ToInt64(rdr["SM_ClassId"]);
                    DataObject.SM_UPLOADFILE = Convert.ToString(rdr["SM_UploadFile"]);
                    DataObject.SM_CREATEDATE = Convert.ToDateTime(rdr["SM_CreateDate"]);


                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentPaidReceipt
        public StudentPaidReceiptResponse GetStudentPaidReceipt(StudentPaidReceiptRequest obj)
        {

            StudentPaidReceiptResponse response = new StudentPaidReceiptResponse();
            StudentPaidReceiptFeesHeadList DataObject = null;
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FEESCOLLECTIONID", obj.FEESCOLLECTIONID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentPaidReceipt", arrParams.ToArray());
            if (ds != null && ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    response.FEESCOLLECTIONID = Convert.ToInt64(ds.Tables[0].Rows[0]["FEESCOLLECTIONID"]);
                    response.SM_SESSIONNAME = Convert.ToString(ds.Tables[0].Rows[0]["SM_SESSIONNAME"]);
                    response.RECIPTNO = Convert.ToString(ds.Tables[0].Rows[0]["RECIPTNO"]);
                    response.ADMISSIONID = Convert.ToString(ds.Tables[0].Rows[0]["ADMISSIONID"]);
                    response.CM_CLASSNAME = Convert.ToString(ds.Tables[0].Rows[0]["CM_CLASSNAME"]);
                    response.INSTALLMENT = Convert.ToString(ds.Tables[0].Rows[0]["INSTALLMENT"]);
                    response.FEESDATE = Convert.ToString(ds.Tables[0].Rows[0]["FEESDATE"]);
                    response.FEESDATE = Convert.ToString(ds.Tables[0].Rows[0]["FEESDATE"]);
                    response.STUDENT_NAME = Convert.ToString(ds.Tables[0].Rows[0]["SD_StudentName"]);
                    response.SEXNAME = Convert.ToString(ds.Tables[0].Rows[0]["Sd_SexId"]);
                    response.PAYMODE = Convert.ToString(ds.Tables[0].Rows[0]["PAYMODE"]);
                    response.BANKNAME = Convert.ToString(ds.Tables[0].Rows[0]["BANKNAME"]);
                    response.CHQNO = Convert.ToString(ds.Tables[0].Rows[0]["CHQNO"]);
                    response.CHQDATE = Convert.ToString(ds.Tables[0].Rows[0]["CHQDATE"]);
                    response.PAIDAMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["PAIDAMOUNT"]);
                    response.TOTALFEESDUEAMOUNT = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalFeesDueAmount"]);
                    response.DISCOUNTEDBY = Convert.ToString(ds.Tables[0].Rows[0]["discountedBy"]);
                    response.SCM_SCHOOLNAME = Convert.ToString(ds.Tables[0].Rows[0]["SCM_SCHOOLNAME"]);

                }

                foreach (DataRow rdr in ds.Tables[1].Rows)
                {
                    DataObject = new StudentPaidReceiptFeesHeadList();

                    DataObject.FEM_FEESNAME = Convert.ToString(rdr["FEM_FEESNAME"]);
                    DataObject.INSTALMENTNO = Convert.ToInt64(rdr["INSTALMENTNO"]);
                    DataObject.INSTALMENTAMOUNT = Convert.ToDecimal(rdr["INSTALMENTAMOUNT"]);
                    DataObject.PYMENTAMOUNT = Convert.ToDecimal(rdr["PYMENTAMOUNT"]);
                    response.StudentPaidReceiptFeesHeadList.Add(DataObject);

                }
            }


            return response;
        }
        #endregion
        #region GetClassWiseBirthday
        public List<ClassWiseBirthdayResponse> GetClassWiseBirthday(ClassWiseBirthdayRequest obj)
        {
            List<ClassWiseBirthdayResponse> ListObject = new List<ClassWiseBirthdayResponse>();
            ClassWiseBirthdayResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_ClassId", obj.SD_ClassId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetClassWiseBirthday", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClassWiseBirthdayResponse();
                    DataObject.STUDENTID = Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.STUDENTNAME = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.PHOTO = Convert.ToString(rdr["SD_Photo"]);
                    DataObject.DOB = Convert.ToString(rdr["SD_DOB"]);
                    DataObject.BIRTHDAY_STATUS = Convert.ToString(rdr["Birthday_Status"]);
                    DataObject.CLASSID = Convert.ToString(rdr["SD_ClassId"]);
                    DataObject.CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SECTIONNAME = Convert.ToString(rdr["SECM_SECTIONNAME"]);
                    DataObject.AGE = Convert.ToString(rdr["Age"]);
                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentMarksheet
        public StudentMarksheetResponse GetStudentMarksheet(StudentMarksheetRequest obj)
        {
            StudentMarksheetResponse response = new StudentMarksheetResponse();
            StudentMarksheetHeadList DataObject = null;
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            arrParams.Add(new SqlParameter("@SD_ClassId", obj.SD_ClassId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_APIGetMarksheet", arrParams.ToArray());
            if (ds != null && ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {


                    response.CLASSID = Convert.ToInt64(ds.Tables[0].Rows[0]["SMT_ClassId"]);
                    response.STUDENT_ID = Convert.ToString(ds.Tables[0].Rows[0]["SMT_StudentId"]);
                    response.SESSIONID = Convert.ToInt64(ds.Tables[0].Rows[0]["SME_SessionId"]);
                    response.CLASS_NAME = Convert.ToString(ds.Tables[0].Rows[0]["CLASSNAME"]);
                    response.SUBJECT_GROUP_NAME = Convert.ToString(ds.Tables[0].Rows[0]["SubjectGroupName"]);
                    response.STUDENT_NAME = Convert.ToString(ds.Tables[0].Rows[0]["StudentName"]);
                    response.ROLLNO = Convert.ToString(ds.Tables[0].Rows[0]["ROLLNO"]);
                    response.SESSIONNAME = Convert.ToString(ds.Tables[0].Rows[0]["SESSIONNAME"]);
                    response.SCHOOLNAME = Convert.ToString(ds.Tables[0].Rows[0]["SCHOOLNAME"]);
                    response.REGISTRATION_NO = Convert.ToString(ds.Tables[0].Rows[0]["RegNo"]);
                    response.TERM_NAME = Convert.ToString(ds.Tables[0].Rows[0]["TM_TermName"]);

                }

                foreach (DataRow rdr in ds.Tables[1].Rows)
                {
                    DataObject = new StudentMarksheetHeadList();

                    DataObject.SUBJECT_NAME = Convert.ToString(rdr["SM_SubjectName"]);
                    DataObject.MARKS_OBTAINED = Convert.ToString(rdr["SMT_MarksObtained"]);
                    DataObject.MARKS_OBTAINEDP = Convert.ToString(rdr["SMT_MarksObtainedP"]);
                    DataObject.MARKS_OBTAINED_ORAL = Convert.ToString(rdr["SMT_MarksObtainedOral"]);
                    DataObject.GRADE = Convert.ToString(rdr["SMT_Grade"]);
                    DataObject.FULL_MARKS = Convert.ToString(rdr["SME_FullMarks"]);
                    DataObject.PASS_MARKS = Convert.ToString(rdr["SME_PassMarks"]);
                    DataObject.FULL_MARKSP = Convert.ToString(rdr["SME_FullMarksP"]);
                    DataObject.PASS_MARKSP = Convert.ToString(rdr["SME_PassMarksP"]);
                    DataObject.FULL_MARKS_ORAL = Convert.ToString(rdr["SME_FullMarksOral"]);
                    DataObject.PASS_MARKS_ORAL = Convert.ToString(rdr["SME_PassMarksOral"]);
                    DataObject.IS_PRACTICAL = Convert.ToString(rdr["IsPractical"]);
                    DataObject.IS_ORAL = Convert.ToString(rdr["IsOral"]);
                    DataObject.IS_ABSENTW = Convert.ToString(rdr["isAbsentW"]);
                    DataObject.IS_ABSENTP = Convert.ToString(rdr["isAbsentP"]);
                    DataObject.SUJECT_MAINGROUP = Convert.ToString(rdr["SubMainGrp"]);
                    DataObject.IS_OPTIONAL = Convert.ToString(rdr["isOptional"]);
                    response.StudentMarksheetHeadList.Add(DataObject);

                }
            }

            return response;
        }
        #endregion
        #region GetStudentWiseAttendence
        public AttendenceResponse GetStudentWiseAttendence(AttendenceRequest obj)
        {
            var response = new AttendenceResponse
            {
                Present = new List<string>(),
                HalfDay = new List<string>(),
                Absent = new List<string>(),
                Leave = new List<string>(),
                Holiday = new Dictionary<string, string>(),
                NoExam = new List<string>()
            };
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            arrParams.Add(new SqlParameter("@Year", obj.Year));
            arrParams.Add(new SqlParameter("@Month", obj.Month));
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentWiseAttendanceData", arrParams.ToArray());
            if (ds != null)
            {
                foreach (DataRow rdr in ds.Tables[0].Rows)
                {
                    response.Present.Add(rdr["Dates"].ToString());

                }
                foreach (DataRow rdr in ds.Tables[1].Rows)
                {
                    response.Absent.Add(rdr["Dates"].ToString());

                }
                foreach (DataRow rdr in ds.Tables[2].Rows)
                {
                    response.Holiday.Add(rdr["Dates"].ToString(), rdr["HolidayNames"].ToString());

                }
            }


            return response;
        }
        #endregion
        #region GetRoutine
        public List<RoutineResponse> GetRoutine(RoutineRequest obj)
        {
            List<RoutineResponse> ListObject = new List<RoutineResponse>();
            RoutineResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            arrParams.Add(new SqlParameter("@CWTR_Class", obj.CWTR_Class));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetRoutine", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new RoutineResponse();

                    DataObject.CWTR_TITLE = Convert.ToString(rdr["CWTR_Title"]);
                    DataObject.CWTR_DESCRIPTION = Convert.ToString(rdr["CWTR_Description"]);
                    DataObject.CWTR_UPLOADFILE = Convert.ToString(rdr["CWTR_UploadFile"]);
                    DataObject.CWTR_CREATEDATE = Convert.ToDateTime(rdr["CWTR_CreateDate"]);
                    DataObject.CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.SCM_SCHOOLNAME = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetFeeSummary
        #endregion
        #region GetStudentFeesummary
        public List<StudentFeeSummaryResponse> GetStudentFeeSummary(StudentFeeSummaryRequest obj)
        {
            List<StudentFeeSummaryResponse> ListObject = new List<StudentFeeSummaryResponse>();
            StudentFeeSummaryResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            //arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_APIGetFeeSummary", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentFeeSummaryResponse();
                    DataObject.FEESNAME = Convert.ToString(rdr["FEM_FEESNAME"]);
                    DataObject.INSTALMENTNO = Convert.ToString(rdr["CF_INSTALLMENTNO"]);
                    DataObject.FEES_AMOUNT = Convert.ToDecimal(rdr["FEESAMOUNT"]);
                    DataObject.INSTALMENT_AMOUNT = Convert.ToDecimal(rdr["CF_INSAMOUNT"]);
                    DataObject.PAID_AMOUNT = Convert.ToDecimal(rdr["PmntAmnt"]);
                    DataObject.DUE_MONTH = Convert.ToDateTime(rdr["DUEDATE"]);
                    DataObject.FEESID = Convert.ToInt64(rdr["CF_FEESID"]);

                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentDueFee
        public List<StudentDueFeeResponse> GetStudentDueFee(StudentDueFeeRequest obj)
        {
            List<StudentDueFeeResponse> ListObject = new List<StudentDueFeeResponse>();
            StudentDueFeeResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@STUDENTID", obj.STUDENTID));
            //arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_APIGetDueFees", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentDueFeeResponse();
                    DataObject.STUDENTID = Convert.ToString(rdr["STUDENTID"]);
                    DataObject.CLASSNAME = Convert.ToString(rdr["ClassName"]);
                    DataObject.FEES_HEAD = Convert.ToString(rdr["FEM_FEESNAME"]);
                    DataObject.INSTALLMENTNO = Convert.ToString(rdr["CF_INSTALLMENTNO"]);
                    DataObject.PAYABLE_AMOUNT = Convert.ToDecimal(rdr["PAYABLE_AMOUNT"]);
                    DataObject.INSTALMENT_AMOUNT = Convert.ToDecimal(rdr["CF_INSAMOUNT"]);
                    DataObject.DUE_AMOUNT = Convert.ToDecimal(rdr["CF_DUEAMOUNT"]);
                    DataObject.DUE_DATE = Convert.ToDateTime(rdr["CF_DUEDATE"]);
                    DataObject.TOTAL_DUEAMOUNT = Convert.ToDecimal(rdr["TOTALDUEAMOUNT"]);


                    ListObject.Add(DataObject);


                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region GetStudentLiveClass
        public List<StudentLiveClassResponse> GetStudentLiveClass(StudentLiveClassRequest obj)
        {
            List<StudentLiveClassResponse> ListObject = new List<StudentLiveClassResponse>();
            StudentLiveClassResponse DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_CurrentClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionId", obj.SD_CurrentSessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentLiveClass", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentLiveClassResponse();
                    DataObject.CWLS_ID = Convert.ToInt64(rdr["CWLS_ID"]);
                    DataObject.CWLS_SBM_SubjectName = Convert.ToString(rdr["CWLS_SBM_SubjectName"]);
                    DataObject.CWLS_Class_ID = Convert.ToInt64(rdr["CWLS_Class_ID"]);
                    DataObject.CWLS_ClassDate = Convert.ToDateTime(rdr["CWLS_ClassDate"]);
                    DataObject.CWLS_ClassTime = Convert.ToDateTime(rdr["CWLS_ClassTime"]);

                    DataObject.CWLS_Section_ID = Convert.ToInt64(rdr["CWLS_Section_ID"]);
                    DataObject.CWLS_SubGr_ID = Convert.ToInt64(rdr["CWLS_SubGr_ID"]);
                    DataObject.CWLS_Sub_ID = Convert.ToInt64(rdr["CWLS_Sub_ID"]);

                    DataObject.CWLS_Title = Convert.ToString(rdr["CWLS_Title"]);
                    DataObject.CWLS_Link = Convert.ToString(rdr["CWLS_Link"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #endregion

        #region Transport

        public List<TransportDetailsResponse> GetTransportDetails(TransportDetailsRequest obj)
        {
            TransportDetailsResponse DataObject = null;
            List<TransportDetailsResponse> list = new List<TransportDetailsResponse>();
            List<SqlParameter> arrParams = new List<SqlParameter>
            {
                new SqlParameter("@SchoolId", obj.SchoolId),
                new SqlParameter("@SessionId", obj.SessionId),
                new SqlParameter("@ClassId", obj.ClassId),
                new SqlParameter("@SectionId", obj.SectionId),
                new SqlParameter("@StudentId", obj.StudentId),
            };

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(),CommandType.StoredProcedure,"SP_Get_TransportDetails",arrParams.ToArray()))
            {
                if (rdr.Read())
                {
                    DataObject = new TransportDetailsResponse
                    {
                        StudentId = Convert.ToString(rdr["StudentId"]),
                        StudentName = Convert.ToString(rdr["StudentName"]),
                        ClassId = Convert.ToInt64(rdr["ClassId"]),
                        ClassName = Convert.ToString(rdr["ClassName"]),
                        SectionId = Convert.ToInt64(rdr["SectionId"]),
                        SectionName = Convert.ToString(rdr["SectionName"]),
                       // BusName = Convert.ToString(rdr["ASM_School_ID"]),
                        PickupStop = Convert.ToString(rdr["PickupStop"]),
                        DropLocation = Convert.ToString(rdr["DropLocation"]),

                        MonthlyPaymentStatus = new List<MonthlyPaymentStatus>()
                    };
                }

                if (DataObject != null && rdr.NextResult())
                {
                    while (rdr.Read())
                    {
                        DataObject.MonthlyPaymentStatus.Add(new MonthlyPaymentStatus
                        {
                            TransactionNo = Convert.ToString(rdr["TransactionNo"]),
                            MonthId = Convert.ToInt32(rdr["MonthId"]),
                            MonthName = Convert.ToString(rdr["MonthName"]),
                            PaymentAmount = Convert.ToDecimal(rdr["PaymentAmount"]),
                            PaymentDate = Convert.ToDateTime(rdr["PaymentDate"]),
                            PaymentMode = Convert.ToString(rdr["PaymentMode"])
                        });
                    }
                }
            }
            if (DataObject != null)
                list.Add(DataObject);

            return list;
        }

        #endregion

    }
}
   