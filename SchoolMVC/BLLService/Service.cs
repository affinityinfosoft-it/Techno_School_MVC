using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SchoolMVC.Models;
using SchoolMVC.Repositories;
using System.Data.SqlClient;
using BussinessObject.FeesCollection;
using System.Data;
using BussinessObject;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.FacultyPortal.Models.Response;
using SchoolMVC.Areas.FacultyPortal.Models.Request;
using System.Configuration;
using SchoolMVC.Areas.Notification.Models.Response;
using SchoolMVC.Areas.Notification.Models.Request;

namespace SchoolMVC.BLLService
{
    public class Service
    {
        IRepository common;
        ILoginRepository login;
        IMasterRepository master;
        IStudentRepository student;
        IFacultyRepository faculty;

        public Service()
        {
            this.common = new CommonRepository();
            this.login = new LoginRpository();
            this.master = new MasterRepository();
            this.student = new StudentRepository();
            this.faculty = new FacultyRepository();
        }

        #region GetGlobalSelect
        public List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {
            return common.GetGlobalSelect<T>(MainTableName, MainFieldName, PId);
        }
        #endregion
        #region GetGlobalSelectOne
        public T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new()
        {

            return common.GetGlobalSelectOne<T>(MainTableName, MainFieldName, PId);
        }
        #endregion
        #region GlobalDeleteWithIsDelete
        public long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {
            return common.GlobalDelete(MainTableName, MainFieldName, PId, TransTableName, TransFieldName);
        }
        #endregion
        #region GetLogin
        public UserMaster_UM GetLogin(UserMaster_UM user)
        {
            return login.GetLogin(user);
        }
        #endregion
        #region AddEditSession
        public long AddEditSession(SessionMasters_SM Ses)
        {
            return master.AddEditSession(Ses);
        }
        #endregion
        #region InsertUpdateState
        public long InsertUpdateState(StateMaster_STM states)
        {
            return master.InsertUpdateState(states);
        }
        #endregion
        #region InsertUpdateDistrict
        public long InsertUpdateDistrict(DistrictMasters_DM district)
        {
            return master.InsertUpdateDistrict(district);
        }
        #endregion
        #region DuplicateDataCheck
        public string DuplicateDataCheck(string TableName, string DataField, string DataFieldValue, string OptionalField, long? OptionalFieldValue)
        {
            return common.DuplicateDataCheck(TableName, DataField, DataFieldValue, OptionalField, OptionalFieldValue);
        }
        #endregion
        #region InsertUpdateTerm
        public long InsertUpdateTerm(TermMaster_TM term)
        {
            return master.InsertUpdateTerm(term);
        }
        #endregion
        #region InsertUpdateClassType
        public long InsertUpdateClassType(ClassTypeMaster_CTM Ct)
        {
            return master.InsertUpdateClassType(Ct);
        }
        #endregion
        #region InsertUpdateClass
        public long InsertUpdateClass(ClassMaster_CM Class)
        {
            return master.InsertUpdateClass(Class);
        }
        #endregion
        #region InsertUpdateSection
        public long InsertUpdateSection(SectionMaster_SECM sec)
        {
            return master.InsertUpdateSection(sec);
        }
        #endregion
        #region InsertUpdateBoard
        public long InsertUpdateBoard(BoardMasters_BM board)
        {
            return master.InsertUpdateBoard(board);
        }
        #endregion
        #region InsertUpdateCaste
        public long InsertUpdateCaste(CasteMaster_CSM caste)
        {
            return master.InsertUpdateCaste(caste);
        }
        #endregion
        #region InsertUpdateVernacular
        public long InsertUpdateVernacular(VernacularMaster_VM vm)
        {
            return master.InsertUpdateVernacular(vm);
        }
        #endregion
        #region InsertUpdateHouse
        public long InsertUpdateHouse(HouseMasters_HM house)
        {
            return master.InsertUpdateHouse(house);
        }
        #endregion
        #region InsertUpdateStream
        public long InsertUpdateStream(StreamMasters_STRM stream)
        {
            return master.InsertUpdateStream(stream);
        }
        #endregion
        #region InsertUpdateBloodGroup
        public long InsertUpdateBloodGroup(BloodGroupMasters_BGM bloodgrp)
        {
            return master.InsertUpdateBloodGroup(bloodgrp);
        }
        #endregion
        #region InsertUpdateGrade
        public long InsertUpdateGrade(GradeMaster_GM grade)
        {
            return master.InsertUpdateGrade(grade);
        }
        #endregion

        #region InsertUpdateForm
        public long InsertUpdateForm(FormMaster_FM form)
        {
            return master.InsertUpdateForm(form);
        }
        #endregion

        #region InsertUpdateTeachingAid
        public long InsertUpdateTeachingAid(TeachingAid_TA teachinga)
        {
            return master.InsertUpdateTeachingAid(teachinga);
        }
        #endregion



        #region InsertUpdateHoliday
        public long InsertUpdateHoliday(HolidayMaster_HM holidays)
        {
            return master.InsertUpdateHoliday(holidays);
        }
        #endregion
        #region InsertUpdateNotice
        public long InsertUpdateNotice(NoticeMasters_NM notice)
        {
            return master.InsertUpdateNotice(notice);
        }



