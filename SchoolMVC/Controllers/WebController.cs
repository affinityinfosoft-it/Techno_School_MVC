using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;

namespace SchoolMVC.Controllers
{
    public class WebController : BaseController
    {
        // GET: Web
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admission(decimal? id)
        {
            StudetDetails_SD oRecords = new StudetDetails_SD();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
                ViewBag.SD_ClassId = new SelectList(Classlist, "CM_CLASSID", "CM_CLASSNAME");
                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SESSIONID", null);
                ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME");
                var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME");
                var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
                ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME");
                var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
                ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME");
                var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
                ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME");
                var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", null);
                ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");
                var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
                ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME");

            }
            if (id != null)
            {
                editId = (Int64)id;
                oRecords = service.GetGlobalSelectOne<StudetDetails_SD>("StudetDetails_SD", "SD_Id", editId);
                ViewBag.Sex = oRecords.Sd_SexId;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
                ViewBag.SD_ClassId = new SelectList(Classlist, "CM_CLASSID", "CM_CLASSNAME", oRecords.SD_ClassId);
                var Sessionlist = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SESSIONID", null);
                ViewBag.SD_SessionId = new SelectList(Sessionlist, "SM_SESSIONID", "SM_SESSIONNAME", oRecords.SD_SessionId);
                var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.SD_StateId = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", oRecords.SD_StateId);
                var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
                ViewBag.SD_DistrictId = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", oRecords.SD_DistrictId);
                var BloodList = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
                ViewBag.SD_BloodGroupId = new SelectList(BloodList, "BGM_BLDGRPID", "BGM_BLDGRPNAME", oRecords.SD_BloodGroupId);
                var MotherTongueList = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
                ViewBag.SD_MotherTongueId = new SelectList(MotherTongueList, "VM_VERNACULARID", "VM_VERNACULARNAME", oRecords.SD_MotherTongueId);
                var Categories = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", null);
                ViewBag.SD_StudentCategoryId = new SelectList(Categories, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY", oRecords.SD_StudentCategoryId);
                var CasteList = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
                ViewBag.SD_CasteId = new SelectList(CasteList, "CSM_CASTEID", "CSM_CASTENAME", oRecords.SD_CasteId);
            }
            return View(oRecords);
        }


        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}