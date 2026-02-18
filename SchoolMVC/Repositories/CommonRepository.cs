using AccountManagementSystem.Models;
using BussinessObject;
using BussinessObject.FeesCollection;
using SchoolMVC.GlobalClass;
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
    public class CommonRepository : IRepository
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;
        }
        #region GetGlobalSelect
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            DataSet ds = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            arrParams.Add(new SqlParameter("@PId", PId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "GlobalSelect_SP", arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }
        #endregion
        #region GlobalDelete
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@MainTableName", MainTableName));
            arrParams.Add(new SqlParameter("@MainFieldName", MainFieldName));
            if (TransTableName != null) arrParams.Add(new SqlParameter("@TransTableName", TransTableName));
            if (TransFieldName != null) arrParams.Add(new SqlParameter("@TransFieldName", TransFieldName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalDelete", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);

        }
        #endregion
        #region DuplicateDataCheck
        public string DuplicateDataCheck(string TableName, string DataField, string DataFieldValue, string OptionalField, long? OptionalFieldValue)
        {
            DataSet ds = new DataSet();
            string data = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@DataFieldValue", DataFieldValue));
            arrParams.Add(new SqlParameter("@TableName", TableName));
            arrParams.Add(new SqlParameter("@DataField", DataField));
            arrParams.Add(new SqlParameter("@OptionalField", OptionalField));
            arrParams.Add(new SqlParameter("@OptionalFieldValue", OptionalFieldValue));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_DuplicateDataChecking", arrParams.ToArray());
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    data = ds.Tables[0].Rows[0]["GetData"].ToString();
                }
            }

            return data;

        }
        #endregion
        #region MarkSheet
        public List<DDLList> GetClassWiseDetails(string TransactionType, long classId, long schoolId, long? sessionId, long? SubGrId)
        {
            List<DDLList> DDLObjList = new List<DDLList>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", TransactionType));
            if (sessionId != null) arrParams.Add(new SqlParameter("@SESSIONID", sessionId));
            arrParams.Add(new SqlParameter("@SCHOOLID", schoolId));
            arrParams.Add(new SqlParameter("@CLASSID", classId));
            if (SubGrId != null) arrParams.Add(new SqlParameter("@SubGrId", SubGrId));

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "usp_ClassWiseSubject_SubGroup", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DDLObjList.Add(new DDLList
                    {
                        Text = Convert.ToString(rdr["Text"]),
                        Value = Convert.ToInt32(rdr["Value"]),
                    });
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DDLObjList;
        }
        public List<clsStudentList> getStudentsList(long classId, long sectionId, long subjectId, long HS, long schoolId, long sessionId)
        {
            List<clsStudentList> students = new List<clsStudentList>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "getStudentsList"));
            arrParams.Add(new SqlParameter("@classId", classId));
            arrParams.Add(new SqlParameter("@sectionId", sectionId));
            if (subjectId != 0) arrParams.Add(new SqlParameter("@subjectId", subjectId));
            arrParams.Add(new SqlParameter("@HS", HS));
            arrParams.Add(new SqlParameter("@schoolId", schoolId));
            arrParams.Add(new SqlParameter("@sessionId", sessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "usp_StudetDetails_SD", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    students.Add(new clsStudentList
                    {
                        StudentId = Convert.ToString(rdr["StudentID"]),
                        StudentName = Convert.ToString(rdr["StudentName"]),
                        Roll = Convert.ToInt32(rdr["RollNo"])
                    });
                }
                rdr.Close();
            }
            rdr.Dispose();
            return students;
        }
        public GradeMaster_GM GradeCheck(decimal? Marks, long schoolId, long sessionId)
        {

            GradeMaster_GM CEObj = new GradeMaster_GM();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "CheckGrade"));
            arrParams.Add(new SqlParameter("@Marks", Marks));
            arrParams.Add(new SqlParameter("@SME_SchoolId", schoolId));
            arrParams.Add(new SqlParameter("@SME_SessionId", sessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "MarksDetailsEntry_SP", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    CEObj.GM_GradeName = Convert.ToString(rdr["GradeName"]);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return CEObj;
        }
        public long InsertUpdateMarks(clsStudentMarksEntry marks)
        {
            try
            {
                DataTable marksTrans = new DataTable();

                marksTrans.Columns.Add("SMT_SME_ExamGroupId", typeof(long));
                marksTrans.Columns.Add("SMT_ClassId", typeof(long));
                marksTrans.Columns.Add("SMT_SubjectGrpID", typeof(long));
                marksTrans.Columns.Add("SMT_SubjectId", typeof(long));
                marksTrans.Columns.Add("SMT_SME_TermId", typeof(long));
                marksTrans.Columns.Add("SMT_StudentId", typeof(string));
                marksTrans.Columns.Add("SMT_MarksObtained", typeof(decimal));
                marksTrans.Columns.Add("SMT_MarksObtainedP", typeof(decimal));
                marksTrans.Columns.Add("Grade", typeof(string));
                foreach (var SAL in marks.StudentMarksTransactionList)
                {
                    DataRow dr = marksTrans.NewRow();
                    //dr["SMT_SME_ExamGroupId"] = marks.SME_ExamGroupId;
                    dr["SMT_SME_ExamGroupId"] = marks.SME_ExamGroupId ?? (object)DBNull.Value;
                    dr["SMT_ClassId"] = marks.SME_ClassId;
                    dr["SMT_SubjectGrpID"] = marks.SME_SubjectGrpID;
                    dr["SMT_SubjectId"] = marks.SME_SubjectId;
                    dr["SMT_SME_TermId"] = marks.SME_TermId;
                    dr["SMT_StudentId"] = SAL.StudentId;
                    dr["SMT_MarksObtained"] = SAL.SMT_MarksObtained;
                    dr["SMT_MarksObtainedP"] = SAL.SMT_MarksObtainedP == null ? 0 : SAL.SMT_MarksObtainedP;
                    dr["Grade"] = SAL.Grade;
                    marksTrans.Rows.Add(dr);
                }
                List<SqlParameter> arrParams = new List<SqlParameter>();
                if (marks.SME_Id == 0 || marks.SME_Id == null)
                {
                    arrParams.Add(new SqlParameter("@TransactionType", "Insert"));
                }

                if (marks.SME_Id >= 0 && marks.SME_Id != null)
                {
                    arrParams.Add(new SqlParameter("@TransactionType", "Update"));

                }
                arrParams.Add(new SqlParameter("@SME_Id", marks.SME_Id));
                arrParams.Add(new SqlParameter("@SME_ClassId", marks.SME_ClassId));
                arrParams.Add(new SqlParameter("@SME_SectionId", marks.SME_SectionId));
                arrParams.Add(new SqlParameter("@SME_SubGrpId", marks.SME_SubjectGrpID));
                arrParams.Add(new SqlParameter("@SME_SchoolId", marks.SME_SchoolId));
                arrParams.Add(new SqlParameter("@SME_SessionId", marks.SME_SessionId));
                arrParams.Add(new SqlParameter("@SME_SubjectId", marks.SME_SubjectId));
                arrParams.Add(new SqlParameter("@SME_FullMarks", marks.SME_FullMarks));
                arrParams.Add(new SqlParameter("@SME_PassMarks", marks.SME_PassMarks));
                arrParams.Add(new SqlParameter("@SME_FullMarksP", marks.SME_FullMarksP));
                arrParams.Add(new SqlParameter("@SME_PassMarksP", marks.SME_PassMarksP));
                //arrParams.Add(new SqlParameter("@SME_ExamGroupId", marks.SME_ExamGroupId)); 
                arrParams.Add(new SqlParameter("@SME_ExamGroupId", (object)marks.SME_ExamGroupId ?? DBNull.Value));
                arrParams.Add(new SqlParameter("@SME_TermId", marks.SME_TermId));
                arrParams.Add(new SqlParameter("@IsPractical", marks.IsPractical));
                arrParams.Add(new SqlParameter("@UserId", marks.UserId));
                arrParams.Add(new SqlParameter("@StudentMarksTransactionList", marksTrans));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "MarksDetailsEntry_SP", arrParams.ToArray());
                return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<clsStudentMarksEntry> GetStudentMarksList(clsStudentMarksEntry marks)
        {
            List<clsStudentMarksEntry> CEMObjList = new List<clsStudentMarksEntry>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SelectStudentForMarksDetails"));
            arrParams.Add(new SqlParameter("@SME_ClassId", marks.SME_ClassId));
            arrParams.Add(new SqlParameter("@SME_SectionId", marks.SME_SectionId));
            arrParams.Add(new SqlParameter("@SME_SchoolId", marks.SME_SchoolId));
            arrParams.Add(new SqlParameter("@SME_SessionId", marks.SME_SessionId));
            arrParams.Add(new SqlParameter("@UserId", marks.UserId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "MarksDetailsEntry_SP", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    clsStudentMarksEntry CEObj = new clsStudentMarksEntry();
                    CEObj.SME_Id = Convert.ToInt32(rdr["SME_Id"]);
                    CEObj.SME_ClassId = Convert.ToInt32(rdr["SME_ClassId"]);
                    CEObj.TM_TermName = Convert.ToString(rdr["TM_TermName"]);
                    CEObj.SectionName = Convert.ToString(rdr["SECM_SECTIONNAME"]);
                    CEObj.ClassName = Convert.ToString(rdr["CM_CLASSNAME"]);
                    CEObj.SM_SubjectName = Convert.ToString(rdr["SBM_SubjectName"]);
                    CEObj.SME_FullMarks = Convert.ToInt32(rdr["SME_FullMarks"]);
                    CEObj.SME_PassMarks = Convert.ToInt32(rdr["SME_PassMarks"]);
                    CEObj.SubGrpName = Convert.ToString(rdr["SGM_SubjectGroupName"]);
                    CEObj.SME_SubjectGrpID = rdr["SME_SubGrpId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SME_SubGrpId"]);
                    CEMObjList.Add(CEObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CEMObjList;
        }
        public clsStudentMarksEntry GetStudentMarks(clsStudentMarksEntry marks)
        {
            DataSet ds = new DataSet();
            clsStudentMarksEntry CEObj = new clsStudentMarksEntry();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SelectStudentForMarksEdit"));
            arrParams.Add(new SqlParameter("@SME_SchoolId", marks.SME_SchoolId));
            arrParams.Add(new SqlParameter("@SME_SessionId", marks.SME_SessionId));
            arrParams.Add(new SqlParameter("@SME_Id", marks.SME_Id));
            arrParams.Add(new SqlParameter("@UserId", marks.UserId)); ;
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "MarksDetailsEntry_SP", arrParams.ToArray());
            if (ds != null && ds.Tables.Count >= 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var rdr = ds.Tables[0].Rows[0];
                    CEObj.SME_Id = Convert.ToInt32(rdr["SME_Id"]);
                    CEObj.SME_SectionId = Convert.ToInt32(rdr["SME_SectionId"]);
                    CEObj.SME_ClassId = Convert.ToInt32(rdr["SME_ClassId"]);
                    CEObj.SME_SubjectId = Convert.ToInt32(rdr["SME_SubjectId"]);
                    CEObj.SME_TermId = Convert.ToInt32(rdr["SME_TermId"]);
                    CEObj.SME_FullMarks = Convert.ToDecimal(rdr["SME_FullMarks"]);
                    CEObj.SME_PassMarks = Convert.ToDecimal(rdr["SME_PassMarks"]);
                    CEObj.SME_FullMarksP = Convert.ToDecimal(rdr["SME_FullMarksP"]);
                    CEObj.SME_PassMarksP = Convert.ToDecimal(rdr["SME_PassMarksP"]);
                    CEObj.IsPractical = Convert.ToBoolean(rdr["IsPractical"]);
                    CEObj.SME_SubjectGrpID = rdr["SME_SubGrpId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SME_SubGrpId"]);
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow rdr in ds.Tables[1].Rows)
                    {
                        clsStudentMarksTransaction trans = new clsStudentMarksTransaction();
                        trans.StudentId = Convert.ToString(rdr["StudentId"]);
                        trans.Roll = Convert.ToString(rdr["ROLLNO"]);
                        trans.StudentName = Convert.ToString(rdr["STUDENTNAME"]);
                        trans.SMT_MarksObtained = Convert.ToDecimal(rdr["SMT_MarksObtained"]);
                        trans.SMT_MarksObtainedP = Convert.ToDecimal(rdr["SMT_MarksObtainedP"]);
                        trans.Grade = Convert.ToString(rdr["SMT_Grade"]);
                        CEObj.StudentMarksTransactionList.Add(trans);
                    }
                }
            }

            return CEObj;
        }
        public List<clsStudentList> studentsForMarkSheet(clsStudentList query)
        {
            //query.StudentId = query.StudentId.Replace(" ", "");
            List<clsStudentList> dbResults = new List<clsStudentList>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "StudentsForMarksPrint"));
            arrParams.Add(new SqlParameter("@SchoolId", query.SchoolId));
            arrParams.Add(new SqlParameter("@SessionId", query.SessionId));
            if (query.StudentId == "" || query.StudentId == null)
            {
                if (query.ClassId != 0) arrParams.Add(new SqlParameter("@ClassId", query.ClassId));
                if (query.SectionId != 0) arrParams.Add(new SqlParameter("@SectionId", query.SectionId));
            }
            if (query.TermId != 0 && query.TermId != null) arrParams.Add(new SqlParameter("@TermId", query.TermId));
            if (query.StudentId != "" && query.StudentId != null) arrParams.Add(new SqlParameter("@StudentId", query.StudentId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "usp_StudentMarkSheet", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    dbResults.Add(new clsStudentList
                    {
                        SME_ID = long.Parse(rdr["SME_Id"].ToString()),
                        ClassId = Convert.ToInt32(rdr["SME_ClassId"]),
                        ClassName = Convert.ToString(rdr["CM_CLASSNAME"]),
                        SectionId = Convert.ToInt32(rdr["SME_SectionId"]),
                        SectionName = Convert.ToString(rdr["SECM_SECTIONNAME"]),
                        TermId = Convert.ToInt32(rdr["SME_TermId"]),
                        TermName = Convert.ToString(rdr["TM_TermName"]),
                        StudentId = Convert.ToString(rdr["SMT_StudentId"]),
                        StudentName = Convert.ToString(rdr["StudentName"]),
                        Roll = Convert.ToInt32(rdr["ROLLNO"]),
                    });
                }
                rdr.Close();
            }
            rdr.Dispose();
            return dbResults;
        }
        public List<clsStudentList> studentsForCouncilRegistration(clsStudentList query)
        {
            List<clsStudentList> dbResults = new List<clsStudentList>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "StudentsForCouncilReg"));
            arrParams.Add(new SqlParameter("@SchoolId", query.SchoolId));
            arrParams.Add(new SqlParameter("@SessionId", query.SessionId));
            if (query.ClassId != 0) arrParams.Add(new SqlParameter("@ClassId", query.ClassId));
            if (query.SectionId != 0) arrParams.Add(new SqlParameter("@SectionId", query.SectionId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "usp_StudentCouncilRegistration", arrParams.ToArray());
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
                            ClassId = Convert.ToInt32(rdr["CLASSID"]),
                            Roll = Convert.ToInt32(rdr["ROLLNO"]),
                            SectionId = Convert.ToInt32(rdr["SECTIONID"]),
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
        #endregion MarkSheet
        #region AdmissionFees
        public List<StudentList> SearchNonAdmittedStudent(FeesSearchBO SearchBO)
        {
            try
            {
                List<StudentList> dbResults = new List<StudentList>();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "Searchstudent"));
                arrParams.Add(new SqlParameter("@SchoolId", SearchBO.SchoolId));
                arrParams.Add(new SqlParameter("@SessionId", SearchBO.SessionId));
                if (SearchBO.FormNo != null) arrParams.Add(new SqlParameter("@FormNo", SearchBO.FormNo));
                if (SearchBO.AdmissionNo != null) arrParams.Add(new SqlParameter("@AdmissionNo", SearchBO.AdmissionNo));
                if (SearchBO.ClassId > 0) arrParams.Add(new SqlParameter("@ClassId", SearchBO.ClassId));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        dbResults.Add(new StudentList
                        {
                            SD_Id = rdr["SD_Id"] == DBNull.Value ? 0 : Convert.ToInt64(rdr["SD_Id"]),
                            SD_FormNo = rdr["SD_FormNo"] == DBNull.Value ? null : Convert.ToString(rdr["SD_FormNo"]),
                            SD_AppliactionNo = rdr["SD_AppliactionNo"] == DBNull.Value ? null : Convert.ToString(rdr["SD_AppliactionNo"]),
                            CM_CLASSNAME = rdr["CM_CLASSNAME"] == DBNull.Value ? null : Convert.ToString(rdr["CM_CLASSNAME"]),
                            ClassId = rdr["SD_ClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_ClassId"]),
                            SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_StudentName"]),
                            SD_FatherName = rdr["SD_FatherName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_FatherName"]),
                            SD_AppliactionDate = rdr["SD_AppliactionDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["SD_AppliactionDate"]),
                            SD_DOB = rdr["SD_DOB"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["SD_DOB"]),
                           // SD_StudentCategoryId = rdr["SD_StudentCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_StudentCategoryId"]),
                            CAT_STUDENTCATEGORY = rdr["CAT_STUDENTCATEGORY"] == DBNull.Value ? null : Convert.ToString(rdr["CAT_STUDENTCATEGORY"]),
                        });
                    }
                    rdr.Close();
                }
                rdr.Dispose();
                return dbResults;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<studentFeesDetails> getStudentFeesDetails(FeesSearchBO SearchBO)
        {
            try
            {
                List<studentFeesDetails> dbResults = new List<studentFeesDetails>();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "Fees"));
                arrParams.Add(new SqlParameter("@SchoolId", SearchBO.SchoolId));
                arrParams.Add(new SqlParameter("@SessionId", SearchBO.SessionId));
                if (SearchBO.FormNo != null) arrParams.Add(new SqlParameter("@FormNo", SearchBO.FormNo));
                if (SearchBO.AdmissionNo != null) arrParams.Add(new SqlParameter("@AdmissionNo", SearchBO.AdmissionNo));
                if (SearchBO.ClassId > 0) arrParams.Add(new SqlParameter("@ClassId", SearchBO.ClassId));
                if (SearchBO.CategoryId > 0) arrParams.Add(new SqlParameter("@STUDENTCATEGORYID", SearchBO.CategoryId));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);

                SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        var studentFees = new studentFeesDetails
                        {
                            FeesName = rdr["FEM_FEESNAME"] == DBNull.Value ? null : Convert.ToString(rdr["FEM_FEESNAME"]),
                            FeesId = rdr["CF_FEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_FEESID"]),
                            NoOfInstallment = rdr["FEM_NOOFINSTALLMENT"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FEM_NOOFINSTALLMENT"]),
                            FeesAmount = rdr["FEESAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["FEESAMOUNT"]),
                            NoOfFins = rdr["CF_NOOFINS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_NOOFINS"]),
                            InstallmentNo = rdr["CF_INSTALLMENTNO"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_INSTALLMENTNO"]),
                            DueDate = rdr["DUEDATE"] == DBNull.Value ? null : Convert.ToString(rdr["DUEDATE"]),
                            InsAmount = rdr["CF_INSAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CF_INSAMOUNT"]),
                            PaymentAmount = rdr["PmntAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PmntAmnt"]),
                            AdustAmnt = rdr["AdustAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["AdustAmnt"]),
                            DueAmt = rdr["DueAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["DueAmt"]),
                            ClassId = rdr["CF_CLASSID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_CLASSID"]),
                            ClassFeesId = rdr["CF_CLASSFEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_CLASSFEESID"]),
                            IsAdmissionTime = rdr["IsAdmissionTime"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsAdmissionTime"]),
                        };
                        if (studentFees.IsAdmissionTime == true)
                        {
                            studentFees.IsPaid = true;
                            studentFees.IsPaidChecked = "true";
                        }
                        else
                        {
                            studentFees.IsPaid = false;
                            studentFees.IsPaidChecked = "false";
                            studentFees.DueAmt = studentFees.PaymentAmount;
                            studentFees.PaymentAmount = 0;
                        }
                        studentFees.IsDisc = false;
                        studentFees.IsDiscChecked = "false";

                        dbResults.Add(studentFees);
                    }
                    rdr.Close();
                }
                rdr.Dispose();
                return dbResults;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion AdmissionFees

        public decimal InsertUpdateFeesCollection(FeesCollectionBO collection, string type)
        {
            DataTable dtfeesTrns = new DataTable();

            try
            {
                dtfeesTrns.Columns.Add("FEESID", typeof(long));
                dtfeesTrns.Columns.Add("FEESAMNT", typeof(decimal));
                dtfeesTrns.Columns.Add("TOTALINSTLMNT", typeof(decimal));
                dtfeesTrns.Columns.Add("INSTALMENTNO", typeof(long));
                dtfeesTrns.Columns.Add("DUEDATE", typeof(string));
                dtfeesTrns.Columns.Add("INSTALLMENTAMOUNT", typeof(decimal));
                dtfeesTrns.Columns.Add("PAYMENTAMOUNT", typeof(decimal));
                dtfeesTrns.Columns.Add("ADJUSTMENT", typeof(decimal));
                dtfeesTrns.Columns.Add("DUEAMT", typeof(decimal));
                dtfeesTrns.Columns.Add("CLASSID", typeof(long));
                dtfeesTrns.Columns.Add("CLASSFEESID", typeof(long));
                dtfeesTrns.Columns.Add("IsDisc", typeof(bool));
                dtfeesTrns.Columns.Add("IsPaid", typeof(bool));

                // Late Fee columns
                dtfeesTrns.Columns.Add("LATEFEES", typeof(decimal));
                dtfeesTrns.Columns.Add("LATEDISCOUNT", typeof(decimal));
                dtfeesTrns.Columns.Add("IsLateDisc", typeof(bool));
                // Wave Logic
                dtfeesTrns.Columns.Add("IsWave", typeof(bool)); 
                long ClassId = collection.CM_CLASSID.HasValue ? (long)collection.CM_CLASSID.Value : 0;
                if (ClassId <= 0)
                {
                    throw new Exception("Invalid ClassId while saving Fees Collection.");
                }

                foreach (var Col_ttrn in collection.collectionsTrans)
                {
                    DataRow dr = dtfeesTrns.NewRow();

                    dr["FEESID"] = Col_ttrn.FEESID ?? 0;
                    dr["FEESAMNT"] = Col_ttrn.FEESAMNT ?? 0;
                    dr["TOTALINSTLMNT"] = Col_ttrn.TOTALINSTLMNT ?? 0;
                    dr["INSTALMENTNO"] = Col_ttrn.INSTALMENTNO ?? 0;
                    dr["DUEDATE"] = Col_ttrn.DUEDATE;
                    dr["INSTALLMENTAMOUNT"] = Col_ttrn.INSTALLMENTAMOUNT ?? 0;
                    dr["PAYMENTAMOUNT"] = Col_ttrn.PAYMENTAMOUNT ?? 0;
                    dr["ADJUSTMENT"] = Col_ttrn.DISCOUNT ?? 0;
                    dr["DUEAMT"] = Col_ttrn.DUEAMT ?? 0;
                    dr["CLASSID"] = ClassId;
                    dr["CLASSFEESID"] = Col_ttrn.CLASSFEESID ?? 0;
                    dr["IsDisc"] = Col_ttrn.IsDisc ?? false;
                    dr["IsPaid"] = Col_ttrn.IsPaid ?? false;

                    // Late fees
                    dr["LATEFEES"] = Col_ttrn.LATEFEES ?? 0;
                    dr["LATEDISCOUNT"] = Col_ttrn.LATEDISCOUNT ?? 0;
                    dr["IsLateDisc"] = Col_ttrn.IsLateDisc ?? false;
                    // Wave Logic
                    dr["IsWave"] = Col_ttrn.IsWave ?? false; ;
                    dtfeesTrns.Rows.Add(dr);
                }

                List<SqlParameter> arrParams = new List<SqlParameter>();

                arrParams.Add(new SqlParameter("@TransactionType",
                    collection.feesCollectionId > 0 ? "UpdateFessCollection" : "FeesInsert"));

                arrParams.Add(new SqlParameter("@FeesColType", type));
                arrParams.Add(new SqlParameter("@FEESCOLLECTIONID", collection.feesCollectionId));
                arrParams.Add(new SqlParameter("@AdmissionNo", collection.admissionNo));
                arrParams.Add(new SqlParameter("@CLASSID", ClassId));
                arrParams.Add(new SqlParameter("@FEESDATE", collection.feesDate));
                arrParams.Add(new SqlParameter("@FormNo", collection.formNo));

                if (type == "C")
                {
                    arrParams.Add(new SqlParameter("@StudentID", collection.studentId));
                    arrParams.Add(new SqlParameter("@StuRegNo", collection.studenRegistrationId));
                }

                arrParams.Add(new SqlParameter("@TOTALAMOUNT", collection.totalAmount));
                arrParams.Add(new SqlParameter("@ADJUSTAmount", collection.discount));
                arrParams.Add(new SqlParameter("@PAIDAMOUNT", collection.paidAmount));
                arrParams.Add(new SqlParameter("@LATEFEES", collection.totallatefees));
                arrParams.Add(new SqlParameter("@LATEDISCOUNT", collection.latefeesdiscount));
                arrParams.Add(new SqlParameter("@PAIDLATEFEES", collection.totalpaidlatefees));
                arrParams.Add(new SqlParameter("@TOTALPAIDAMOUNT", collection.totalpaidamount));
                arrParams.Add(new SqlParameter("@TOTALDUE", collection.totalDue));
                arrParams.Add(new SqlParameter("@PAYMODE", collection.paymodeType));
                arrParams.Add(new SqlParameter("@CHQNO", collection.cheqDDNo));
                arrParams.Add(new SqlParameter("@CHQDATE", collection.cheqDDDate));
                arrParams.Add(new SqlParameter("@BANKNAME", collection.bankName));
                arrParams.Add(new SqlParameter("@CardTrnsRefNo", collection.Card_TrnsRefNo));
                arrParams.Add(new SqlParameter("@BRANCHNAME", collection.branchName));
                arrParams.Add(new SqlParameter("@CREATEDUID", collection.userId));
                arrParams.Add(new SqlParameter("@SCHOOLID", collection.schoolId));
                arrParams.Add(new SqlParameter("@SESSIONID", collection.sessionId));
                arrParams.Add(new SqlParameter("@discountedBy", collection.discountedBy));
                arrParams.Add(new SqlParameter("@CollectionTransaction", dtfeesTrns));

                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal)
                {
                    Direction = ParameterDirection.Output
                };
                arrParams.Add(OutPutId);

                SqlHelper.ExecuteNonQuery(
                    GetConnectionString(),
                    CommandType.StoredProcedure,
                    "Usp_FeesCollection",
                    arrParams.ToArray()
                );

                return Convert.ToDecimal(OutPutId.Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dtfeesTrns.Dispose();
            }
        }

        public FormBO getAsyncFeesUpdate(long Id)
        
        {
            try
            {
                DataSet ds = new DataSet();
                FormBO form = new FormBO();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@FEESCOLLECTIONID", Id));
                arrParams.Add(new SqlParameter("@TransactionType", "GetFeesCollectionData"));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);

                ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (ds != null && ds.Tables.Count >= 1)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        var rdr = ds.Tables[0].Rows[0];
                        form.RECIPTNO = rdr["RECIPTNO"] == DBNull.Value ? null : Convert.ToString(rdr["RECIPTNO"]);
                        form.TotalAmount = rdr["TOTALAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TOTALAMOUNT"]);
                        form.PaidAmount = rdr["PAIDAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PAIDAMOUNT"]);
                        form.Discount = rdr["ADJUSTAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["ADJUSTAMOUNT"]);
                        form.TotalDue = rdr["DueAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["DueAmount"]);
                        form.DiscountedBy = rdr["discountedBy"] == DBNull.Value ? null : Convert.ToString(rdr["discountedBy"]);
                        form.FeesDate = rdr["FEESDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["FEESDATE"]);
                        form.BankName = rdr["BANKNAME"] == DBNull.Value ? null : Convert.ToString(rdr["BANKNAME"]);
                        form.BranchName = rdr["BRANCHNAME"] == DBNull.Value ? null : Convert.ToString(rdr["BRANCHNAME"]);
                        form.CheqDDNo = rdr["CHQNO"] == DBNull.Value ? null : Convert.ToString(rdr["CHQNO"]);
                        form.CheqDDDate = rdr["CHQDATE"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["CHQDATE"]);
                        form.Card_RefNo = rdr["Card_TrnsRefNo"] == DBNull.Value ? null : Convert.ToString(rdr["Card_TrnsRefNo"]);
                        form.PaymodeType = rdr["PAYMODE"] == DBNull.Value ? null : Convert.ToString(rdr["PAYMODE"]);
                        form.StudentInformation.AdmissionNo = rdr["ADMISSIONID"] == DBNull.Value ? null : Convert.ToString(rdr["ADMISSIONID"]);
                        form.StudentInformation.ClassId = rdr["CLASSID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CLASSID"]);
                        form.StudentInformation.ClassName = rdr["CM_CLASSNAME"] == DBNull.Value ? null : Convert.ToString(rdr["CM_CLASSNAME"]);
                        form.StudentInformation.formNo = rdr["FORMNO"] == DBNull.Value ? null : Convert.ToString(rdr["FORMNO"]);
                        form.StudentInformation.SD_StudentCategoryId = rdr["SD_StudentCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_StudentCategoryId"]);
                        form.StudentInformation.StudentName = rdr["SD_StudentName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_StudentName"]);
                        form.StudentInformation.StudentId = rdr["STUDENTID"] == DBNull.Value ? null : Convert.ToString(rdr["STUDENTID"]);
                        form.StudentInformation.SECM_SECTIONNAME = rdr["SECM_SECTIONNAME"] == DBNull.Value ? null : Convert.ToString(rdr["SECM_SECTIONNAME"]);
                        form.StudentInformation.SD_CurrentRoll = rdr["SD_CurrentRoll"] == DBNull.Value ? null : Convert.ToString(rdr["SD_CurrentRoll"]);
                        form.StudentInformation.SD_FatherName = rdr["SD_FatherName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_FatherName"]);

                        form.StudentInformation.RegNo = rdr["REGID"] == DBNull.Value ? null : Convert.ToString(rdr["REGID"]);
                    }
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow rdr in ds.Tables[1].Rows)
                        {
                            var fees = new studentFeesDetails()
                            {
                                AdustAmnt = rdr["AdustAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["AdustAmnt"]),
                                ClassFeesId = rdr["CLASSFEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CLASSFEESID"]),
                                ClassId = rdr["CLASSID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CLASSID"]),
                                DueAmt = rdr["DUEAMT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["DUEAMT"]),
                                DueDate = rdr["DUEDATE"] == DBNull.Value ? null : Convert.ToString(rdr["DUEDATE"]),
                                FeesAmount = rdr["FEESAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["FEESAMOUNT"]),
                                FeesId = rdr["FEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FEESID"]),
                                FeesName = rdr["FEESNAME"] == DBNull.Value ? null : Convert.ToString(rdr["FEESNAME"]),
                                InsAmount = rdr["INSAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["INSAMOUNT"]),
                                InstallmentNo = rdr["INSTALLMENTNO"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["INSTALLMENTNO"]),
                                IsDisc = rdr["IsDisc"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsDisc"]),
                                IsPaid = rdr["IsPaid"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsPaid"]),
                                NoOfFins = rdr["NOOFINS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["NOOFINS"]),
                                NoOfInstallment = rdr["FEM_NOOFINSTALLMENT"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FEM_NOOFINSTALLMENT"]),
                                PaymentAmount = rdr["PmntAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PmntAmnt"]),

                            };
                            fees.Payable = (fees.PaymentAmount + fees.DueAmt);
                            fees.IsDiscChecked = fees.IsDisc == true ? "true" : "false";
                            fees.IsPaidChecked = fees.IsPaid == true ? "true" : "false";
                            form.StudentFees.Add(fees);
                        }
                    }
                }
                return form;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public LateFeesSlap FindLateFeesAmount(long schoolId, long sessionId, DateTime dueDate, DateTime collectionDate)   //int fineTypeId,
        {
            LateFeesSlap CEObj = new LateFeesSlap();
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
        new SqlParameter("@TransactionType", "FindLateFeesAmount"),
        //new SqlParameter("@FineTypeId", fineTypeId),
        new SqlParameter("@SchoolId", schoolId),
        new SqlParameter("@SessionId", sessionId),
        new SqlParameter("@DueDate", dueDate),
         new SqlParameter("@CollectionDate", collectionDate)
    };

            SqlParameter OutPutId = new SqlParameter("@OutputId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray()))
            {
                if (rdr != null && rdr.Read())
                {
                    CEObj.Slap_Amount = rdr["FineAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["FineAmount"]);
                    CEObj.FineTypeId = rdr["FineTypeUsed"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FineTypeUsed"]);
                }
            }
            return CEObj;
        }

        public List<AdditionalFeesBO> GetAdditionalFeesIds()
        {

            try
            {
                List<AdditionalFeesBO> addFeesIds = new List<AdditionalFeesBO>();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "FindAdditionalFeesIds"));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        addFeesIds.Add(new AdditionalFeesBO
                        {
                            FeesCode = Convert.ToString(rdr["FEM_FeesCode"]),
                            FeesId = Convert.ToInt32(rdr["FEM_FEESID"]),
                        });
                    }
                    rdr.Close();
                }
                rdr.Dispose();
                return addFeesIds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FeesCollectionBO> GetFeeSCollectionList(FeesCollectionBO obj, string FeesColType)
        {
            List<FeesCollectionBO> ListObject = new List<FeesCollectionBO>();
            FeesCollectionBO DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "Select"));
            arrParams.Add(new SqlParameter("@SchoolId", obj.schoolId));
            arrParams.Add(new SqlParameter("@SessionId", obj.sessionId));
            if (obj.CM_CLASSID != null) arrParams.Add(
    new SqlParameter("@ClassId", obj.CM_CLASSID ?? (object)DBNull.Value)
);
            if (obj.admissionNo != null) arrParams.Add(new SqlParameter("@ADMISSIONID", obj.admissionNo));
            if (obj.formNo != null) arrParams.Add(new SqlParameter("@FORMNO", obj.formNo));
            if (obj.studentId != null) arrParams.Add(new SqlParameter("@StudentID", obj.studentId));
            if (!string.IsNullOrWhiteSpace(obj.studentName))
                arrParams.Add(new SqlParameter("@StuName", obj.studentName));
            else
                arrParams.Add(new SqlParameter("@StuName", DBNull.Value));
            if (obj.fromDate.HasValue && obj.fromDate.Value > DateTime.MinValue)
                arrParams.Add(new SqlParameter("@FromDate", obj.fromDate));
            else
                arrParams.Add(new SqlParameter("@FromDate", DBNull.Value));

            if (obj.toDate.HasValue && obj.toDate.Value > DateTime.MinValue)
                arrParams.Add(new SqlParameter("@ToDate", obj.toDate));
            else
                arrParams.Add(new SqlParameter("@ToDate", DBNull.Value));
            arrParams.Add(new SqlParameter("@FeesColType", FeesColType));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new FeesCollectionBO();
                    DataObject.admissionNo = Convert.ToString(rdr["ADMISSIONID"]);
                    DataObject.CM_CLASSID = Convert.ToInt32(rdr["CLASSID"]);
                    DataObject.feesDate = Convert.ToDateTime(rdr["FEESDATE"]);
                    DataObject.formNo = Convert.ToString(rdr["FORMNO"]);
                    DataObject.rcptNo = Convert.ToString(rdr["RECIPTNO"]);
                    DataObject.studenRegistrationId = Convert.ToString(rdr["REGID"]);
                    DataObject.studentId = Convert.ToString(rdr["STUDENTID"]);
                    DataObject.totalAmount = Convert.ToDecimal(rdr["TOTALAMOUNT"]);
                    DataObject.discount = Convert.ToDecimal(rdr["ADJUSTAMOUNT"]);
                    DataObject.paidAmount = Convert.ToDecimal(rdr["PAIDAMOUNT"]);
                    DataObject.paymodeType = Convert.ToString(rdr["PAYMODE"]);
                    DataObject.cheqDDNo = Convert.ToString(rdr["CHQNO"]);
                    //DataObject.cheqDDDate = Convert.ToDateTime(rdr["CHQDATE"]);
                    DataObject.bankName = Convert.ToString(rdr["BANKNAME"]);
                    DataObject.branchName = Convert.ToString(rdr["BRANCHNAME"]);
                    DataObject.className = Convert.ToString(rdr["CM_CLASSNAME"]);
                    DataObject.feesCollectionId = Convert.ToInt32(rdr["FEESCOLLECTIONID"]);
                    DataObject.totalDue = Convert.ToInt32(rdr["DueAmount"]);
                    DataObject.studentName = Convert.ToString(rdr["SD_StudentName"]);
                    //CEObj.Edit = "";
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #region SessionFees
        public List<StudentList> FetchAdmittedStudent(FeesSearchBO SearchBO)
        {
            try
            {
                List<StudentList> dbResults = new List<StudentList>();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "FetchStudentsForSessionCollection"));
                arrParams.Add(new SqlParameter("@SchoolId", SearchBO.SchoolId));
                arrParams.Add(new SqlParameter("@SessionId", SearchBO.SessionId));
                if (SearchBO.RegNo != null) arrParams.Add(new SqlParameter("@StuRegNo", SearchBO.RegNo));
                if (SearchBO.AdmissionNo != null) arrParams.Add(new SqlParameter("@AdmissionNo", SearchBO.AdmissionNo));
                if (SearchBO.StudentId != null) arrParams.Add(new SqlParameter("@StudentID", SearchBO.StudentId));
                if (SearchBO.ClassId > 0) arrParams.Add(new SqlParameter("@ClassId", SearchBO.ClassId));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        dbResults.Add(new StudentList
                        {
                            SD_FormNo = rdr["SD_FormNo"] == DBNull.Value ? null : Convert.ToString(rdr["SD_FormNo"]),
                            SD_AppliactionNo = rdr["SD_AppliactionNo"] == DBNull.Value ? null : Convert.ToString(rdr["SD_AppliactionNo"]),
                            CM_CLASSNAME = rdr["CM_CLASSNAME"] == DBNull.Value ? null : Convert.ToString(rdr["CM_CLASSNAME"]),
                            ClassId = rdr["SD_ClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_ClassId"]),
                            SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? null : Convert.ToString(rdr["SD_StudentId"]),
                            SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_StudentName"]),
                            SD_FatherName = rdr["SD_FatherName"] == DBNull.Value ? null : Convert.ToString(rdr["SD_FatherName"]),
                            SD_RegNo = rdr["SD_RegistrationNo"] == DBNull.Value ? null : Convert.ToString(rdr["SD_RegistrationNo"]),
                            SD_AppliactionDate = rdr["SD_AppliactionDate"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["SD_AppliactionDate"]),
                            SD_DOB = rdr["SD_DOB"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(rdr["SD_DOB"]),
                            SD_StudentCategoryId = rdr["SD_StudentCategoryId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_StudentCategoryId"]),
                            CAT_STUDENTCATEGORY = rdr["CAT_STUDENTCATEGORY"] == DBNull.Value ? null : Convert.ToString(rdr["CAT_STUDENTCATEGORY"]),
                        });
                    }
                    rdr.Close();
                }
                rdr.Dispose();
                return dbResults;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<studentFeesDetails> getStudentSessionFeesDetails(FeesSearchBO SearchBO)
        {
            try
            {
                List<studentFeesDetails> dbResults = new List<studentFeesDetails>();
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "SessionFees"));
                arrParams.Add(new SqlParameter("@SchoolId", SearchBO.SchoolId));
                arrParams.Add(new SqlParameter("@SessionId", SearchBO.SessionId));
                if (SearchBO.FormNo != null) arrParams.Add(new SqlParameter("@FormNo", SearchBO.FormNo));
                if (SearchBO.ClassId > 0) arrParams.Add(new SqlParameter("@ClassId", SearchBO.ClassId));
                if (SearchBO.StudentId != null) arrParams.Add(new SqlParameter("@StudentID", SearchBO.StudentId));
                if (SearchBO.AdmissionNo != null) arrParams.Add(new SqlParameter("@AdmissionNo", SearchBO.AdmissionNo));
                if (SearchBO.RegNo != null) arrParams.Add(new SqlParameter("@StuRegNo", SearchBO.RegNo));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);

                SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        dbResults.Add(new studentFeesDetails
                        {
                            StudentId = rdr["STUDENTID"] == DBNull.Value ? null : Convert.ToString(rdr["STUDENTID"]),
                            FeesName = rdr["FEM_FEESNAME"] == DBNull.Value ? null : Convert.ToString(rdr["FEM_FEESNAME"]),
                            FeesId = rdr["FEM_FEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["FEM_FEESID"]),
                            NoOfInstallment = rdr["CF_INSTALLMENTNO"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_INSTALLMENTNO"]),
                            FeesAmount = rdr["CF_FEESAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CF_FEESAMOUNT"]),
                            NoOfFins = rdr["CF_NOOFINS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_NOOFINS"]),
                            InstallmentNo = rdr["CF_INSTALLMENTNO"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_INSTALLMENTNO"]),
                            DueDate = rdr["CF_DUEDATE"] == DBNull.Value ? null : Convert.ToString(rdr["CF_DUEDATE"]),
                            InsAmount = rdr["CF_INSAMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["CF_INSAMOUNT"]),
                            PaymentAmount = rdr["PmntAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PmntAmnt"]),
                            AdustAmnt = rdr["AdustAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["AdustAmnt"]),
                            DueAmt = rdr["DueAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["DueAmt"]),
                            ClassId = rdr["CF_CLASSID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_CLASSID"]),
                            ClassFeesId = rdr["CF_CLASSFEESID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["CF_CLASSFEESID"]),
                            IsPaid = true,
                            IsDisc = false,
                            IsPaidChecked = "true",
                            IsDiscChecked = "false",
                            Payable = rdr["PmntAmnt"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["PmntAmnt"])
                        });
                    }
                    rdr.Close();
                }
                rdr.Dispose();
                return dbResults;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #endregion SessionFees
        #region Menu
        public Int64 InsertAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId)
        {

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            Int64 ID = Convert.ToInt64(OutPutId.Value);
            return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
        }
        public List<T> GetAnyList<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new()
        //public List<TEntity> GetAnyList(TEntity TEntity, List<SqlParameter> arrParams, string SP_Name) 
        {
            DataSet ds = new DataSet();
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, SP_Name, arrParams.ToArray());
            return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        public static DataSet GetGlobalMasterSet(GlobalDataList global)
        {
            DataSet dt = new DataSet();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", global.TransactionType));
            //arrParams.Add(new SqlParameter("@FDate", global.ParamFromDate));
            //arrParams.Add(new SqlParameter("@TDate", global.ParamToDate));

            if (global.Param != null)
            {

                arrParams.Add(new SqlParameter("@" + global.Param, global.paramString));
            }

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            //SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            dt = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, global.StoreProcedure, arrParams.ToArray());

            return dt;
        }

        public T GetGlobalMaster<T>(GlobalDataList global) where T : class, new()
        {
            DataSet ds = GetGlobalMasterSet(global);
            return Utility.DataTableToList<T>(ds.Tables[0]).FirstOrDefault();
        }

        public List<T> GetGlobalMasterList<T>(GlobalDataList global) where T : class, new()
        {
            List<T> List = new List<T>();
            DataSet ds = GetGlobalMasterSet(global);
            if (ds.Tables[0].Rows.Count > 0)
            {
                List = Utility.DataTableToList<T>(ds.Tables[0]);
            }
            return List;
            //return Utility.DataTableToList<T>(ds.Tables[0]);
        }
        #endregion
        #region others
        public void SaveSmsTracker(List<SMSBO> smsBOs)
        {
            foreach (var item in smsBOs)
            {
                try
                {
                    List<SqlParameter> arrParams = new List<SqlParameter>();
                    arrParams.Add(new SqlParameter("@TransType", "Save"));
                    arrParams.Add(new SqlParameter("@trackingCode", item.trackingCode));
                    arrParams.Add(new SqlParameter("@mobileNo", item.mobileNo));
                    arrParams.Add(new SqlParameter("@trackingNo", item.trackingNo));
                    arrParams.Add(new SqlParameter("@msg", item.msg));
                    arrParams.Add(new SqlParameter("@remarks", item.remarks));
                    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_smsTracker", arrParams.ToArray());
                }
                catch (SqlException ex) { continue; }
            }
        }
        //public DashBoardBO GetDashBoardData(DashBoardBO dash)
        //{
        //    DataSet ds = new DataSet();
        //    DashBoardBO dbDashBoard = new DashBoardBO();
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    arrParams.Add(new SqlParameter("@SchoolId", dash.dash_SchoolId));
        //    arrParams.Add(new SqlParameter("@SessionId", dash.dash_SessionId));

        //    ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "DASHBOARD_SP", arrParams.ToArray());
        //    if (ds != null && ds.Tables.Count > 1)
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            var rdr = ds.Tables[0].Rows[0];
        //            dbDashBoard.TOTALSTUDENTS = Convert.ToInt32(rdr["TOTALSTUDENTS"]);
        //            dbDashBoard.TOTALMALE = Convert.ToInt32(rdr["TOTALMALE"]);
        //            dbDashBoard.TOTALFEMALE = Convert.ToInt32(rdr["TOTALFEMALE"]);
        //            dbDashBoard.TOTALPAYMENTS = Convert.ToInt32(rdr["TOTALPAYMENTS"]);
        //            dbDashBoard.Notice = Convert.ToString(rdr["Notice"]);
        //            dbDashBoard.TotalTC = Convert.ToInt32(rdr["TotalTC"]);
        //            dbDashBoard.TotalDropOut = Convert.ToInt32(rdr["TotalDropOut"]);
        //        }
        //        if (ds.Tables[1].Rows.Count > 0)
        //        {
        //            foreach (DataRow rdr in ds.Tables[1].Rows)
        //            {
        //                dbDashBoard.DashboardList.Add(new clsBarChart
        //                {
        //                    CM_CLASSNAME = rdr["CM_CLASSNAME"] == DBNull.Value ? null : Convert.ToString(rdr["CM_CLASSNAME"]),
        //                    NOOFSTUDENTS = rdr["NOOFSTUDENTS"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["NOOFSTUDENTS"]),
        //                });
        //            }
        //        }
        //    }
        //    return dbDashBoard;
        //}
        public DashBoardBO GetDashBoardData(DashBoardBO dash)
        {
            DataSet ds = new DataSet();
            DashBoardBO dbDashBoard = new DashBoardBO();

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SchoolId", dash.dash_SchoolId));
            arrParams.Add(new SqlParameter("@SessionId", dash.dash_SessionId));

            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "DASHBOARD_SP", arrParams.ToArray());

            if (ds != null && ds.Tables.Count > 0)
            {
                // ---------- TABLE 0 : Dashboard Numbers ----------
                if (ds.Tables[0].Rows.Count > 0)
                {
                    var rdr = ds.Tables[0].Rows[0];
                    dbDashBoard.TOTALSTUDENTS = Convert.ToInt32(rdr["TOTALSTUDENTS"]);
                    dbDashBoard.TOTALMALE = Convert.ToInt32(rdr["TOTALMALE"]);
                    dbDashBoard.TOTALFEMALE = Convert.ToInt32(rdr["TOTALFEMALE"]);
                    dbDashBoard.TOTALPAYMENTS = Convert.ToInt32(rdr["TOTALPAYMENTS"]);
                    dbDashBoard.Notice = Convert.ToString(rdr["Notice"]);
                    dbDashBoard.TotalTC = Convert.ToInt32(rdr["TotalTC"]);
                    dbDashBoard.TotalDropOut = Convert.ToInt32(rdr["TotalDropOut"]);
                }

                // ---------- TABLE 1 : Class Wise Strength ----------
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow rdr in ds.Tables[1].Rows)
                    {
                        dbDashBoard.DashboardList.Add(new clsBarChart
                        {
                            CM_CLASSNAME = Convert.ToString(rdr["CM_CLASSNAME"]),
                            NOOFSTUDENTS = Convert.ToInt32(rdr["NOOFSTUDENTS"])
                        });
                    }
                }

                // ---------- TABLE 2 : Class Wise Attendance ----------
                if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                {
                    dbDashBoard.AttendanceList = new List<clsAttendanceChart>();

                    foreach (DataRow rdr in ds.Tables[2].Rows)
                    {
                        dbDashBoard.AttendanceList.Add(new clsAttendanceChart
                        {
                            CLASSNAME = Convert.ToString(rdr["CLASSNAME"]),
                            TOTALSTUDENTS = Convert.ToInt32(rdr["TOTALSTUDENTS"]),
                            PRESENTCOUNT = Convert.ToInt32(rdr["PRESENTCOUNT"])
                             
                        });
                    }
                }
            }

            return dbDashBoard;
        }

        public long DeleteClassWisefees(int ClassId, int CF_CATEGORYID, int CF_FEESID)
        {

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@ClassId", ClassId));
            arrParams.Add(new SqlParameter("@CF_CATEGORYID", CF_CATEGORYID));
            arrParams.Add(new SqlParameter("@CF_FEESID", CF_FEESID));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassWiseFeesDelete", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);

        }
        public long InsertUpdateStudentCouncilRegistration(List<clsStudentList> students, long SchoolId, long SessionId)
        {
            try
            {
                DataTable councilStudents = new DataTable();
                councilStudents.Columns.Add("StudentId", typeof(string));
                councilStudents.Columns.Add("RollNo", typeof(long));
                councilStudents.Columns.Add("RegNo", typeof(string));
                councilStudents.Columns.Add("SchoolId", typeof(long));
                councilStudents.Columns.Add("SessionId", typeof(long));
                foreach (var item in students)
                {
                    DataRow dr = councilStudents.NewRow();
                    dr["StudentId"] = item.StudentId;
                    dr["RollNo"] = item.Roll;
                    dr["RegNo"] = item.SD_RegistrationNo;
                    dr["SchoolId"] = SchoolId;
                    dr["SessionId"] = SessionId;
                    councilStudents.Rows.Add(dr);
                }
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "InsertUpdate"));
                arrParams.Add(new SqlParameter("@CouncilStudents", councilStudents));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "usp_StudentCouncilRegistration", arrParams.ToArray());
                return Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string CheckHoliday(string date, long schoolId, long sessionId)
        {
            string HolidayName = "";
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SCHOOLID", schoolId));
            arrParams.Add(new SqlParameter("@SESSIONID", sessionId));
            arrParams.Add(new SqlParameter("@DATE", date));

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_CheckHoliday", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    HolidayName = Convert.ToString(rdr["HM_HolidayName"]);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return HolidayName;
        }
        public long DeleteFeesCollection(long? FEESID, long? SchoolId, long? SessionId)
        {

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FEESCOLLECTIONID", FEESID));
            arrParams.Add(new SqlParameter("@SCHOOLID", SchoolId));
            arrParams.Add(new SqlParameter("@SESSIONID", SessionId));
            arrParams.Add(new SqlParameter("@TransactionType", "Delete"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "Usp_FeesCollection", arrParams.ToArray());

            long ReturnValue = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return Convert.ToInt64(ReturnValue);

        }

        public string DeleteHostelCollection(string HTM_TransId, long? SchoolId, long? SessionId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@HTM_TransId", HTM_TransId));
            arrParams.Add(new SqlParameter("@hostel_SchoolId", SchoolId));
            arrParams.Add(new SqlParameter("@hostel_SessionId", SessionId));
            arrParams.Add(new SqlParameter("@TransType", "Delete"));

            SqlParameter outParam = new SqlParameter("@OutPutId", SqlDbType.VarChar, 50);
            outParam.Direction = ParameterDirection.Output;
            arrParams.Add(outParam);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_HosteFeesCollection",
                arrParams.ToArray()
            );

            return outParam.Value == DBNull.Value ? null : outParam.Value.ToString();
        }


        public string DeleteTransportCollection(string TD_TransId, long? SchoolId, long? SessionId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TD_TransId", TD_TransId));
            arrParams.Add(new SqlParameter("@TR_SchoolId", SchoolId));
            arrParams.Add(new SqlParameter("@TR_SessionId", SessionId));
            arrParams.Add(new SqlParameter("@TransType", "Delete"));

            SqlParameter outParam = new SqlParameter("@OutPutId", SqlDbType.VarChar, 50);
            outParam.Direction = ParameterDirection.Output;
            arrParams.Add(outParam);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_TransportFeesCollection",
                arrParams.ToArray()
            );

            return outParam.Value == DBNull.Value ? null : outParam.Value.ToString();
          }


       #endregion
       
    }
}