        public List<StudentSearchModel> SearchStudents(string searchTerm, long? schoolId, long? sessionId)
        {
            List<StudentSearchModel> students = new List<StudentSearchModel>();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_SearchStudentList", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", string.IsNullOrEmpty(searchTerm) ? (object)DBNull.Value : searchTerm);
                    cmd.Parameters.AddWithValue("@SchoolId", schoolId ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@SessionId", sessionId ?? (object)DBNull.Value);

                    con.Open();
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            StudentSearchModel student = new StudentSearchModel
                            {
                                SD_StudentId = rdr["SD_StudentId"] != DBNull.Value ? rdr["SD_StudentId"].ToString() : "",
                                SD_AdmissionNo = rdr["SD_AppliactionNo"] != DBNull.Value ? rdr["SD_AppliactionNo"].ToString() : "",
                                SD_StudentName = rdr["SD_StudentName"] != DBNull.Value ? rdr["SD_StudentName"].ToString() : "",
                                ClassName = rdr["ClassName"] != DBNull.Value ? rdr["ClassName"].ToString() : "",
                                SectionName = rdr["SectionName"] != DBNull.Value ? rdr["SectionName"].ToString() : ""
                            };

                            students.Add(student);
                        }
                    }
                }
            }

            return students;
        }





        #endregion
        public long InsertUpdateLessonPlan(LessonPlan_LP lessonpaln)
        {
            return master.InsertUpdateLessonPlan(lessonpaln);
        }
        #region InsertUpdatePeriod
        public long InsertUpdatePeriod(PeriodMaster_PM period)
        {
            return master.InsertUpdatePeriod(period);
        }
        //public List<PeriodMaster_PM> PeriodList(PeriodMaster_PM period)
        //{
        //    return master.PeriodList(period);
        //}
        #endregion
        #region InsertUpdateClassTypeboard
        public long InsertUpdateClassTypeboard(ClassTypeBoard_CTB classboard)
        {
            return master.InsertUpdateClassTypeboard(classboard);
        }
        #endregion
        #region InsertUpdateRoute
        public long InsertUpdateRoute(RouteMastes_RT route)
        {
            return master.InsertUpdateRoute(route);
        }
        #endregion
        #region InsertUpdateRoutewiseDrop
        public long InsertUpdateRoutewiseDrop(RoutewiseDropMaster_RDM obj)
        {
            return master.InsertUpdateRoutewiseDrop(obj);
        }
        #endregion
        #region InsertUpdateSubjectGroup
        public long InsertUpdateSubjectGroup(SubjectGroupMaster_SGM obj)
        {
            return master.InsertUpdateSubjectGroup(obj);
        }
        #endregion
        #region InsertUpdateSubject
        public long InsertUpdateSubject(SubjectMaster_SBM obj)
        {
            return master.InsertUpdateSubject(obj);
        }
        #endregion
        #region InsertUpdateClassWiseSubject
        public long InsertUpdateClassWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            return master.InsertUpdateClassWiseSubject(obj);
        }
        #endregion
        #region InsertUpdateAssignmentMaster
        public long InsertUpdateAssignmentMaster(AssignmentMaster_ASM obj)
        {
            return master.InsertUpdateAssignmentMaster(obj);
        }

        public long UpdateAssignmentMarks(AssignmentMarkUpdate assignmentMarkUpdate)
        {
            return master.UpdateAssignmentMarks(assignmentMarkUpdate);
        }
        #endregion
        #region InsertUpdateStudentDiary
        public long InsertUpdateStudentDiary(StudentDiary_STD obj)
        {
            return master.InsertUpdateStudentDiary(obj);
        }
        #endregion

        #region InsertUpdateStudentEarlyLeave
        public long InsertUpdateStudentEarlyLeave(StudentEarlyLeave_ERL obj)
        {
            return master.InsertUpdateStudentEarlyLeave(obj);
        }
        #endregion

        #region InsertUpdateExamGroup
        public long InsertUpdateExamGroup(ExamGroupMaster_EGM obj, List<long> classIds)
        {
            return master.InsertUpdateExamGroup(obj, classIds);
        }
        #endregion
        #region InsertUpdateClasswiseLiveclass
        public long InsertUpdateClasswiseLiveclass(ClassWiseLiveclass_CWLS obj)
        {
            return master.InsertUpdateClasswiseLiveclass(obj);
        }
        #endregion

        #region InsertUpdateUserMaster
        public long InsertUpdateUserMaster(UserMaster_UM obj)
        {
            return master.InsertUpdateUserMaster(obj);
        }

        public List<UserMaster_UM> UserMasterList(UserMaster_UM obj)
        {
            return master.UserMasterList(obj);
        }

        #endregion


        #region InsertUpdateFaculty
        public long InsertUpdateFaculty(FacultyProfileMasters_FPM obj)
        {
            return master.InsertUpdateFaculty(obj);
        }
        #endregion
        #region InsertUpdateClasswiseFaculty
        public long InsertUpdateClasswiseFaculty(ClassWiseFacultyMasters_CWF obj)
        {
            return master.InsertUpdateClasswiseFaculty(obj);
        }
        #endregion
        #region InsertUpdateRoutine
        public long InsertUpdateRoutine(ClassWiseTeacherRoutine_CWTR obj)
        {
            return master.InsertUpdateRoutine(obj);
        }
        #endregion
        #region InsertUpdateEnquery
        //public string InsertUpdateEnquery(StudentEnquery_ENQ obj)
        //{
        //    return student.InsertUpdateEnquery(obj);
        //}
        public Tuple<string, string> InsertUpdateEnquery(StudentEnquery_ENQ obj)
        {
            return student.InsertUpdateEnquery(obj);
        }
       
        #endregion
        #region InsertUpdateMiscellaneousHead
        public long InsertUpdateMiscellaneousHead(MiscellaneousHeadMaster_MISC misc)
        {
            return master.InsertUpdateMiscellaneousHead(misc);
        }
        #endregion
        public List<StudentModel> GetStudentsByClass(int schoolId, int sessionId, string classId, string sectionId = null, string studentId = null, int cardType = 0)
        {
            return master.GetStudentsByClass(schoolId, sessionId, classId, sectionId, studentId, cardType).ToList();
        }
        public StudentManual GetStudentManual(string studentId)
        {
            return master.GetStudentManual(studentId);
        }

        #region
        public string InsertUpdateAdmissionDetails(StudetDetails_SD obj)
        {
            return student.InsertUpdateAdmissionDetails(obj);
        }
        #endregion
        #region MarkSheet
        public List<DDLList> GetClassWiseDetails(string TransactionType, long classId, long schoolId, long? sessionId, long? SubGrId)
        {
            return common.GetClassWiseDetails(TransactionType, classId, schoolId, sessionId, SubGrId);
        }
        public List<clsStudentList> getStudentsList(long classId, long sectionId, long subjectId, long HS, long schoolId, long sessionId)
        {
            return common.getStudentsList(classId, sectionId, subjectId, HS, schoolId, sessionId);
        }
        public GradeMaster_GM GradeCheck(decimal? Marks, long schoolId, long sessionId)
        {
            return common.GradeCheck(Marks, schoolId, sessionId);
        }
        public long InsertUpdateMarks(clsStudentMarksEntry marks)
        {
            return common.InsertUpdateMarks(marks);
        }
        public long InsertUpdateStudentCouncilRegistration(List<clsStudentList> students, long SchoolId, long SessionId)
        {
            return common.InsertUpdateStudentCouncilRegistration(students, SchoolId, SessionId);
        }
        public List<clsStudentMarksEntry> GetStudentMarksList(clsStudentMarksEntry marks)
        {
            return common.GetStudentMarksList(marks);
        }
        public clsStudentMarksEntry GetStudentMarks(clsStudentMarksEntry marks)
        {
            return common.GetStudentMarks(marks);
        }
        public List<clsStudentList> studentsForMarkSheet(clsStudentList query)
        {
            return common.studentsForMarkSheet(query);
        }
        public List<clsStudentList> studentsForCouncilRegistration(clsStudentList query)
        {
            return common.studentsForCouncilRegistration(query);
        }
        #endregion
        #region AdmissionFees
        public List<StudentList> SearchNonAdmittedStudent(FeesSearchBO SearchBO)
        {
            return common.SearchNonAdmittedStudent(SearchBO);
        }
        public List<studentFeesDetails> getStudentFeesDetails(FeesSearchBO SearchBO)
        {
            return common.getStudentFeesDetails(SearchBO);
        }
        public List<FeesCollectionBO> GetFeeSCollectionList(FeesCollectionBO obj, string FeesColType)
        {
            return common.GetFeeSCollectionList(obj, FeesColType);
        }
        #endregion AdmissionFees
        #region FeesRealated
        public decimal InsertUpdateFeesCollection(FeesCollectionBO collection, string type)
        {
            return common.InsertUpdateFeesCollection(collection, type);
        }
        public FormBO getAsyncFeesUpdate(long Id)
        {
            return common.getAsyncFeesUpdate(Id);
        }


        public LateFeesSlap FindLateFeesAmount(long schoolId, long sessionId, DateTime dueDate, DateTime collectionDate)  //int fineTypeId,
        {
            return common.FindLateFeesAmount(schoolId, sessionId, dueDate, collectionDate); //fineTypeId,
        }





        public List<AdditionalFeesBO> GetAdditionalFeesIds()
        {
            return common.GetAdditionalFeesIds();
        }
        #endregion
        #region SessionFees
        public List<StudentList> FetchAdmittedStudent(FeesSearchBO SearchBO)
        {
            return common.FetchAdmittedStudent(SearchBO);
        }
        public List<studentFeesDetails> getStudentSessionFeesDetails(FeesSearchBO SearchBO)
        {
            return common.getStudentSessionFeesDetails(SearchBO);
        }
        #endregion SessionFees
        #region InsertUpdateCategory
        public long InsertUpdateCategory(STUDENTCATEGORY_CAT obj)
        {
            return master.InsertUpdateCategory(obj);
        }
        #endregion
        #region ClasswiseFees
        public long InsertUpdateClasswiseFees(ClassWisefees_CF obj)
        {
            return master.InsertUpdateClasswiseFees(obj);
        }
        public long InsertUpdateClasswiseFeesForward(ClassWisefees_CF obj)
        {
            return master.InsertUpdateClasswiseFeesForward(obj);
        }


        public List<ClassWisefees_CF> GetClassWiseFeesList(Int64 SchoolId, Int64 SessionId, int? ClassId)
        {
            return master.GetClassWiseFeesList(SchoolId, SessionId, ClassId);
        }
        #endregion
        #region GetAdmissionStudentList
        public List<StudetDetails_SD> GetAdmissionStudentList(StudetDetails_SD obj)
        {
            return student.GetAdmissionStudentList(obj);
        }
        #endregion
        #region GetEnqueryList
        public List<StudentEnquery_ENQ> GetEnqueryList(StudentEnquery_ENQ obj)
        {
            return student.GetEnqueryList(obj);
        }
        #endregion
        #region InsertUpdateFees
        public long InsertUpdateFees(FeesMaster_FEM feeObj)
        {
            return master.InsertUpdateFees(feeObj);
        }
        public long InsertUpdateLateFeesSlap(LateFeesSlap feeObj)
        {
            return master.InsertUpdateLateFeesSlap(feeObj);
        }

        #endregion
        #region AddEditSchool
        public long AddEditSchool(SchoolMasters_SCM obj)
        {
            return master.AddEditSchool(obj);
        }
        #endregion
        #region MenuRelated Service


        #region GetGlobalMasterList
        public List<T> GetGlobalMasterList<T>(GlobalDataList global) where T : class, new()
        {
            return common.GetGlobalMasterList<T>(global);
        }
        #endregion
        #region GetGlobalMaster
        public T GetGlobalMaster<T>(GlobalDataList global) where T : class, new()
        {
            return common.GetGlobalMaster<T>(global);
        }
        #endregion
        #region InsertUpdateRoleMaster
        //public decimal InsertUpdateAnyMaster<T>(T model, List<SqlParameter> arrParams, string spName, SqlParameter OutPutId) where T : class, new()
        public Int64 InsertUpdateRoleMaster(RoleMasterModel TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.RoleId == 0)
            {
                arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
            }
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            arrParams.Add(new SqlParameter("@RoleName", TEntity.RoleName));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion

        #region InsertUpdateRoleMaster
        public Int64 InsertUpdateMenuMaster(MenuMasterModel TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            if (TEntity.MenuId == 0)
            {
                arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            }
            else
            {
                arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
            }
            arrParams.Add(new SqlParameter("@MenuId", TEntity.MenuId));
            arrParams.Add(new SqlParameter("@MenuName", TEntity.MenuName));

            arrParams.Add(new SqlParameter("@ParentMenuId", TEntity.ParentMenuId));
            arrParams.Add(new SqlParameter("@ModuleId", TEntity.ModuleId));
            arrParams.Add(new SqlParameter("@ActionUrl", TEntity.ActionUrl));
            arrParams.Add(new SqlParameter("@DisplayPosition", TEntity.DisplayPosition));
            arrParams.Add(new SqlParameter("@CreatedBy", TEntity.CreatedBy));


            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion

        #region GetAccessRights
        public List<T> GetAccessRightList<T>(AccessRightsVM TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            arrParams.Add(new SqlParameter("@ModuleId", TEntity.ModuleId));
            return common.GetAnyList<T>(arrParams, SP_Name);
        }

        public Int64 InsertUpdateAccessRights(AccessRightsVM TEntity, string SP_Name)
        {

            DataTable dt = new DataTable();


            dt.Columns.Add("MenuId", typeof(Int64));
            dt.Columns.Add("CanView", typeof(bool));
            dt.Columns.Add("CanAdd", typeof(bool));
            dt.Columns.Add("CanEdit", typeof(bool));
            dt.Columns.Add("CanDelete", typeof(bool));
            dt.Columns.Add("CanSubmit", typeof(bool));

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "INSERT"));

            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            arrParams.Add(new SqlParameter("@ModuleId", TEntity.ModuleId));


            foreach (var item in TEntity.lstAccessRights)
            {

                DataRow dr = dt.NewRow();
                dr["MenuId"] = item.MenuId;
                dr["CanView"] = item.CanView;
                dr["CanAdd"] = item.CanAdd;
                dr["CanEdit"] = item.CanEdit;
                dr["CanDelete"] = item.CanDelete;
                dr["CanSubmit"] = item.CanSubmit; ;

                dt.Rows.Add(dr);
            }

            arrParams.Add(new SqlParameter("@AccessRightsList", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }


        public List<T> GetMenuList<T>(MenuMasterModel TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@RoleId", TEntity.RoleId));
            return common.GetAnyList<T>(arrParams, SP_Name);
        }

        #endregion

        #endregion
        #region GetGroupWiseSubject
        public List<ClsSubGrWiseSubSetting_CSGWS> GetGroupWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            return master.GetGroupWiseSubject(obj);
        }
        #endregion
        #region  InsertUpdateStudentwiseSubject
        public long InsertUpdateStudentwiseSubject(StudentwiseSubjectSetting_SWS Subject)
        {
            return master.InsertUpdateStudentwiseSubject(Subject);
        }
        #endregion
        #region  GetStudentListForSubjectSettings
        public List<StudetDetails_SD> GetStudentListForSubjectSettings(StudetDetails_SD obj)
        {
            return master.GetStudentListForSubjectSettings(obj);
        }
        #endregion
        #region SaveSmsTracker
        public void SaveSmsTracker(List<SMSBO> smsBOs)
        {
            common.SaveSmsTracker(smsBOs);
        }
        #endregion
        #region DashBoard
        public DashBoardBO GetDashBoardData(DashBoardBO dash)
        {
            return common.GetDashBoardData(dash);
        }
        #endregion DashBoard
        #region others
        public decimal InsertUpdateSecRollSetting(Sec_Roll_Setting_SR obj)
        {
            return master.InsertUpdateSecRollSetting(obj);
        }
        public List<StudentwiseSubjectSetting_SWS> StudentWiseSubjectListForEdit(StudentwiseSubjectSetting_SWS obj)
        {
            return master.StudentWiseSubjectListForEdit(obj);
        }
        public List<StudentwiseSubjectSetting_SWS> GetIndividualStudentWiseSubjectForEdit(string Sid)
        {
            return master.GetIndividualStudentWiseSubjectForEdit(Sid);
        }
        public List<FacultyProfileMasters_FPM> LoadFacultyByClass(FacultyProfileMasters_FPM obj)
        {
            return master.LoadFacultyByClass(obj);
        }

        public long DeleteClassWisefees(int ClassId, int CF_CATEGORYID, int CF_FEESID)
        {
            return common.DeleteClassWisefees(ClassId, CF_CATEGORYID, CF_FEESID);
        }
        public List<FeesMaster_FEM> GetFeesList(bool? IsAdmission)
        {
            return master.GetFeesList(IsAdmission);
        }
        public List<StudentExamAttandanceMaster_SEA> GetStudentListForExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            return student.GetStudentListForExamAttendance(obj);
        }
        public string InsertUpdateExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            return student.InsertUpdateExamAttendance(obj);
        }

        public List<StudetDetails_SD> GetStudentListRollSecSetting(StudetDetails_SD obj)
        {
            return master.GetStudentListRollSecSetting(obj);
        }
        public List<StudentAttendenceMaster_SAM> GetStudentListForAttendance(StudentAttendenceMaster_SAM obj)
        {
            return student.GetStudentListForAttendance(obj);
        }
        public string InsertUpdateAttendance(StudentAttendenceMaster_SAM obj)
        {
            return student.InsertUpdateAttendance(obj);
        }
        public List<StudentAttendanceDetailsResponse> GetStudentAttendanceDetails(StudentAttendanceDetailsRequest request)
        {
            return student.GetStudentAttendanceDetails(request);
        }
        public string CheckHoliday(string date, long schoolId, long sessionId)
        {
            return common.CheckHoliday(date, schoolId, sessionId);
        }
        public List<StudentExamAttandanceMaster_SEA> ExamAttendanceList(StudentExamAttandanceMaster_SEA obj)
        {
            return student.ExamAttendanceList(obj);
        }
        public string DeleteExamAttendance(int? id)
        {
            return student.DeleteExamAttendance(id);
        }
        public string DeleteStudentWiseSubject(string id)
        {
            return student.DeleteStudentWiseSubject(id);
        }

        public List<StudentAttendenceMaster_SAM> AttendanceList(StudentAttendenceMaster_SAM obj)
        {
            return student.AttendanceList(obj);
        }
       

        public List<clsStudentList> studentsForPromotion(clsStudentList query)
        {
            return student.studentsForPromotion(query);
        }
        public long UpdateStudentPromotion(clsStudentList obj)
        {
            return student.UpdateStudentPromotion(obj);
        }
        public string DeleteAttendance(int? id)
        {
            return student.DeleteAttendance(id);
        }
        public string CheckDuplicity(ClassWiseTeacherRoutine_CWTR obj)
        {
            return master.CheckDuplicity(obj);
        }
        public List<ClassWiseTeacherRoutine_CWTR> GetRoutineList(Int64 SchoolId, Int64 SessionId, int? ClassId)
        {
            return master.GetRoutineList(SchoolId, SessionId, ClassId);
        }
        public decimal UpdateTransport(StudetDetails_SD data)
        {
            return master.UpdateTransport(data);
        }
        public decimal InsertStudentForHostel(HostelMaster_HM data)
        {
            return master.InsertStudentForHostel(data);
        }
        public List<StudetDetails_SD> GetStudentListForTransportSettings(StudetDetails_SD obj)
        {
            return master.GetStudentListForTransportSettings(obj);
        }
        public List<StudetDetails_SD> GetStudentListForHostelSettings(StudetDetails_SD obj)
        {
            return master.GetStudentListForHostelSettings(obj);
        }
        public List<StudetDetails_SD> TransportAvailedStudentList(StudetDetails_SD obj)
        {
            return master.TransportAvailedStudentList(obj);
        }
        public long DeleteTransportFromStudents(string StudentId)
        {
            return master.DeleteTransportFromStudents(StudentId);
        }
        public List<StudetDetails_SD> HostelAviledStudentList(StudetDetails_SD obj)
        {
            return master.HostelAviledStudentList(obj);
        }
        public long DeleteHostelSetting(string StudentId)
        {
            return master.DeleteHostelSetting(StudentId);
        }
        public List<HostelMaster_HM> GetHotelFeesCollectionList(HostelMaster_HM obj)
        {
            return student.GetHotelFeesCollectionList(obj);
        }
        public string InsertHostelFeesCollection(HostelTransactionMaster_HTM obj)
        {
            return student.InsertHostelFeesCollection(obj);
        }
        public List<TransportFeesTransaction_TR> GetTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        {
            return student.GetTransportFeesCollectionList(obj);
        }
        public string InsertTransportFeesCollection(TransportFeesTransaction_TR obj)
        {
            return student.InsertTransportFeesCollection(obj);
        }
        public List<TransportFeesTransaction_TR> LoadTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        {
            return student.LoadTransportFeesCollectionList(obj);
        }
        public List<HostelTransactionMaster_HTM> LoadHostelFeesCollectionList(HostelTransactionMaster_HTM obj)
        {
            return student.LoadHostelFeesCollectionList(obj);
        }
        public List<TransportFeesTransaction_TR> GetTransportFeesCollectionListForEdit(TransportFeesTransaction_TR obj)
        {
            return student.GetTransportFeesCollectionListForEdit(obj);
        }
        public List<StudentApplication_AP> GetApplicationList(string candidateId)
        {
            return master.GetApplicationList(candidateId);
        }

        public List<ClassWiseFacultyMasters_CWF> GetClassWiseFacultyList(Int64? SchoolId, Int64? SessionId, Int64? ClassId, Int64? SubId, Int64? FacId)
        {
            return master.GetClassWiseFacultyList(SchoolId, SessionId, ClassId, SubId, FacId);
        }
        public List<HostelTransactionMaster_HTM> GetHostelFeesCollectionListForEdit(HostelTransactionMaster_HTM obj)
        {
            return student.GetHostelFeesCollectionListForEdit(obj);
        }
        public long DeleteFeesCollection(long? FEESID, long? SchoolId, long? SessionId)
        {
            return common.DeleteFeesCollection(FEESID, SchoolId, SessionId);
        }
        #region InsertUpdateDesignation
        public long InsertUpdateDesignation(DesignationMaster_DM designation)
        {
            return master.InsertUpdateDesignation(designation);
        }

        public string DeleteHostelCollection(string HTM_TransId, long? SchoolId, long? SessionId)
        {
            return common.DeleteHostelCollection(HTM_TransId, SchoolId, SessionId);
        }

        public string DeleteTransportCollection(string TD_TransId, long? SchoolId, long? SessionId)
        {
            return common.DeleteTransportCollection(TD_TransId, SchoolId, SessionId);
        }

        #endregion
        #region InsertUpdateBank
        public long InsertUpdateBank(BankMaster_BM bank)
        {
            return master.InsertUpdateBank(bank);
        }
        #endregion
        #region InsertUpdateLeaveType
        public long InsertUpdateLeaveType(LeaveType_LT leavetype)
        {
            return master.InsertUpdateLeaveType(leavetype);
        }
        #endregion
        #region InsertUpdateTransportType
        public long InsertUpdateTransportType(TransportType_TT transport)
        {
            return master.InsertUpdateTransportType(transport);
        }
        #endregion
        #region InsertUpdateHostelRoomNo
        public long InsertUpdateHostelRoom(HostelRoomMaster_HR bank)
        {
            return master.InsertUpdateHostelRoom(bank);
        }
        #endregion
        #endregion
        #region TC Master
        public List<TCMaster> GetAllStudent(TCMaster obj)
        {
            return student.GetAllStudent(obj);
        }
        public long CancelStudents(TCMaster TCMaster)
        {
            return student.CancelStudents(TCMaster);
        }
        public List<TCMaster> GetAllTCStudent(TCMaster obj)
        {
            return student.GetAllTCStudent(obj);
        }
        ///print tc certificate
        public TCCertificate GetTCCertificate(string studentId)
        {
            return student.GetTCCertificate(studentId);
        }
        public string DeleteTC(string SD_StudentId)
        {
            return student.DeleteTC(SD_StudentId);
        }

        #endregion
        #region InsertUpdateMenuMaster
        //public Int64 Menu_Master_InsUp(_MenuMaster TEntity, string SP_Name)
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    //SqlParameter OutPutId=new SqlParameter();
        //    if (TEntity.MenuId == 0)
        //    {
        //        arrParams.Add(new SqlParameter("@TransType", "INSERT"));
        //    }
        //    else
        //    {
        //        arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
        //        arrParams.Add(new SqlParameter("@MenuId", TEntity.MenuId));
        //    }
        //    arrParams.Add(new SqlParameter("@MenuName", TEntity.MenuName));
        //    arrParams.Add(new SqlParameter("@ActionUrl", TEntity.ActionUrl));
        //    arrParams.Add(new SqlParameter("@ModuleId", TEntity.ModuleId));
        //    arrParams.Add(new SqlParameter("@IsActive", TEntity.IsActive));
        //    arrParams.Add(new SqlParameter("@IsBaseMenu", TEntity.IsBaseMenu));
        //    arrParams.Add(new SqlParameter("@IsSubMenu", TEntity.IsSubMenu));
        //    arrParams.Add(new SqlParameter("@IsMenu", TEntity.IsMenu));
        //    arrParams.Add(new SqlParameter("@DisplayPosition", TEntity.DisplayPosition));
        //    arrParams.Add(new SqlParameter("@CreatedBy", TEntity.CreatedBy));
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //    OutPutId.Direction = ParameterDirection.Output;
        //    arrParams.Add(OutPutId);
        //    return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        //}
        //public List<_MenuMaster> ModuleddlList(_MenuMaster obj)
        //{
        //    return master.ModuleddlList(obj);
        //}
        #endregion
        #region Discontinue Student List
        public List<StudetDetails_SD> GetDisStudentList(StudetDetails_SD obj)
        {
            return student.GetDisStudentList(obj);
        }
        public Int64 DisContudentsINS(StudetDetails_SD TEntity, string SP_Name)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("StudentId", typeof(string));
            dt.Columns.Add("IsActive", typeof(int));
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", TEntity.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", TEntity.SD_SessionId));
            foreach (var item in TEntity.StudentDetailsList)
            {
                DataRow dr = dt.NewRow();
                dr["StudentId"] = item.SD_StudentId;
                dr["IsActive"] = 0;
                dt.Rows.Add(dr);
            }
            arrParams.Add(new SqlParameter("@StudentDetailsList", dt));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }
        #endregion
        #region Class Change
        public List<T> GetClassWiseStudent<T>(StudetDetails_SD TEntity, string SP_Name) where T : class, new()
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_SchoolId", TEntity.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", TEntity.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", TEntity.SD_CurrentClassId));
            arrParams.Add(new SqlParameter("@TransType", "select"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            return common.GetAnyList<T>(arrParams, SP_Name);
        }
        #endregion
        #region
        public Int64 StudentClassChangeUpdate(StudetDetails_SD TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            arrParams.Add(new SqlParameter("@SD_StudentId", TEntity.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_SchoolId", TEntity.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", TEntity.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", TEntity.SD_CurrentClassId));
            arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }

        #endregion
        #region
        public Int64 DiscontinueStudentListUpdate(StudetDetails_SD TEntity, string SP_Name)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            //SqlParameter OutPutId=new SqlParameter();
            arrParams.Add(new SqlParameter("@SD_StudentId", TEntity.SD_StudentId));
            arrParams.Add(new SqlParameter("@SD_SchoolId", TEntity.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_SessionId", TEntity.SD_SessionId));
            arrParams.Add(new SqlParameter("@TransType", "UPDATE"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            return common.InsertAnyMasters(arrParams, SP_Name, OutPutId);
        }

        #endregion
        internal StudetDetails_SD GetGlobalSelectOne(string tableName, string columnName, string value)
        {
            StudetDetails_SD student = null;

            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
                {
                    string query = "SELECT TOP 1 SD_StudentId, SD_StudentName FROM "
                                   + tableName + " WHERE " + columnName + " = @value";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@value", value);
                        con.Open();

                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            if (rdr.Read())
                            {
                                student = new StudetDetails_SD();
                                student.SD_StudentId = rdr["SD_StudentId"].ToString();
                                student.SD_StudentName = rdr["SD_StudentName"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching student details: " + ex.Message, ex);
            }

            return student;
        }

        #region StudentPortal Add by Uttaran API Integration
        public StudetDetails_SD GetStudentLogin(StudetDetails_SD obj)
        {
            return student.GetStudentLogin(obj);
        }
        public List<NoticeResponse> GetNotice(NoticeRequest obj)
        {
            return student.GetNotice(obj);
        }
        public List<RoutineResponse> GetRoutine(RoutineRequest obj)
        {
            return student.GetRoutine(obj);
        }
        public StudentMarksheetResponse GetStudentMarksheet(StudentMarksheetRequest obj)
        {
            return student.GetStudentMarksheet(obj);
        }
        public List<StudentPaidDetailsResponse> GetStudentPaidDetails(StudentPaidDetailsRequest obj)
        {
            return student.GetStudentPaidDetails(obj);
        }
        public StudentPaidReceiptResponse GetStudentPaidReceipt(StudentPaidReceiptRequest obj)
        {
            return student.GetStudentPaidReceipt(obj);
        }
        public List<ClassWiseBirthdayResponse> GetClassWiseBirthday(ClassWiseBirthdayRequest obj)
        {
            return student.GetClassWiseBirthday(obj);
        }
        public AttendenceResponse GetStudentWiseAttendence(AttendenceRequest obj)
        {
            return student.GetStudentWiseAttendence(obj);
        }
        #region InsertUpdateGetSyllabus
        public long InsertUpdateSyllabus(ClassWiseSyllabusMasters_CWSM syllabus)
        {
            return master.InsertUpdateSyllabus(syllabus);
        }

        public List<SyllabusResponse> GetSyllabus(SyllabusRequest obj)
        {
            return student.GetSyllabus(obj);
        }
       
        #endregion
        #region GetStudentLiveClass
        //public long InsertUpdateSyllabus(ClassWiseSyllabusMasters_CWSM syllabus)
        //{
        //    return master.InsertUpdateSyllabus(syllabus);
        //}

        public List<StudentLiveClassResponse> GetStudentLiveClass(StudentLiveClassRequest obj)
        {
            return student.GetStudentLiveClass(obj);
        }
        public List<StudentWiseFacultyResponse> GetStudentWiseFacultyList(StudentWiseFacultyRequest obj)
        {
            return student.GetStudentWiseFacultyList(obj);
        }
        #endregion
        public List<StudentFeeSummaryResponse> GetStudentFeeSummary(StudentFeeSummaryRequest obj)
        {
            return student.GetStudentFeeSummary(obj);
        }
        public List<StudentDueFeeResponse> GetStudentDueFee(StudentDueFeeRequest obj)
        {
            return student.GetStudentDueFee(obj);
        }
        #endregion

        #region Student Assignment
        public List<StudentAssignmentsListResponse> GetStudentAssignmentList(StudentAssignmentsListRequest obj)
        {
            return student.GetStudentAssignmentList(obj);
        }
        public string DeleteStudentAssignment(int? id)
        {
            return student.DeleteStudentAssignment(id);
        }
        public long InsertUpdateStudentAssignmentMaster(StudentAssignmentMaster obj)
        {
            return student.InsertUpdateStudentAssignmentMaster(obj);
        }
        public StudentAssignmentsListResponse GetStudentAssignmentById(StudentAssignmentDtlsRequest studentAssignmentDtlsRequest)
        {
            return student.GetStudentAssignmentById(studentAssignmentDtlsRequest);
        }


        #endregion

        #region FacultyPortal
        public FacultyProfileMasters_FPM GetFacultyLogin(FacultyProfileMasters_FPM obj)
        {
            return faculty.GetFacultyLogin(obj);
        }

        public List<AttendanceListResponse> GetAttendanceList(AttendanceListRequest obj)
        {
            return faculty.GetAttendanceList(obj);
        }

        public List<ListForAttendanceResponse> GetListForAttendance(ListForAttendanceRequest obj)
        {
            return faculty.GetListForAttendance(obj);
        }

        public List<AssignmentsListResponse> GetAssignmentList(AssignmentsListRequest obj)
        {
            return faculty.GetAssignmentList(obj);
        }
        public List<UploadedStudentListResponse> GetUploadedStudentList(AssignmentsListRequest obj)
        {
            return faculty.GetUploadedStudentList(obj);
        }
        public string DeleteAssignment(int? id)
        {
            return faculty.DeleteAssignment(id);
        }

        public string DeleteClasswiseLiveclass(int? id)
        {
            return faculty.DeleteClasswiseLiveclass(id);
        }

        #endregion

        public List<LessonPlanList_VM> GetLessonPlanList(long? facultyId, long? subjectId,
                                                 DateTime? fromDate, DateTime? toDate,
                                                 long? schoolId, long? sessionId)
        {
            return master.GetLessonPlanList(facultyId, subjectId, fromDate, toDate, schoolId, sessionId);
        }
        public long DeleteExamGroup(long id)
        {
            return master.DeleteExamGroup(id);
        }

        //public List<StudentApplication_AP> GetApprovalList(Int64 SchoolId)
        //{
        //    return master.GetApprovalList(SchoolId);
        //}

        //public string InsertUpdateMiscCollection(MiscellaneousTransactionMaster obj)
        //{
        //    return student.InsertUpdateMiscCollection(obj);
        public DbResult InsertUpdateMiscCollection(MiscellaneousTransactionMaster obj)
        {
        return student.InsertUpdateMiscCollection(obj);
        }
        public void DeleteMiscCollection(string id)
        {
            student.DeleteMiscCollection(id);
        }
        //}
        public List<MiscellaneousTransactionMaster> MiscFeesCollectionList(MiscellaneousTransactionMaster obj)
        {
            return student.MiscFeesCollectionList(obj);
        }

        #region DropOut
        public List<DropOut_DOP> GetAllDropOutStudent(DropOut_DOP obj)
        {
            return student.GetAllDropOutStudent(obj);
        }
        public List<DropOut_DOP> GetAllDStudent(DropOut_DOP obj)
        {
            return student.GetAllDStudent(obj);
        }


        public long CancelDStudents(DropOut_DOP DropOut_DOP)
        {
            return student.CancelDStudents(DropOut_DOP);
        }
        public string DeleteDropOut(string SD_StudentId)
        {
            return student.DeleteDropOut(SD_StudentId);
        }

        #endregion
        public long InsertUpdateClassSecFaculty(ClassSecFaculty_CSF csf)
        {
            return master.InsertUpdateClassSecFaculty(csf);
        }
        public List<ClassSecFaculty_CSF> GetClassSecFaculty(long? schoolId, long? sessionId)
        {
            return master.GetClassSecFaculty(schoolId, sessionId);
        }
        #region ClassFeesForward
        public List<ClassWisefees_CF> GetClassFeesForward(ClassFeesForward query)
        {
            return master.GetClassFeesForward(query);
        }
        #endregion

        #region MeritList
        public int ApproveApplications(OnlineMeritApproval_STD obj, out string outputMsg)
        {
            return master.ApproveApplications(obj, out outputMsg);
        }

        public List<StudentApplication_AP> GetApprovalList(Int64 SchoolId, string className, string isApproved, string isAdmitted)
        {
            return master.GetApprovalList(SchoolId, className, isApproved, isAdmitted);
        }
        #endregion

        #region  PettyCash
        //public List<StudetDetails_SD> GetStudentListForSubjectSettings(StudetDetails_SD obj)
        //{
        //    return master.GetStudentListForSubjectSettings(obj);
        //}

        public AccountMaster GetAccountMasterDetails(string TransType, int? Id)
        {

            AccountMaster AccountMasterDetails = master.GetAccountMasterDetails(TransType, Id);

            return AccountMasterDetails;
        }

        public List<AccountGroup> getAccountGroupList()
        {
            List<AccountGroup> AccountGroupList = master.getAccountGroupList("AccountGroupMaster", null, null, null);
            return AccountGroupList;
        }

        public int? InsertAccountMasterDetails(AccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.AM_AccountId != null && AccountMasterDetails.AM_AccountId != 0)
            {
                TransType = "UPDATE";
            }

            return master.InsertUpdateAccountMaster(TransType, AccountMasterDetails);

        }

        public List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster)
        {
            List<AccountMaster> CFRecords = master.AccountMasterList(TransType, AccountMaster);
            return CFRecords;
        }

        public List<AccountMaster> GetAccountMasterDropdownList()
        {
            List<AccountMaster> AccountGroupList = master.GetAccountMasterDropdownList("AccountMaster", null, null, null);
            return AccountGroupList;
        }

        public SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id)
        {

            SubAccountMaster AccountMasterDetails = master.GetSubAccountMasterDetails(TransType, Id);
            AccountMasterDetails.AccountMasterList = master.GetAccountMasterDropdownList("AccountMaster", null, null, null);

            return AccountMasterDetails;
        }
        public int? InsertSubAccountMasterDetails(SubAccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.SAM_SubId != null && AccountMasterDetails.SAM_SubId != 0)
            {
                TransType = "UPDATE";
            }

            AccountMasterDetails.SAM_SubId = master.InsertUpdateSubAccountMaster(TransType, AccountMasterDetails);


            return AccountMasterDetails.SAM_SubId;
        }
        public List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster)
        {
            List<SubAccountMaster> CFRecords = master.SubAccountMasterList(TransType, SubAccountMaster);
            return CFRecords;
        }
        public int? InsertAccountMasterOpeningDetails(AccountMaster AccountMasterDetails)
        {
            string TransType = "INSERT";
            if (AccountMasterDetails.AM_AccountOpId != 0)
            {
                TransType = "UPDATE";
            }

            AccountMasterDetails.AM_AccountOpId = master.InsertAccountMasterOpeningDetails(TransType, AccountMasterDetails);


            return AccountMasterDetails.AM_AccountOpId;
        }

        public List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id)
        {
            List<SubAccountMaster> CFRecords = master.SubAccountMasterOpeningbalanceList(TransType, Id);
            return CFRecords;
        }

        public int? InsertSubAccountMasterOpeningDetails(SubAccountMaster SubAccountMasterDetails)
        {
            int? val = 0;
            string TransType = "INSERT";

            val = master.InsertSubAccountMasterOpeningDetails(TransType, SubAccountMasterDetails);


            return val;
        }

        public string InsertUpdateAccountMasterOpening(int? AccId)
        {

            return master.InsertUpdateAccountMasterOpening(AccId);
        }

        public List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj)
        {
            return master.AccountVoucherTypeList(PerObj);
        }
        public string GetVoucherID(long? SessionId, int? VoucherTypeId)
        {

            return master.GetVoucherID(SessionId, VoucherTypeId);
        }
        public int? InsertVoucherEntryDetails(AccountLedger AccountLedgerS)
        {
            int? val = 0;
            string TransType = "INSERT";

            val = master.InsertVoucherEntryDetails(TransType, AccountLedgerS);


            return val;
        }
        public List<AccountLedger> VoucherEntryList(AccountLedger PerObj)
        {
            return master.VoucherEntryList(PerObj);

        }

        #endregion
        #region Library Master
        public long InsertUpdateBookMaster(BookMaster_BM book)
        {
            return master.InsertUpdateBookMaster(book);
        }
        public List<BookMaster_BM> BookMasterList(BookMaster_BM book)
        {
            return master.BookMasterList(book);
        }
        public long InsertUpdateBookCopyMaster(BookCopyMaster_BCM copy)
        {
            return master.InsertUpdateBookCopyMaster(copy);
        }
        public List<BookCopyMaster_BCM> BookCopyMasterList(BookCopyMaster_BCM copy)
        {
            return master.BookCopyMasterList(copy);
        }

        public long InsertUpdateLibrarySettingMaster(LibrarySetting setting)
        {
            return master.InsertUpdateLibrarySettingMaster(setting);
        }
        public List<LibrarySetting> LibrarySettingMasterList(LibrarySetting setting)
        {
            return master.LibrarySettingMasterList(setting);
        }

        public long InsertUpdateCategoryMaster(CategoryMaster category)
        {
            return master.InsertUpdateCategoryMaster(category);
        }
        public List<CategoryMaster> CategoryMasterList(CategoryMaster category)
        {
            return master.CategoryMasterList(category);
        }

        public long InsertUpdateSupplierMaster(SupplierMaster supplier)
        {
            return master.InsertUpdateSupplierMaster(supplier);
        }
        public List<SupplierMaster> SupplierMasterList(SupplierMaster supplier)
        {
            return master.SupplierMasterList(supplier);
        }
        public long InsertUpdateMemberMaster(MemberMaster member)
        {
            return master.InsertUpdateMemberMaster(member);
        }
        public List<MemberMaster> MemberMasterList(MemberMaster member)
        {
            return master.MemberMasterList(member);
        }
        public List<IssueTransaction> GetDueDaysCount(IssueTransaction issue)
        {
            return master.GetDueDaysCount(issue);
        }
        #endregion
        #region Transport
        public List<TransportDetailsResponse> GetTransportDetails(TransportDetailsRequest obj)
        {
            return student.GetTransportDetails(obj);
        }
        #endregion

        #region RegisterDevice
        public RegistrationResponse RegisterDevice(RegistrationRequest obj)
        {
            var deviceId = master.InsertUpdateDevice(obj);

            // Logic to determine if it's a new device or not
            var isNewDevice = deviceId > 0 ? true : false;

            return new RegistrationResponse
            {
                DeviceId = deviceId,
                IsNewDevice = isNewDevice
            };
        }
        #endregion
    }
}