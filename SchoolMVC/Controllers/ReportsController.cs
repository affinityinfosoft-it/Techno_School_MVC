using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolMVC.Controllers
{
    public class ReportsController : BaseController
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        #region Enquery Report
        public ActionResult EnqueryReport()
        {
            if (UserModel == null) return returnLogin("~/Reports/EnqueryReport");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        #endregion
        #region Admission Realated Reports
        public ActionResult AdmissionDetails()
        {
            if (UserModel == null) return returnLogin("~/Reports/AdmissionDetails");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
            return View();
        }
        public ActionResult StudentRegister()
        {
            if (UserModel == null) return returnLogin("~/Reports/StudentRegister");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);

            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        public ActionResult StudentStrength()
        {
            if (UserModel == null) return returnLogin("~/Reports/StudentStrength");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
            ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");
            return View();
        }
        public ActionResult GenderWiseStudentStrength()
        {
            if (UserModel == null) return returnLogin("~/Reports/GenderWiseStudentStrength");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        public ActionResult StudentCasteGenderReport()
        {
            if (UserModel == null) return returnLogin("~/Reports/StudentCasteGenderReport");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
            ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");
            return View();
        }
        #endregion
        #region Fees Collection Related Reports
        public ActionResult FeesCollectionRegister()
        {
            if (UserModel == null) return returnLogin("~/Reports/FeesCollectionRegister");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        public ActionResult FeesCollectionDetails()
        {
            if (UserModel == null) return returnLogin("~/Reports/FeesCollectionDetails");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");

            return View();
        }
        public ActionResult DateWiseFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/DateWiseFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;


            return View();
        }
        public ActionResult ClassSecWiseFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/ClassSecWiseFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        public ActionResult MonthWiseFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/MonthWiseFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        public ActionResult ClassSecHeadWiseFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/ClassSecHeadWiseFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        #endregion
        #region Routine Realated reports
        public ActionResult ClassSectionWiseRoutine()
        {
            if (UserModel == null) return returnLogin("~/Reports/StudentStrength");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        public ActionResult TeacherWiseRoutine()
        {
            if (UserModel == null) return returnLogin("~/Reports/StudentStrength");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            ViewBag.FacultyId = new SelectList(FacultyList, "FP_Id", "FP_Name");
            return View();
        }
        #endregion
        #region TransportFees Collection
        public ActionResult TransportFeesCollectionReport()
        {
            if (UserModel == null) return returnLogin("~/Reports/TransportFeesCollectionReport");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        public ActionResult ClassSecWiseTransportFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/ClassSecWiseTransportFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        public ActionResult DateWiseFeesTransportCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/DateWiseFeesTransportCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;


            return View();
        }
        public ActionResult MonthWiseTransportFeesCollection()
        {
            if (UserModel == null) return returnLogin("~/Reports/MonthWiseTransportFeesCollection");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        #endregion
        #region HostelFees Collection
        public ActionResult HostelFeesCollectionReport()
        {
            if (UserModel == null) return returnLogin("~/Reports/TransportFeesCollectionReport");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            return View();
        }
        #endregion
        #region Promotion Report
        public ActionResult PromotionReport()
        {
            if (UserModel == null) return returnLogin("~/Reports/PromotionReport");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;

            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }

        #endregion

    }
}