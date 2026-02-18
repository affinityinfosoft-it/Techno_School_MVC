using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;

namespace SchoolMVC.Controllers
{
    public class MastersController : BaseController
    {
        // GET: Masters
        public ActionResult Index()
        {
            return View();
        }
        #region School
        public ActionResult School()
        {
            if (UserModel == null) return returnLogin("~/Masters/School");
            SchoolMasters_SCM School = new SchoolMasters_SCM();
            School = service.GetGlobalSelectOne<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var StateList = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
            ViewBag.SCM_STATEID = new SelectList(StateList, "STM_STATEID", "STM_STATENAME", School.SCM_STATEID);
            var DistList = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", null);
            ViewBag.SCM_DISTRICTID = new SelectList(DistList, "DM_DISTRICTID", "DM_DISTRICTNAME", School.SCM_DISTRICTID);
            var list = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
            ViewBag.SCM_NATIONID = new SelectList(list, "NM_NATIONID", "NM_NATIONNAME", School.SCM_NATIONID);
            return View(School);
        }
        #endregion
        #region Master
        #region Session Master
        public ActionResult Session(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Session");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("SessionList", "Masters");
            }
            SessionMasters_SM sessions = new SessionMasters_SM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                sessions = service.GetGlobalSelectOne<SessionMasters_SM>("SessionMasters_SM", "SM_SESSIONID", editId);
            }
            return View(sessions);
        }
        public ActionResult SessionList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            SessionMasters_SM sessions = new SessionMasters_SM();
            return View(sessions);
        }
        #endregion
        #region Blood Group Master
        public ActionResult BloodGroup(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/BloodGroup");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("BloodGroupList", "Masters");
            }
            BloodGroupMasters_BGM blood = new BloodGroupMasters_BGM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                blood = service.GetGlobalSelectOne<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", editId);

            }

            return View(blood);
        }
        public ActionResult BloodGroupList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            BloodGroupMasters_BGM bglist = new BloodGroupMasters_BGM();
            return View(bglist);
        }
        #endregion
        #region Bus Route Master
        public ActionResult BusRoute(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/BusRoute");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("BusRouteList", "Masters");
            }
            RouteMastes_RT data = new RouteMastes_RT();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<RouteMastes_RT>("RouteMastes_RT", "RT_ROUTEID", editId);

            }

            return View(data);
        }
        public ActionResult BusRouteList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            RouteMastes_RT list = new RouteMastes_RT();
            return View(list);
        }
        #endregion
        #region Caste Master
        public ActionResult Caste(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Caste");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("CasteList", "Masters");
            }
            CasteMaster_CSM caste = new CasteMaster_CSM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                caste = service.GetGlobalSelectOne<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", editId);
            }
            return View(caste);
        }
        public ActionResult CasteList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            CasteMaster_CSM castelist = new CasteMaster_CSM();
            return View(castelist);
        }
        #endregion
        #region ClassType Master
        public ActionResult ClassType(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassType");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassTypeList", "Masters");
            }
            ClassTypeMaster_CTM Terms = new ClassTypeMaster_CTM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                Terms = service.GetGlobalSelectOne<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", editId);

            }

            return View(Terms);
        }
        public ActionResult ClassTypeList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassTypeMaster_CTM Ctype = new ClassTypeMaster_CTM();
            return View(Ctype);
        }
        #endregion
        #region Class Master
        public ActionResult Class(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Class");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassList", "Masters");
            }
            ClassMaster_CM Classes = new ClassMaster_CM();
            Int64 editId = 0;
            if (id == null)
            {
                var TypeList = service.GetGlobalSelect<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", null);
                ViewBag.CM_CTM_TYPEID = new SelectList(TypeList, "CTM_TYPEID", "CTM_TYPENAME");

            }
            else
            {
                editId = (Int64)id;
                Classes = service.GetGlobalSelectOne<ClassMaster_CM>("ClassMaster_vw", "CM_CLASSID", editId);
                var TypeList = service.GetGlobalSelect<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", null);
                ViewBag.CM_CTM_TYPEID = new SelectList(TypeList, "CTM_TYPEID", "CTM_TYPENAME", Classes.CM_CTM_TYPEID);
            }
            return View(Classes);
        }
        public ActionResult ClassList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassMaster_CM Class = new ClassMaster_CM();
            return View(Class);
        }
        #endregion
        #region Distric Master
        public ActionResult District(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/District");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("DistrictList", "Masters");
            }
            DistrictMasters_DM District = new DistrictMasters_DM();
            Int64 editId = 0;
            if (id == null)
            {
                var Nationlist = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.DM_NATIONID = new SelectList(Nationlist, "NM_NATIONID", "NM_NATIONNAME");
                var Statelist = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.DM_STATEID = new SelectList(Statelist, "STM_STATEID", "STM_STATENAME");

            }
            else
            {
                editId = (Int64)id;
                District = service.GetGlobalSelectOne<DistrictMasters_DM>("DistrictMasters_DM", "DM_DISTRICTID", editId);
                var Nationlist = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.DM_NATIONID = new SelectList(Nationlist, "NM_NATIONID", "NM_NATIONNAME", District.DM_NATIONID);
                var Statelist = service.GetGlobalSelect<StateMaster_STM>("StateMaster_STM", "STM_STATEID", null);
                ViewBag.DM_STATEID = new SelectList(Statelist, "STM_STATEID", "STM_STATENAME", District.DM_STATEID);
            }
            return View(District);
        }
        public ActionResult DistrictList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StateMaster_STM state = new StateMaster_STM();
            return View(state);
        }
        #endregion
        #region Board Master
        public ActionResult Board(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Board");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("BoardList", "Masters");
            }
            BoardMasters_BM board = new BoardMasters_BM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                board = service.GetGlobalSelectOne<BoardMasters_BM>("BoardMasters_BM", "BM_BOARDID", editId);
            }

            return View(board);
        }
        public ActionResult BoardList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            BoardMasters_BM blist = new BoardMasters_BM();
            return View(blist);
        }
        #endregion
        #region FeesMaster
        public ActionResult FeesMasterList()
        {
            if (UserModel == null) return returnLogin("~/Masters/FeesMasterList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            FeesMaster_FEM FemList = new FeesMaster_FEM();
            return View(FemList);
        }
        public ActionResult FeesMaster(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/FeesMaster");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("FeesMasterList", "Masters");
            }
            FeesMaster_FEM objFem = new FeesMaster_FEM();
            Int64 editId = 0;
            if (id == null)
            {
                ViewBag._Action = "add";
                var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", null);
                ViewBag.FM_SchoolId = new SelectList(SchoolList, "SCM_SCHOOLID", "SCM_SCHOOLNAME");
                var insTypeList = service.GetGlobalSelect<InsType_INTYP>("InsType_INTYP", "INTYP_INSTYPEID", null);
                ViewBag.FM_InstypeId = new SelectList(insTypeList, "INTYP_INSTYPEID", "INTYP_INSTYPENAME", "INTYP_INSTYPEVALUE");
            }
            if (id != null)
            {
                ViewBag._Action = "update";
                editId = (Int64)id;
                objFem = service.GetGlobalSelectOne<FeesMaster_FEM>("FeesMaster_FEM", "FEM_FEESID", editId);
                var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", null);
                ViewBag.FM_SchoolId = new SelectList(SchoolList, "SCM_SCHOOLID", "SCM_SCHOOLNAME", objFem.FEM_SCHOOLID);
                var insTypeList = service.GetGlobalSelect<InsType_INTYP>("InsType_INTYP", "INTYP_INSTYPEID", null);
                ViewBag.FM_InstypeId = new SelectList(insTypeList, "INTYP_INSTYPEID", "INTYP_INSTYPENAME", objFem.FEM_INSTYPEID);
                objFem.FEM_FEESID = editId;
                objFem.REFUNDABLE = objFem.FEM_ISREFUNDABLE;
                objFem.HOSTELFEES = objFem.FEM_ISHOSTELFEES;
                objFem.TRANSPORTFEES = objFem.FEM_ISTRANSPORTFEES;
                objFem.DUPDOCFEES = objFem.FEM_ISDUPDOCFEES;
                objFem.DUPIDFEES = objFem.FEM_ISDUPIDFEES;
                objFem.PROCESSINGFEES = objFem.FEM_ISPROCESSINGFEES;
                objFem.FEM_NONE = objFem.FEM_NONE;
            }
            return View(objFem);
        }
        #endregion
        #region Faculty Master
        public ActionResult Faculty(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Faculty");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("FacultyList", "Masters");
            }
            FacultyProfileMasters_FPM data = new FacultyProfileMasters_FPM();
            Int64 editId = 0;
            if (id == null)
            {
                var DesigList = service.GetGlobalSelect<DesignationMaster_DM>("DesignationMaster_DM", "DM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.FP_DesignationId = new SelectList(DesigList, "DM_Id", "DM_Name");
                var DegreeList = service.GetGlobalSelect<DegreeMasters_DEGM>("DegreeMasters_DEGM", "DEGM_Id", null);
                ViewBag.FP_Max_DEGM_Id = new SelectList(DegreeList, "DEGM_Id", "DEGM_DegreeName");
            }
            else
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_Id", editId);
                var DesigList = service.GetGlobalSelect<DesignationMaster_DM>("DesignationMaster_DM", "DM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.FP_DesignationId = new SelectList(DesigList, "DM_Id", "DM_Name", data.FP_DesignationId);
                var DegreeList = service.GetGlobalSelect<DegreeMasters_DEGM>("DegreeMasters_DEGM", "DEGM_Id", null);
                ViewBag.FP_Max_DEGM_Id = new SelectList(DegreeList, "DEGM_Id", "DEGM_DegreeName", data.FP_Max_DEGM_Id);
            }
            return View(data);
        }
        public ActionResult FacultyList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            FacultyProfileMasters_FPM list = new FacultyProfileMasters_FPM();
            return View(list);
        }
        #endregion
        #region Fees Catagory Master
        public ActionResult FeesCategory(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/FeesCategory");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("FeesCategoryList", "Masters");
            }
            STUDENTCATEGORY_CAT data = new STUDENTCATEGORY_CAT();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_CATEGORYID", editId);

            }

            return View(data);
        }
        public ActionResult FeesCategoryList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            STUDENTCATEGORY_CAT list = new STUDENTCATEGORY_CAT();
            return View(list);
        }
        #endregion
        #region Grade Master
        public ActionResult Grade(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Grade");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("GradeList", "Masters");
            }
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CWTR_Class = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            GradeMaster_GM grade = new GradeMaster_GM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                grade = service.GetGlobalSelectOne<GradeMaster_GM>("GradeMaster_GM", "GM_Grade_Id", editId);

            }

            return View(grade);
        }
        public ActionResult GradeList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            GradeMaster_GM glist = new GradeMaster_GM();
            return View(glist);
        }
        #endregion
        #region Holiday Master
        public ActionResult Holiday(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Holiday");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("HolidayList", "Masters");
            }
            HolidayMaster_HM data = new HolidayMaster_HM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<HolidayMaster_HM>("HolidayMaster_HM", "HM_Id", editId);

            }

            return View(data);
        }
        public ActionResult HolidayList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            HolidayMaster_HM list = new HolidayMaster_HM();
            return View(list);
        }
        #endregion
        #region House Master
        public ActionResult House(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/House");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("HouseList", "Masters");
            }
            HouseMasters_HM house = new HouseMasters_HM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                house = service.GetGlobalSelectOne<HouseMasters_HM>("HouseMasters_HM", "HM_HOUSEID", editId);
            }
            return View(house);
        }
        public ActionResult HouseList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            HouseMasters_HM hlist = new HouseMasters_HM();
            return View(hlist);
        }
        #endregion
        #region Notice Master

        public ActionResult Notice(decimal? id)
        {
            if (UserModel == null)
                return returnLogin("~/Masters/Notice");

            var data = new NoticeMasters_NM();

            // Load dropdowns
            var NtypeList = service.GetGlobalSelect<NoticeType_NT>("NoticeType_NT", "NT_ID", null);
            var facultyRaw = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            var classRaw = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);

            // prepare sorted class list
            var sortedClassList = classRaw
                .OrderBy(c => c.CM_FROMAGE.ToString().Length)
                .ThenBy(c => c.CM_FROMAGE)
                .ToList();

            // Build strongly-typed faculty select list
            var facultySelect = facultyRaw
                .Select(f => new SelectListItem
                {
                    Value = f.FP_Id.ToString(),   // ensure string values
                    Text = f.FP_Name ?? String.Empty
                }).ToList();

            long? loggedFacultyId = UserModel.UM_FP_ID;

            // If Add New
            if (id == null)
            {
                ViewBag.NM_NtId = new SelectList(NtypeList, "NT_ID", "NT_NAME");
                ViewBag.NM_ClassId = new MultiSelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                ViewBag.FacultySelect = facultySelect;

                if (loggedFacultyId != null && loggedFacultyId > 0)
                {
                    data.NM_FacultyId = loggedFacultyId;
                    ViewBag.IsFacultyReadonly = true;
                }
                else
                {
                    ViewBag.IsFacultyReadonly = false;
                }

                return View(data);
            }

            // Edit existing
            long editId = Convert.ToInt64(id);
            data = service.GetGlobalSelectOne<NoticeMasters_NM>("NoticeMasters_NM", "NM_Id", editId);

            var selectedClassIds = string.IsNullOrEmpty(data.NM_ClassId)
                ? new List<long>()
                : data.NM_ClassId.Split(',').Select(long.Parse).ToList();

            ViewBag.NM_NtId = new SelectList(NtypeList, "NT_ID", "NT_NAME", data.NM_NtId);
            ViewBag.NM_ClassId = new MultiSelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", selectedClassIds);
            ViewBag.FacultySelect = facultySelect;

            if (loggedFacultyId != null && loggedFacultyId > 0)
            {
                // For faculty users, lock them to their own id (don't override stored data shown)
                data.NM_FacultyId = loggedFacultyId;    // optional: force the model to logged faculty
                ViewBag.IsFacultyReadonly = true;
            }
            else
            {
                ViewBag.IsFacultyReadonly = false;
            }

            return View(data);
        }

        public ActionResult NoticeList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            NoticeMasters_NM list = new NoticeMasters_NM();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.NM_ClassId = new SelectList(sortedClassList, "CM_CLASSNAME", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region Period Master
        public ActionResult Period(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Period");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("PeriodList", "Masters");
            }
            PeriodMaster_PM period = new PeriodMaster_PM();
            Int64 editId = 0;
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.PM_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");

            }
            else
            {
                editId = (Int64)id;
                period = service.GetGlobalSelectOne<PeriodMaster_PM>("PeriodMaster_PM", "PM_Id ", editId);
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.PM_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", period.PM_ClassId);


            }

            return View(period);
        }
        public ActionResult PeriodList()
        {

            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            PeriodMaster_PM list = new PeriodMaster_PM();
            //var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            //ViewBag.PM_ClassId = new SelectList(ClassList, "CM_CLASSNAME", "CM_CLASSNAME");
            return View(list);

        }
        #endregion
        #region Section Master
        public ActionResult Section(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Section");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("SectionList", "Masters");
            }
            SectionMaster_SECM sec = new SectionMaster_SECM();
            Int64 editId = 0;
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SECM_CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            }
            else
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;
                var DataList = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_vw", "SECM_CM_CLASSID", editId);
                ViewBag.DataList = DataList;
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SECM_CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", sec.SECM_CM_CLASSID);
                sec.CM_CLASSID = DataList.FirstOrDefault().SECM_CM_CLASSID;
            }
            return View(sec);
        }
        public ActionResult SectionList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            SectionMaster_SECM state = new SectionMaster_SECM();
            return View(state);
        }

        [HttpGet]
        public JsonResult GetSectionsByClassId(long classId)
        {
            try
            {
                var sectionList = service.GetGlobalSelect<SectionMaster_SECM>(
                    "SectionMaster_vw",
                    "SECM_CM_CLASSID",
                    classId
                );

                return Json(new { IsSuccess = true, Data = sectionList }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { IsSuccess = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region State Master
        public ActionResult State(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/State");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("StateList", "Masters");
            }
            StateMaster_STM State = new StateMaster_STM();
            Int64 editId = 0;
            if (id == null)
            {
                var list = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.STM_NATIONID = new SelectList(list, "NM_NATIONID", "NM_NATIONNAME");
            }
            else
            {
                editId = (Int64)id;
                State = service.GetGlobalSelectOne<StateMaster_STM>("StateMaster_STM", "STM_STATEID", editId);
                var list = service.GetGlobalSelect<NationMaster_NM>("NationMaster_NM", "NM_NATIONID", null);
                ViewBag.STM_NATIONID = new SelectList(list, "NM_NATIONID", "NM_NATIONNAME", State.STM_NATIONID);
            }
            return View(State);
        }
        public ActionResult StateList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StateMaster_STM state = new StateMaster_STM();
            return View(state);
        }
        #endregion
        #region Stream Master
        public ActionResult Stream(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Stream");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("StreamList", "Masters");
            }
            StreamMasters_STRM stream = new StreamMasters_STRM();
            Int64 editId = 0;
            if (id == null)
            {
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.STRM_CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");


            }
            else
            {
                editId = (Int64)id;
                stream = service.GetGlobalSelectOne<StreamMasters_STRM>("StreamMasters_STRM", "STRM_STREAMID", editId);
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.STRM_CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", stream.STRM_CM_CLASSID);
            }
            return View(stream);
        }
        public ActionResult StreamList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StreamMasters_STRM list = new StreamMasters_STRM();
            return View(list);
        }
        #endregion
        #region Subject Master
        public ActionResult Subject(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Subject");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("SubjectList", "Masters");
            }
            SubjectMaster_SBM data = new SubjectMaster_SBM();
            Int64 editId = 0;
            if (id == null)
            {
                var GroupList = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SubjectGroupID", null);
                ViewBag.SBM_SubGr_Id = new SelectList(GroupList, "SGM_SubjectGroupID", "SGM_SubjectGroupName");


            }
            else
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_Id", editId);
                var GroupList = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SubjectGroupID", null);
                ViewBag.SBM_SubGr_Id = new SelectList(GroupList, "SGM_SubjectGroupID", "SGM_SubjectGroupName", data.SBM_SubGr_Id);
            }

            return View(data);
        }
        public ActionResult SubjectList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            SubjectMaster_SBM list = new SubjectMaster_SBM();
            return View(list);
        }
        #endregion
        #region Subject Group Master
        public ActionResult SubjectGroup(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/SubjectGroup");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("SubjectGroupList", "Masters");
            }
            SubjectGroupMaster_SGM data = new SubjectGroupMaster_SGM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SubjectGroupID", editId);

            }

            return View(data);
        }
        public ActionResult SubjectGroupList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            SubjectGroupMaster_SGM list = new SubjectGroupMaster_SGM();
            return View(list);
        }
        #endregion
        #region Term Master
        public ActionResult Term(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Term");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("TermList", "Masters");
            }
            TermMaster_TM Terms = new TermMaster_TM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                Terms = service.GetGlobalSelectOne<TermMaster_TM>("TermMaster_TM", "TM_Id", editId);
            }

            return View(Terms);
        }
        public ActionResult TermList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            TermMaster_TM state = new TermMaster_TM();
            return View(state);
        }
        #endregion
        #region Vernacular Master
        public ActionResult Vernacular(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Vernacular");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("VernacularList", "Masters");
            }
            VernacularMaster_VM vm = new VernacularMaster_VM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                vm = service.GetGlobalSelectOne<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", editId);
            }
            return View(vm);
        }
        public ActionResult VernacularList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            VernacularMaster_VM vlist = new VernacularMaster_VM();
            return View(vlist);
        }
        #endregion
        #region LateFees
        public ActionResult LateFeesMasterList()
        {
            if (UserModel == null) return returnLogin("~/Masters/LateFeesMasterList");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            return View("LateFeesMasterList");
        }

        public ActionResult LateFeesMaster(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/LateFeesMaster");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("LateFeesMasterList", "Masters");
            }

            LateFeesSlap lateFee = new LateFeesSlap();
            Int64 editId = 0;

            if (id == null)
            {

                var list = service.GetGlobalSelect<FineTypeMaster>("FineTypeMaster_FTM", "FineTypeId", null);
                ViewBag.Slap_FineTypeID = new SelectList(list, "FineTypeId", "FineTypeName", lateFee.Slap_FineTypeID);

            }
            else
            {

                editId = (Int64)id;
                lateFee = service.GetGlobalSelectOne<LateFeesSlap>("MSTR_LateFeesSlap", "ID", editId);

                var list = service.GetGlobalSelect<FineTypeMaster>("FineTypeMaster_FTM", "FineTypeId", null);
                ViewBag.Slap_FineTypeID = new SelectList(list, "FineTypeId", "FineTypeName");
            }

            return View(lateFee);
        }




        #endregion LateFees
        #region Route Wise Drop Mapping Master
        public ActionResult RouteWiseDropMapping(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/RouteWiseDropMapping");
            RoutewiseDropMaster_RDM data = new RoutewiseDropMaster_RDM();
            Int64 editId = 0;
            if (id == null)
            {
                var RouteList = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.RDM_ROUTEID = new SelectList(RouteList, "RT_ROUTEID", "RT_ROUTENAME");

            }
            else
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;
                ViewBag.DataList = service.GetGlobalSelect<RoutewiseDropMaster_RDM>("Routewisedropmaster_vw", "RDM_ROUTEID", editId);
                ViewBag.htnId = editId;
                var RouteList = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.RDM_ROUTEID = new SelectList(RouteList, "RT_ROUTEID", "RT_ROUTENAME");
            }

            return View(data);
        }
        public ActionResult RouteWiseDropMappingList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            RoutewiseDropMaster_RDM list = new RoutewiseDropMaster_RDM();
            return View(list);
        }
        #endregion
        #region Designation
        public ActionResult Designation(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Designation");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("DesignationList", "Masters");
            }
            DesignationMaster_DM designation = new DesignationMaster_DM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                designation = service.GetGlobalSelectOne<DesignationMaster_DM>("DesignationMaster_DM", "DM_Id", editId);

            }

            return View(designation);
        }
        public ActionResult DesignationList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            DesignationMaster_DM list = new DesignationMaster_DM();
            return View(list);
        }
        #endregion
        #region Bank
        public ActionResult Bank(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Bank");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("BankList", "Masters");
            }
            BankMaster_BM bank = new BankMaster_BM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                bank = service.GetGlobalSelectOne<BankMaster_BM>("BankMaster_BM", "BankId", editId);
            }
            return View(bank);
        }
        public ActionResult BankList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            BankMaster_BM list = new BankMaster_BM();
            return View(list);
        }
        #endregion
        #region Hostel Room No
        public ActionResult HostelRoomList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            HostelRoomMaster_HR list = new HostelRoomMaster_HR();
            return View(list);
        }
        public ActionResult HostelRoom(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/HostelRoom");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("HostelRoomList", "Masters");
            }
            HostelRoomMaster_HR hostelroom = new HostelRoomMaster_HR();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                hostelroom = service.GetGlobalSelectOne<HostelRoomMaster_HR>("HostelRoomMaster_HR", "HR_HostelRoomId", editId);
            }
            return View(hostelroom);
        }
        #endregion
        #region TransportType master
        public ActionResult TransportType(decimal? id)
        {
            if (UserModel == null)
                return returnLogin("~/Masters/TransportType");

            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("TransportTypeList", "Masters");
            }

            TransportType_TT transport = new TransportType_TT();
            if (id != null)
            {
                long editId = Convert.ToInt64(id);
                transport = service.GetGlobalSelectOne<TransportType_TT>("TransportType_TT", "TypeId", editId);
            }
            return View(transport);
        }

        public ActionResult TransportTypeList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            TransportType_TT list = new TransportType_TT();
            return View(list);
        }
        #endregion
        #region LeaveCat Master
        public ActionResult LeaveType(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/LeaveType");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("LeaveTypeList", "Masters");
            }
            LeaveType_LT leavetype = new LeaveType_LT();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                leavetype = service.GetGlobalSelectOne<LeaveType_LT>("LeaveType_LT", "LeaveTypeId ", editId);
            }
            return View(leavetype);
        }
        public ActionResult LeaveTypeList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            LeaveType_LT list = new LeaveType_LT();
            return View(list);
        }
        #endregion
        #region StudentDiary
        public ActionResult StudentDiary(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/StudentDiary");
            StudentDiary_STD data = new StudentDiary_STD();
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                //ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", data.ClassId);

            }
            Int64 editId = 0;

            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<StudentDiary_STD>("StudentDiary_STD", "DiaryId ", editId);
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                //ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", data.ClassId);

            }

            return View(data);
        }
        public ActionResult StudentDiaryList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StudentDiary_STD list = new StudentDiary_STD();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region Student EarlyLeaveEntry Master
        public ActionResult EarlyLeaveEntry(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/EarlyLeaveEntry");
            StudentEarlyLeave_ERL data = new StudentEarlyLeave_ERL();

            if (id == null)
            {

                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.ERL_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", data.ERL_ClassId);

                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.ERL_FacultyId = new SelectList(FacultyList, "FP_Id", "FP_Name", data.ERL_FacultyId);

            }
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<StudentEarlyLeave_ERL>("StudentEarlyLeave_ERL", "ERL_ID", editId);
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.ERL_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", data.ERL_ClassId);
                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.ERL_FacultyId = new SelectList(FacultyList, "FP_Id", "FP_Name", data.ERL_FacultyId);

            }

            return View(data);
        }
        public ActionResult EarlyLeaveEntryList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            StudentEarlyLeave_ERL list = new StudentEarlyLeave_ERL();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ERL_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            ViewBag.ERL_FacultyId = new SelectList(FacultyList, "FP_Id", "FP_Name");
            return View(list);
        }

        #endregion
        #region StudentId Card
        public ActionResult StudentIdCard()
        {
            if (UserModel == null) return returnLogin("~/Masters/StudentIdCard");
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }

        public ActionResult GenerateIdCards(int? schoolId, int? sessionId, string[] classId, string[] sectionId, string studentId = null, int cardType = 0)
        {
            if (schoolId == null || sessionId == null)
            {
                return Content("Invalid School or Session selected.");
            }
            string classIds = classId != null ? string.Join(",", classId) : null;
            string sectionIds = sectionId != null ? string.Join(",", sectionId) : null;

            var students = service.GetStudentsByClass(schoolId.Value, sessionId.Value, classIds, sectionIds, studentId, cardType);

            ViewBag.SchoolId = schoolId.Value;
            ViewBag.SessionId = sessionId.Value;
            ViewBag.ClassId = classId;
            ViewBag.SectionId = sectionId;
            ViewBag.StudentId = studentId;
            ViewBag.CardType = cardType;
            return View(students);
        }

        #endregion
        #region ExamGroup

        public ActionResult ExamGroup(long? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/ExamGroup");

            // Terms
            var terms = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_Id", null)
                               .Where(r => r.TM_SchoolId == UserModel.UM_SCM_SCHOOLID
                                        && r.TM_SessionId == UserModel.UM_SCM_SESSIONID)
                               .ToList();
            ViewBag.Terms = terms;

            // Classes
            var classList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = classList.OrderBy(c => c.CM_FROMAGE.ToString().Length)
                                           .ThenBy(c => c.CM_FROMAGE)
                                           .ToList();
            ViewBag.ClassList = sortedClassList;

            var model = new ExamGroupPostDto();

            if (id != null)
            {
                long editId = (long)id;

                // Get one row to identify the group
                var firstRow = service.GetGlobalSelectOne<ExamGroupMaster_EGM>("ExamGroupMaster_EGM", "EGM_Id", editId);

                if (firstRow != null)
                {
                    var allRows = service.GetGlobalSelect<ExamGroupMaster_EGM>("ExamGroupMaster_EGM", "EGM_SchoolId", firstRow.EGM_SchoolId)
                                         .Where(x => x.EGM_Name == firstRow.EGM_Name &&
                                                     x.EGM_SchoolId == firstRow.EGM_SchoolId &&
                                                     x.EGM_SessionId == firstRow.EGM_SessionId)
                                         .ToList();

                    model.EGM_Id = firstRow.EGM_Id;
                    model.EGM_Name = firstRow.EGM_Name;
                    model.EGM_SchoolId = firstRow.EGM_SchoolId;
                    model.EGM_SessionId = firstRow.EGM_SessionId;
                    model.Userid = firstRow.Userid;

                    model.ExamGroupDetails = allRows.Select(x => new ExamGroupDetailDto
                    {
                        TermId = x.EGM_TermId,
                        ClassId = x.EGM_ClassId,
                        SelectExam = true,
                        IsFinal = x.IsFinal
                    }).ToList();
                }
            }

            return View(model);
        }


        public ActionResult ExamGroupList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ExamGroupMaster_EGM list = new ExamGroupMaster_EGM();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region MiscellaneousHead
            public ActionResult MiscellaneousHead(decimal? id)
            {
                if (UserModel == null) return returnLogin("~/Masters/MiscellaneousHead");
                if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
                {
                    TempData["_Message"] = "You are not authorized..!!";
                    return RedirectToAction("MiscellaneousHeadList", "Masters");
                }
                MiscellaneousHeadMaster_MISC misc = new MiscellaneousHeadMaster_MISC();
                Int64 editId = 0;
                if (id != null)
                {
                    editId = (Int64)id;
                    misc = service.GetGlobalSelectOne<MiscellaneousHeadMaster_MISC>("MiscellaneousHeadMaster_MISC", "MISC_Id", editId);

                }

                return View(misc);
            }
            public ActionResult MiscellaneousHeadList()
            {
                var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
                GetRights(url);
                MiscellaneousHeadMaster_MISC mlist = new MiscellaneousHeadMaster_MISC();
                return View(mlist);
            }
        #endregion

        #region Teaching Aid Master
        public ActionResult TeachingAid(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/TeachingAid");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("TeachingAidList", "Masters");
            }
            TeachingAid_TA teaching = new TeachingAid_TA();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                teaching = service.GetGlobalSelectOne<TeachingAid_TA>("TeachingAid_TA", "TA_TeachingAid_Id", editId);

            }

            return View(teaching);
        }
            public ActionResult TeachingAidList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            TeachingAid_TA Tlist = new TeachingAid_TA();
            return View(Tlist);
        }
        #endregion

        #endregion
        #region Transaction
        #region Configuration
        #region Class Wise Faculty Master
        public ActionResult ClassWiseFaculty(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassWiseFaculty");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassWiseFacultyList", "Masters");
            }
            ClassWiseFacultyMasters_CWF data = new ClassWiseFacultyMasters_CWF();
            Int64 editId = 0;
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWF_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.CWF_FacId = new SelectList(FacultyList, "FP_Id", "FP_Name");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWF_SubjectId = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
            }
            if (id != null)
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;
                data = service.GetGlobalSelectOne<ClassWiseFacultyMasters_CWF>("ClassWiseFacultyMasters_CWF", "CWF_Id", editId);
                ViewBag.DataList = service.GetGlobalSelect<ClassWiseFacultyMasters_CWF>("vw_ClasswiseFacultyMasters", "CWF_FacId", data.CWF_FacId);
                ViewBag.Class_Id = data.CWF_ClassId;
                ViewBag.SecId = data.CWF_SectionId;
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWF_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.CWF_FacId = new SelectList(FacultyList, "FP_Id", "FP_Name");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWF_SubjectId = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
            }

            return View(data);
        }
        public ActionResult ClassWiseFacultyList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassWiseFacultyMasters_CWF list = new ClassWiseFacultyMasters_CWF();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CWF_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.CWF_SubjectId = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
            var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            ViewBag.CWF_FacId = new SelectList(FacultyList, "FP_Id", "FP_Name");
            return View(list);
        }
        #endregion
        #region HostelSetting
        public ActionResult HostelSetting(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/HostelSetting");
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var RoomList = service.GetGlobalSelect<HostelRoomMaster_HR>("HostelRoomMaster_HR", "HR_HostelSchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.ddlHostelRoomNo = new SelectList(RoomList, "HR_HostelRoomId", "HR_HostelRoomNo");
            StudetDetails_SD data = new StudetDetails_SD();

            return View(data);
        }
        public ActionResult HostelAvailedList()
        {
            if (UserModel == null) return returnLogin("~/Masters/HostelSetting");
            //if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            //{
            //    TempData["_Message"] = "You are not authorized..!!";
            //    return RedirectToAction("TransportSettingList", "Masters");
            //}
            StudetDetails_SD data = new StudetDetails_SD();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(data);
        }
        #endregion
        #region TransportSetting
        public ActionResult TransportSetting(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/TransportSetting");
            StudetDetails_SD data = new StudetDetails_SD();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var TransportsTypes = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "TypeId", null);
            ViewBag.SD_TransportTypeId = new SelectList(TransportsTypes, "TypeId", "TransportType");
            var Routes = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            ViewBag.SD_RouteId = new SelectList(Routes, "RT_ROUTEID", "RT_ROUTENAME");
            var Monthlist = service.GetGlobalSelect<MonthMaster_MM>("MonthMaster_MM", "MonthId", null);
            ViewBag.ddlMonth = new SelectList(Monthlist, "MonthId", "MonthName");
            return View(data);
        }
        public ActionResult TransportAvailedList(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/TransportSetting");
            //if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            //{
            //    TempData["_Message"] = "You are not authorized..!!";
            //    return RedirectToAction("TransportSettingList", "Masters");
            //}
            StudetDetails_SD data = new StudetDetails_SD();
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.SD_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(data);
        }
        #endregion
        #region Class Wise Subject Master
        public ActionResult ClassWiseSubject(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassWiseSubject");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassWiseSubjectList", "Masters");
            }
            ClsSubGrWiseSubSetting_CSGWS data = new ClsSubGrWiseSubSetting_CSGWS();
            Int64 editId = 0;
            if (id == null)
            {
                var GroupList = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CSGWS_SubGr_Id = new SelectList(GroupList, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
                var SubjectList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CSGWS_Sub_Id = new SelectList(SubjectList, "SBM_Id", "SBM_SubjectName");
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CSGWS_Class_Id = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");

            }
            else
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;

                data = service.GetGlobalSelectOne<ClsSubGrWiseSubSetting_CSGWS>("ClsSubGrWiseSubSetting_CSGWS", "CSGWS_Id", editId);
                ViewBag.DataList = service.GetGlobalSelect<ClsSubGrWiseSubSetting_CSGWS>("vw_ClassWiseSubjectMasters", "CSGWS_Class_Id", data.CSGWS_Class_Id);
                ViewBag.Class_Id = data.CSGWS_Class_Id;
                var GroupList = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", null);
                ViewBag.CSGWS_SubGr_Id = new SelectList(GroupList, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
                var SubjectList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CSGWS_Sub_Id = new SelectList(SubjectList, "SBM_Id", "SBM_SubjectName");
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CSGWS_Class_Id = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            }

            return View(data);
        }
        public ActionResult ClassWiseSubjectList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClsSubGrWiseSubSetting_CSGWS list = new ClsSubGrWiseSubSetting_CSGWS();
            return View(list);
        }
        #endregion
        #region Class Wise Fees
        public ActionResult ClassWiseFees(decimal? id, int? ClassId, int? CF_CATEGORYID, int? CF_FEESID, int? CF_NOOFINS)
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassWiseFees");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassWiseFeesList", "Masters");
            }
            ClassWisefees_CF data = new ClassWisefees_CF();
            Int64 editId = 0;
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CF_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var CategoryList = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CF_CATEGORYID = new SelectList(CategoryList, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY");
                var FeesHeadList = service.GetGlobalSelect<FeesMaster_FEM>("FeesMaster_FEM", "FEM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CF_FEESID = new SelectList(FeesHeadList, "FEM_FEESID", "FEM_FEESNAME");
            }
            if (id != null)
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;
                //data = service.GetGlobalSelectOne<ClassWisefees_CF>("ClassWisefees_CF", "CF_CLASSFEESID", editId);
                ViewBag.CF_CLASSFEESID = id;
                ViewBag.Class = ClassId;

                var SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
                var SessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);

                var datalist = service.GetClassWiseFeesList(SchoolId, SessionId, ClassId).ToList();
                ViewBag.DataList = datalist.Where(r => r.CF_CATEGORYID == CF_CATEGORYID && r.CF_FEESID == CF_FEESID).ToList();

                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CF_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", ClassId);

                var CategoryList = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CF_CATEGORYID = new SelectList(CategoryList, "CAT_CATEGORYID", "CAT_STUDENTCATEGORY", CF_CATEGORYID);

                var FeesHeadList = service.GetGlobalSelect<FeesMaster_FEM>("FeesMaster_FEM", "FEM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CF_FEESID = new SelectList(FeesHeadList, "FEM_FEESID", "FEM_FEESNAME", CF_FEESID);
                data.CF_INSTALLMENTNO = CF_NOOFINS;

            }

            return View(data);
        }
        public ActionResult ClassWiseFeesList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassWisefees_CF list = new ClassWisefees_CF();
            return View(list);
        }
        #endregion

        #region Class Fees Forward
        public ActionResult ClassFeesForward()
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassFeesForward");
            ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
            var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            ViewBag.Pro_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        #endregion
        #region Class Type Board Mapping Master
        public ActionResult ClassTypeBoardMapping(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/ClassTypeBoardMapping");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassTypeBoardMappingList", "Masters");
            }
            ClassTypeBoard_CTB data = new ClassTypeBoard_CTB();
            Int64 editId = 0;
            if (id == null)
            {
                var TypeList = service.GetGlobalSelect<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", null);
                ViewBag.CTB_TYPEID = new SelectList(TypeList, "CTM_TYPEID", "CTM_TYPENAME");
                var BoardList = service.GetGlobalSelect<BoardMasters_BM>("BoardMasters_BM", "BM_BOARDID", null);
                ViewBag.CTB_BOARDID = new SelectList(BoardList, "BM_BOARDID", "BM_BOARDNAME");

            }
            else
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<ClassTypeBoard_CTB>("ClassTypeBoard_CTB", "CTB_SCHBRDID", editId);
                var TypeList = service.GetGlobalSelect<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", null);
                ViewBag.CTB_TYPEID = new SelectList(TypeList, "CTM_TYPEID", "CTM_TYPENAME", data.CTB_TYPEID);
                var BoardList = service.GetGlobalSelect<BoardMasters_BM>("BoardMasters_BM", "BM_BOARDID", null);
                ViewBag.CTB_BOARDID = new SelectList(BoardList, "BM_BOARDID", "BM_BOARDNAME", data.CTB_BOARDID);
            }

            return View(data);
        }
        public ActionResult ClassTypeBoardMappingList()
        {

            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            PeriodMaster_PM list = new PeriodMaster_PM();
            return View(list);
        }
        #endregion
        #region Routine Master
        public ActionResult Routine(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/Routine");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("RoutineList", "Masters");
            }
            ClassWiseTeacherRoutine_CWTR data = new ClassWiseTeacherRoutine_CWTR();
            Int64 editId = 0;
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWTR_Class = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.CWTR_Teacher = new SelectList(FacultyList, "FP_Id", "FP_Name");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.SBM_SubjectName).ToList();
                ViewBag.CWTR_Subject = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
                var SecList = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM", "SECM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWTR_Section = new SelectList(SecList, "SECM_SECTIONID", "SECM_SECTIONNAME");
                var dayList = service.GetGlobalSelect<DayMaster_DM>("DayMaster_DM", "DM_DayId", null);
                ViewBag.CWTR_Day = new SelectList(dayList, "DM_DayId", "DM_DayName");
                //var Periods = service.GetGlobalSelect<PeriodMaster_PM>("PeriodMaster_PM", "PM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                //ViewBag.Period = new SelectList(Periods, "PM_Id", "PM_Period");
            }
            if (id != null)
            {
                editId = (Int64)id;
                ViewBag.FilterId = id;
                data = service.GetGlobalSelectOne<ClassWiseTeacherRoutine_CWTR>("ClassWiseTeacherRoutine_CWTR", "CWTR_Id", editId);
                // var r = service.GetGlobalSelect<ClassWiseTeacherRoutine_CWTR>("vw_RoutineMasters", "CWTR_Section", data.CWTR_Section);
                ViewBag.DataList = service.GetGlobalSelect<ClassWiseTeacherRoutine_CWTR>("vw_RoutineMasters", "CWTR_Section", data.CWTR_Section);

                ViewBag.Class = data.CWTR_Class;
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWTR_Class = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
                ViewBag.CWTR_Teacher = new SelectList(FacultyList, "FP_Id", "FP_Name");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.SBM_SubjectName).ToList();
                ViewBag.CWTR_Subject = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
                var SecList = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM", "SECM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWTR_Section = new SelectList(SecList, "SECM_SECTIONID", "SECM_SECTIONNAME");
                var dayList = service.GetGlobalSelect<DayMaster_DM>("DayMaster_DM", "DM_DayId", null);
                ViewBag.CWTR_Day = new SelectList(dayList, "DM_DayId", "DM_DayName");
                //var Periods = service.GetGlobalSelect<PeriodMaster_PM>("PeriodMaster_PM", "PM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                //ViewBag.Period = new SelectList(Periods, "PM_Id", "PM_Period");
            }

            return View(data);
        }
        public ActionResult RoutineList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassWiseTeacherRoutine_CWTR list = new ClassWiseTeacherRoutine_CWTR();
            return View(list);
        }
        #endregion
        #region Student Wise Subject
        public ActionResult StudentWiseSubject(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/StudentWiseSubject");
            StudetDetails_SD data = new StudetDetails_SD();
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SWS_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var SubjcetGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.ddlSubGroup = new SelectList(SubjcetGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
            }
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<StudetDetails_SD>("StudetDetails_SD", "SD_Id", editId);
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SWS_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var SubjcetGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.ddlSubGroup = new SelectList(SubjcetGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
            }

            return View(data);
        }
        public ActionResult StudentWiseSubjectSetting(string id)
        {
            if (UserModel == null) return returnLogin("~/Masters/StudentWiseSubject");
            StudentwiseSubjectSetting_SWS data = new StudentwiseSubjectSetting_SWS();

            string editId = "";
            if (id != null)
            {
                editId = (string)id;
                var DataList = service.GetIndividualStudentWiseSubjectForEdit(editId).ToList();
                ViewBag.DataList = DataList;
                var SubjcetGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.SWS_SubGroupId = new SelectList(SubjcetGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
                data.SWS_ClassId = DataList.FirstOrDefault().SWS_ClassId;
                data.SWS_StudentSID = DataList.FirstOrDefault().SWS_StudentSID;
            }

            return View(data);
        }
        public ActionResult StudentWiseSubjectList()
        {
            StudentwiseSubjectSetting_SWS list = new StudentwiseSubjectSetting_SWS();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SWS_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region Section Roll Setting
        public ActionResult SectionRollSetting(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/SectionRollSetting");
            Sec_Roll_Setting_SR data = new Sec_Roll_Setting_SR();


            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SR_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");

            }
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<Sec_Roll_Setting_SR>("Sec_Roll_Setting_SR", "SD_Id", editId);
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            }

            return View(data);
        }
        #endregion
        #region Class Wise Syllabus Master
        public ActionResult ClassWiseSyllabus(decimal? id)
        {

            if (UserModel == null) return returnLogin("~/Masters/ClassWiseSyllabus");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("ClassWiseSyllabusList", "Masters");
            }
            ClassWiseSyllabusMasters_CWSM syllabus = new ClassWiseSyllabusMasters_CWSM();
            Int64 editId = 0;
            if (id == null)
            {

                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SM_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");


            }
            else
            {
                editId = (Int64)id;
                syllabus = service.GetGlobalSelectOne<ClassWiseSyllabusMasters_CWSM>("SyllabusMaster_SM", "SM_ID", editId);
                var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.SM_ClassId = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", syllabus.SM_ClassId);
            }
            return View(syllabus);
        }
        public ActionResult ClassWiseSyllabusList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassWiseSyllabusMasters_CWSM list = new ClassWiseSyllabusMasters_CWSM();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.SM_ClassId = new SelectList(sortedClassList, "CM_CLASSNAME", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region ClassWiseAssignmentMaster
        public ActionResult ClassWiseAssignment(decimal? id)
        {
            if (UserModel == null)
            return returnLogin("~/Masters/ClassWiseAssignment");
            AssignmentMaster_ASM data = new AssignmentMaster_ASM();
            var facultyRaw = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            var facultySelect = facultyRaw.Select(f => new SelectListItem{Value = f.FP_Id.ToString(),Text = f.FP_Name ?? ""}).ToList();
            ViewBag.FacultySelect = facultySelect;

            //  Logged faculty
            long? loggedFacultyId = UserModel.UM_FP_ID;
            if (loggedFacultyId != null && loggedFacultyId > 0)
            {
                data.ASM_FP_Id = loggedFacultyId.Value;
                ViewBag.IsFacultyReadonly = true;
            }
            else
            {
                ViewBag.IsFacultyReadonly = false;
            }

            //  Dropdown data
            var classList = service.GetGlobalSelect<ClassMaster_CM>( "ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = classList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ASM_Class_ID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            var subjectGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            ViewBag.ASM_SubGr_ID = new SelectList(subjectGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
            var subList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.ASM_Sub_ID = new SelectList(subList, "SBM_Id", "SBM_SubjectName");

            //  EDIT MODE
            if (id != null)
            {
                long editId = Convert.ToInt64(id);
                data = service.GetGlobalSelectOne<AssignmentMaster_ASM>("AssignmentMaster_ASM", "ASM_ID", editId);
            }
            return View(data);
        }

        public ActionResult ClassWiseAssignmentList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            AssignmentMaster_ASM list = new AssignmentMaster_ASM();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.ASM_Class_ID = new SelectList(sortedClassList, "CM_CLASSNAME", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region ClasswiseLiveclass
        public ActionResult ClassWiseLiveclass(decimal? id)
        {

            if (UserModel == null) return returnLogin("~/Masters/ClassWiseLiveclass");
            ClassWiseLiveclass_CWLS data = new ClassWiseLiveclass_CWLS();
            if (id == null)
            {
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWLS_Class_ID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var SubjcetGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWLS_SubGr_ID = new SelectList(SubjcetGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWLS_Sub_ID = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");

            }
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                data = service.GetGlobalSelectOne<ClassWiseLiveclass_CWLS>("ClassWiseLiveclass_CWLS", "CWLS_ID", editId);
                var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
                ViewBag.CWLS_Class_ID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                var SubjcetGroup = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWLS_SubGr_ID = new SelectList(SubjcetGroup, "SGM_SubjectGroupID", "SGM_SubjectGroupName");
                var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
                ViewBag.CWLS_Sub_ID = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
            }

            return View(data);
        }
        public ActionResult ClassWiseLiveclassList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            ClassWiseLiveclass_CWLS list = new ClassWiseLiveclass_CWLS();
            var ClassList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = ClassList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            ViewBag.CWLS_Class_ID = new SelectList(sortedClassList, "CM_CLASSNAME", "CM_CLASSNAME");
            return View(list);
        }
        #endregion
        #region Form Master
        public ActionResult FormMaster(decimal? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/FormMaster");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("FormMasterList", "Masters");
            }
            FormMaster_FM form = new FormMaster_FM();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                form = service.GetGlobalSelectOne<FormMaster_FM>("FormMaster_FM", "FM_Form_Id", editId);

            }

            return View(form);
        }
        public ActionResult FormMasterList()
        
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            FormMaster_FM flist = new FormMaster_FM();
            return View(flist);
        }
        #endregion
        #region StudyMaterial

        #endregion
        #region LessonPlan
        public ActionResult LeassonPlan(decimal? id)
        {
            if (UserModel == null)
                return returnLogin("~/Masters/LeassonPlan");

            var data = new LessonPlan_LP();
            var facultyRaw = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            var classRaw = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            var sortedClassList = classRaw.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            var facultySelect = facultyRaw.Select(f => new SelectListItem { Value = f.FP_Id.ToString(), Text = f.FP_Name ?? String.Empty }).ToList();
            long? loggedFacultyId = UserModel.UM_FP_ID;
            // Teaching Aid : Filter by School + Session
            var teachingAidRaw = service.GetGlobalSelect<TeachingAid_TA>("TeachingAid_TA", "TA_SCM_SchoolID", UserModel.UM_SCM_SCHOOLID)
                                        .Where(x => x.TA_SM_SessionID == UserModel.UM_SCM_SESSIONID)
                                        .ToList();

            var teachingAidSelect = teachingAidRaw.Select(t => new SelectListItem

            {
                Value = t.TA_TeachingAid_Id.ToString(),
                Text = t.TA_TeachingAidName
            })
                    .ToList();

            ViewBag.TeachingAidList = teachingAidSelect;



            if (id == null)
            {
                ViewBag.LP_ClassId = new MultiSelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
                ViewBag.FacultySelect = facultySelect;

                if (loggedFacultyId != null && loggedFacultyId > 0)
                {
                    data.LP_FacultyID = loggedFacultyId;
                    ViewBag.IsFacultyReadonly = true;
                }
                else
                {
                    ViewBag.IsFacultyReadonly = false;
                }

                return View(data);
            }

            long editId = Convert.ToInt64(id);
            data = service.GetGlobalSelectOne<LessonPlan_LP>("LessonPlan_LP", "LP_Id", editId);
            var selectedClassIds = string.IsNullOrEmpty(data.LP_ClassId) ? new List<long>() : data.LP_ClassId.Split(',').Select(long.Parse).ToList();
            ViewBag.LP_ClassId = new MultiSelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME", selectedClassIds);
            ViewBag.FacultySelect = facultySelect;
            if (loggedFacultyId != null && loggedFacultyId > 0)
            {
                data.LP_FacultyID = loggedFacultyId;
                ViewBag.IsFacultyReadonly = true;
            }
            else
            {
                ViewBag.IsFacultyReadonly = false;
            }

            return View(data);
        }

        public ActionResult LessonPlanList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            LessonPlan_LP list = new LessonPlan_LP();
            var SubList = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            ViewBag.LP_SubjectId = new SelectList(SubList, "SBM_Id", "SBM_SubjectName");
            var FacultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            ViewBag.LP_FacultyID = new SelectList(FacultyList, "FP_Id", "FP_Name");
            return View(list);
        }


        #endregion
        #region ClassSecFaculty

        public ActionResult ClassSecFaculty()
        {
            if (UserModel == null)
                return returnLogin("~/Masters/ClassSecFaculty");

            // Class List
            var classList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM","CM_SCM_SCHOOLID",UserModel.UM_SCM_SCHOOLID);
            ViewBag.ClassList = classList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            // Section List
            ViewBag.SectionList = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM","SECM_SCM_SCHOOLID",UserModel.UM_SCM_SCHOOLID);

            // Faculty List
            var facultyList = service.GetGlobalSelect<FacultyProfileMasters_FPM>("FacluttyProfileMasters_FPM", "FP_SchoolId",UserModel.UM_SCM_SCHOOLID).OrderBy(f => f.FP_Name).ToList();
            ViewBag.FacultyList = new SelectList( facultyList, "FP_Id","FP_Name");

            return View();
        }

        #endregion
        #endregion
        #endregion
        #region StudentPortal StudentApplication
        public ActionResult StudentApproval()
        {
            return View();
        }
        public ActionResult ApprovalList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            //StudentApplication_AP approvallist = new StudentApplication_AP();
            //var Classlist = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            //var sortedClassList = Classlist.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();

            //ViewBag.CM_CLASSID = new SelectList(sortedClassList, "CM_CLASSID", "CM_CLASSNAME");
            return View();
        }
        #endregion


        #region User Management Master
        public ActionResult User(long? id)
        {
            if (UserModel == null) return returnLogin("~/Masters/User");
            if ((bool)System.Web.HttpContext.Current.Session["CanAdd"] != true)
            {
                TempData["_Message"] = "You are not authorized..!!";
                return RedirectToAction("UserMasterList", "Masters");
            }
            UserMaster_UM data = new UserMaster_UM();
            Int64 editId = 0;
            if (id == null)
            {
                var RoleList = service.GetGlobalSelect<RoleMasterModel>("RoleMasters", "RoleId", null);
                ViewBag.UM_ROLEID = new SelectList(RoleList, "RoleId", "RoleName");

                var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", null);
                ViewBag.UM_SCM_SCHOOLID = new SelectList(SchoolList, "SCM_SCHOOLID", "SCM_SCHOOLNAME");
            }
            else
            {
                editId = (Int64)id;

                data = service.GetGlobalSelectOne<UserMaster_UM>("UserMaster_UM", "UM_USERID", editId);

                var RoleList = service.GetGlobalSelect<RoleMasterModel>("RoleMasters", "RoleId", null);
                ViewBag.UM_ROLEID = new SelectList(RoleList, "RoleId", "RoleName", data.UM_ROLEID);
                //var DesigList = service.GetGlobalSelect<DesignationMaster_DM>("DesignationMaster_DM", "DM_Id", null);

                //ViewBag.FP_DesignationId = new SelectList(DesigList, "DM_Id", "DM_Name", data.FP_DesignationId);
                var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", null);
                ViewBag.UM_SCM_SCHOOLID = new SelectList(SchoolList, "SCM_SCHOOLID", "SCM_SCHOOLNAME",data.UM_SCM_SCHOOLID);
            }

            return View(data);


        }
        public ActionResult UserMasterList()
        {
            var url = Request.RequestContext.HttpContext.Request.Url.AbsolutePath;
            GetRights(url);
            UserMaster_UM list = new UserMaster_UM();

            //// EMAIL CONTENT

            // string subject = "User Login Credential Details";
            // string message =
            //     "Dear " + list.UM_USERNAME + "<br/><br/>" +
            //     "Your registration has been completed successfully.<br/><br/>" +
            //     "<b>Login ID:</b> " + list.UM_LOGINID + "<br/>" +
            //     "<b>Password:</b> " + list.UM_PASSWORD + "<br/><br/>" +
            //     "Please keep this information safe.<br/><br/>" +
            //     "Regards,<br/>" +
            //     "School Administration";

            // Utils.sendEmail(
            //     list.UM_USEREMAIL,
            //     subject,
            //     message,
            //     false,
            //     false
            //     );

            return View(list);
        }
        #endregion
    }
}

