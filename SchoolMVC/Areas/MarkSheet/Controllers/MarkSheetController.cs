using SchoolMVC.Areas.MarkSheet.Models;
using SchoolMVC.Controllers;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SchoolMVC.Areas.MarkSheet.Controllers
{
    public class MarkSheetController : BaseController
    {
        StatusResponse Status = new StatusResponse();
        [HttpGet]
        public ActionResult Search()
        {
            if (UserModel == null) return returnLogin("~/MarkSheet/MarkSheet/Search");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            try
            {
                MarkSheetModel model = new MarkSheetModel();
                model.GetStudentMarksList = new List<clsStudentMarksEntry>();
                clsStudentMarksEntry marks = new clsStudentMarksEntry
                {
                    SME_SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0,
                    SME_SessionId = UserModel.UM_SCM_SESSIONID ?? 0
                };
                model.GetStudentMarksList = service.GetStudentMarksList(marks);
                return View("Search", model);
            }
            catch (Exception ex)
            {
                ViewBag._Message = "There are issues in displaying the page. Please contact site Administrator.";
                return View("Error");
            }
        }
        public ActionResult AddMarksEntry()
        {
            if (UserModel == null) return returnLogin("~/MarkSheet/MarkSheet/AddMarksEntry");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("Search", "MarkSheet", new { area = "MarkSheet" });
            }
            var terms = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_Id", null);
            terms = terms.Where(r => r.TM_SchoolId == UserModel.UM_SCM_SCHOOLID && r.TM_SessionId == UserModel.UM_SCM_SESSIONID).ToList();
            ViewBag.TM_Id = new SelectList(terms, "TM_Id", "TM_TermName");
            return View("AddMarksEntry");
        }
        public JsonResult InsertUpdateMarks(clsStudentMarksEntry marks)
        {
            marks.SME_SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            marks.SME_SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            marks.UserId = UserModel.UM_USERID ?? 0;
            try
            {
                var id = service.InsertUpdateMarks(marks);
                if (id == -1) Status.Message = "Record(s) already exit...duplicate entry!!.";
                else Status.Message = "Record has been saved successfully.";
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Id = id;
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record has not been saved successfully.";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentMarks(clsStudentMarksEntry marks)
        {
            marks.SME_SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            marks.SME_SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            marks.UserId = UserModel.UM_USERID ?? 0;
            clsStudentMarksEntry objMarksList = service.GetStudentMarks(marks);
            if (objMarksList != null)
            {
                if (objMarksList.StudentMarksTransactionList != null && objMarksList.StudentMarksTransactionList.Count > 0)
                    objMarksList.StudentMarksTransactionList = objMarksList.StudentMarksTransactionList.GroupBy(x => x.StudentId).Select(x => x.FirstOrDefault()).ToList(); // remove duplicate items
            }
            return Json(objMarksList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult generateMarkSheet()
        {
            if (UserModel == null) return returnLogin("~/MarkSheet/MarkSheet/generateMarkSheet");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            var terms = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_Id", null);
            terms = terms.Where(r => r.TM_SchoolId == UserModel.UM_SCM_SCHOOLID && r.TM_SessionId == UserModel.UM_SCM_SESSIONID).ToList();
            ViewBag.ddlTerm = new SelectList(terms, "TM_Id", "TM_TermName");
            return View();
        }
        [HttpPost]
        public JsonResult studentsForMarkSheet(clsStudentList query)
        {
            query.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            query.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            List<clsStudentList> objStudentsList = service.studentsForMarkSheet(query);
            objStudentsList = objStudentsList.GroupBy(x => x.StudentId).Select(x => x.FirstOrDefault()).ToList();
            return Json(objStudentsList.OrderBy(r => r.Roll).ToList(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult allStudentMarkSheet(List<clsStudentList> students)
        {
            Session["PrintMarksheetStudents"] = students;
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult StudentCouncilRegistration()
        {

            return View("StudentCouncilRegistration");
        }

        [HttpPost]
        public JsonResult studentsForCouncilRegistration(clsStudentList query)
        {
            query.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            query.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            List<clsStudentList> objStudentsList = service.studentsForCouncilRegistration(query);
            objStudentsList = objStudentsList.GroupBy(x => x.StudentId).Select(x => x.FirstOrDefault()).ToList();
            return Json(objStudentsList.OrderBy(r => r.Roll).ToList(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertUpdateStudentCouncilRegistration(List<clsStudentList> clsStudents)
        {
            var SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            var SessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            try
            {
                var id = service.InsertUpdateStudentCouncilRegistration(clsStudents, SchoolId, SessionId);
                Status.Message = "Record has been saved successfully.";
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Id = id;
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record has not been saved successfully.";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
    }
}