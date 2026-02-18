using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using SchoolMVC.Models;
using Rotativa;
using Rotativa.MVC;
using BussinessObject.FeesCollection;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;
namespace SchoolMVC.Controllers
{
    public class StudentManagementController : BaseController
    {
        // GET: StudentManagement
        public ActionResult Index()
        {
            return View();
        }
        #region Attendence
        #region Daily
        public ActionResult StudentAttendance()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/StudentAttendance");
            StudentAttendenceMaster_SAM list = new StudentAttendenceMaster_SAM
            {
                SAM_Date = DateTime.Today
            };

            return View(list);
        }
        public ActionResult AttendanceList()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/AttendanceList");
            StudentAttendenceMaster_SAM list = new StudentAttendenceMaster_SAM();
            return View(list);
        }
        #endregion
        #region Exam
        public ActionResult StudentExamAttendance()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/StudentExamAttendance");

            StudentExamAttandanceMaster_SEA oRecords = new StudentExamAttandanceMaster_SEA
            {
                SEA_Attn_Date = DateTime.Today
            };
            var Terms = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.SEA_TermId = new SelectList(Terms, "TM_Id", "TM_TermName");
            return View(oRecords);
        }
        public ActionResult ExamAttendanceList()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/ExamAttendanceList");
            //var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            //GetRights(url);
            var SubjectList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.SEA_SubjectId = new SelectList(SubjectList, "SBM_Id", "SBM_SubjectName");
            var Terms = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.SEA_TermId = new SelectList(Terms, "TM_Id", "TM_TermName");
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SEA_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudentExamAttandanceMaster_SEA list = new StudentExamAttandanceMaster_SEA();
            return View(list);
        }
        #endregion
        #endregion
        #region Enquery
        public ActionResult Enquery(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/Enquery");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("EnqueryList", "StudentManagement");
            }
            StudentEnquery_ENQ oRecords = new StudentEnquery_ENQ();
            oRecords.ENQ_Date = DateTime.Today;
            Int64 editId = 0;
            if (id == null)
            {
                //var FormAmount = service.GetGlobalSelect<MSTR_FormAmount>("MSTR_FormAmount", "Slap_SchooIID", UserModel.UM_SCM_SCHOOLID);
                //oRecords.ENQ_FormAmount = FormAmount.Count == 0 ? 0 : FormAmount.FirstOrDefault().Slap_Amount;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.ENQ_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.ENQ_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");
                var FormAmount = service.GetGlobalSelect<FormMaster_FM>("FormMaster_FM", "FM_SCM_SchoolID", UserModel.UM_SCM_SCHOOLID);
                oRecords.ENQ_FormAmount = FormAmount.Count == 0 ? 0 : FormAmount.FirstOrDefault().FM_FormAmount;


            }
            if (id != null)
            {
                editId = (Int64)id;
                oRecords = service.GetGlobalSelectOne<StudentEnquery_ENQ>("StudentEnquery_ENQ", "ENQ_Id", editId);
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.ENQ_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.ENQ_ClassId);
                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.ENQ_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME", oRecords.ENQ_SessionId);
                var FormAmount = service.GetGlobalSelect<FormMaster_FM>("FormMaster_FM", "FM_SCM_SchoolID", UserModel.UM_SCM_SCHOOLID);
                oRecords.ENQ_FormAmount = FormAmount.Count == 0 ? 0 : FormAmount.FirstOrDefault().FM_FormAmount;

            }
            return View(oRecords);
        }
        public ActionResult EnqueryList()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/EnqueryList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ENQ_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudentEnquery_ENQ list = new StudentEnquery_ENQ();
            return View(list);
        }
        #endregion
        #region Admission old Before Edit admission
        //public ActionResult Admission(decimal? id, int? EnQNo)
        //{
        //    if (UserModel == null) return returnLogin("~/StudentManagement/Admission");
        //    if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
        //    {
        //        TempData["_Message"] = "You are not authorized..!!";
        //        return RedirectToAction("AdmittedStudentList", "StudentManagement");
        //    }
        //    StudetDetails_SD oRecords = new StudetDetails_SD();
        //    oRecords.SD_AppliactionDate = DateTime.Today;
        //    Int64 editId = 0;
        //    if (id == null && EnQNo == null)
        //    {
        //        var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
        //        ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
        //        var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");
        //        var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
        //        ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
        //        var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
        //        ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");
        //        var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
        //        ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME");
        //        var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
        //        ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
        //        var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", null);
        //        ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");
        //        var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
        //        ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");
        //        ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
        //        var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
        //        ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName");
        //        ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
        //        ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");
        //        var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
        //        ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME");
        //        var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
        //        ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
        //        var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
        //        ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME");
        //        var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
        //        ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType");
        //        var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME");
        //        var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
        //        ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName");
        //    }
        //    if (id != null && EnQNo == null)
        //    {
        //        editId = (Int64)id;
        //        oRecords = service.GetGlobalSelectOne<StudetDetails_SD>("StudetDetails_SD", "SD_Id", editId);
        //        ViewBag.Sex = oRecords.Sd_SexId;
        //        var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

        //        ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.SD_ClassId);
        //        var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME", oRecords.SD_SessionId);

        //        var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
        //        ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", oRecords.SD_StateId);

        //        var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
        //        ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", oRecords.SD_DistrictId);

        //        var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
        //        ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME", oRecords.SD_BloodGroupId);

        //        var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
        //        ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_MotherTongueId);

        //        var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", null);
        //        ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY", oRecords.SD_StudentCategoryId);

        //        var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
        //        ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME", oRecords.SD_CasteId);

        //        ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_SecondLanguageId);

        //        var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
        //        ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName", oRecords.SD_ReligionId);

        //        ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", oRecords.SD_PermanentStateId);
        //        ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", oRecords.SD_PermanentDistrictId);

        //        var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
        //        ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME", oRecords.SD_HouseId);

        //        var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
        //        ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName", oRecords.SD_TCType);

        //        var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
        //        ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME", oRecords.SD_NationalityId);

        //        var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
        //        ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType", oRecords.SD_TransportTypeId);

        //        var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME", oRecords.SD_RouteId);
        //        var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
        //        ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName", oRecords.SD_TransportMonthId);
        //        ViewBag.hdnRTId = oRecords.SD_RouteId;
        //        ViewBag.hdnTTId = oRecords.SD_TransportTypeId;
        //    }
        //    if (id == null && EnQNo != null)
        //    {
        //        oRecords.ENQ_Id = EnQNo;
        //        var EnqDetails = service.GetGlobalSelectOne<StudetDetails_SD>("StudentEnquery_ENQ", "ENQ_Id", EnQNo);
        //        oRecords.SD_StudentName = EnqDetails.ENQ_StudentName;
        //        oRecords.SD_FatherName = EnqDetails.ENQ_GuardianName;
        //        oRecords.SD_FormNo = EnqDetails.ENQ_FormNo;
        //        var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
        //        ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", EnqDetails.ENQ_ClassId);
        //        oRecords.SD_ContactNo1 = EnqDetails.ENQ_MobNo;
        //        var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SESSIONID", null);
        //        ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");
        //        var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
        //        ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
        //        var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
        //        ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");
        //        var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
        //        ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME");
        //        var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
        //        ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
        //        var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", null);
        //        ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");
        //        var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
        //        ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");
        //        ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
        //        var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
        //        ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName");
        //        ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
        //        ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");
        //        var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
        //        ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME");
        //        var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
        //        ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
        //        var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
        //        ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME");
        //        var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
        //        ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType");
        //        var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //        ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME", oRecords.SD_RouteId);
        //        var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
        //        ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName");
        //    }
        //    return View(oRecords);
        //}
        //public ActionResult AdmittedStudentList()
        //{
        //    if (UserModel == null) return returnLogin("~/StudentManagement/AdmittedStudentList");
        //    var url = Request.RequestContext.HttpContext.Request.RawUrl;
        //    GetRights(url);
        //    var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
        //    var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
        //    ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
        //    StudetDetails_SD list = new StudetDetails_SD();
        //    return View(list);
        //}
        #endregion
        #region Admission 2Edit
        public ActionResult Admission(long? id, int? EnQNo, long? SD_Id, bool checkRights = true)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/Admission");

            // Only apply the Add-rights check when we are actually adding (not editing) AND checkRights == true
            if (checkRights && (bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("AdmittedStudentList", "StudentManagement");
            }

            StudetDetails_SD oRecords = new StudetDetails_SD();
            oRecords.SD_AppliactionDate = DateTime.Today;

            // Treat SD_Id like id (so your existing Edit link keeps working)
            long? editId = id ?? SD_Id;

            // ========= EDIT (id / SD_Id provided) =========
            if (editId.HasValue && !EnQNo.HasValue)
            {
                long eid = editId.Value;
                oRecords = service.GetGlobalSelectOne<StudetDetails_SD>("StudetDetails_SD", "SD_Id", eid);

                ViewBag.Sex = oRecords.Sd_SexId;

                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.SD_ClassId);

                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME", oRecords.SD_SessionId);

                var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", oRecords.SD_StateId);
                var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
                ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", oRecords.SD_DistrictId);

                var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
                ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME", oRecords.SD_BloodGroupId);

                var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
                ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_MotherTongueId);
                ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_SecondLanguageId);
                ViewBag.SD_ThirdLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_ThirdLanguageId);



                var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY", oRecords.SD_StudentCategoryId);

                var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
                ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME", oRecords.SD_CasteId);

                var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
                ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName", oRecords.SD_ReligionId);

                ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", oRecords.SD_PermanentStateId);
                ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", oRecords.SD_PermanentDistrictId);

                var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
                ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME", oRecords.SD_HouseId);

                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName", oRecords.SD_TCType);

                var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME", oRecords.SD_NationalityId);

                var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
                ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType", oRecords.SD_TransportTypeId);

                var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME", oRecords.SD_RouteId);

                var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
                ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName", oRecords.SD_TransportMonthId);

                ViewBag.hdnRTId = oRecords.SD_RouteId;
                ViewBag.hdnTTId = oRecords.SD_TransportTypeId;

                return View(oRecords);
            }

            // ========= FROM ENQUIRY (EnQNo provided) =========
            if (!editId.HasValue && EnQNo.HasValue)
            {
                oRecords.ENQ_Id = EnQNo;

                // Load from enquiry table (kept as in your code, adjust model/type if needed)
                var EnqDetails = service.GetGlobalSelectOne<StudetDetails_SD>("StudentEnquery_ENQ", "ENQ_Id", EnQNo);

                oRecords.SD_StudentName = EnqDetails.ENQ_StudentName;
                oRecords.SD_FatherName = EnqDetails.ENQ_GuardianName;
                //oRecords.SD_FormNo = EnqDetails.ENQ_FormNo;
                oRecords.SD_FormNo = !string.IsNullOrEmpty(EnqDetails.ENQ_FormNo)
                     ? EnqDetails.ENQ_FormNo
                     : EnqDetails.ENQ_EnqueryNo.ToString();
                oRecords.SD_ContactNo1 = EnqDetails.ENQ_MobNo;
                oRecords.SD_DOB = EnqDetails.ENQ_DOB;
                oRecords.Sd_SexId = EnqDetails.ENQ_SexId;

                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", EnqDetails.ENQ_ClassId);

                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");

                var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
                var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
                ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");

                var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
                ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME");

                var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
                ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
                ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
                ViewBag.SD_ThirdLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");

                var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");

                var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
                ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");

                var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
                ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName");

                ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
                ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");

                var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
                ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME");

                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");

                var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME");

                var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
                ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType");

                var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME", oRecords.SD_RouteId);

                var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
                ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName");

                return View(oRecords);
            }

            // ========= NEW (no id / SD_Id / EnQNo) =========
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");

                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");

                var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
                var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
                ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");

                var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
                ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME");

                var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
                ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
                ViewBag.SD_SecondLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
                ViewBag.SD_ThirdLanguageId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");


                var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");

                var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
                ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");

                var ReligionList = service.GetGlobalSelect<ReligionMaster_RM>("ReligionMaster_RM", "RM_ReligionId", null);
                ViewBag.SD_ReligionId = new SelectList(ReligionList, "RM_ReligionId", "RM_ReligionName");

                ViewBag.SD_PermanentStateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
                ViewBag.SD_PermanentDistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");

                var HouseList = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", null);
                ViewBag.SD_HouseId = new SelectList(HouseList, "HM_HOUSEID", "HM_HOUSENAME");

                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.SD_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");

                var Nationilities = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.SD_NationalityId = new SelectList(Nationilities, "NM_NATIONID", "NM_NATIONNAME");

                var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
                ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType");

                var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME");

                var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
                ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName");

                return View(oRecords);
            }
        }
        public ActionResult AdmittedStudentList()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/AdmittedStudentList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudetDetails_SD list = new StudetDetails_SD();
            return View(list);
        }

        public ActionResult StudentManual(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "StudentId is required");

            var model = service.GetStudentManual(id);
            if (model == null)
                return HttpNotFound("Student not found");

            // Minimal working version in Rotativa.MVC 2.0.3
            return new ViewAsPdf("~/Views/StudentManagement/StudentManualPDF.cshtml", model)
            {
                FileName = "StudentManual_" + model.SD_StudentId + ".pdf"
            };
        }

        #endregion
        #region TC
        public ActionResult TC(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/TC");

            TCMaster oRecords = new TCMaster();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.TC_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.TC_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
            }
            if (id != null)
            {
                editId = (Int64)id;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.TC_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.CM_CLASSID);
                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.TC_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
            }
            return View(oRecords);
        }
        public ActionResult TCList(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/TCList");
            TCMaster oRecords = new TCMaster();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.TC_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.TC_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
            }
            if (id != null)
            {
                editId = (Int64)id;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.TC_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.CM_CLASSID);
                var TCType = service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null);
                ViewBag.TC_TCType = new SelectList(TCType, "TCTypeId", "TcTypeName");
            }
            return View(oRecords);
        }
        public ActionResult TCCertificate(string id)
        {
            if (string.IsNullOrEmpty(id))
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "StudentId is required");

            var model = service.GetTCCertificate(id);
            if (model == null)
                return HttpNotFound("Student not found");

            // Minimal working version in Rotativa.MVC 2.0.3
            return new ViewAsPdf("~/Views/StudentManagement/TCCertificate.cshtml", model)
            {
                FileName = "TCCertificate_" + model.SD_StudentId + ".pdf"
            };
        }


        #endregion
        #region Promotion
        public ActionResult Promotion()
        {
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.Pro_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View("Promotion");
        }
        #endregion
        #region Fees Collection Hostel and Transport
        #region Hostel
        public ActionResult HostelFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/HostelFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            
            HostelTransactionMaster_HTM list = new HostelTransactionMaster_HTM();
            var BankList = service.GetGlobalSelect<BankMaster_BM>("BankMaster_BM", "SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.HFD_BankId = new SelectList(BankList, "BankId", "BankName");
            return View(list);
        }
        public ActionResult HosteltFeesCollectionList(decimal? id)
        {
            Int64 editId = 0;
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            if (UserModel == null) return returnLogin("~/StudentManagement/HosteltFeesCollectionList");
            HostelTransactionMaster_HTM list = new HostelTransactionMaster_HTM();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            if (id != null)
            {
                editId = (Int64)id;
            }
            return View(list);
        }



        [HttpGet]
        public ActionResult HostelFeesCollectionListView(string HTM_TransId, string HFD_FeesTransId)
{
    try
    {
        HostelTransactionMaster_HTM model = new HostelTransactionMaster_HTM();

        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
        using (SqlCommand cmd = new SqlCommand("SP_HosteFeesCollection", con))
        {
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@TransType", "PrintReceipt");
            cmd.Parameters.AddWithValue("@HTM_TransId", HTM_TransId);
            cmd.Parameters.AddWithValue("@HFD_FeesTransId", HFD_FeesTransId);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                model.HTM_Id = rdr["HTM_Id"] == DBNull.Value ? 0L : Convert.ToInt64(rdr["HTM_Id"]);
                model.HTM_InstalmentNo = rdr["HTM_InstalmentNo"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["HTM_InstalmentNo"]);
                model.HTM_TransId = rdr["HTM_TransId"] == DBNull.Value ? string.Empty : rdr["HTM_TransId"].ToString();
                model.HTM_StudentId = rdr["HTM_StudentId"] == DBNull.Value ? string.Empty : rdr["HTM_StudentId"].ToString();
                model.HFD_Paymentmode = rdr["HFD_Paymentmode"] == DBNull.Value ? string.Empty : rdr["HFD_Paymentmode"].ToString();
                model.BankName = rdr["BankName"] == DBNull.Value ? string.Empty : rdr["BankName"].ToString();
                model.HFD_BranchName = rdr["HFD_BranchName"] == DBNull.Value ? string.Empty : rdr["HFD_BranchName"].ToString();
                model.HFD_CheqDDNo = rdr["HFD_CheqDDNo"] == DBNull.Value ? string.Empty : rdr["HFD_CheqDDNo"].ToString();
                model.HFD_Card_TrnsRefNo = rdr["HFD_Card_TrnsRefNo"] == DBNull.Value ? string.Empty : rdr["HFD_Card_TrnsRefNo"].ToString();
                model.HTM_MonthlyFare = rdr["HTM_MonthlyFare"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["HTM_MonthlyFare"]);
                model.HTM_PaidAmount = rdr["HTM_PaidAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["HTM_PaidAmount"]);
                model.HFD_CheqDDDate = rdr["HFD_CheqDDDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["HFD_CheqDDDate"]);
                model.HFD_FeesCollectionDate = rdr["HFD_FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["HFD_FeesCollectionDate"]);
                model.FeesName = "Hostel Fees";
                model.SCM_SCHOOLNAME = rdr["SCM_SCHOOLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLNAME"].ToString();
                model.SCM_FULLNAME = rdr["SCM_FULLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_FULLNAME"].ToString();
                model.SCM_SCHOOLADDRESS1 = rdr["SCM_SCHOOLADDRESS1"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLADDRESS1"].ToString();
                model.SM_SESSIONNAME = rdr["SM_SESSIONNAME"] == DBNull.Value ? string.Empty : rdr["SM_SESSIONNAME"].ToString();
                model.SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? string.Empty : rdr["SD_StudentName"].ToString();
                model.CM_CLASSNAME = rdr["CM_CLASSNAME"] == DBNull.Value ? string.Empty : rdr["CM_CLASSNAME"].ToString();
                model.SECM_SECTIONNAME = rdr["SECM_SECTIONNAME"] == DBNull.Value ? string.Empty : rdr["SECM_SECTIONNAME"].ToString();
            }
        }

        Session["HostelFees_Model"] = model;

        return View(model);
    }
    catch (Exception ex)
    {
        ViewBag._Message = "Error loading Hostel Fees Receipt: " + ex.Message;
        return View("Error");
    }
}




  
        #endregion
        #region TransportFees Collection
        public ActionResult TransportFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/TransportFeesCollection");
            TransportFeesTransaction_TR list = new TransportFeesTransaction_TR();
            var BankList = service.GetGlobalSelect<BankMaster_BM>("BankMaster_BM", "SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.TD_BankId = new SelectList(BankList, "BankId", "BankName");
            return View(list);
        }
        public ActionResult TransportFeesCollectionList(decimal? id)
        {
            Int64 editId = 0;
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            if (UserModel == null) return returnLogin("~/StudentManagement/TransportFeesCollectionList");
            TransportFeesTransaction_TR list = new TransportFeesTransaction_TR();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            if (id != null)
            {
                editId = (Int64)id;
            }
            return View(list);
        }

        [HttpGet]
        public ActionResult TransportFeesCollectionListView(string TD_TransId, string TR_TransId)
        {
            try
            {
                TransportFeesTransaction_TR model = new TransportFeesTransaction_TR();

                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
                using (SqlCommand cmd = new SqlCommand("SP_TransportFeesCollection", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransType", "PrintReceipt");
                    cmd.Parameters.AddWithValue("@TD_TransId", TD_TransId);
                    cmd.Parameters.AddWithValue("@TR_TransId", TR_TransId);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        model.TR_Id = rdr["TR_Id"] == DBNull.Value ? 0L : Convert.ToInt64(rdr["TR_Id"]);
                        model.TR_InstallmentMonth = rdr["TR_InstallmentMonth"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["TR_InstallmentMonth"]);
                        model.TD_TransId = rdr["TR_TransId"] == DBNull.Value ? string.Empty : rdr["TR_TransId"].ToString();
                        model.TR_StudentId = rdr["TR_StudentId"] == DBNull.Value ? string.Empty : rdr["TR_StudentId"].ToString();
                        model.BankName = rdr["BankName"] == DBNull.Value ? string.Empty : rdr["BankName"].ToString();
                        model.TD_Paymentmode = rdr["TD_Paymentmode"] == DBNull.Value ? string.Empty : rdr["TD_Paymentmode"].ToString();
                        model.TD_BranchName = rdr["TD_BranchName"] == DBNull.Value ? string.Empty : rdr["TD_BranchName"].ToString();
                        model.TD_CheqDDNo = rdr["TD_CheqDDNo"] == DBNull.Value ? string.Empty : rdr["TD_CheqDDNo"].ToString();
                        model.TD_Card_TrnsRefNo = rdr["TD_Card_TrnsRefNo"] == DBNull.Value ? string.Empty : rdr["TD_Card_TrnsRefNo"].ToString();
                        model.TR_MonthlyFare = rdr["TR_MonthlyFare"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["TR_MonthlyFare"]);
                        model.TR_PaidAmount = rdr["TR_PaidAmount"] == DBNull.Value ? 0m : Convert.ToDecimal(rdr["TR_PaidAmount"]);
                        model.TD_CheqDDDate = rdr["TD_CheqDDDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["TD_CheqDDDate"]);
                        model.TD_FeesCollectionDate = rdr["TD_FeesCollectionDate"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["TD_FeesCollectionDate"]);
                        model.FeesName = "Transport Fees";
                        model.SCM_SCHOOLNAME = rdr["SCM_SCHOOLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLNAME"].ToString();
                        model.SCM_FULLNAME = rdr["SCM_FULLNAME"] == DBNull.Value ? string.Empty : rdr["SCM_FULLNAME"].ToString();
                        model.SCM_SCHOOLADDRESS1 = rdr["SCM_SCHOOLADDRESS1"] == DBNull.Value ? string.Empty : rdr["SCM_SCHOOLADDRESS1"].ToString();
                        model.SM_SESSIONNAME = rdr["SM_SESSIONNAME"] == DBNull.Value ? string.Empty : rdr["SM_SESSIONNAME"].ToString();
                        model.SD_StudentName = rdr["SD_StudentName"] == DBNull.Value ? string.Empty : rdr["SD_StudentName"].ToString();
                        model.CM_CLASSNAME = rdr["CM_CLASSNAME"] == DBNull.Value ? string.Empty : rdr["CM_CLASSNAME"].ToString();
                        model.SECM_SECTIONNAME = rdr["SECM_SECTIONNAME"] == DBNull.Value ? string.Empty : rdr["SECM_SECTIONNAME"].ToString();
                    }
                }

                Session["TransportlFees_Model"] = model;

                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "Error loading Hostel Fees Receipt: " + ex.Message;
                return View("Error");
            }
        }
    
        #endregion
        #endregion
        #region Discontinue Student
        public ActionResult DiscontinueStudent()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/DiscontinueStudent");
            StudetDetails_SD oRecords = new StudetDetails_SD();
            oRecords.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            oRecords.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            //oRecords.StudentDetailsList = service.GetDisStudentList<StudetDetails_SD>(oRecords, "SP_DiscontinueStudent");
            return View(oRecords);
        }
        public ActionResult DiscontinueStudentList()
        {
            StudetDetails_SD oRecords = new StudetDetails_SD();
            oRecords.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            oRecords.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            if (UserModel == null) return returnLogin("~/StudentManagement/DiscontinueStudentList");
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            //oRecords.TransType = "SELECTALL";
            //oRecords.StudentDetailsList = service.GetDisStudentList<StudetDetails_SD>(oRecords, "SP_DiscontinueStudent");
            return View(oRecords);
        }
        #endregion
        #region Student Class Change
        public ActionResult StudentClassChange()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/StudentClassChange");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StudetDetails_SD oRecords = new StudetDetails_SD();
            oRecords.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            oRecords.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            //oRecords.StudentDetailsList = service.GetDisStudentList<StudetDetails_SD>(oRecords, "SP_DiscontinueStudent");
            return View(oRecords);
        }
        #endregion
        #region AppliedStudentList
        public ActionResult AppliedStudentList()
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/AppliedStudentList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.SA_ST_CLASS = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            StudentApplication_AP list = new StudentApplication_AP();
            return View(list);
        }
        #endregion
        #region  DropOut
        public ActionResult DropOut(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/DropOut");

            DropOut_DOP oRecords = new DropOut_DOP();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.DOP_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
               
            }
            if (id != null)
            {
                editId = (Int64)id;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.DOP_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.CM_CLASSID);
               
            }
            return View(oRecords);
        }
        public ActionResult DropOutList(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/StudentManagement/DropOutList");
            DropOut_DOP oRecords = new DropOut_DOP();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.DOP_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                
            }
            if (id != null)
            {
                editId = (Int64)id;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.DOP_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", oRecords.CM_CLASSID);

            }
            return View(oRecords);
        }
        #endregion
        #region Certificate Wizard
       
        public ActionResult Wizard(decimal? id)
        {
            if (UserModel == null)
            return returnLogin("~/StudentManagement/Wizard");
            var data = new CertificateType();
            var certificateList = service.GetGlobalSelect<CertificateType>("CertificateType_CER", "CER_ID", null);
            if (id == null)
            {
                ViewBag.CER_ID = new SelectList(certificateList, "CER_ID", "CER_NAME");
                return View(data);
            }
            long editId = Convert.ToInt64(id);
            data = service.GetGlobalSelectOne<CertificateType>("CertificateType_CER", "CER_ID", editId);

            ViewBag.CER_ID = new SelectList(certificateList, "CER_ID", "CER_NAME");

            return View(data);
        }

        public ActionResult Bonafide(string studentId)
        {
            var data = GetStudentDetails(studentId);
            return View(data);
        }

        public ActionResult Character(string studentId)
        {
            var data = GetStudentDetails(studentId);
            return View(data);
        }

        public ActionResult BirthCertificate(string studentId)
        {
            var data = GetStudentDetails(studentId);
            return View(data);
        }

       

        // Load Student Full Details
        public StudetDetails_SD GetStudentDetails(string studentId)
        {
            StudetDetails_SD obj = new StudetDetails_SD();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentCertificateDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@TransType", "StudentDetails");

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    obj.SD_StudentId = dr["StudentId"].ToString();
                    obj.SD_StudentName = dr["StudentName"].ToString();
                    obj.SD_FatherName = dr["FatherName"].ToString();
                    obj.SD_MotherName = dr["MotherName"].ToString();
                    obj.SD_PermanentAddress = dr["PermanentAddress"].ToString();
                    obj.SD_CM_CLASSNAME = dr["ClassName"].ToString();
                    obj.SECM_SECTIONNAME = dr["SectionName"].ToString();
                    obj.SM_SESSIONNAME = dr["SessionName"].ToString();
                    obj.SD_DOB = dr["DOB"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(dr["DOB"]);
                    obj.SD_AppliactionDate = dr["AdmissionDate"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(dr["AdmissionDate"]);

                }
            }

            return obj;
        }

        public ActionResult FeesMaster(string studentId)
        {
            var data = GetFeesDetails(studentId);
            return View(data);
        }
 
        public StudetDetails_SD GetFeesDetails(string studentId)
        {
            StudetDetails_SD obj = new StudetDetails_SD();
            obj.FeesList = new List<FeesDetail_SD>();   // Add list to store fees rows

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ToString()))
            {
                SqlCommand cmd = new SqlCommand("SP_GetStudentCertificateDetails", con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@TransType", "FeesDetails");

                con.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    if (dr.Read())
                    {
                        obj.SD_StudentId = dr["StudentId"].ToString();
                        obj.SD_StudentName = dr["StudentName"].ToString();
                        obj.SD_FatherName = dr["FatherName"].ToString();
                        obj.SD_MotherName = dr["MotherName"].ToString();
                        obj.SD_CM_CLASSNAME = dr["ClassName"].ToString();
                        obj.SECM_SECTIONNAME = dr["SectionName"].ToString();
                        obj.SM_SESSIONNAME = dr["SessionName"].ToString();

                        obj.SD_AppliactionDate = dr["AdmissionDate"] == DBNull.Value ?
                            (DateTime?)null : Convert.ToDateTime(dr["AdmissionDate"]);

                        obj.StartDate = dr["SM_STARTDATE"] == DBNull.Value ?
                            (DateTime?)null : Convert.ToDateTime(dr["SM_STARTDATE"]);

                        obj.EndDate = dr["SM_ENDDATE"] == DBNull.Value ?
                            (DateTime?)null : Convert.ToDateTime(dr["SM_ENDDATE"]);
                    }


                    if (dr.NextResult())
                    {
                        while (dr.Read())
                        {
                            FeesDetail_SD item = new FeesDetail_SD();

                            item.fem_feesname = dr["fem_feesname"].ToString();
                            item.Installment = dr["Installment"] == DBNull.Value ? (int?)null : Convert.ToInt32(dr["Installment"]);
                            item.InstallmentAmt = dr["InstallmentAmt"] == DBNull.Value ? 0 : Convert.ToDecimal(dr["InstallmentAmt"]);

                            obj.FeesList.Add(item);
                        }
                    }
                }
            }
            return obj;
        }

        #endregion

        
    }
}