using BussinessObject;
using BussinessObject.FeesCollection;
using SchoolMVC.Areas.FacultyPortal.Models.Request;
using SchoolMVC.Areas.FacultyPortal.Models.Response;
using SchoolMVC.Areas.Notification.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Request;
using SchoolMVC.Areas.StudentPortal.Models.Response;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolMVC.Repositories
{
    interface IRepository
    {
        List<T> GetGlobalSelect<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new();
        long GlobalDelete(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null);
        T GetGlobalSelectOne<T>(string MainTableName, string MainFieldName, Int64? PId) where T : class, new();
        string DuplicateDataCheck(string TableName, string DataField, string DataFieldValue, string OptionalField, long? OptionalFieldValue);
        long DeleteClassWisefees(int ClassId, int CF_CATEGORYID, int CF_FEESID);
        #region Marksheet
        List<DDLList> GetClassWiseDetails(string TransactionType, long classId, long schoolId, long? sessionId, long? SubGrId);
        List<clsStudentList> getStudentsList(long classId, long sectionId, long subjectId, long HS, long schoolId, long sessionId);
        GradeMaster_GM GradeCheck(decimal? Marks, long schoolId, long sessionId);
        long InsertUpdateMarks(clsStudentMarksEntry marks);
        long InsertUpdateStudentCouncilRegistration(List<clsStudentList> students, long SchoolId, long SessionId);
        List<clsStudentMarksEntry> GetStudentMarksList(clsStudentMarksEntry marks);
        clsStudentMarksEntry GetStudentMarks(clsStudentMarksEntry marks);
        List<clsStudentList> studentsForMarkSheet(clsStudentList query);
        List<clsStudentList> studentsForCouncilRegistration(clsStudentList query);
        string CheckHoliday(string date, long schoolId, long sessionId);
        #endregion Marksheet

        #region AdmissionFees
        List<StudentList> SearchNonAdmittedStudent(FeesSearchBO SearchBO);
        List<studentFeesDetails> getStudentFeesDetails(FeesSearchBO SearchBO);
        List<FeesCollectionBO> GetFeeSCollectionList(FeesCollectionBO obj, string FeesColType);
        #endregion AdmissionFees
        decimal InsertUpdateFeesCollection(FeesCollectionBO collection, string type);
        FormBO getAsyncFeesUpdate(long Id);

        LateFeesSlap FindLateFeesAmount(long schoolId, long sessionId, DateTime dueDate, DateTime collectionDate); // int fineTypeId,

        List<AdditionalFeesBO> GetAdditionalFeesIds();

        #region SessionFees
        List<StudentList> FetchAdmittedStudent(FeesSearchBO SearchBO);
        List<studentFeesDetails> getStudentSessionFeesDetails(FeesSearchBO SearchBO);
        #endregion SessionFees
        #region menu
        Int64 InsertAnyMasters(List<SqlParameter> arrParams, string SP_Name, SqlParameter OutPutId);
        // TEntity GetAnyItemDetails(int pk_intId);

        //List<TEntity> GetAnyItemList();

        T GetGlobalMaster<T>(GlobalDataList global) where T : class, new();
        List<T> GetGlobalMasterList<T>(GlobalDataList global) where T : class, new();

        List<T> GetAnyList<T>(List<SqlParameter> arrParams, string SP_Name) where T : class, new();
        #endregion
        void SaveSmsTracker(List<SMSBO> smsBOs);
        DashBoardBO GetDashBoardData(DashBoardBO dash);
        long DeleteFeesCollection(long? FEESID, long? SchoolId, long? SessionId);

        string DeleteHostelCollection(string HTM_TransId, long? SchoolId, long? SessionId);
        string DeleteTransportCollection(string TD_TransId, long? SchoolId, long? SessionId);
    }
    #region IRepository_Login
    interface ILoginRepository
    {
        UserMaster_UM GetLogin(UserMaster_UM user);
    }
    #endregion
    #region Masters
    interface IMasterRepository
    {
        long AddEditSession(SessionMasters_SM Ses);
        long InsertUpdateState(StateMaster_STM state);
        long InsertUpdateDistrict(DistrictMasters_DM district);
        long InsertUpdateTerm(TermMaster_TM term);
        long InsertUpdateClassType(ClassTypeMaster_CTM Ct);
        long InsertUpdateClass(ClassMaster_CM Class);
        long InsertUpdateSection(SectionMaster_SECM sec);
        long InsertUpdateBoard(BoardMasters_BM board);
        long InsertUpdateCaste(CasteMaster_CSM caste);
        long InsertUpdateVernacular(VernacularMaster_VM vm);
        long InsertUpdateHouse(HouseMasters_HM house);
        long InsertUpdateStream(StreamMasters_STRM stream);
        long InsertUpdateBloodGroup(BloodGroupMasters_BGM bloodgrp);
        long InsertUpdateGrade(GradeMaster_GM grade);
        long InsertUpdateTeachingAid(TeachingAid_TA teachinga);
        long InsertUpdateForm(FormMaster_FM form);
        long InsertUpdateHoliday(HolidayMaster_HM holidays);
        long InsertUpdateNotice(NoticeMasters_NM notice);
        long InsertUpdateLessonPlan(LessonPlan_LP lessonplan);
        long InsertUpdatePeriod(PeriodMaster_PM period);
        //List<PeriodMaster_PM> PeriodList(PeriodMaster_PM obj);
        long InsertUpdateMiscellaneousHead(MiscellaneousHeadMaster_MISC misc);
        long InsertUpdateClassTypeboard(ClassTypeBoard_CTB classboard);
        long InsertUpdateRoute(RouteMastes_RT route);
       
        long InsertUpdateRoutewiseDrop(RoutewiseDropMaster_RDM obj);
        long InsertUpdateSubjectGroup(SubjectGroupMaster_SGM obj);
        long InsertUpdateSubject(SubjectMaster_SBM obj);
        //long InsertUpdateSubjectgroupWiseSubject(SubjectGroupWiseSubjectSetting_SGS obj);
        long InsertUpdateClassWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj);
        long InsertUpdateAssignmentMaster(AssignmentMaster_ASM obj);

        long InsertUpdateStudentDiary(StudentDiary_STD obj);
        long InsertUpdateStudentEarlyLeave(StudentEarlyLeave_ERL obj);
        long InsertUpdateExamGroup(ExamGroupMaster_EGM EGM, List<long> classIds);
        long InsertUpdateClasswiseLiveclass(ClassWiseLiveclass_CWLS obj);

        long InsertUpdateUserMaster(UserMaster_UM obj);
        List<UserMaster_UM> UserMasterList(UserMaster_UM obj);
        long InsertUpdateFaculty(FacultyProfileMasters_FPM obj);
        long InsertUpdateClasswiseFaculty(ClassWiseFacultyMasters_CWF obj);
        long InsertUpdateRoutine(ClassWiseTeacherRoutine_CWTR obj);
        long InsertUpdateCategory(STUDENTCATEGORY_CAT obj);
        long InsertUpdateClasswiseFees(ClassWisefees_CF obj);
        long InsertUpdateClasswiseFeesForward(ClassWisefees_CF obj);
        
        List<ClassWisefees_CF> GetClassWiseFeesList(Int64 SchoolId, Int64 SessionId, int? ClassId);
        long InsertUpdateFees(FeesMaster_FEM feeObj);
        long InsertUpdateLateFeesSlap(LateFeesSlap feeObj);
        long AddEditSchool(SchoolMasters_SCM obj);
        List<ClsSubGrWiseSubSetting_CSGWS> GetGroupWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj);
        long InsertUpdateStudentwiseSubject(StudentwiseSubjectSetting_SWS Subject);
        List<StudetDetails_SD> GetStudentListForSubjectSettings(StudetDetails_SD obj);
        decimal InsertUpdateSecRollSetting(Sec_Roll_Setting_SR obj);
        List<StudentwiseSubjectSetting_SWS> StudentWiseSubjectListForEdit(StudentwiseSubjectSetting_SWS obj);
        List<StudentwiseSubjectSetting_SWS> GetIndividualStudentWiseSubjectForEdit(string Sid);
        List<FacultyProfileMasters_FPM> LoadFacultyByClass(FacultyProfileMasters_FPM obj);
        List<FeesMaster_FEM> GetFeesList(bool? IsAdmission);
        List<StudetDetails_SD> GetStudentListRollSecSetting(StudetDetails_SD obj);
        string CheckDuplicity(ClassWiseTeacherRoutine_CWTR obj);
        List<ClassWiseTeacherRoutine_CWTR> GetRoutineList(Int64 SchoolId, Int64 SessionId, int? ClassId);
        decimal UpdateTransport(StudetDetails_SD data);
        decimal InsertStudentForHostel(HostelMaster_HM data);
        List<StudetDetails_SD> GetStudentListForTransportSettings(StudetDetails_SD obj);
        List<StudetDetails_SD> GetStudentListForHostelSettings(StudetDetails_SD obj);
        List<StudetDetails_SD> TransportAvailedStudentList(StudetDetails_SD obj);
        long DeleteTransportFromStudents(string StudentId);
        List<StudetDetails_SD> HostelAviledStudentList(StudetDetails_SD obj);
        long DeleteHostelSetting(string StudentId);
        List<ClassWiseFacultyMasters_CWF> GetClassWiseFacultyList(Int64? SchoolId, Int64? SessionId, Int64? ClassId, Int64? SubId, Int64? FacId);
        List<StudentApplication_AP> GetApplicationList(string candidateId);
        long InsertUpdateDesignation(DesignationMaster_DM designation);
        long InsertUpdateBank(BankMaster_BM bank);
        long InsertUpdateLeaveType(LeaveType_LT leavetype);
        long InsertUpdateTransportType(TransportType_TT transport);
        long InsertUpdateHostelRoom(HostelRoomMaster_HR hostel);
        List<_MenuMaster> ModuleddlList(_MenuMaster obj);
        long InsertUpdateClassSecFaculty(ClassSecFaculty_CSF csf);

        long InsertUpdateDevice(RegistrationRequest obj);
        List<ClassSecFaculty_CSF> GetClassSecFaculty(long? schoolId, long? sessionId);


        long InsertUpdateSyllabus(ClassWiseSyllabusMasters_CWSM syllabus);
 
      
        StudentManual GetStudentManual(string studentId);


        IEnumerable<StudentModel> GetStudentsByClass(int schoolId, int sessionId, string classId, string sectionId, string studentId, int cardType);
        List<LessonPlanList_VM> GetLessonPlanList(long? facultyId, long? subjectId,
                                          DateTime? fromDate, DateTime? toDate,
                                          long ?schoolId, long? sessionId);

        long DeleteExamGroup(long EGM_Id);

        List<StudentApplication_AP> GetApprovalList(Int64 SchoolId, string className, string isApproved, string isAdmitted);
        int ApproveApplications(OnlineMeritApproval_STD obj, out string outputMsg);
        long UpdateAssignmentMarks(AssignmentMarkUpdate assignmentMarkUpdate);
        List<ClassWisefees_CF> GetClassFeesForward(ClassFeesForward query);

        #region Pettycash
        //List<StudetDetails_SD> GetStudentListForSubjectSettings(StudetDetails_SD obj);

        AccountMaster GetAccountMasterDetails(string TransType, int? Id);
        List<AccountGroup> getAccountGroupList(string TransType, long? PId, long? SId, long? XId);

        //long InsertUpdateClassSecFaculty(ClassSecFaculty_CSF csf);
        int? InsertUpdateAccountMaster(string TransType, AccountMaster AccountMasterDetails);

        List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster);
        List<AccountMaster> GetAccountMasterDropdownList(string TransType, long? PId, long? SId, long? XId);
        SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id);
        int? InsertUpdateSubAccountMaster(string TransType, SubAccountMaster AccountMasterDetails);
        List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster);
        int? InsertAccountMasterOpeningDetails(string TransType, AccountMaster AccountMasterDetails);
        List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id);
        int? InsertSubAccountMasterOpeningDetails(string TransType, SubAccountMaster SubAccountMasterDetails);
        string InsertUpdateAccountMasterOpening(int? AccId);
        List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj);
        string GetVoucherID(long? SessionId, int? VoucherTypeId);
        //int? InsertVoucherEntryDetails(AccountLedger AccountLedgerS);
        int? InsertVoucherEntryDetails(string TransType, AccountLedger AccountLedgerS);
        List<AccountLedger> VoucherEntryList(AccountLedger PerObj);
        #endregion

        #region Library Master
        long InsertUpdateBookMaster(BookMaster_BM book);
        List<BookMaster_BM> BookMasterList(BookMaster_BM book);
        long InsertUpdateBookCopyMaster(BookCopyMaster_BCM copy);
        List<BookCopyMaster_BCM> BookCopyMasterList(BookCopyMaster_BCM copy);
        long InsertUpdateLibrarySettingMaster(LibrarySetting setting);
        List<LibrarySetting> LibrarySettingMasterList(LibrarySetting setting);
        long InsertUpdateCategoryMaster(CategoryMaster category);
        List<CategoryMaster> CategoryMasterList(CategoryMaster category);
        long InsertUpdateSupplierMaster(SupplierMaster supplier);
        List<SupplierMaster> SupplierMasterList(SupplierMaster supplier);
        long InsertUpdateMemberMaster(MemberMaster member);
        List<MemberMaster> MemberMasterList(MemberMaster member);
        List<IssueTransaction> GetDueDaysCount(IssueTransaction issue);
        #endregion
    }

    #endregion
    #region Student
    interface IStudentRepository
    {

        Tuple<string, string> InsertUpdateEnquery(StudentEnquery_ENQ obj);
        //string InsertUpdateEnquery(StudentEnquery_ENQ obj);
        string InsertUpdateAdmissionDetails(StudetDetails_SD obj);
        List<StudetDetails_SD> GetAdmissionStudentList(StudetDetails_SD obj);
        List<StudentEnquery_ENQ> GetEnqueryList(StudentEnquery_ENQ obj);
        List<StudentExamAttandanceMaster_SEA> GetStudentListForExamAttendance(StudentExamAttandanceMaster_SEA obj);
        string InsertUpdateExamAttendance(StudentExamAttandanceMaster_SEA obj);
        //List<StudentExamAttandanceMaster_SEA> GetStudentListForExamAttendance_BuSubject(StudentExamAttandanceMaster_SEA obj);
        List<StudentAttendenceMaster_SAM> GetStudentListForAttendance(StudentAttendenceMaster_SAM obj);
        string InsertUpdateAttendance(StudentAttendenceMaster_SAM obj);
        List<StudentAttendanceDetailsResponse> GetStudentAttendanceDetails(StudentAttendanceDetailsRequest request);
        List<StudentExamAttandanceMaster_SEA> ExamAttendanceList(StudentExamAttandanceMaster_SEA obj);
        string DeleteExamAttendance(int? id);
        string DeleteStudentWiseSubject(string id);
        List<StudentAttendenceMaster_SAM> AttendanceList(StudentAttendenceMaster_SAM obj);
        List<clsStudentList> studentsForPromotion(clsStudentList query);
        long UpdateStudentPromotion(clsStudentList obj);
        string DeleteAttendance(int? id);
        List<HostelMaster_HM> GetHotelFeesCollectionList(HostelMaster_HM obj);
        string InsertHostelFeesCollection(HostelTransactionMaster_HTM obj);
        List<TransportFeesTransaction_TR> GetTransportFeesCollectionList(TransportFeesTransaction_TR obj);
        string InsertTransportFeesCollection(TransportFeesTransaction_TR obj);
        List<TransportFeesTransaction_TR> LoadTransportFeesCollectionList(TransportFeesTransaction_TR obj);
        List<HostelTransactionMaster_HTM> LoadHostelFeesCollectionList(HostelTransactionMaster_HTM obj);
        List<TransportFeesTransaction_TR> GetTransportFeesCollectionListForEdit(TransportFeesTransaction_TR obj);
        List<HostelTransactionMaster_HTM> GetHostelFeesCollectionListForEdit(HostelTransactionMaster_HTM obj);
        List<TCMaster> GetAllStudent(TCMaster obj);
        List<TCMaster> GetAllTCStudent(TCMaster obj);
        ///print tc certificate
        TCCertificate GetTCCertificate(string studentId);
        long CancelStudents(TCMaster TCMaster);
        string DeleteTC(string SD_StudentId);
        List<StudetDetails_SD> GetDisStudentList(StudetDetails_SD obj);
        StudetDetails_SD GetStudentLogin(StudetDetails_SD obj);
        List<NoticeResponse> GetNotice(NoticeRequest obj);
        StudentMarksheetResponse GetStudentMarksheet(StudentMarksheetRequest obj);
        List<StudentPaidDetailsResponse> GetStudentPaidDetails(StudentPaidDetailsRequest obj);
        StudentPaidReceiptResponse GetStudentPaidReceipt(StudentPaidReceiptRequest obj);
        List<ClassWiseBirthdayResponse> GetClassWiseBirthday(ClassWiseBirthdayRequest obj);
        List<SyllabusResponse> GetSyllabus(SyllabusRequest obj);
        List<StudentLiveClassResponse> GetStudentLiveClass(StudentLiveClassRequest obj);
        List<StudentWiseFacultyResponse> GetStudentWiseFacultyList(StudentWiseFacultyRequest obj);
        AttendenceResponse GetStudentWiseAttendence(AttendenceRequest obj);
        List<RoutineResponse> GetRoutine(RoutineRequest obj);
        List<StudentFeeSummaryResponse> GetStudentFeeSummary(StudentFeeSummaryRequest obj);
        List<StudentDueFeeResponse> GetStudentDueFee(StudentDueFeeRequest obj);
        //string InsertUpdateAssignment(AssignmentMaster_ASM obj);
        DbResult InsertUpdateMiscCollection(MiscellaneousTransactionMaster obj);
        void DeleteMiscCollection(string id);
        
        List<MiscellaneousTransactionMaster> MiscFeesCollectionList(MiscellaneousTransactionMaster obj);
        List<DropOut_DOP> GetAllDropOutStudent(DropOut_DOP obj);
        List<DropOut_DOP> GetAllDStudent(DropOut_DOP obj);
        long CancelDStudents(DropOut_DOP DropOut_DOP);
        string DeleteDropOut(string SD_StudentId);
       //ASSIGNMENT
        long InsertUpdateStudentAssignmentMaster(StudentAssignmentMaster obj);
        List<StudentAssignmentsListResponse> GetStudentAssignmentList(StudentAssignmentsListRequest obj);
        StudentAssignmentsListResponse GetStudentAssignmentById(StudentAssignmentDtlsRequest studentAssignmentDtlsRequest);
        string DeleteStudentAssignment(int? id);
        List<TransportDetailsResponse> GetTransportDetails(TransportDetailsRequest obj);
    }
    #endregion
    #region Faculty

    interface IFacultyRepository
    {
        FacultyProfileMasters_FPM GetFacultyLogin(FacultyProfileMasters_FPM obj);

        List<AttendanceListResponse> GetAttendanceList(AttendanceListRequest obj);

        List<ListForAttendanceResponse> GetListForAttendance(ListForAttendanceRequest obj);
        List<AssignmentsListResponse> GetAssignmentList(AssignmentsListRequest obj);
        List<UploadedStudentListResponse> GetUploadedStudentList(AssignmentsListRequest obj);

        string DeleteAssignment(int? id);

        string DeleteClasswiseLiveclass(int? id);

    }


    #endregion


}
