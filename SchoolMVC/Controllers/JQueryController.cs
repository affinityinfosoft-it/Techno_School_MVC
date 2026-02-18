using BussinessObject;
using BussinessObject.FeesCollection;
using System.Data;
using System.Data.SqlClient;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;


namespace SchoolMVC.Controllers
{
    public class JQueryController : BaseController
    {
        private long? PM_Id;
        public static UserMaster_UM LoginUModel { get; set; }
        // GET: JQuery
        #region Login
        [HttpPost]
        public JsonResult GetLogin(UserMaster_UM user)
        {
            StatusResponse Status = new StatusResponse();
            //try
            //{
            var login = service.GetLogin(user);
            if (login.UM_LOGINID != null)
            {
                UserMaster_UM users = new UserMaster_UM();
                users.UM_USERID = login.UM_USERID;
                users.UM_LOGINID = login.UM_LOGINID;
                users.UM_USERTYPE = login.UM_USERTYPE;
                users.UM_USERNAME = login.UM_USERNAME;
                users.UM_SCM_SCHOOLID = login.UM_SCM_SCHOOLID;
                //Add on 30/11/18
                users.UM_ROLEID = login.UM_ROLEID;
                //add by uttaran get facultyid
                users.UM_FP_ID = login.UM_FP_ID;
                //
                users.Schoollist = login.Schoollist;
                users.Sessionlist = login.Sessionlist;
                System.Web.HttpContext.Current.Session["UserLoginModel"] = users;
            }
            return Json(login, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region LoginSelectSchoolAndSession
        [HttpPost]
        public JsonResult SelectSchoolAndSession(UserMaster_UM user)
        {
            LoginUModel = (UserMaster_UM)System.Web.HttpContext.Current.Session["UserLoginModel"];
            if (LoginUModel.UM_LOGINID != null)
            {
                UserMaster_UM users = new UserMaster_UM();
                users.UM_USERID = LoginUModel.UM_USERID;
                users.UM_LOGINID = LoginUModel.UM_LOGINID;
                users.UM_USERTYPE = LoginUModel.UM_USERTYPE;
                users.UM_USERNAME = LoginUModel.UM_USERNAME;
                users.UM_SCM_SCHOOLID = user.UM_SCM_SCHOOLID;
                //var SList = service.GetGlobalSelect<SchoolMasters_SCM>("SchoolMasters_SCM", "SCM_SCHOOLID", user.UM_SCM_SCHOOLID);
                //user.UM_SCHOOLNAME = SList;
                users.UM_SCM_SESSIONID = user.UM_SCM_SESSIONID;
                //
                users.UM_ROLEID = LoginUModel.UM_ROLEID;
                //
                users.UM_FP_ID = LoginUModel.UM_FP_ID;
                users.Schoollist = LoginUModel.Schoollist;
                users.Sessionlist = LoginUModel.Sessionlist;
                System.Web.HttpContext.Current.Session["UserModel"] = users;
                //System.Web.HttpContext.Current.Session["UserLoginModel"] = null;
                response.ExMessage = "";
                response.IsSuccess = true;
                response.Message = "SUCCESS";
            }
            else
            {
                response.ExMessage = "";
                response.IsSuccess = false;
                response.Message = "Invalid";
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Common Delete Action
        [HttpPost]
        public JsonResult DeleteData(string MainTableName, string MainFieldName, long? PId, string TransTableName = null, string TransFieldName = null)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                long id = service.GlobalDelete(MainTableName, MainFieldName, PId, TransTableName, TransFieldName);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region BindDropdown
        public JsonResult BindDropdown(string TableName, string MainFieldName, Int64 Pid)
        {

            var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>(TableName, MainFieldName, Pid);
            return Json(SchoolList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SessionMaster
        //public JsonResult InsertUpdateSession(SessionMasters_SM ses)
        //{
        //    ses.SM_CREATEDUID = Convert.ToInt64(UserModel.UM_USERID);
        //    ses.SM_SCM_SCHOOLID = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
        //    string[] SCodes = ses.SM_SESSIONNAME.Split('-');
        //    ses.SM_SESSIONCODE = SCodes[0].Substring(2, 2) + "-" + SCodes[1].Substring(2, 2);
        //    StatusResponse Status = new StatusResponse();
        //    try
        //    {
        //        if (ses.Userid != null)
        //        {
        //            var sess = service.AddEditSession(ses);
        //            if (sess >= 1)
        //            {
        //                Status.ExMessage = "";
        //                Status.IsSuccess = true;
        //                Status.Message = "Operation has been done successfully. ";
        //            }
        //            else
        //            {
        //                Status.Id = -1;
        //                Status.ExMessage = "";
        //                Status.IsSuccess = false;
        //                Status.Message = "Data already exists. ";
        //            }
        //        }

        //        else
        //        {
        //            Status.Id = -1;
        //            Status.ExMessage = "";
        //            Status.IsSuccess = false;
        //            Status.Message = "Something wrong happened. ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Status.Id = -1;
        //        Status.IsSuccess = false;
        //        Status.ExMessage = ex.Message;
        //        Status.Message = "Something wrong happened...";
        //    }
        //    return Json(Status, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult InsertUpdateSession(SessionMasters_SM ses)
        {
            StatusResponse Status = new StatusResponse();

            try
            {
                // Logged-in user validation
                if (UserModel == null)
                {
                    Status.IsSuccess = false;
                    Status.Message = "User session expired. Please login again.";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }

                // Required field validation
                if (string.IsNullOrWhiteSpace(ses.SM_SESSIONNAME))
                {
                    Status.IsSuccess = false;
                    Status.Message = "Session Name is required.";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }

                // Assign audit fields
                ses.SM_CREATEDUID = Convert.ToInt64(UserModel.UM_USERID);
                ses.SM_SCM_SCHOOLID = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);

                // ---------- SAFE SESSION CODE GENERATION ----------
                // Expected format: 2023-2024
                if (!ses.SM_SESSIONNAME.Contains("-"))
                {
                    Status.IsSuccess = false;
                    Status.Message = "Invalid Session Name format. Example: 2023-2024";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }

                string[] years = ses.SM_SESSIONNAME
                                    .Split('-')
                                    .Select(x => x.Trim())
                                    .ToArray();

                if (years.Length != 2 || years[0].Length < 2 || years[1].Length < 2)
                {
                    Status.IsSuccess = false;
                    Status.Message = "Invalid Session Name format. Example: 2023-2024";
                    return Json(Status, JsonRequestBehavior.AllowGet);
                }

                // Always take last 2 digits safely
                ses.SM_SESSIONCODE =
                    years[0].Substring(years[0].Length - 2) + "-" +
                    years[1].Substring(years[1].Length - 2);

                // ---------- INSERT / UPDATE ----------
                if (ses.Userid != null) // Edit or Save
                {
                    var result = service.AddEditSession(ses);

                    if (result >= 1)
                    {
                        Status.IsSuccess = true;
                        Status.Message = "Operation has been done successfully.";
                    }
                    else
                    {
                        Status.IsSuccess = false;
                        Status.Message = "Data already exists.";
                    }
                }
                else
                {
                    Status.IsSuccess = false;
                    Status.Message = "Invalid request.";
                }
            }
            catch (Exception ex)
            {
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Something went wrong while saving session.";
            }

            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSessionList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
            // return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StateMasters
        public JsonResult InsertUpdateState(StateMaster_STM states)
        {
            StatusResponse Status = new StatusResponse();
            try
            {

                if (states.Userid != null)
                {

                    var state = service.InsertUpdateState(states);
                    if (state >= 1)
                    {
                        Status.ExMessage = "";
                        Status.IsSuccess = true;
                        Status.Message = "Operation has been done successfully. ";
                    }
                    else
                    {
                        Status.Id = -1;
                        Status.ExMessage = "";
                        Status.IsSuccess = false;
                        Status.Message = "Data already exixts. ";
                    }
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Something wrong happened. ";
                }

            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StateList()
        {
            var query = service.GetGlobalSelect<StateMaster_STM>("StateMaster_vw", "STM_STATEID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region DistrictMaster
        public JsonResult InsertUpdateDistrict(DistrictMasters_DM district)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (district.Userid != null)
                {

                    var districts = service.InsertUpdateDistrict(district);
                    if (districts >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts.";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DistrictList()
        {
            var query = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMaster_vw", "DM_DISTRICTID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TermMaster
        public JsonResult InsertUpdateTerm(TermMaster_TM term)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (term.Userid != null)
                {

                    term.TM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    term.TM_SessionId = UserModel.UM_SCM_SESSIONID;
                    var terms = service.InsertUpdateTerm(term);
                    if (terms >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }

                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }

                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TermList()
        {
            var query = service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClassTypeMaster
        public JsonResult InsertUpdateClassType(ClassTypeMaster_CTM Ct)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (Ct.Userid != null)
                {

                    Ct.CTM_CREATEDUID = UserModel.UM_USERID;
                    var terms = service.InsertUpdateClassType(Ct);
                    if (terms >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exixts. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassTypeList()
        {
            ///it is global
            //var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<ClassTypeMaster_CTM>("ClassTypeMaster_CTM", "CTM_TYPEID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClassMaster
        public JsonResult InsertUpdateClass(ClassMaster_CM Cm)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (Cm.Userid != null)
                {

                    Cm.CM_CREATEDUID = UserModel.UM_USERID;
                    Cm.CM_SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    var classes = service.InsertUpdateClass(Cm);
                    if (classes >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassList()
        {
            ClassMaster_CM cm = new ClassMaster_CM();
            var schoolId = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_vw", "CM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            cm.CM_SCM_SCHOOLID = schoolId;
            //return Json(new { Data = query}, JsonRequestBehavior.AllowGet);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetClassDdl(long schoolId)
        {
            //var classList = service.GetGlobalSelect<GetClassDdl>("ClassMaster_CM", "CM_CLASSID", schoolId);
            //return Json(classList, JsonRequestBehavior.AllowGet);

            var classList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", schoolId);
            return Json(classList, JsonRequestBehavior.AllowGet);

        }

        #endregion
        #region SectionMaster
        public JsonResult InsertUpdateSection(SectionMaster_SECM sec)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (sec.Userid != null)
                {

                    sec.SECM_CREATEDUID = UserModel.UM_USERID;
                    sec.SECM_SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    sec.SECM_SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
                    var sections = service.InsertUpdateSection(sec);
                    if (sections >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SectionList()
        {
            var query = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_vw", "SECM_SCM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region BoardMaster
        public JsonResult InsertUpdateBoard(BoardMasters_BM board)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (board.Userid != null)
                {

                    board.BM_CREATEDUID = UserModel.UM_USERID;
                    var boards = service.InsertUpdateBoard(board);
                    if (boards >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BoardList()
        {
            /////IT IS GLOBAL
            var query = service.GetGlobalSelect<BoardMasters_BM>("BoardMasters_BM", "BM_BOARDID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region CasteMaster
        public JsonResult InsertUpdateCaste(CasteMaster_CSM caste)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (caste.Userid != null)
                {

                    caste.CSM_CREATEDUID = UserModel.UM_USERID;
                    var castes = service.InsertUpdateCaste(caste);
                    if (castes >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CasteList()
        {
            /////IT IS GLOBAL
            var query = service.GetGlobalSelect<CasteMaster_CSM>("CasteMaster_CSM", "CSM_CASTEID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region VernacularMaster
        public JsonResult InsertUpdateVernacular(VernacularMaster_VM vm)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (vm.Userid != null)
                {

                    vm.VM_CREATEDUID = UserModel.UM_USERID;
                    var vms = service.InsertUpdateVernacular(vm);
                    if (vms >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult VernacularList()
        {
            /////IT IS GLOBAL
            var query = service.GetGlobalSelect<VernacularMaster_VM>("VernacularMaster_VM", "VM_VERNACULARID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region HouseMaster
        public JsonResult InsertUpdateHouse(HouseMasters_HM house)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (house.Userid != null)
                {

                    house.HM_CREATEDUID = UserModel.UM_USERID;
                    house.HM_SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    var houses = service.InsertUpdateHouse(house);
                    if (houses >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult HouseList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<HouseMasters_HM>("HouseMasters_HM", "HM_SCM_SCHOOLID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StreamMaster
        public JsonResult InsertUpdateStream(StreamMasters_STRM stream)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (stream.Userid != null)
                {

                    stream.STRM_CREATEDUID = UserModel.UM_USERID;
                    stream.STRM_SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    stream.STRM_SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
                    var streams = service.InsertUpdateStream(stream);
                    if (streams >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StreamList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<StreamMasters_STRM>("StreamMaster_vw", "STRM_SCM_SCHOOLID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region BloodGroupMaster
        public JsonResult InsertUpdateBloodGroup(BloodGroupMasters_BGM bloodgrp)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (bloodgrp.Userid != null)
                {

                    bloodgrp.BGM_CREATEDUID = UserModel.UM_USERID;
                    var blood = service.InsertUpdateBloodGroup(bloodgrp);
                    if (blood >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BloodList()
        {
            ////IT IS GLOBAL
            var query = service.GetGlobalSelect<BloodGroupMasters_BGM>("BloodGroupMasters_BGM", "BGM_BLDGRPID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GradeMaster
        public JsonResult InsertUpdateGrade(GradeMaster_GM grade)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (grade.Userid != null)
                {

                    grade.GM_SCM_SchoolID = UserModel.UM_SCM_SCHOOLID;
                    grade.GM_SM_SessionID = UserModel.UM_SCM_SESSIONID;
                    var blood = service.InsertUpdateGrade(grade);
                    if (blood >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GradeList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<GradeMaster_GM>("GradeMaster_GM", "GM_SCM_SchoolID", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region HolidayMaster
        public JsonResult InsertUpdateHoliday(HolidayMaster_HM holiday)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (holiday.Userid != null)
                {

                    holiday.HM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    holiday.HM_SessionId = UserModel.UM_SCM_SESSIONID;
                    var blood = service.InsertUpdateHoliday(holiday);
                    if (blood >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult HolidayList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;
            var allNotices = service.GetGlobalSelect<HolidayMaster_HM>("HolidayMaster_HM", "HM_SchoolId", Schoolid);
            var filteredHoliday = allNotices.Where(x => x.HM_SessionId == SessionId).ToList();
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = filteredHoliday, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region NoticeMaster
        [HttpPost]
        public JsonResult InsertUpdateNotice(NoticeMasters_NM notice)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                if (notice.Userid != null)
                {
                    // Assign current user-related info
                    notice.NM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    notice.NM_SessionId = UserModel.UM_SCM_SESSIONID;
                    notice.NM_CreatedBy = UserModel.UM_USERID;

                    long result = service.InsertUpdateNotice(notice);

                    if (result == -11)
                    {
                        // Duplicate found
                        status.Id = -1;
                        status.IsSuccess = false;
                        status.Message = "A notice with the same title for the same class and section already exists.";
                    }
                    else if (result > 0)
                    {
                        // Success
                        status.Id = result;
                        status.IsSuccess = true;
                        status.Message = "Notice saved successfully.";
                    }
                    else
                    {
                        // Unknown failure
                        status.Id = -1;
                        status.IsSuccess = false;
                        status.Message = "Something went wrong while saving the notice.";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "Unauthorized user or session expired.";
                }
            }
            catch (Exception ex)
            {
                status.Id = -1;
                status.IsSuccess = false;
                status.ExMessage = ex.Message;
                status.Message = "An error occurred while processing the request.";
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult NoticeList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;
            var filteredNotices = service.GetGlobalSelect<NoticeMasters_NM>("vw_ClassWiseNoticeList", "NM_SchoolId", Schoolid)
                                .Where(x => x.NM_SessionId == SessionId)
                                .ToList();
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];

            return Json(new { Data = filteredNotices, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SearchStudentList(string searchTerm, long? schoolId, long? sessionId)
        {
            try
            {
                var students = service.SearchStudents(searchTerm, schoolId, sessionId);

                var result = students.Select(s => new
                {
                    SD_StudentId = s.SD_StudentId,
                    SD_AdmissionNo = s.SD_AdmissionNo,
                    SD_StudentName = s.SD_StudentName,
                    ClassName = s.ClassName,
                    SectionName = s.SectionName
                }).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpGet]
        public JsonResult GetClassesByFaculty(long? facultyId)
        {
            if (UserModel == null)
                return Json(new { success = false, message = "Session expired." }, JsonRequestBehavior.AllowGet);

            // If logged-in user is a faculty, force using their FP_ID (overrides any client value)
            if (UserModel.UM_FP_ID.HasValue && UserModel.UM_FP_ID.Value > 0)
            {
                facultyId = UserModel.UM_FP_ID.Value;
            }
            else
            {
                // else use passed-in value (admin case)
                if (!facultyId.HasValue || facultyId.Value == 0)
                {
                    return Json(new { success = true, data = new List<object>() }, JsonRequestBehavior.AllowGet);
                }
            }

            // now facultyId is reliable
            var effectiveFacultyId = facultyId.Value;

            // mappings
            var allMappings = service.GetGlobalSelect<ClassWiseFacultyMasters_CWF>(
                "ClassWiseFacultyMasters_CWF",
                "CWF_SchoolId",
                UserModel.UM_SCM_SCHOOLID
            );

            var sessionId = UserModel.UM_SCM_SESSIONID;
            var mappedClassIds = allMappings
                .Where(x => x.CWF_FacId == effectiveFacultyId && x.CWF_SessionId == sessionId)
                .Select(x => x.CWF_ClassId).Distinct().ToList();

            var allClasses = service.GetGlobalSelect<ClassMaster_CM>(
                "ClassMaster_CM",
                "CM_SCM_SCHOOLID",
                UserModel.UM_SCM_SCHOOLID
            );

            var filteredClasses = allClasses
                .Where(c => mappedClassIds.Contains(c.CM_CLASSID))
                .OrderBy(c => c.CM_FROMAGE)
                .Select(c => new { Id = c.CM_CLASSID, Name = c.CM_CLASSNAME })
                .ToList();

            return Json(new { success = true, data = filteredClasses }, JsonRequestBehavior.AllowGet);
        }



        #endregion
        #region PeriodMaster
        public JsonResult InsertUpdatePeriod(PeriodMaster_PM period)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (period.Userid != null)
                {

                    period.PM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    period.PM_SessionId = UserModel.UM_SCM_SESSIONID;
                    period.PM_CreateBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdatePeriod(period);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult PeriodList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<PeriodMaster_PM>("PeriodMaster_vw", "PM_SchoolId", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);


        }


        #endregion
        #region ClassTypeboardMaster
        public JsonResult InsertUpdateClassTypeboard(ClassTypeBoard_CTB classboard)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (classboard.Userid != null)
                {

                    classboard.CTB_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    classboard.CTB_CREATEDUID = UserModel.UM_USERID;
                    var periods = service.InsertUpdateClassTypeboard(classboard);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClasstypeBoardList()
        {
            var query = service.GetGlobalSelect<ClassTypeBoard_CTB>("ClasstypeBoardMaster_vw", "CTB_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region RouteMaster
        public JsonResult InsertUpdateRoute(RouteMastes_RT route)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (route.Userid != null)
                {

                    route.RT_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    route.RT_CREATEDUID = UserModel.UM_USERID;
                    var periods = service.InsertUpdateRoute(route);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RouteList()
        {
            var query = service.GetGlobalSelect<RouteMastes_RT>("RouteMastes_RT", "RT_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region RoutewiseDropMaster
        public JsonResult InsertUpdateRoutewiseDrop(RoutewiseDropMaster_RDM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (obj.Userid != null)
                {

                    obj.RDM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    obj.RDM_CREATEDUID = UserModel.UM_USERID;
                    obj.RDM_SESSIONID = UserModel.UM_SCM_SESSIONID;
                    var getdata = service.InsertUpdateRoutewiseDrop(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RoutewiseDropList()
        {
            var query = service.GetGlobalSelect<RoutewiseDropMaster_RDM>("Routewisedropmaster_vw", "RDM_SCHOOLID", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SubjectGroupMaster
        public JsonResult InsertUpdateSubjectGroup(SubjectGroupMaster_SGM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.SGM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    var getdata = service.InsertUpdateSubjectGroup(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubjectGroupList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<SubjectGroupMaster_SGM>("SubjectGroupMaster_SGM", "SGM_SCHOOLID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SubjectMaster
        public JsonResult InsertUpdateSubject(SubjectMaster_SBM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.SBM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    var getdata = service.InsertUpdateSubject(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SubjectList()
        {
            var query = service.GetGlobalSelect<SubjectMaster_SBM>("SubjectMaster_SBM", "SBM_SchoolId", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClassWiseSubjectMaster
        public JsonResult InsertUpdateClassWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.CSGWS_School_Id = UserModel.UM_SCM_SCHOOLID;

                    var getdata = service.InsertUpdateClassWiseSubject(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseSubjectList()
        {
            var query = service.GetGlobalSelect<ClsSubGrWiseSubSetting_CSGWS>("vw_ClassWiseSubjectMasters", "CSGWS_School_Id", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseSubjectBy_ClassList(int? ClassId)
        {
            var query = service.GetGlobalSelect<ClsSubGrWiseSubSetting_CSGWS>("vw_ClassWiseSubjectMasters", "CSGWS_Class_Id", ClassId);
            return Json(query, JsonRequestBehavior.AllowGet);
            //bool CanEdit = (bool)Session["CanEdit"];
            //bool CanDelete = (bool)Session["CanDelete"];
            //return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region User Master
        public JsonResult InsertUpdateUserMaster(UserMaster_UM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                //if (obj.Userid != null)
                //{
                // obj.UM_SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                obj.UM_CREATEDUID = Convert.ToString(UserModel.UM_USERID);
                //obj.FP_SessionId = UserModel.UM_SCM_SESSIONID;
                var getdata = service.InsertUpdateUserMaster(obj);
                if (getdata >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";

                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "User already exists...";
                }
                //}

                //else
                //{
                //    status.Id = -1;
                //    status.ExMessage = "";
                //    status.IsSuccess = false;
                //    status.Message = "Something wrong happened. ";
                //}
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UserMasterList(UserMaster_UM obj)
        {
            obj.UM_SCM_SCHOOLID = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            //Int32 SessionId = Convert.ToInt32(UserModel.UM_SCM_SESSIONID);
            var query = service.UserMasterList(obj);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion


        #region FacultyMaster
        public JsonResult InsertUpdateFaculty(FacultyProfileMasters_FPM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.FP_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.FP_CreateBy = UserModel.UM_USERID;
                    obj.FP_SessionId = UserModel.UM_SCM_SESSIONID;
                    var getdata = service.InsertUpdateFaculty(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Faculty Code already exists...";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FacultyList()
        {
            var query = service.GetGlobalSelect<FacultyProfileMasters_FPM>("vw_FacultyProfileMasterList", "FP_SchoolId", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClasswiseFacultyMaster
        [HttpPost]
        public JsonResult InsertUpdateClasswiseFaculty(ClassWiseFacultyMasters_CWF obj)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                
                if (UserModel == null)
                {
                    status.IsSuccess = false;
                    status.Message = "Session expired. Please login again.";
                    status.Id = -1;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                if (obj == null || obj.ClasswiseFacultyList == null || obj.ClasswiseFacultyList.Count == 0)
                {
                    status.IsSuccess = false;
                    status.Message = "Please add at least one record.";
                    status.Id = -1;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                if (obj.Userid != null)
                { 

                    obj.CWF_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.CWF_CreateBy = UserModel.UM_USERID;
                    obj.CWF_SessionId = UserModel.UM_SCM_SESSIONID;

                    long getdata = service.InsertUpdateClasswiseFaculty(obj);

                    if (getdata == 1)
                    {
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.";
                        status.Id = 1;
                    }
                    else if (getdata == -1)
                    {
                        status.IsSuccess = false;
                        status.Message = "This teacher is already assigned to this subject in this class.";
                        status.Id = -1;
                    }
                    else
                    {
                        status.IsSuccess = false;
                        status.Message = "Something went wrong.";
                        status.Id = 0;
                    }
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "Invalid user request.";
                    status.Id = -1;
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClassWiseFacultyList(Int64? ClassId, Int64? SubId, Int64? FacId)
        {
            Int32 SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            Int32 SessionId = Convert.ToInt32(UserModel.UM_SCM_SESSIONID);
            var query = service.GetClassWiseFacultyList(SchoolId, SessionId, ClassId, SubId, FacId);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region RoutineMaster
        public JsonResult InsertUpdateRoutine(ClassWiseTeacherRoutine_CWTR obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.CWTR_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.CWTR_CreateBy = UserModel.UM_USERID;
                    obj.CWTR_SessionId = UserModel.UM_SCM_SESSIONID;
                    var getdata = service.InsertUpdateRoutine(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation hes been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RoutineList(int? ClassId)
        {
            long SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            long SessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
            var query = service.GetRoutineList(SchoolId, SessionId, ClassId);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadFacultyByClass(FacultyProfileMasters_FPM obj)
        {
            obj.FP_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.FP_SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.LoadFacultyByClass(obj);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CheckDuplicity(ClassWiseTeacherRoutine_CWTR obj)
        {
            obj.CWTR_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.CWTR_SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.CheckDuplicity(obj);
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public JsonResult GetPeriodByClass(long Id)
        {
            var list = service.GetGlobalSelect<PeriodMaster_PM>(
                "PeriodMaster_PM",
                "PM_ClassId",
                Id
            )
            .OrderBy(x => x.PM_Period)
            .ToList();

            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Student Enquery
        //public JsonResult InsertUpdateEnquery(StudentEnquery_ENQ obj)
        //{
        //    StatusResponse status = new StatusResponse();
        //    try
        //    {
        //        if (obj.Userid != null)
        //        {

        //            obj.ENQ_SchoolId = UserModel.UM_SCM_SCHOOLID;
        //            obj.ENQ_CreatedBy = UserModel.UM_USERID;
        //            obj.ENQ_SessionId = UserModel.UM_SCM_SESSIONID;

        //            var getdata = service.InsertUpdateEnquery(obj);
        //            if (getdata != null && getdata != "")
        //            {
        //                if (obj.ENQ_Id == null)
        //                {
        //                    var msgText = "Thank you for applying with TIGPS. Your Generated Enquiry no. is : " + getdata.ToString() + ".";
        //                    List<SMSBO> smsBOs = new List<SMSBO>();

        //                    if (obj.ENQ_MobNo != null && obj.ENQ_MobNo != "")
        //                    {
        //                        SMSBO smsBO = Utils.sendSMS(obj.ENQ_MobNo, msgText);
        //                        smsBO.trackingCode = getdata.ToString();
        //                        smsBOs.Add(smsBO);
        //                    }
        //                    if (obj.ENQ_AlternativeMobNo != null && obj.ENQ_AlternativeMobNo != "")
        //                    {
        //                        SMSBO smsBO = Utils.sendSMS(obj.ENQ_AlternativeMobNo, msgText);
        //                        smsBO.trackingCode = getdata.ToString();
        //                        smsBOs.Add(smsBO);
        //                    }

        //                    if (smsBOs.Count > 0) { service.SaveSmsTracker(smsBOs); }
        //                }
        //                status.ExMessage = "";
        //                status.IsSuccess = true;
        //                status.Message = "Operation has been done successfully.......";
        //            }
        //            else
        //            {
        //                status.Id = -1;
        //                status.ExMessage = "";
        //                status.IsSuccess = false;
        //                status.Message = "Data already exists. ";
        //            }
        //        }

        //        else
        //        {
        //            status.Id = -1;
        //            status.ExMessage = "";
        //            status.IsSuccess = false;
        //            status.Message = "Something wrong happened. ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status.ExMessage = ex.Message;
        //        status.IsSuccess = false;
        //        status.Message = "Something wrong happened.";
        //        status.Id = -1;
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult InsertUpdateEnquery(StudentEnquery_ENQ obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {
                    obj.ENQ_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.ENQ_CreatedBy = UserModel.UM_USERID;
                    obj.ENQ_SessionId = UserModel.UM_SCM_SESSIONID;

                    // Call the repository method and get both OutputId and EnquiryNo
                    var result = service.InsertUpdateEnquery(obj);
                    string outputId = result.Item1;  // OutputId (could be -1 for duplicate)
                    string enquiryNo = result.Item2;   // EnquiryNo (if inserted successfully)

                    // Check the OutputId to determine if it's a duplicate or a new entry
                    if (outputId == "-1")
                    {
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists for this combination of StudentName, GuardianName, MobNo, and SchoolId.";
                    }
                    else
                    {
                        var msgText = "Thank you for applying with TIGPS. Your Generated Enquiry no. is : " + enquiryNo + ".";
                        List<SMSBO> smsBOs = new List<SMSBO>();

                        // Send SMS to the provided mobile numbers
                        if (obj.ENQ_MobNo != null && obj.ENQ_MobNo != "")
                        {
                            SMSBO smsBO = Utils.sendSMS(obj.ENQ_MobNo, msgText);
                            smsBO.trackingCode = enquiryNo.ToString();
                            smsBOs.Add(smsBO);
                        }
                        if (obj.ENQ_AlternativeMobNo != null && obj.ENQ_AlternativeMobNo != "")
                        {
                            SMSBO smsBO = Utils.sendSMS(obj.ENQ_AlternativeMobNo, msgText);
                            smsBO.trackingCode = enquiryNo.ToString();
                            smsBOs.Add(smsBO);
                        }

                        if (smsBOs.Count > 0)
                        {
                            service.SaveSmsTracker(smsBOs);
                        }

                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully. Your enquiry number is " + enquiryNo + ".";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something went wrong. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something went wrong.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }


        public JsonResult EnqueryList(StudentEnquery_ENQ obj, DateTime? FromDate, DateTime? ToDate)
        {
            obj.ENQ_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.ENQ_SessionId = UserModel.UM_SCM_SESSIONID;
            obj.FromDate = FromDate;
            obj.ToDate = ToDate;
            var query = service.GetEnqueryList(obj);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Student AdmissionDetails
        //public JsonResult InsertUpdateAdmissionDetails(StudetDetails_SD obj)
        //{
        //    StatusResponse status = new StatusResponse();
        //    try
        //    {
        //        if (obj.Userid != null)
        //        {

        //            obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
        //            obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
        //            obj.SD_CreatedBy = UserModel.UM_USERID;

        //            var getdata = service.InsertUpdateAdmissionDetails(obj);
        //            //var result = service.InsertUpdateAdmissionDetails(obj);
        //            //string outputId = result.;  // OutputId (could be -1 for duplicate)
        //            //string enquiryNo = result.Item2;   // EnquiryNo (if inserted successfully)

        //            // Check the OutputId to determine if it's a duplicate or a new entry
        //            if (getdata == "-1")
        //            {
        //                status.ExMessage = "";
        //                status.IsSuccess = false;
        //                status.Message = "Data already exists for this combination of StudentName, GuardianName, MobNo, and School.";
        //            }
        //            if (getdata != null && getdata != "")
        //            {
        //                if (obj.SD_Id == null)
        //                {
        //                    var msgText = "Thank you for applying with TIGPS. Your Generated Admission No. is : " + getdata.ToString() + " against submitted Form No: " + obj.SD_FormNo + ".";
        //                    List<SMSBO> smsBOs = new List<SMSBO>();

        //                    if (obj.SD_ContactNo1 != null && obj.SD_ContactNo1 != "")
        //                    {
        //                        SMSBO smsBO = Utils.sendSMS(obj.SD_ContactNo1, msgText);
        //                        smsBO.trackingCode = getdata.ToString();
        //                        smsBOs.Add(smsBO);
        //                    }
        //                    if (obj.SD_ContactNo2 != null && obj.SD_ContactNo2 != "")
        //                    {
        //                        SMSBO smsBO = Utils.sendSMS(obj.SD_ContactNo2, msgText);
        //                        smsBO.trackingCode = getdata.ToString();
        //                        smsBOs.Add(smsBO);
        //                    }
        //                    if (smsBOs.Count > 0) { service.SaveSmsTracker(smsBOs); }
        //                    status.CanRedirect = true;
        //                }
        //                status.Param = getdata.ToString(); // admissionNo
        //                status.ExMessage = "";
        //                status.IsSuccess = true;
        //                status.Message = "Operation has been done successfully.......";
        //            }
        //            else
        //            {
        //                status.Id = -1;
        //                status.ExMessage = "";
        //                status.IsSuccess = false;
        //                status.Message = "Data already exists. ";
        //            }
        //        }

        //        else
        //        {
        //            status.Id = -1;
        //            status.ExMessage = "";
        //            status.IsSuccess = false;
        //            status.Message = "Something wrong happened. ";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status.ExMessage = ex.Message;
        //        status.IsSuccess = false;
        //        status.Message = "Something wrong happened.";
        //        status.Id = -1;
        //    }
        //    return Json(status, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult InsertUpdateAdmissionDetails(StudetDetails_SD obj)
        {
            var status = new StatusResponse();

            try
            {
                // Basic validation
                if (obj == null || obj.Userid == null)
                {
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened.";
                    status.ExMessage = "";
                    status.Param = "";
                    status.CanRedirect = false;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                // Set server-side values (never trust client values)
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.SD_CreatedBy = UserModel.UM_USERID;

                // Call service (expected: admission no string OR "-1" for duplicate OR ""/null for failure)
                var getdata = service.InsertUpdateAdmissionDetails(obj);

                // 1) DUPLICATE CASE
                if (!string.IsNullOrWhiteSpace(getdata) && getdata.Trim() == "-1")
                {
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "Data already exists for this combination of StudentName, GuardianName, MobNo, and School.";
                    status.ExMessage = "";
                    status.Param = "";
                    status.CanRedirect = false;

                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                // 2) SUCCESS CASE (admission no returned)
                if (!string.IsNullOrWhiteSpace(getdata))
                {
                    // If Insert (SD_Id null) -> send sms and redirect
                    if (obj.SD_Id == null)
                    {
                        try
                        {
                            var msgText = "Thank you for applying with TIGPS. Your Generated Admission No. is : "
                                          + getdata.Trim()
                                          + " against submitted Form No: "
                                          + (obj.SD_FormNo ?? "") + ".";

                            var smsBOs = new List<SMSBO>();

                            if (!string.IsNullOrWhiteSpace(obj.SD_ContactNo1))
                            {
                                SMSBO smsBO = Utils.sendSMS(obj.SD_ContactNo1, msgText);
                                smsBO.trackingCode = getdata.Trim();
                                smsBOs.Add(smsBO);
                            }

                            if (!string.IsNullOrWhiteSpace(obj.SD_ContactNo2))
                            {
                                SMSBO smsBO = Utils.sendSMS(obj.SD_ContactNo2, msgText);
                                smsBO.trackingCode = getdata.Trim();
                                smsBOs.Add(smsBO);
                            }

                            if (smsBOs.Count > 0)
                            {
                                service.SaveSmsTracker(smsBOs);
                            }

                            status.CanRedirect = true;
                        }
                        catch (Exception smsEx)
                        {
                            // Don't fail admission if SMS fails
                            // You may log smsEx here
                            status.CanRedirect = true; // keep redirect true if insert succeeded
                        }
                    }
                    else
                    {
                        // Update case: typically no redirect needed
                        status.CanRedirect = false;
                    }

                    status.Id = 1;
                    status.Param = getdata.Trim(); // admission no
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                    status.ExMessage = "";

                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                // 3) FAILURE CASE (null/blank from service)
                status.Id = -1;
                status.IsSuccess = false;
                status.Message = "Something went wrong.";
                status.ExMessage = "";
                status.Param = "";
                status.CanRedirect = false;

                return Json(status, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                status.Id = -1;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.ExMessage = ex.Message; // or ex.ToString() for full stack
                status.Param = "";
                status.CanRedirect = false;

                return Json(status, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult AdmittedStudentsList(StudetDetails_SD obj, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.FromDate = FromDate;
                obj.ToDate = ToDate;
                var query = service.GetAdmissionStudentList(obj);

                bool CanEdit = (bool)Session["CanEdit"];
                bool CanDelete = (bool)Session["CanDelete"];

                return new JsonResult
                {
                    Data = new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
        #region MarkSheet

        public JsonResult getClassess()
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            var query = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null);
            var results = query.Where(r => r.CM_SCM_SCHOOLID == schoolId).OrderBy(r => r.CM_CLASSID).ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getClasses()
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            var classList = service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_SCM_SCHOOLID", schoolId);
            var sortedClassList = classList.OrderBy(c => c.CM_FROMAGE.ToString().Length).ThenBy(c => c.CM_FROMAGE).ToList();
            var result = sortedClassList.Select(c => new { CM_CLASSID = c.CM_CLASSID, CM_CLASSNAME = c.CM_CLASSNAME });
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult ClassWiseSection(long classId)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM", "SECM_SECTIONID", null);
            var results = query.Where(r => r.SECM_CM_CLASSID == classId && r.SECM_SCM_SCHOOLID == schoolId).OrderBy(r => r.SECM_SECTIONNAME).ToList(); // && r.SECM_SM_SESSIONID == sessionId
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseSubjectGroup(long classId)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetClassWiseDetails("GetSubjectGroup", classId, schoolId, sessionId, null).OrderBy(r => r.Text).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseSubject(long classId, long? SubGrId)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetClassWiseDetails("GetSubjectFromSubectGroup", classId, schoolId, null, SubGrId).OrderBy(r => r.Text).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getStudentsList(long classId, long sectionId, long subjectId, long HS)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.getStudentsList(classId, sectionId, subjectId, HS, schoolId, sessionId);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GradeCheck(decimal? Marks)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            GradeMaster_GM objGrade = service.GradeCheck(Marks, schoolId, sessionId);
            return Json(objGrade, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Student FeesCategory
        public JsonResult InsertUpdateFeesCategory(STUDENTCATEGORY_CAT obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {
                    obj.CAT_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                    obj.CAT_CREATEDUID = UserModel.UM_USERID;

                    var getdata = service.InsertUpdateCategory(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FeesCategoryList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<STUDENTCATEGORY_CAT>("STUDENTCATEGORY_CAT", "CAT_SCHOOLID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region loadNoOfInstallment
        public JsonResult loadNoOfInstallment()
        {
            var query = service.GetGlobalSelect<FeesMaster_FEM>("FeesMaster_FEM", "FEM_FEESID", null);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClasswiseFees
        public JsonResult InsertUpdateClasswiseFees(ClassWisefees_CF obj)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                obj.CF_CREATEDUID = UserModel.UM_USERID;
                obj.CF_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                obj.CF_SESSIONID = UserModel.UM_SCM_SESSIONID;

                long result = service.InsertUpdateClasswiseFees(obj);

                if (result == -1)
                {
                    status.IsSuccess = false;
                    status.Message = "Fees already exists for the selected Class, Category and Fees Head.";
                }
                else if (result > 0)
                {
                    status.IsSuccess = true;
                    status.Message = "Class wise fees saved successfully.";
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "Unable to save data.";
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUpdateClasswiseFeesForward(int CF_CLASSID, int CF_CATEGORYID, int CF_FEESID, ClassWisefees_CF obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                obj.CF_CLASSID = CF_CLASSID;
                obj.CF_CATEGORYID = CF_CATEGORYID;
                obj.CF_FEESID = CF_FEESID;
                obj.CF_CREATEDUID = UserModel.UM_USERID;
                obj.CF_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                obj.CF_SESSIONID = UserModel.UM_SCM_SESSIONID;

                var getdata = service.InsertUpdateClasswiseFeesForward(obj);
                if (getdata >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Data already exists. ";
                }

            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClasswiseFeesList()
        {
            Int64 SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            Int64 SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            var query = service.GetClassWiseFeesList(SchoolId, SessionId, null);

            var feesdetailsGroups = query.GroupBy(x => new { x.CF_CLASSID, x.CF_FEESID, x.CF_CATEGORYID }).ToList();
            List<ClassWisefees_CF> feesList = new List<ClassWisefees_CF>();

            foreach (var group in feesdetailsGroups)
            {
                ClassWisefees_CF detail = new ClassWisefees_CF()
                {
                    FeesHeadName = group.FirstOrDefault().FeesHeadName,
                    CF_CLASSFEESID = group.FirstOrDefault().CF_CLASSFEESID,
                    CF_CLASSID = group.FirstOrDefault().CF_CLASSID,
                    CF_CATEGORYID = group.FirstOrDefault().CF_CATEGORYID,
                    CF_FEESID = group.FirstOrDefault().CF_FEESID,
                    CF_NOOFINS = group.FirstOrDefault().CF_NOOFINS,
                    CF_FEESAMOUNT = group.FirstOrDefault().CF_FEESAMOUNT,
                    CF_INSTALLMENTNO = group.FirstOrDefault().CF_INSTALLMENTNO,
                    CF_INSAMOUNT = group.FirstOrDefault().CF_INSAMOUNT,
                    Testdata = group.FirstOrDefault().Testdata,
                    DUEDATES = group.FirstOrDefault().DUEDATES,
                    ClassName = group.FirstOrDefault().ClassName,
                    CAT_CATEGORYNAME = group.FirstOrDefault().CAT_CATEGORYNAME,
                    IsAdmissionTime = group.FirstOrDefault().IsAdmissionTime,
                };
                feesList.Add(detail);
            }

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = feesList, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult ClasswiseFeesForwardList(ClassFeesForward query)
        //{
        //    Int64 SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
        //    Int64 SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
        //    var query = service.GetClassWiseFeesList(SchoolId, SessionId, null);

        //    var feesdetailsGroups = query.GroupBy(x => new { x.CF_CLASSID, x.CF_FEESID, x.CF_CATEGORYID }).ToList();
        //    List<ClassWisefees_CF> feesList = new List<ClassWisefees_CF>();

        //    foreach (var group in feesdetailsGroups)
        //    {
        //        ClassWisefees_CF detail = new ClassWisefees_CF()
        //        {
        //            FeesHeadName = group.FirstOrDefault().FeesHeadName,
        //            CF_CLASSFEESID = group.FirstOrDefault().CF_CLASSFEESID,
        //            CF_CLASSID = group.FirstOrDefault().CF_CLASSID,
        //            CF_CATEGORYID = group.FirstOrDefault().CF_CATEGORYID,
        //            CF_FEESID = group.FirstOrDefault().CF_FEESID,
        //            CF_NOOFINS = group.FirstOrDefault().CF_NOOFINS,
        //            CF_FEESAMOUNT = group.FirstOrDefault().CF_FEESAMOUNT,
        //            CF_INSTALLMENTNO = group.FirstOrDefault().CF_INSTALLMENTNO,
        //            CF_INSAMOUNT = group.FirstOrDefault().CF_INSAMOUNT,
        //            Testdata = group.FirstOrDefault().Testdata,
        //            DUEDATES = group.FirstOrDefault().DUEDATES,
        //            ClassName = group.FirstOrDefault().ClassName,
        //            CAT_CATEGORYNAME = group.FirstOrDefault().CAT_CATEGORYNAME,
        //            IsAdmissionTime = group.FirstOrDefault().IsAdmissionTime,
        //        };
        //        feesList.Add(detail);
        //    }

        //    bool CanEdit = (bool)Session["CanEdit"];
        //    bool CanDelete = (bool)Session["CanDelete"];
        //    return Json(new { Data = feesList, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        //}


        public JsonResult ClasswiseFeesForwardList(ClassFeesForward model)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            model.SchoolId = schoolId;
            model.SessionId = sessionId;


            var result = service.GetClassFeesForward(model);


            //var feesdetailsGroups = result
            //    .GroupBy(x => new { x.CF_CLASSID, x.CF_FEESID, x.CF_CATEGORYID })
            //    .ToList();

            List<ClassWisefees_CF> feesList = new List<ClassWisefees_CF>();

            foreach (var group in result)
            {
                var first = group;

                feesList.Add(new ClassWisefees_CF
                {
                    FeesHeadName = first.FeesHeadName,
                    CF_CLASSFEESID = first.CF_CLASSFEESID,
                    CF_CLASSID = first.CF_CLASSID,
                    CF_CATEGORYID = first.CF_CATEGORYID,
                    CF_FEESID = first.CF_FEESID,
                    CF_NOOFINS = first.CF_NOOFINS,
                    CF_FEESAMOUNT = first.CF_FEESAMOUNT,
                    CF_INSTALLMENTNO = first.CF_INSTALLMENTNO,
                    CF_INSAMOUNT = first.CF_INSAMOUNT,
                    Testdata = first.Testdata,
                    DUEDATES = first.DUEDATES,
                    ClassName = first.ClassName,
                    CAT_CATEGORYNAME = first.CAT_CATEGORYNAME,
                    IsAdmissionTime = first.IsAdmissionTime
                });
            }

            bool canEdit = Session["CanEdit"] != null && (bool)Session["CanEdit"];
            bool canDelete = Session["CanDelete"] != null && (bool)Session["CanDelete"];

            return Json(
                new
                {
                    Data = feesList,
                    CanEdit = canEdit,
                    CanDelete = canDelete
                },
                JsonRequestBehavior.AllowGet
            );
        }

        [HttpPost]
        public JsonResult DeleteClassWisefees(int ClassId, int CF_CATEGORYID, int CF_FEESID)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                long id = service.DeleteClassWisefees(ClassId, CF_CATEGORYID, CF_FEESID);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region FeesCollection
        [HttpPost]
        public JsonResult FeesCollectionList(FeesCollectionBO obj, string FeesColType)
        {
            obj.schoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            obj.sessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
            var query = service.GetFeeSCollectionList(obj, FeesColType);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            bool CanView = (bool)Session["CanView"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete, CanView = CanView }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult DeleteFeesCollection(long? FEESID)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                var SchoolId = UserModel.UM_SCM_SCHOOLID;
                var SessionId = UserModel.UM_SCM_SESSIONID;
                long id = service.DeleteFeesCollection(FEESID, SchoolId, SessionId);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion FeesCollection
        #region FeesMaster
        public JsonResult InsertUpdateFees(FeesMaster_FEM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                obj.FEM_CREATEDUID = UserModel.UM_USERID;
                obj.FEM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;

                var getdata = service.InsertUpdateFees(obj);
                if (getdata >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Data already exists. ";
                }

            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FeesMasterList(bool? IsAdmission)
        {
            var SchoolId = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;

            var query = service.GetFeesList(IsAdmission).ToList();
            query = query.Where(r => r.FEM_ISACTIVE == true && r.FEM_SCHOOLID == SchoolId).OrderByDescending(r => r.FEM_FEESID).ToList();
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion FeesMaster
        #region loadFeesInstypes
        public JsonResult loadFeesInstypes()
        {
            var query = service.GetGlobalSelect<InsType_INTYP>("InsType_INTYP", "INTYP_INSTYPEID", null);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetSessionForSchool
        public JsonResult GetSessionForSchool(int? SchoolId)
        {
            var query = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", SchoolId);
            return Json(query.OrderByDescending(a => a.SM_SESSIONNAME), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SchoolMaster
        public JsonResult GetScoolDdl()
        {
            var schoolList = service.GetGlobalSelect<GetSchoolDdl>("SchoolMasters_SCM", "SCM_SCHOOLID", null);
            return Json(schoolList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateSchool(SchoolMasters_SCM obj)
        {
            obj.SCM_CREATEDUID = Convert.ToInt64(UserModel.UM_USERID);
            obj.SCM_SCHOOLID = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            StatusResponse Status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {
                    var sess = service.AddEditSchool(obj);
                    if (sess >= 1)
                    {
                        Status.ExMessage = "";
                        Status.IsSuccess = true;
                        Status.Message = "Operation has been done successfully. ";
                    }
                    else
                    {
                        Status.Id = -1;
                        Status.ExMessage = "";
                        Status.IsSuccess = false;
                        Status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Something wrong happened...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region GetDistrictListByState
        public JsonResult GetDistrictListByState(int StateId)
        {
            var query = service.GetGlobalSelect<DistrictMasters_DM>("DistrictMasters_DM", "DM_STATEID", StateId);

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetIndividualStudentData
        public JsonResult GetIndividualStudentData(int Id)
        {
            var results = service.GetGlobalSelect<StudetDetails_SD>("StudetDetails_SD", "SD_Id", Id);
            var query = results[0];
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetSectionByClass
        [HttpPost]
        public JsonResult GetSectionByClass(int Id)
        {
            var query = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM", "SECM_CM_CLASSID", Id);

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region GetGroupWiseSubject
        public JsonResult GetGroupWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            var y = obj.Userid;
            obj.CSGWS_School_Id = UserModel.UM_SCM_SCHOOLID;
            obj.Session_Id = UserModel.UM_SCM_SESSIONID;
            var query = service.GetGroupWiseSubject(obj);

            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StudentWiseSubjectMaster
        public JsonResult InsertUpdateStudentwiseSubject(StudentwiseSubjectSetting_SWS subject)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                subject.SWS_CreatedBy = UserModel.UM_USERID;
                subject.SWS_SchoolId = UserModel.UM_SCM_SCHOOLID;
                subject.SWS_SessionId = UserModel.UM_SCM_SESSIONID;

                var data = service.InsertUpdateStudentwiseSubject(subject);

                if (data == -1)
                {
                    status.IsSuccess = false;
                    status.Message = "This subject already assigned to the student.";
                    status.Id = -1;
                    return Json(status, JsonRequestBehavior.AllowGet);
                }

                status.IsSuccess = true;
                status.Message = "Operation has been done successfully.";
                status.Id = data;
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;   // ✅ SHOW REAL MESSAGE
                status.Id = -1;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GetStudentListForSubjectSettings(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.GetStudentListForSubjectSettings(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public JsonResult StudentWiseSubjectListForEdit(StudentwiseSubjectSetting_SWS obj)
        {
            try
            {
                obj.SWS_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SWS_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.StudentWiseSubjectListForEdit(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteStudentWiseSubject(string id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteStudentWiseSubject(id); // pass string
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }



        #endregion
        #region DashBoard
        [HttpPost]
        public JsonResult GetDashBoardData()
        {
            DashBoardBO dash = new DashBoardBO
            {
                dash_SchoolId = UserModel.UM_SCM_SCHOOLID,
                dash_SessionId = UserModel.UM_SCM_SESSIONID,
            };

            var result = service.GetDashBoardData(dash);
            List<long> totalStudentsAttendance = new List<long>();
            List<string> classes = new List<string>();
            List<long> studentNos = new List<long>();

            foreach (var item in result.DashboardList)
            {
                classes.Add(item.CM_CLASSNAME);
                studentNos.Add(item.NOOFSTUDENTS);
            }

            // Attendance Chart
            List<string> attendanceClasses = new List<string>();
            List<long> presentCounts = new List<long>();

            foreach (var item in result.AttendanceList)
            {
                attendanceClasses.Add(item.CLASSNAME);
                presentCounts.Add(item.PRESENTCOUNT);
                totalStudentsAttendance.Add(item.TOTALSTUDENTS);

            }



            return Json(new
            {
                result = result,
                classes = classes,
                studentNos = studentNos,
                attendanceClasses = attendanceClasses,
                presentCounts = presentCounts,
                totalStudentsAttendance = totalStudentsAttendance
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion DashBoard
        #region SecRollSetting
        public JsonResult InsertUpdateSecRollSetting(Sec_Roll_Setting_SR obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                obj.SR_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                obj.SR_CREATEDUID = UserModel.UM_USERID;
                obj.SR_SESSIONID = UserModel.UM_SCM_SESSIONID;
                var getdata = service.InsertUpdateSecRollSetting(obj);
                if (getdata >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation hes been done successfully.......";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Data already exists. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStudentListRollSecSetting(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.GetStudentListRollSecSetting(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public JsonResult RoutineList()
        //{
        //    var query = service.GetGlobalSelect<ClassWiseTeacherRoutine_CWTR>("vw_RoutineMasters", "CWTR_SchoolId", UserModel.UM_SCM_SCHOOLID);
        //    bool CanEdit = (bool)Session["CanEdit"];
        //    bool CanDelete = (bool)Session["CanDelete"];
        //    return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        //}
        #endregion
        #region MVCUtils
        [HttpPost]
        public JsonResult FinancialCalender(int MonthId, string Session)
        {
            try
            {
                var result = Utils.FinancialCalender(MonthId, Session);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(DateTime.Now.ToString("dd/MM/yyyy"), JsonRequestBehavior.AllowGet);

            }
        }

        #endregion  MVCUtils
        #region LateFeesMaster
        public JsonResult InsertUpdateLateFeesSlap(LateFeesSlap obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                obj.UserId = UserModel.UM_USERID ?? 0;
                obj.Slap_SchooIID = UserModel.UM_SCM_SCHOOLID ?? 0;
                obj.Slap_SessionID = UserModel.UM_SCM_SESSIONID ?? 0;
                //obj.IsActive = true;
                var getdata = service.InsertUpdateLateFeesSlap(obj);
                if (getdata >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Data already exists. ";
                }

            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LateFeesSlapMasterList()
        {
            var SchoolId = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;

            var lateFeesList = service.GetGlobalSelect<LateFeesSlap>("MSTR_LateFeesSlap", "ID", null)
                                      .Where(r => r.Slap_SchooIID == SchoolId && r.Slap_SessionID == SessionId)
                                      .OrderByDescending(r => r.Id)
                                      .ToList();

            var fineTypes = service.GetGlobalSelect<FineTypeMaster>("FineTypeMaster_FTM", "null", null).ToList();

            var query = (from lf in lateFeesList
                         join ft in fineTypes on lf.Slap_FineTypeID equals ft.FineTypeId into ftGroup
                         from ft in ftGroup.DefaultIfEmpty()
                         select new
                         {
                             lf.Id,
                             lf.Slap_Name,
                             lf.Slap_Amount,
                             IsActive = lf.IsActive,                   // boolean for JS
                             Status = lf.IsActive ? "Active" : "Inactive",  // string for display
                             FineTypeName = ft != null ? ft.FineTypeName : ""
                         }).ToList();

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];

            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }



        #endregion LateFeesMaster
        #region MiscellaneousHead Master
        public JsonResult InsertUpdateMiscellaneousHead(MiscellaneousHeadMaster_MISC misc)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (UserModel != null)
                {
                    misc.MISC_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    misc.MISC_SessionId = UserModel.UM_SCM_SESSIONID;
                    misc.MISC_CreatedBy = UserModel.UM_USERID;
                    misc.MISC_ModifiedBy = UserModel.UM_USERID;

                    var result = service.InsertUpdateMiscellaneousHead(misc);
                    if (result >= 1)
                    {
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.IsSuccess = false;
                        status.Message = "Data already exists.";
                    }
                }
                else
                {
                    status.IsSuccess = false;
                    status.Message = "User session expired.";
                }
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.ExMessage = ex.Message;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MiscellaneousHeadList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<MiscellaneousHeadMaster_MISC>("MiscellaneousHeadMaster_MISC", "MISC_SchoolId", UserModel.UM_SCM_SCHOOLID);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetFeesHeadAmount(int feesHeadId)
        {
            try
            {
                // Example: assuming "FeesMaster_FEM" contains fee details
                var feeData = service.GetGlobalSelectOne<FeesMaster_FEM>("FeesMaster_FEM", "FEM_FEESID", feesHeadId);

                if (feeData != null)
                {
                    return Json(new { success = true, amount = feeData.FEM_AMOUNT }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Fees Head not found." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]


        #endregion
        #region RoutewiseDropListbyRouteId
        public JsonResult RoutewiseDropListbyRouteId(int? id)
        {
            var query = service.GetGlobalSelect<RoutewiseDropMaster_RDM>("RoutewiseDropMaster_RDM", "RDM_ROUTEID", id);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ExamAttendance
        public JsonResult GetStudentListForExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            obj.SEA_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.SEA_SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.GetStudentListForExamAttendance(obj);
            //bool CanEdit = (bool)Session["CanEdit"];
            //bool CanDelete = (bool)Session["CanDelete"];
            //return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ExamAttendanceList(StudentExamAttandanceMaster_SEA obj, DateTime? FromDate, DateTime? ToDate)
        {
            obj.SEA_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.SEA_SessionId = UserModel.UM_SCM_SESSIONID;
            obj.FromDate = FromDate;
            obj.ToDate = ToDate;
            var query = service.ExamAttendanceList(obj);

            return Json(query, JsonRequestBehavior.AllowGet);

        }
        public JsonResult InsertUpdateExamAttendance(StudentExamAttandanceMaster_SEA obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.SEA_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.SEA_SessionId = UserModel.UM_SCM_SESSIONID;
                    var getdata = service.InsertUpdateExamAttendance(obj);
                    if (getdata != "")
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteExamAttendance(int? id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteExamAttendance(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region GetStudentListForAttendance

        public JsonResult GetStudentListForAttendance(StudentAttendenceMaster_SAM obj)
        {
            obj.SAM_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.SAM_SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.GetStudentListForAttendance(obj);
            //bool CanEdit = (bool)Session["CanEdit"];
            //bool CanDelete = (bool)Session["CanDelete"];
            //return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
            return Json(query, JsonRequestBehavior.AllowGet);

        }

        public JsonResult InsertUpdateAttendance(StudentAttendenceMaster_SAM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.SAM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.SAM_SessionId = UserModel.UM_SCM_SESSIONID;
                    var getdata = service.InsertUpdateAttendance(obj);
                    if (getdata != "")
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AttendanceList(StudentAttendenceMaster_SAM obj, DateTime? FromDateS, DateTime? ToDateS)
        {
            obj.SAM_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.SAM_SessionId = UserModel.UM_SCM_SESSIONID;
            obj.FromDateS = FromDateS;
            obj.ToDateS = ToDateS;
            var query = service.AttendanceList(obj);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteAttendance(int? id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteAttendance(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region CheckHoliday
        public JsonResult CheckHoliday(string date)
        {
            var schoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            var sessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
            var query = service.CheckHoliday(date, schoolId, sessionId);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StudentPromotion
        //public JsonResult GetSchoolWiseSessions()
        //{
        //    var Schoolid = UserModel.UM_SCM_SCHOOLID;
        //    var query = service.GetGlobalSelect<SessionMasters_SM>("SessionMasters_SM", "SM_SCM_SCHOOLID", Schoolid);
        //    return Json(query, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult GetSchoolWiseSessions()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var currentSessionId = UserModel.UM_SCM_SESSIONID;

            var sessions = service.GetGlobalSelect<SessionMasters_SM>(
                "SessionMasters_SM",
                "SM_SCM_SCHOOLID",
                Schoolid
            ).ToList();

            return Json(new
            {
                SessionList = sessions,
                CurrentSessionId = currentSessionId
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult studentsForPromotion(clsStudentList query)
        {
            query.SchoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            query.SessionId = UserModel.UM_SCM_SESSIONID ?? 0;
            List<clsStudentList> objStudentsList = service.studentsForPromotion(query);
            return Json(objStudentsList.OrderBy(r => r.Roll).ToList(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateStudentPromotion(clsStudentList obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                obj.SchoolId = UserModel.UM_SCM_SCHOOLID;
                var result = service.UpdateStudentPromotion(obj);
                if (result > 0)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Data already exists. ";
                }

            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TransportSetting
        public JsonResult UpdateTransport(StudetDetails_SD obj)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                var ReturnData = service.UpdateTransport(obj);
                if (ReturnData >= 1)
                {
                    Status.ExMessage = "";
                    Status.IsSuccess = true;
                    Status.Message = "Operation has been done successfully. ";
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Data already exixts. ";
                }


            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTransportFromStudents(string StudentId)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                var ReturnData = service.DeleteTransportFromStudents(StudentId);
                if (ReturnData >= 1)
                {
                    Status.ExMessage = "";
                    Status.IsSuccess = true;
                    Status.Message = "Data has been deleted successfully.. ";
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Data already exixts. ";
                }


            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentListForTransportSettings(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.GetStudentListForTransportSettings(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult TransportAvailedStudentList(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.TransportAvailedStudentList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
        #region HostelSetting
        public JsonResult InsertStudentForHostel(HostelMaster_HM obj)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                obj.hostel_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.hostel_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.Hostel_CreatedBy = UserModel.UM_USERID;
                var ReturnData = service.InsertStudentForHostel(obj);
                if (ReturnData >= 1)
                {
                    Status.ExMessage = "";
                    Status.IsSuccess = true;
                    Status.Message = "Operation has been done successfully. ";
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Data already exixts. ";
                }


            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStudentListForHostelSettings(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.GetStudentListForHostelSettings(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult HostelAviledStudentList(StudetDetails_SD obj)
        {
            try
            {
                obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.HostelAviledStudentList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult DeleteHostelSetting(string StudentId)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                var ReturnData = service.DeleteHostelSetting(StudentId);
                if (ReturnData >= 1)
                {
                    Status.ExMessage = "";
                    Status.IsSuccess = true;
                    Status.Message = "Data has been deleted successfully.. ";
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Data already exixts. ";
                }
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Hostel Fees Collection
        public JsonResult GetHotelFeesCollectionList(HostelMaster_HM obj)
        {
            try
            {
                obj.hostel_SchoolId = UserModel.UM_SCM_SCHOOLID;

                var query = service.GetHotelFeesCollectionList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public JsonResult InsertHostelFeesCollection(HostelTransactionMaster_HTM obj)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                obj.HTM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.HTM_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.HTM_CreatedBy = UserModel.UM_USERID;

                var ReturnData = service.InsertHostelFeesCollection(obj);
                if (!string.IsNullOrEmpty(ReturnData))
                {
                    Status.IsSuccess = true;
                    Status.Message = "Operation has been done successfully.";
                    Status.HFD_FeesTransId = ReturnData;
                }
                else
                {
                    Status.Id = -1;
                    Status.ExMessage = "";
                    Status.IsSuccess = false;
                    Status.Message = "Data already exixts. ";
                }


            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened. ";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadHostelFeesCollectionList(HostelTransactionMaster_HTM obj)
        {
            try
            {
                obj.HTM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                var query = service.LoadHostelFeesCollectionList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult GetHostelFeesCollectionListForEdit(HostelTransactionMaster_HTM obj)
        {
            try
            {
                obj.HTM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                var query = service.GetHostelFeesCollectionListForEdit(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult DeleteHostelCollection(string HTM_TransId)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                var schoolId = UserModel.UM_SCM_SCHOOLID;
                var sessionId = UserModel.UM_SCM_SESSIONID;
                string deletedId = service.DeleteHostelCollection(HTM_TransId, schoolId, sessionId);

                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Transport Fees Collection
        public JsonResult GetTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        {
            try
            {
                obj.TR_SchoolId = UserModel.UM_SCM_SCHOOLID;
                var query = service.GetTransportFeesCollectionList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult GetTransportFeesCollectionListForEdit(TransportFeesTransaction_TR obj)
        {
            try
            {
                obj.TR_SchoolId = UserModel.UM_SCM_SCHOOLID;
                var query = service.GetTransportFeesCollectionListForEdit(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public JsonResult InsertTransportFeesCollection(TransportFeesTransaction_TR obj)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                obj.TR_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.TR_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.TR_CreatedBy = UserModel.UM_USERID;
                var ReturnData = service.InsertTransportFeesCollection(obj);

                if (!string.IsNullOrEmpty(ReturnData))
                {
                    Status.IsSuccess = true;
                    Status.Message = "Operation has been done successfully.";
                    Status.TR_TransId = ReturnData;
                }
                else
                {
                    Status.Id = -1;
                    Status.IsSuccess = false;
                    Status.Message = "Data already exists.";
                }
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.Message = "Something wrong happened.";
                Status.ExMessage = ex.Message;
            }

            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LoadTransportFeesCollectionList(TransportFeesTransaction_TR obj)
        {
            try
            {
                obj.TR_SchoolId = UserModel.UM_SCM_SCHOOLID;
                var query = service.LoadTransportFeesCollectionList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost]
        public JsonResult DeleteTransportCollection(string TD_TransId)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                var schoolId = UserModel.UM_SCM_SCHOOLID;
                var sessionId = UserModel.UM_SCM_SESSIONID;

                service.DeleteTransportCollection(TD_TransId, schoolId, sessionId);

                status.IsSuccess = true;
                status.Message = "Record deleted successfully";
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }
            return Json(status);
        }



        #endregion
        #region Designation
        public JsonResult InsertUpdateDesignation(DesignationMaster_DM designation)
        {

            StatusResponse status = new StatusResponse();
            try
            {

                if (designation.Userid != null)
                {
                    designation.DM_SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
                    // designation.CreatedBy = UserModel.UM_USERID;
                    var desig = service.InsertUpdateDesignation(designation);
                    if (desig >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";

                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DesignationList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<DesignationMaster_DM>("DesignationMaster_DM", "DM_SchoolId", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Hostel Room
        public JsonResult HostelRoomList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<HostelRoomMaster_HR>("HostelRoomMaster_HR", "HR_HostelSchoolId", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertUpdateHostelRoom(HostelRoomMaster_HR hostel)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (hostel.Userid != null)
                {
                    hostel.HR_HostelSchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
                    hostel.HR_HostelSessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
                    var hostelroom = service.InsertUpdateHostelRoom(hostel);
                    if (hostelroom >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";

                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Bank
        public JsonResult BankList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<BankMaster_BM>("BankMaster_BM", "SchoolId", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertUpdateBank(BankMaster_BM bank)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (bank.Userid != null)
                {
                    bank.SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
                    bank.CreatedBy = UserModel.UM_USERID;
                    var result = service.InsertUpdateBank(bank);
                    if (result >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";

                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TC Master
        public JsonResult GetAllStudent(TCMaster obj)
        {
            obj.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
            obj.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
            var query = service.GetAllStudent(obj);
            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult BindTCTypes(List<TCMaster> ob)
        {
            return Json(service.GetGlobalSelect<TCTypeMaster_TM>("TCTypeMaster_TM", "TCTypeId", null), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CancelStudents(List<TCMaster> ob)
        {
            try
            {
                TCMaster TCMaster = new TCMaster();
                TCMaster.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                TCMaster.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
                TCMaster.UM_USERID = UserModel.UM_USERID;
                foreach (var SEM in ob)
                {
                    TCMaster.SD_StudentId = SEM.SD_StudentId;
                    TCMaster.SD_TCNo = SEM.SD_TCNo;
                    TCMaster.TCTypeId = SEM.TCTypeId;
                    TCMaster.CM_CLASSID = SEM.CM_CLASSID;
                    TCMaster.SECM_SECTIONID = SEM.SECM_SECTIONID;
                    TCMaster.TC_Fees = SEM.TC_Fees;
                    ///ADDED NEW 
                    TCMaster.DOB_Words = SEM.DOB_Words;
                    TCMaster.DOB_Proof = SEM.DOB_Proof;
                    TCMaster.Last_Exam = SEM.Last_Exam;
                    TCMaster.FailedStatus = SEM.FailedStatus;
                    TCMaster.Q_Promotion = SEM.Q_Promotion;
                    TCMaster.Last_DuePaid = SEM.Last_DuePaid;
                    TCMaster.Fee_Consession = SEM.Fee_Consession;
                    TCMaster.No_Wdays = SEM.No_Wdays;
                    TCMaster.No_WPdays = SEM.No_WPdays;
                    TCMaster.NCC_Cadet_Scout_Guide = SEM.NCC_Cadet_Scout_Guide;
                    TCMaster.Games_Activities = SEM.Games_Activities;
                    TCMaster.School_Category = SEM.School_Category;
                    TCMaster.Genral_Conduct = SEM.Genral_Conduct;
                    TCMaster.AP_Date = SEM.AP_Date;
                    TCMaster.Issue_Date = SEM.Issue_Date;
                    TCMaster.Reason_Leave = SEM.Reason_Leave;
                    TCMaster.Remarks = SEM.Remarks;

                    service.CancelStudents(TCMaster);
                }
                var result = "Students are canceled successfully";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json("Students are not canceled successfully", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult GetAllTCStudent(TCMaster obj)
        {
            obj.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
            obj.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
            var query = service.GetAllTCStudent(obj);
            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTC(string SD_StudentId)
        {
            StatusResponse Status = new StatusResponse();

            try
            {
                string val = service.DeleteTC(SD_StudentId);

                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.IsSuccess = false;
                Status.Message = "Record not deleted.";
                Status.ExMessage = ex.Message;
            }

            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Discontinue Student List
        public JsonResult GetDisStudentList(StudetDetails_SD obj)
        {
            obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.GetDisStudentList(obj);
            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DisContudentsINS(StudetDetails_SD StudetDetails_SD)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                StudetDetails_SD.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                StudetDetails_SD.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                service.DisContudentsINS(StudetDetails_SD, "SP_DiscontinueStudent");
                response.ExMessage = "";
                response.IsSuccess = true;
                response.Message = "Record saved successfully...";
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                response.Id = -1;
                response.IsSuccess = false;
                response.ExMessage = ex.Message;
                response.Message = "Error...";
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Class change
        [HttpGet]
        public ActionResult StudentClassChange(int SD_CurrentClassId)
        {
            StudetDetails_SD TEntity = new StudetDetails_SD();
            TEntity.SD_CurrentClassId = SD_CurrentClassId;
            TEntity.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            TEntity.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            var StudList = service.GetClassWiseStudent<StudetDetails_SD>(TEntity, "SP_ClassChange");
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = StudList, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
            //return View(ObjRightsVM);
        }

        #endregion
        #region StudentClassChangeUpdate
        [HttpPost]
        public ActionResult StudentClassChangeUpdate(int SD_CurrentClassId, string SD_StudentId)
        {
            StudetDetails_SD TEntity = new StudetDetails_SD();
            TEntity.SD_StudentId = SD_StudentId;
            TEntity.SD_CurrentClassId = SD_CurrentClassId;
            TEntity.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            TEntity.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            var Data = service.StudentClassChangeUpdate(TEntity, "SP_ClassChange");
            return Json(Data, JsonRequestBehavior.AllowGet);
            //return View(ObjRightsVM);
        }
        #endregion
        #region Class Wise Syllabus
        public JsonResult InsertUpdateSyllabus(ClassWiseSyllabusMasters_CWSM syllabus)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (syllabus.Userid != null)
                {
                    syllabus.SM_CreateBy = UserModel.UM_USERID;
                    syllabus.SM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    //syllabus.SM_ClassId = UserModel.CM_CLASSID;
                    syllabus.SM_SessionId = UserModel.UM_SCM_SESSIONID;
                    var syllabuss = service.InsertUpdateSyllabus(syllabus);
                    if (syllabuss >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ClassWiseSyllabusList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<ClassWiseSyllabusMasters_CWSM>("vw_List_SyllabusMaster_SM", "SM_SchoolId", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region DiscontinueStudentListUpdate
        [HttpPost]
        public ActionResult DiscontinueStudentListUpdate(string SD_StudentId)
        {
            StudetDetails_SD TEntity = new StudetDetails_SD();
            TEntity.SD_StudentId = SD_StudentId;
            TEntity.SD_SessionId = UserModel.UM_SCM_SESSIONID;
            TEntity.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
            var Data = service.DiscontinueStudentListUpdate(TEntity, "SP_DiscontinueStudent");
            return Json(Data, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region AssignmentMaster
        public JsonResult InsertUpdateAssignmentMaster(AssignmentMaster_ASM obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.ASM_School_ID = UserModel.UM_SCM_SCHOOLID;
                    obj.ASM_Session_ID = UserModel.UM_SCM_SESSIONID;
                    obj.Userid = UserModel.UM_USERID;
                    var getdata = service.InsertUpdateAssignmentMaster(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseAssignmentList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;
            var filteredAssignment = service.GetGlobalSelect<AssignmentMaster_ASM>("vw_ClassWiseAssignmentList", "ASM_School_ID", Schoolid)
                                            .Where(x => x.ASM_Session_ID == SessionId)
                                            .ToList();

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = filteredAssignment, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteAssignment(int? id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteAssignment(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ClassWiseLiveclass Master
        public JsonResult InsertUpdateClasswiseLiveclass(ClassWiseLiveclass_CWLS obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.CWLS_School_ID = UserModel.UM_SCM_SCHOOLID;
                    obj.CWLS_Session_ID = UserModel.UM_SCM_SESSIONID;
                    obj.Userid = UserModel.UM_USERID;
                    var getdata = service.InsertUpdateClasswiseLiveclass(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ClassWiseLiveclassList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;
            var query = service.GetGlobalSelect<ClassWiseLiveclass_CWLS>("vw_ClassWiseLiveclassList", "CWLS_School_ID", Schoolid);
            var filteredLiveclass = service.GetGlobalSelect<ClassWiseLiveclass_CWLS>("vw_ClassWiseLiveclassList", "CWLS_School_ID", Schoolid)
                                            .Where(x => x.CWLS_Session_ID == SessionId)
                                            .ToList();


            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = filteredLiveclass, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteClasswiseLiveclass(int? id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteClasswiseLiveclass(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region LeaveType Master
        public JsonResult LeaveTypeList()
        {
            long schoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            var query = service.GetGlobalSelect<LeaveType_LT>("LeaveType_LT", "SchoolId", null);
            bool CanEdit = Convert.ToBoolean(Session["CanEdit"]);
            bool CanDelete = Convert.ToBoolean(Session["CanDelete"]);
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUpdateLeaveType(LeaveType_LT leavetype)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (leavetype.Userid != null)
                {
                    leavetype.SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
                    leavetype.SessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
                    leavetype.CreateBy = UserModel.UM_USERID;
                    var result = service.InsertUpdateLeaveType(leavetype);
                    if (result >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";

                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Transport Type
        public JsonResult TransportTypeList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<TransportType_TT>("TransportType_TT", "SchoolId", null);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult InsertUpdateTransportType(TransportType_TT transport)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (transport != null)
                {
                    var result = service.InsertUpdateTransportType(transport);
                    if (result >= 1)
                    {
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.";
                    }
                    else
                    {
                        status.Id = -1;
                        status.IsSuccess = false;
                        status.Message = "Data already exists.";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "Invalid input.";
                }
            }
            catch (Exception ex)
            {
                status.Id = -1;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.ExMessage = ex.Message; // Optional: show to dev/logs
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Filter
        public class ClassFilter
        {
            public List<int> ClassIds { get; set; }
        }

        [HttpPost]
        public JsonResult GetSectionByClassA(ClassFilter filter)
        {
            List<SectionMaster_SECM> allSections = new List<SectionMaster_SECM>();

            if (filter != null && filter.ClassIds != null && filter.ClassIds.Any())
            {
                foreach (var classId in filter.ClassIds)
                {
                    var sections = service.GetGlobalSelect<SectionMaster_SECM>("SectionMaster_SECM", "SECM_CM_CLASSID", classId);
                    allSections.AddRange(sections);
                }
            }

            return Json(allSections.Distinct().ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StudentDiary
        public JsonResult InsertUpdateStudentDiary(StudentDiary_STD obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.SesssioId = UserModel.UM_SCM_SESSIONID;
                    obj.Userid = UserModel.UM_USERID;
                    var getdata = service.InsertUpdateStudentDiary(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult StudentDiaryList()
        {

            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;
            var filteredAssignment = service.GetGlobalSelect<StudentDiary_STD>("vw_StudentDiaryList", "SchoolId", Schoolid)
                                            .Where(x => x.SesssioId == SessionId)
                                            .ToList();

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = filteredAssignment, CanEdit, CanDelete }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteStudentDiary(int? id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                string val = service.DeleteAssignment(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region StudentEarlyLeave
        public JsonResult InsertUpdateStudentEarlyLeave(StudentEarlyLeave_ERL obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null)
                {

                    obj.ERL_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.ERL_SesssioId = UserModel.UM_SCM_SESSIONID;
                    obj.Userid = UserModel.UM_USERID;
                    var getdata = service.InsertUpdateStudentEarlyLeave(obj);
                    if (getdata >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult EarlyLeaveEntryList()
        {

            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var SessionId = UserModel.UM_SCM_SESSIONID;

            var filteredAssignment = service.GetGlobalSelect<StudentEarlyLeave_ERL>("vw_StudentEarlyLeave", "ERL_SchoolId", Schoolid)
                                            .Where(x => x.ERL_SesssioId == SessionId)
                                            .ToList();

            bool CanEdit = Session["CanEdit"] != null && (bool)Session["CanEdit"];
            bool CanDelete = Session["CanDelete"] != null && (bool)Session["CanDelete"];

            return Json(new { Data = filteredAssignment, CanEdit, CanDelete }, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region ExamGroupMaster


        [HttpPost]
        public JsonResult InsertUpdateExamGroup(ExamGroupPostDto obj)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (obj.Userid != null && obj.ExamGroupDetails != null && obj.ExamGroupDetails.Any())
                {
                    obj.EGM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.EGM_SessionId = UserModel.UM_SCM_SESSIONID;
                    obj.Userid = UserModel.UM_USERID;

                    foreach (var termGroup in obj.ExamGroupDetails.GroupBy(x => x.TermId))
                    {
                        var classIds = termGroup
                            .Select(x => x.ClassId ?? 0)
                            .Where(id => id > 0)
                            .ToList();

                        var egm = new ExamGroupMaster_EGM
                        {
                            EGM_Id = obj.EGM_Id,
                            EGM_Name = obj.EGM_Name,
                            EGM_SchoolId = obj.EGM_SchoolId,
                            EGM_SessionId = obj.EGM_SessionId,
                            Userid = obj.Userid,
                            EGM_TermId = termGroup.Key,
                            IsFinal = termGroup.First().IsFinal
                        };

                        var getdata = service.InsertUpdateExamGroup(egm, classIds);


                        if (getdata < 1)
                        {
                            status.Id = -1;
                            status.IsSuccess = false;
                            status.Message = "Data already exists.";
                            return Json(status, JsonRequestBehavior.AllowGet);
                        }
                    }

                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.";
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "No exams or classes selected.";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something went wrong.";
                status.Id = -1;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }



        public JsonResult ExamGroupList()
        {
            var schoolId = UserModel.UM_SCM_SCHOOLID;
            var sessionId = UserModel.UM_SCM_SESSIONID;

            var examGroups = service.GetGlobalSelect<ExamGroupMaster_EGM>(
                                "vw_ExamGroupList",
                                "EGM_SchoolId",
                                schoolId)
                            .Where(x => x.EGM_SessionId == sessionId)
                            .ToList();

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];

            return Json(new { Data = examGroups, CanEdit, CanDelete }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteExamGroup(long id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                long val = service.DeleteExamGroup(id);
                Status.ExMessage = "";
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }






        #endregion

        #region ADD MARKS ENTRY EXTRA FEATURES

        // GET: Exam Groups
        public JsonResult getExamGroups()
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            var examGroups = service
                .GetGlobalSelect<ExamGroupMaster_EGM>("ExamGroupMaster_EGM", "EGM_Code", null)
                .Where(r => r.EGM_SchoolId == schoolId && r.EGM_SessionId == sessionId)
                .GroupBy(r => r.EGM_Code) // group by CODE
                .Select(g => g.First())
                .OrderBy(r => r.EGM_Name)
                .Select(r => new
                {
                    r.EGM_Code,
                    r.EGM_Name
                })
                .ToList();

            return Json(examGroups, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getTermsByExamGroup(string examGroupId)  // examGroupId = EGM_Code
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            if (string.IsNullOrEmpty(examGroupId))
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);

            var terms = service
                .GetGlobalSelect<ExamGroupMaster_EGM>("ExamGroupMaster_EGM", "EGM_Code", null)
                .Where(r => r.EGM_SchoolId == schoolId &&
                            r.EGM_SessionId == sessionId &&
                            r.EGM_Code == examGroupId)
                .Join(
                    service.GetGlobalSelect<TermMaster_TM>("TermMaster_TM", "TM_Id", null),
                    eg => eg.EGM_TermId,
                    t => t.TM_Id,
                    (eg, t) => new
                    {
                        TM_Id = t.TM_Id.Value,
                        TM_TermName = t.TM_TermName
                    })
                .Distinct()
                .OrderBy(t => t.TM_TermName)
                .ToList();

            return Json(terms, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getClassesByTerm(string examGroupId, long termId)
        {
            long schoolId = UserModel.UM_SCM_SCHOOLID ?? 0;
            long sessionId = UserModel.UM_SCM_SESSIONID ?? 0;

            if (string.IsNullOrEmpty(examGroupId) || termId <= 0)
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);

            var classes = service
                .GetGlobalSelect<ExamGroupMaster_EGM>("ExamGroupMaster_EGM", "EGM_Code", null)
                .Where(r => r.EGM_SchoolId == schoolId &&
                            r.EGM_SessionId == sessionId &&
                            r.EGM_Code == examGroupId &&
                            r.EGM_TermId == termId)
                .Join(
                    service.GetGlobalSelect<ClassMaster_CM>("ClassMaster_CM", "CM_CLASSID", null),
                    eg => eg.EGM_ClassId,
                    c => c.CM_CLASSID,
                    (eg, c) => new
                    {
                        c.CM_CLASSID,
                        c.CM_CLASSNAME
                    })
                .Distinct()
                .OrderBy(c => c.CM_CLASSNAME)
                .ToList();

            return Json(classes, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Online Approval CMS LIST
        [HttpPost]
        public JsonResult GetApprovalList(ApprovalFilterRequest obj)
        {
            long SchoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);

            var query = service.GetApprovalList(
                            SchoolId,
                            obj.CLASS,
                            obj.IsApprove,
                            obj.IsAdmission
                        );

            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];

            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region MeritList
        public JsonResult ApproveApplications(OnlineMeritApproval_STD obj)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                if (obj != null && obj.Applications != null && obj.Applications.Count > 0)
                {
                    // Fill from current logged-in user (same as StudentDiary pattern)
                    obj.SchoolId = UserModel.UM_SCM_SCHOOLID;
                    obj.Userid = UserModel.UM_USERID;

                    string spMessage;
                    int resultCode = service.ApproveApplications(obj, out spMessage);

                    if (resultCode == 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = string.IsNullOrEmpty(spMessage)
                            ? "Merit List has been generated."
                            : spMessage;
                    }
                    else
                    {
                        status.Id = resultCode;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = string.IsNullOrEmpty(spMessage)
                            ? "Failed to generate merit list. Please try again later."
                            : spMessage;
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "No applications selected for approval.";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region MiscFeesCollection

        public JsonResult InsertUpdateMiscCollection(MiscellaneousTransactionMaster obj)
        {
            StatusResponse Status = new StatusResponse();

            try
            {
                obj.MTM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.MTM_SessionId = UserModel.UM_SCM_SESSIONID;
                obj.MTM_CreatedBy = UserModel.UM_USERID;

                var dbResult = service.InsertUpdateMiscCollection(obj);

                if (dbResult.Result.Contains("SUCCESS"))
                {
                    Status.ExMessage = "";
                    Status.IsSuccess = true;
                    Status.Message = "Operation completed successfully.";
                    Status.TransactionId = dbResult.TransactionId;
                }
                else
                {
                    Status.IsSuccess = false;
                    Status.Message = dbResult.Result;
                }
            }
            catch (Exception ex)
            {
                Status.Id = -1;
                Status.ExMessage = ex.Message;
                Status.IsSuccess = false;
                Status.Message = "Something went wrong.";
            }

            return Json(Status, JsonRequestBehavior.AllowGet);
        }


        public JsonResult MiscFeesCollectionList(MiscellaneousTransactionMaster obj)
        {
            try
            {
                obj.MTM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                obj.MTM_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.MiscFeesCollectionList(obj);
                return Json(query, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        [HttpPost]
        public JsonResult CheckStudentId(string studentId)
        {
            try
            {
                var student = service.GetGlobalSelectOne("StudetDetails_SD", "SD_StudentId", studentId);


                if (student != null)
                {
                    return Json(new { success = true, studentName = student.SD_StudentName }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Student ID not found." }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error: " + ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult DeleteMiscCollection(string id)
        {
            StatusResponse Status = new StatusResponse();
            try
            {
                service.DeleteMiscCollection(id);
                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.IsSuccess = false;
                Status.ExMessage = ex.Message;
                Status.Message = "Record not deleted successfully...";
            }
            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region DropOut
        public JsonResult GetAllDropOutStudent(DropOut_DOP obj)
        {
            obj.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
            obj.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
            var query = service.GetAllDropOutStudent(obj);
            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllDStudent(DropOut_DOP obj)
        {
            obj.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
            obj.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
            var query = service.GetAllDStudent(obj);
            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelDStudents(List<DropOut_DOP> ob)
        {
            try
            {
                DropOut_DOP DropOut_DOP = new DropOut_DOP();
                DropOut_DOP.SCM_SCHOOLID = UserModel.UM_SCM_SCHOOLID;
                DropOut_DOP.SM_SESSIONID = UserModel.UM_SCM_SESSIONID;
                DropOut_DOP.UM_USERID = UserModel.UM_USERID;
                foreach (var SEM in ob)
                {
                    DropOut_DOP.SD_StudentId = SEM.SD_StudentId;
                    DropOut_DOP.CM_CLASSID = SEM.CM_CLASSID;
                    DropOut_DOP.SECM_SECTIONID = SEM.SECM_SECTIONID;
                    DropOut_DOP.DOP_Reason = SEM.DOP_Reason;


                    service.CancelDStudents(DropOut_DOP);
                }
                var result = "Students are Droped successfully";
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception Ex)
            {
                return Json("Students are not Droped successfully", JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult DeleteDropOut(string SD_StudentId)
        {
            StatusResponse Status = new StatusResponse();

            try
            {
                string val = service.DeleteDropOut(SD_StudentId);

                Status.IsSuccess = true;
                Status.Message = "Record deleted successfully...";
            }
            catch (Exception ex)
            {
                Status.IsSuccess = false;
                Status.Message = "Record not deleted.";
                Status.ExMessage = ex.Message;
            }

            return Json(Status, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region FormMaster
        public JsonResult InsertUpdateForm(FormMaster_FM form)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (form.Userid != null)
                {

                    form.FM_SCM_SchoolID = UserModel.UM_SCM_SCHOOLID;
                    form.FM_SM_SessionID = UserModel.UM_SCM_SESSIONID;
                    var blood = service.InsertUpdateForm(form);
                    if (blood >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult FormList()
        {
            var Schoolid = UserModel.UM_SCM_SCHOOLID;
            var query = service.GetGlobalSelect<FormMaster_FM>("FormMaster_FM", "FM_SCM_SchoolID", Schoolid);
            bool CanEdit = (bool)Session["CanEdit"];
            bool CanDelete = (bool)Session["CanDelete"];
            return Json(new { Data = query, CanEdit = CanEdit, CanDelete = CanDelete }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region TeachingAidMaster
        public JsonResult InsertUpdateTeachingAid(TeachingAid_TA teachinga)
        {
            StatusResponse status = new StatusResponse();
            try
            {

                if (teachinga.Userid != null)
                {

                    teachinga.TA_SCM_SchoolID = UserModel.UM_SCM_SCHOOLID;
                    teachinga.TA_SM_SessionID = UserModel.UM_SCM_SESSIONID;
                    var blood = service.InsertUpdateTeachingAid(teachinga);
                    if (blood >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult TeachingAidList()
        {
            long schoolId = Convert.ToInt64(UserModel.UM_SCM_SCHOOLID);
            long sessionId = Convert.ToInt64(UserModel.UM_SCM_SESSIONID);
            var data = service.GetGlobalSelect<TeachingAid_TA>("TeachingAid_TA", "TA_SCM_SchoolID", schoolId).Where(x => x.TA_SM_SessionID == sessionId).ToList();
            return Json(new
            {
                Data = data,
                CanEdit = (bool)Session["CanEdit"],
                CanDelete = (bool)Session["CanDelete"]
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Lesson Plan

        [HttpPost]
        public JsonResult InsertUpdateLessonPlan(LessonPlan_LP lessonplan)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                // Assign current user-related info
                lessonplan.LP_SchoolId = UserModel.UM_SCM_SCHOOLID;
                lessonplan.LP_SessionId = UserModel.UM_SCM_SESSIONID;
                lessonplan.LP_CreatedBy = UserModel.UM_USERID;

                long result = service.InsertUpdateLessonPlan(lessonplan);

                if (result == -11)
                {
                    // Duplicate found
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "A lesson plan with the same title for the same class and section already exists.";
                }
                else if (result > 0)
                {
                    // Success
                    status.Id = result;
                    status.IsSuccess = true;
                    status.Message = "Data saved successfully.";
                }
                else
                {
                    // Unknown failure
                    status.Id = -1;
                    status.IsSuccess = false;
                    status.Message = "Something went wrong while saving the lesson plan.";
                }
            }
            catch (Exception ex)
            {
                status.Id = -1;
                status.IsSuccess = false;
                status.ExMessage = ex.Message;
                status.Message = "An error occurred while processing the request.";
            }

            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult LessonPlanListFilter(long? FacultyId, long? SubjectId,
                                       DateTime? FromDate, DateTime? ToDate)
        {
            var list = service.GetLessonPlanList(
                FacultyId,
                SubjectId,
                FromDate,
                ToDate,
                UserModel.UM_SCM_SCHOOLID,
                UserModel.UM_SCM_SESSIONID
            );

            return Json(new { Data = list, CanEdit = Session["CanEdit"], CanDelete = Session["CanDelete"] }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region ClassSecFacultyMaster
        [HttpPost]
        public JsonResult InsertUpdateClassSecFaculty(List<ClassSecFaculty_CSF> list)
        {
            StatusResponse status = new StatusResponse();

            try
            {
                foreach (var csf in list)
                {
                    csf.CSF_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    csf.CSF_SessionId = UserModel.UM_SCM_SESSIONID;

                    // Call repository
                    long id = service.InsertUpdateClassSecFaculty(csf);

                    // Update CSF_Id for JS to store
                    csf.CSF_Id = id;
                }

                status.IsSuccess = true;
                status.Message = "Class–Section–Faculty saved successfully";
                status.Data = list; // return updated list with CSF_Id
            }
            catch (Exception ex)
            {
                status.IsSuccess = false;
                status.Message = ex.Message;
            }

            return Json(status);
        }



        [HttpGet]
        public JsonResult GetClassSecFaculty()
        {
            var data = service.GetClassSecFaculty(
                UserModel.UM_SCM_SCHOOLID,
                UserModel.UM_SCM_SESSIONID
            );

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion
        #region PettyCash
        public JsonResult AccountMasterList(AccountMaster obj)
        {
            try
            {
                //obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                //obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.AccountMasterList("SELECT-ONE", obj);

                //bool CanEdit = (bool)Session["CanEdit"];
                //bool CanDelete = (bool)Session["CanDelete"];

                return new JsonResult
                {
                    Data = new { Data = query },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public JsonResult SubAccountMasterList(SubAccountMaster obj)
        {
            try
            {
                //obj.SD_SchoolId = UserModel.UM_SCM_SCHOOLID;
                //obj.SD_SessionId = UserModel.UM_SCM_SESSIONID;
                var query = service.SubAccountMasterList("SELECT-ONE", obj);

                //bool CanEdit = (bool)Session["CanEdit"];
                //bool CanDelete = (bool)Session["CanDelete"];

                return new JsonResult
                {
                    Data = new { Data = query },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                    MaxJsonLength = int.MaxValue
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Library Master

        public JsonResult InsertUpdateBookMaster(BookMaster_BM book)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (book.Userid != null)
                {
                    book.BM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    book.BM_CreatedBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdateBookMaster(book);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BookMasterList(BookMaster_BM book)
        {
            book.BM_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.BookMasterList(book);
            bool CanEdit = (bool)Session["CanEdit"];
            return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUpdateBookCopyMaster(BookCopyMaster_BCM copy)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                copy.BCM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                copy.BCM_CreatedBy = UserModel.UM_USERID;
                var periods = service.InsertUpdateBookCopyMaster(copy);
                if (periods >= 1)
                {
                    status.ExMessage = "";
                    status.IsSuccess = true;
                    status.Message = "Operation has been done successfully.......";
                }

                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }

            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BookCopyMasterList(BookCopyMaster_BCM copy)
        {
            copy.BCM_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.BookCopyMasterList(copy);
            bool CanEdit = (bool)Session["CanEdit"];
            return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertUpdateLibrarySettingMaster(LibrarySetting setting)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (setting.Userid != null)
                {
                    setting.LS_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    setting.LS_CreatedBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdateLibrarySettingMaster(setting);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetLibrarySettingMasterList(LibrarySetting setting)
        {
            setting.LS_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.LibrarySettingMasterList(setting);
            if (setting.LS_SettingId == null)
            {
                return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool CanEdit = (bool)Session["CanEdit"];
                return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult InsertUpdateCategoryMaster(CategoryMaster category)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (category.Userid != null)
                {
                    category.CM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    category.CM_CreatedBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdateCategoryMaster(category);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCategoryMasterList(CategoryMaster category)
        {
            category.CM_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.CategoryMasterList(category);
            if (category.CM_CategoryId == null)
            {
                return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool CanEdit = (bool)Session["CanEdit"];
                return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertUpdateSupplierMaster(SupplierMaster supplier)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (supplier.Userid != null)
                {
                    supplier.SM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    supplier.SM_CreatedBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdateSupplierMaster(supplier);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetSupplierMasterList(SupplierMaster supplier)
        {
            supplier.SM_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.SupplierMasterList(supplier);
            if (supplier.SM_SupplierId == null)
            {
                return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                bool CanEdit = (bool)Session["CanEdit"];
                return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult InsertUpdateMemberMaster(MemberMaster member)
        {
            StatusResponse status = new StatusResponse();
            try
            {
                if (member.Userid != null)
                {
                    member.MM_SchoolId = UserModel.UM_SCM_SCHOOLID;
                    member.MM_CreatedBy = UserModel.UM_USERID;
                    var periods = service.InsertUpdateMemberMaster(member);
                    if (periods >= 1)
                    {
                        status.ExMessage = "";
                        status.IsSuccess = true;
                        status.Message = "Operation has been done successfully.......";
                    }
                    else
                    {
                        status.Id = -1;
                        status.ExMessage = "";
                        status.IsSuccess = false;
                        status.Message = "Data already exists. ";
                    }
                }
                else
                {
                    status.Id = -1;
                    status.ExMessage = "";
                    status.IsSuccess = false;
                    status.Message = "Something wrong happened. ";
                }
            }
            catch (Exception ex)
            {
                status.ExMessage = ex.Message;
                status.IsSuccess = false;
                status.Message = "Something wrong happened.";
                status.Id = -1;
            }
            return Json(status, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MemberMasterList(MemberMaster member)
        {
            member.MM_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.MemberMasterList(member);
            bool CanEdit = (bool)Session["CanEdit"];
            return Json(new { Data = query, CanEdit = CanEdit }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DueDaysCount(IssueTransaction issue)
        {
            issue.IT_SchoolId = Convert.ToInt32(UserModel.UM_SCM_SCHOOLID);
            var query = service.GetDueDaysCount(issue);

            return Json(new { Data = query }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Generate AI Response

        //[HttpPost]
        //public JsonResult GenerateLessonPlan(LessonPlan_LP request)
        //{
        //    try
        //    {
        //        if (request == null ||
        //            request.Class <= 0 ||
        //            string.IsNullOrWhiteSpace(request.Subject) ||
        //            string.IsNullOrWhiteSpace(request.Chapter) ||
        //            request.DurationMinutes <= 0)
        //        {
        //            return Json(new { success = false, message = "Invalid input." }, JsonRequestBehavior.AllowGet);
        //        }

        //        var apiKey = ConfigurationManager.AppSettings["OpenAI:ApiKey"];
        //        var model = ConfigurationManager.AppSettings["OpenAI:Model"] ?? "gpt-4o";

        //        if (string.IsNullOrWhiteSpace(apiKey))
        //            return Json(new { success = false, message = "OpenAI API key missing in Web.config." }, JsonRequestBehavior.AllowGet);

        //        string prompt = string.Format(
        //              "You are an expert primary Bengali teacher.\n" +
        //              "Generate a lesson plan in Bengali for:\n" +
        //              "Class: {0}\n" +
        //              "Subject: {1}\n" +
        //              "Chapter: {2}\n" +
        //              "Duration: {3} minutes\n\n" +
        //              "Rules:\n" +
        //              "- Audience: Class 1 (age 6-7) style if class is 1\n" +
        //              "- Very simple Bengali language\n" +
        //              "- Practical classroom steps\n" +
        //              "- Include activities, assessment, and homework\n" +
        //              "- Return ONLY valid JSON (no markdown, no extra text).",
        //              request.Class,
        //              request.Subject,
        //              request.Chapter,
        //              request.DurationMinutes
        //          );


        //        // Responses API payload with strict JSON schema
        //        var payloadObj = new
        //        {
        //            model = model,
        //            input = prompt,
        //            text = new
        //            {
        //                format = new
        //                {
        //                    type = "json_schema",
        //                    name = "lesson_plan",
        //                    schema = new
        //                    {
        //                        type = "object",
        //                        additionalProperties = false,
        //                        properties = new
        //                        {
        //                            //title = new { type = "string" },
        //                            //@class = new { type = "string" },
        //                            //subject = new { type = "string" },
        //                            //chapter = new { type = "string" },
        //                            duration_minutes = new { type = "integer" },
        //                            objectives = new { type = "array", items = new { type = "string" } },
        //                            materials_required = new { type = "array", items = new { type = "string" } },
        //                            introduction = new { type = "string" },
        //                            time_breakdown = new
        //                            {
        //                                type = "array",
        //                                items = new
        //                                {
        //                                    type = "object",
        //                                    additionalProperties = false,
        //                                    properties = new
        //                                    {
        //                                        activity = new { type = "string" },
        //                                        minutes = new { type = "integer" }
        //                                    },
        //                                    required = new[] { "activity", "minutes" }
        //                                }
        //                            },
        //                            teaching_steps = new { type = "array", items = new { type = "string" } },
        //                            activities = new { type = "array", items = new { type = "string" } },
        //                            practice_work = new { type = "array", items = new { type = "string" } },
        //                            assessment = new { type = "array", items = new { type = "string" } },
        //                            homework = new { type = "array", items = new { type = "string" } },
        //                            expected_learning_outcome = new { type = "string" }
        //                        },
        //                        required = new[]
        //                        {
        //                            "title","class","subject","chapter","duration_minutes","objectives",
        //                            "materials_required","introduction","time_breakdown","teaching_steps",
        //                            "activities","practice_work","assessment","homework","expected_learning_outcome"
        //                        }
        //                    },
        //                    strict = true
        //                }
        //            }
        //        };

        //        string payloadJson = JsonConvert.SerializeObject(payloadObj);

        //        var httpRequest = (HttpWebRequest)WebRequest.Create("https://api.openai.com/v1/responses");
        //        httpRequest.Method = "POST";
        //        httpRequest.ContentType = "application/json";
        //        httpRequest.Headers["Authorization"] = "Bearer " + apiKey;

        //        var bytes = Encoding.UTF8.GetBytes(payloadJson);
        //        using (var stream = httpRequest.GetRequestStream())
        //        {
        //            stream.Write(bytes, 0, bytes.Length);
        //        }

        //        string rawResponse;
        //        using (var response = (HttpWebResponse)httpRequest.GetResponse())
        //        using (var reader = new StreamReader(response.GetResponseStream()))
        //        {
        //            rawResponse = reader.ReadToEnd();
        //        }

        //        var root = JObject.Parse(rawResponse);

        //        // Prefer output_text if present
        //        string lessonPlanJson = root["output_text"] != null
        //            ? root["output_text"].ToString()
        //            : null;

        //      if (string.IsNullOrWhiteSpace(lessonPlanJson))
        //               {
        //                   var outputArr = root["output"] as JArray;
        //                   if (outputArr != null)
        //                   {
        //                       foreach (var item in outputArr)
        //                       {
        //                           var content = item["content"] as JArray;
        //                           if (content == null) continue;

        //                           foreach (var c in content)
        //                           {
        //                               if (c["text"] != null)
        //                               {
        //                                   lessonPlanJson = c["text"].ToString();
        //                                   break;
        //                               }
        //                           }

        //                           if (!string.IsNullOrWhiteSpace(lessonPlanJson))
        //                               break;
        //                       }
        //                   }
        //               }

        //        if (string.IsNullOrWhiteSpace(lessonPlanJson))
        //            return Json(new { success = false, message = "Could not parse OpenAI response." }, JsonRequestBehavior.AllowGet);

        //        // Validate/deserialize JSON structure
        //        LessonPlanResult parsed;
        //        try
        //        {
        //            parsed = JsonConvert.DeserializeObject<LessonPlanResult>(lessonPlanJson);
        //        }
        //        catch
        //        {
        //            return Json(new
        //            {
        //                success = false,
        //                message = "Model returned invalid JSON.",
        //                raw = lessonPlanJson
        //            }, JsonRequestBehavior.AllowGet);
        //        }

        //        return Json(new
        //        {
        //            success = true,
        //            data = parsed
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (WebException wex)
        //    {
        //        string err = "";
        //        try
        //        {
        //            using (var sr = new StreamReader(wex.Response.GetResponseStream()))
        //                err = sr.ReadToEnd();
        //        }
        //        catch { }

        //        return Json(new
        //        {
        //            success = false,
        //            message = "OpenAI API error",
        //            details = err
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}


        //[HttpPost]
        //public JsonResult GenerateLessonPlan(LessonPlan_LP request)
        //{
        //    try
        //    {
        //        // -----------------------------
        //        // TLS FIX (important for VS2013/.NET old runtime)
        //        // -----------------------------
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

        //        if (request == null ||
        //            request.Class <= 0 ||
        //            string.IsNullOrWhiteSpace(request.Subject) ||
        //            string.IsNullOrWhiteSpace(request.Chapter) ||
        //            request.DurationMinutes <= 0)
        //        {
        //            return Json(new { success = false, message = "Invalid input." }, JsonRequestBehavior.AllowGet);
        //        }

        //        var apiKey = ConfigurationManager.AppSettings["OpenAI:ApiKey"];
        //        var model = ConfigurationManager.AppSettings["OpenAI:Model"] ?? "gpt-4o-mini";

        //        if (string.IsNullOrWhiteSpace(apiKey))
        //            return Json(new { success = false, message = "OpenAI API key missing in Web.config." }, JsonRequestBehavior.AllowGet);

        //        string prompt = string.Format(
        //            "You are an expert primary English teacher.\n" +
        //            "Generate a lesson plan in English for:\n" +
        //            "Class: {0}\n" +
        //            "Subject: {1}\n" +
        //            "Chapter: {2}\n" +
        //            "Duration: {3} minutes\n\n" +
        //            "Rules:\n" +
        //            "- Audience: Class 1 (age 6-7) style if class is 1\n" +
        //            "- Very simple English language\n" +
        //            "- Practical classroom steps\n" +
        //            "- Include activities, assessment, and homework\n" +
        //            "- Return ONLY valid JSON (no markdown, no extra text).",
        //            request.Class,
        //            request.Subject,
        //            request.Chapter,
        //            request.DurationMinutes
        //        );

        //        // Keep required[] exactly aligned with properties[] (fixed)
        //        var payloadObj = new
        //        {
        //            model = model,
        //            input = prompt,
        //            text = new
        //            {
        //                format = new
        //                {
        //                    type = "json_schema",
        //                    name = "lesson_plan",
        //                    strict = true,
        //                    schema = new
        //                    {
        //                        type = "object",
        //                        additionalProperties = false,
        //                        properties = new
        //                        {
        //                            //title = new { type = "string" },
        //                            //@class = new { type = "string" },
        //                            //subject = new { type = "string" },
        //                            //chapter = new { type = "string" },
        //                            //duration_minutes = new { type = "integer" },
        //                            objectives = new { type = "array", items = new { type = "string" } },
        //                            materials_required = new { type = "array", items = new { type = "string" } },
        //                            introduction = new { type = "string" },
        //                            time_breakdown = new
        //                            {
        //                                type = "array",
        //                                items = new
        //                                {
        //                                    type = "object",
        //                                    additionalProperties = false,
        //                                    properties = new
        //                                    {
        //                                        activity = new { type = "string" },
        //                                        minutes = new { type = "integer" }
        //                                    },
        //                                    required = new[] { "activity", "minutes" }
        //                                }
        //                            },
        //                            teaching_steps = new { type = "array", items = new { type = "string" } },
        //                            activities = new { type = "array", items = new { type = "string" } },
        //                            practice_work = new { type = "array", items = new { type = "string" } },
        //                            assessment = new { type = "array", items = new { type = "string" } },
        //                            homework = new { type = "array", items = new { type = "string" } },
        //                            expected_learning_outcome = new { type = "string" }
        //                        },
        //                        required = new[]
        //                {
        //                   "objectives",
        //                    "materials_required","introduction","time_breakdown","teaching_steps",
        //                    "activities","practice_work","assessment","homework","expected_learning_outcome"
        //                }
        //                    }
        //                }
        //            }
        //        };

        //        string payloadJson = JsonConvert.SerializeObject(payloadObj);

        //        var httpRequest = (HttpWebRequest)WebRequest.Create("https://api.openai.com/v1/responses");
        //        httpRequest.Method = "POST";
        //        httpRequest.ContentType = "application/json";
        //        httpRequest.Accept = "application/json";
        //        httpRequest.Headers["Authorization"] = "Bearer " + apiKey;

        //        // Optional but useful for old networks/proxy
        //        httpRequest.Timeout = 120000;       // 120 sec
        //        httpRequest.ReadWriteTimeout = 120000;
        //        httpRequest.KeepAlive = true;
        //        httpRequest.Proxy = WebRequest.DefaultWebProxy;

        //        var bytes = Encoding.UTF8.GetBytes(payloadJson);
        //        httpRequest.ContentLength = bytes.Length;

        //        using (var stream = httpRequest.GetRequestStream())
        //        {
        //            stream.Write(bytes, 0, bytes.Length);
        //        }

        //        //string rawResponse;
        //        //using (var response = (HttpWebResponse)httpRequest.GetResponse())
        //        //using (var reader = new StreamReader(response.GetResponseStream()))
        //        //{
        //        //    rawResponse = reader.ReadToEnd();
        //        //}
        //        string rawResponse = SendOpenAiRequestWithRetry(httpRequest, payloadJson);
        //        var root = JObject.Parse(rawResponse);

        //        // Prefer output_text
        //        string lessonPlanJson = root["output_text"] != null
        //            ? root["output_text"].ToString()
        //            : null;

        //        // Fallback parser
        //        if (string.IsNullOrWhiteSpace(lessonPlanJson))
        //        {
        //            var outputArr = root["output"] as JArray;
        //            if (outputArr != null)
        //            {
        //                foreach (var item in outputArr)
        //                {
        //                    var content = item["content"] as JArray;
        //                    if (content == null) continue;

        //                    foreach (var c in content)
        //                    {
        //                        if (c["text"] != null)
        //                        {
        //                            lessonPlanJson = c["text"].ToString();
        //                            break;
        //                        }
        //                    }

        //                    if (!string.IsNullOrWhiteSpace(lessonPlanJson))
        //                        break;
        //                }
        //            }
        //        }

        //        if (string.IsNullOrWhiteSpace(lessonPlanJson))
        //        {
        //            return Json(new
        //            {
        //                success = false,
        //                message = "Could not parse OpenAI response.",
        //                raw = rawResponse
        //            }, JsonRequestBehavior.AllowGet);
        //        }

        //        LessonPlanResult parsed;
        //        try
        //        {
        //            parsed = JsonConvert.DeserializeObject<LessonPlanResult>(lessonPlanJson);
        //        }
        //        catch
        //        {
        //            return Json(new
        //            {
        //                success = false,
        //                message = "Model returned invalid JSON.",
        //                raw = lessonPlanJson
        //            }, JsonRequestBehavior.AllowGet);
        //        }

        //        return Json(new
        //        {
        //            success = true,
        //            data = parsed
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (WebException wex)
        //    {
        //        string err = "";
        //        try
        //        {
        //            if (wex.Response != null)
        //            {
        //                using (var sr = new StreamReader(wex.Response.GetResponseStream()))
        //                    err = sr.ReadToEnd();
        //            }
        //        }
        //        catch { }

        //        return Json(new
        //        {
        //            success = false,
        //            message = "OpenAI API error",
        //            status = wex.Status.ToString(),
        //            details = string.IsNullOrWhiteSpace(err) ? wex.Message : err
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new
        //        {
        //            success = false,
        //            message = ex.Message
        //        }, JsonRequestBehavior.AllowGet);
        //    }
        //}
        [HttpPost]
        public JsonResult GenerateLessonPlan(LessonPlan_LP request)
        {
            try
            {
                // -----------------------------
                // TLS FIX (important for VS2013/.NET old runtime)
                // -----------------------------
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072; // TLS 1.2

                if (request == null ||
                   
                    string.IsNullOrWhiteSpace(request.Subject) ||
                    string.IsNullOrWhiteSpace(request.Chapter) ||
                    request.DurationMinutes <= 0)
                {
                    return Json(new { success = false, message = "Invalid input." }, JsonRequestBehavior.AllowGet);
                }

                var apiKey = ConfigurationManager.AppSettings["OpenAI:ApiKey"];
                var model = ConfigurationManager.AppSettings["OpenAI:Model"] ?? "gpt-4o-mini";

                if (string.IsNullOrWhiteSpace(apiKey))
                    return Json(new { success = false, message = "OpenAI API key missing in Web.config." }, JsonRequestBehavior.AllowGet);

                // Build the prompt dynamically using user input
                string prompt = string.Format(
                    "You are an expert primary {0} teacher.\n" +
                    "Generate a lesson plan in {0} for:\n" +
                    "Class: {1}\n" +
                    "Subject: {2}\n" +
                    "Chapter: {3}\n" +
                    "Duration: {4} minutes\n\n" +
                    "Rules:\n" +
                    "- Audience: Class {1}  style\n" +
                    "- Very simple {0} language\n" +
                    "- Practical classroom steps\n" +
                    "- Include activities, assessment, and homework\n" +
                    "- Return ONLY valid JSON (no markdown, no extra text).",
                    request.Language == "en" ? "English" : "Bengali", // dynamically detect language based on user input
                    request.Class,
                    request.Subject,
                    request.Chapter,
                    request.DurationMinutes
                );

                var payloadObj = new
                {
                    model = model,
                    input = prompt,
                    text = new
                    {
                        format = new
                        {
                            type = "json_schema",
                            name = "lesson_plan",
                            strict = true,
                            schema = new
                            {
                                type = "object",
                                additionalProperties = false,
                                properties = new
                                {
                                    objectives = new { type = "array", items = new { type = "string" } },
                                    materials_required = new { type = "array", items = new { type = "string" } },
                                    introduction = new { type = "string" },
                                    time_breakdown = new
                                    {
                                        type = "array",
                                        items = new
                                        {
                                            type = "object",
                                            additionalProperties = false,
                                            properties = new
                                            {
                                                activity = new { type = "string" },
                                                minutes = new { type = "integer" }
                                            },
                                            required = new[] { "activity", "minutes" }
                                        }
                                    },
                                    teaching_steps = new { type = "array", items = new { type = "string" } },
                                    activities = new { type = "array", items = new { type = "string" } },
                                    practice_work = new { type = "array", items = new { type = "string" } },
                                    assessment = new { type = "array", items = new { type = "string" } },
                                    homework = new { type = "array", items = new { type = "string" } },
                                    expected_learning_outcome = new { type = "string" }
                                },
                                required = new[]
                        {
                            "objectives",
                            "materials_required",
                            "introduction",
                            "time_breakdown",
                            "teaching_steps",
                            "activities",
                            "practice_work",
                            "assessment",
                            "homework",
                            "expected_learning_outcome"
                        }
                            }
                        }
                    }
                };

                string payloadJson = JsonConvert.SerializeObject(payloadObj);

                var httpRequest = (HttpWebRequest)WebRequest.Create("https://api.openai.com/v1/responses");
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";
                httpRequest.Headers["Authorization"] = "Bearer " + apiKey;

                // Optional but useful for old networks/proxy
                httpRequest.Timeout = 120000;       // 120 sec
                httpRequest.ReadWriteTimeout = 120000;
                httpRequest.KeepAlive = true;
                httpRequest.Proxy = WebRequest.DefaultWebProxy;

                var bytes = Encoding.UTF8.GetBytes(payloadJson);
                httpRequest.ContentLength = bytes.Length;

                using (var stream = httpRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }

                string rawResponse = SendOpenAiRequestWithRetry(httpRequest, payloadJson);
                var root = JObject.Parse(rawResponse);

                // Prefer output_text
                string lessonPlanJson = root["output_text"] != null
                    ? root["output_text"].ToString()
                    : null;

                // Fallback parser
                if (string.IsNullOrWhiteSpace(lessonPlanJson))
                {
                    var outputArr = root["output"] as JArray;
                    if (outputArr != null)
                    {
                        foreach (var item in outputArr)
                        {
                            var content = item["content"] as JArray;
                            if (content == null) continue;

                            foreach (var c in content)
                            {
                                if (c["text"] != null)
                                {
                                    lessonPlanJson = c["text"].ToString();
                                    break;
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(lessonPlanJson))
                                break;
                        }
                    }
                }

                if (string.IsNullOrWhiteSpace(lessonPlanJson))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Could not parse OpenAI response.",
                        raw = rawResponse
                    }, JsonRequestBehavior.AllowGet);
                }

                LessonPlanResult parsed;
                try
                {
                    parsed = JsonConvert.DeserializeObject<LessonPlanResult>(lessonPlanJson);
                }
                catch
                {
                    return Json(new
                    {
                        success = false,
                        message = "Model returned invalid JSON.",
                        raw = lessonPlanJson
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(new
                {
                    success = true,
                    data = parsed
                }, JsonRequestBehavior.AllowGet);
            }
            catch (WebException wex)
            {
                string err = "";
                try
                {
                    if (wex.Response != null)
                    {
                        using (var sr = new StreamReader(wex.Response.GetResponseStream()))
                            err = sr.ReadToEnd();
                    }
                }
                catch { }

                return Json(new
                {
                    success = false,
                    message = "OpenAI API error",
                    status = wex.Status.ToString(),
                    details = string.IsNullOrWhiteSpace(err) ? wex.Message : err
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        private string SendOpenAiRequestWithRetry(HttpWebRequest httpRequest, string payloadJson)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(payloadJson);

            int maxAttempts = 5;
            int delayMs = 1500; // 1.5 sec initial backoff

            for (int attempt = 1; attempt <= maxAttempts; attempt++)
            {
                try
                {
                    // New request per attempt (HttpWebRequest cannot be reused reliably after failure)
                    var req = (HttpWebRequest)WebRequest.Create(httpRequest.RequestUri);
                    req.Method = "POST";
                    req.ContentType = "application/json";
                    req.Accept = "application/json";
                    req.Headers["Authorization"] = httpRequest.Headers["Authorization"];
                    req.Timeout = httpRequest.Timeout;
                    req.ReadWriteTimeout = httpRequest.ReadWriteTimeout;
                    req.Proxy = httpRequest.Proxy;
                    req.KeepAlive = true;

                    using (var stream = req.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }

                    using (var resp = (HttpWebResponse)req.GetResponse())
                    using (var reader = new StreamReader(resp.GetResponseStream()))
                    {
                        return reader.ReadToEnd();
                    }
                }
                catch (WebException wex)
                {
                    string errBody = "";
                    int statusCode = 0;

                    try
                    {
                        var resp = wex.Response as HttpWebResponse;
                        if (resp != null)
                        {
                            statusCode = (int)resp.StatusCode;
                            using (var sr = new StreamReader(resp.GetResponseStream()))
                                errBody = sr.ReadToEnd();
                        }
                    }
                    catch { }

                    // Retry only for 429 / 500 / 502 / 503 / 504
                    if (attempt < maxAttempts &&
                        (statusCode == 429 || statusCode == 500 || statusCode == 502 || statusCode == 503 || statusCode == 504))
                    {
                        System.Threading.Thread.Sleep(delayMs);
                        delayMs = delayMs * 2; // exponential backoff
                        continue;
                    }

                    // No retry left or non-retryable error
                    throw new Exception(
                        "OpenAI API failed. HTTP: " + statusCode +
                        ", Attempt: " + attempt +
                        ", Details: " + errBody +
                        ", Message: " + wex.Message);
                }
            }

            throw new Exception("OpenAI API failed after retries.");
        }

        #endregion
    }
}