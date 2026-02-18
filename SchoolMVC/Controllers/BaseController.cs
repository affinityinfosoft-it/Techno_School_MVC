using SchoolMVC.BLLService;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SchoolMVC.Controllers
{

    public class BaseController : Controller
    {
        //public static UserMaster_UM UserModel { get; set; }

        //public Service service = new Service();
        //public StatusResponse response = new StatusResponse();

        #region Variable Declaration
        public static UserMaster_UM UserModel { get; set; }
        public static Service service;
        public StatusResponse response = new StatusResponse();

        public static List<MenuMasterModel> MenuModel { get; set; }
        #endregion
        public static void GetSession()
        {
            UserModel = (UserMaster_UM)System.Web.HttpContext.Current.Session["UserModel"];
        }


        //public BaseController()
        //{
        //    GetSession();
        //    this.service = new Service();
        //    this.response = new StatusResponse();
        //    if (UserModel != null)
        //    {
        //        var uid = UserModel.UM_LOGINID;
        //        ViewBag.SchoolList = UserModel.Schoollist;
        //        ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;

        //        ViewBag.SessionList = UserModel.Sessionlist;
        //        ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
        //        ViewBag.Userid = UserModel.UM_USERID;
        //        ViewBag.UserType = UserModel.UM_USERTYPE;
        //        //ViewBag.SchoolName = UserModel.UM_SCHOOLNAME;
        //    }

        //}

        #region BaseController Constructor
        public BaseController()
        {
            GetSession();
            service = new Service();
            this.response = new StatusResponse();
            if (UserModel != null)
            {
                var uid = UserModel.UM_LOGINID;
                ViewBag.SchoolList = UserModel.Schoollist;
                ViewBag.SchoolId = UserModel.UM_SCM_SCHOOLID;

                ViewBag.SessionList = UserModel.Sessionlist.OrderByDescending(a => a.UM_SESSIONNAME).ToList();
                ViewBag.SessionId = UserModel.UM_SCM_SESSIONID;
                ViewBag.Userid = UserModel.UM_USERID;
                ViewBag.UserType = UserModel.UM_USERTYPE;
                MenuModel = BuildMenu(UserModel.UM_ROLEID);
            }
            //GlobalDataList global = new GlobalDataList();
            //global.TransactionType = "SELECT_All";
            //global.StoreProcedure = "SP_MenuMaster";
            //var menuList = service.GetGlobalMasterList<MenuMasterModel>(global); //closed by Mirajul on 22_12_2018
        }

        #endregion
        #region BindSchool
        public JsonResult BindSchool(string TableName, string MainFieldName)
        {

            var SchoolList = service.GetGlobalSelect<SchoolMasters_SCM>(TableName, MainFieldName, null);
            return Json(SchoolList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region BindSession
        public JsonResult BindSession(string TableName, string MainFieldName, Int64 Pid)
        {

            var SchoolList = service.GetGlobalSelect<SessionMasters_SM>(TableName, MainFieldName, Pid);
            return Json(SchoolList.OrderByDescending(a => a.SM_SESSIONNAME), JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult home(string returnUrl)
        {
            return string.IsNullOrWhiteSpace(returnUrl) ? (ActionResult)RedirectToAction("HomeDashboard", "Home", new { area = "" }) : Redirect(returnUrl);
        }
        public ActionResult returnLogin(string returnUrl)
        {
            return (ActionResult)RedirectToAction("Index", "Login", new { area = "", returnUrl = returnUrl });
        }
        #region BuildMenu
        public static List<MenuMasterModel> BuildMenu(long RoleId)
        {
            List<MenuMasterModel> lstMenu = new List<MenuMasterModel>();
            MenuMasterModel TEntity = new MenuMasterModel();
            TEntity.RoleId = RoleId;
            //TEntity.ModuleId = ModuleId;

            if (RoleId == 0)
                return lstMenu;

            lstMenu = service.GetMenuList<MenuMasterModel>(TEntity, "SP_GetRoleBaseMenu");
            return lstMenu;
        }
        #endregion

        #region GetRights

        //public static void GetRights(string url)
        //{

        //    long MenuId = MenuModel.Single(x => x.ActionUrl == url).MenuId;
        //    long RoleId = UserModel.UM_ROLEID;
        //    var AssignRightsList = service.GetGlobalSelect<AccessRights>("AccessRights", "AssignRightsId", null)
        //        .Single(x => x.MenuId == MenuId && x.RoleId == RoleId);

        //    System.Web.HttpContext.Current.Session["CanView"] = AssignRightsList.CanView == true ? true : false;
        //    System.Web.HttpContext.Current.Session["CanAdd"] = AssignRightsList.CanAdd == true ? true : false;
        //    System.Web.HttpContext.Current.Session["CanEdit"] = AssignRightsList.CanEdit == true ? true : false;
        //    System.Web.HttpContext.Current.Session["CanDelete"] = AssignRightsList.CanDelete == true ? true : false; 
        //    System.Web.HttpContext.Current.Session["CanSubmit"] = AssignRightsList.CanSubmit == true ? true : false;
        //}

        public static void GetRights(string url)
        {
            var menuItem = MenuModel.SingleOrDefault(x => x.ActionUrl == url);
            if (menuItem == null)
            {
                // Log this or handle it as unauthorized
                System.Web.HttpContext.Current.Session["CanView"] = false;
                System.Web.HttpContext.Current.Session["CanAdd"] = false;
                System.Web.HttpContext.Current.Session["CanEdit"] = false;
                System.Web.HttpContext.Current.Session["CanDelete"] = false;
                System.Web.HttpContext.Current.Session["CanSubmit"] = false;
                return;
            }

            long MenuId = menuItem.MenuId;
            long RoleId = UserModel.UM_ROLEID;

            var assignRights = service.GetGlobalSelect<AccessRights>("AccessRights", "AssignRightsId", null)
                .SingleOrDefault(x => x.MenuId == MenuId && x.RoleId == RoleId);

            if (assignRights == null)
            {
                System.Web.HttpContext.Current.Session["CanView"] = false;
                System.Web.HttpContext.Current.Session["CanAdd"] = false;
                System.Web.HttpContext.Current.Session["CanEdit"] = false;
                System.Web.HttpContext.Current.Session["CanDelete"] = false;
                System.Web.HttpContext.Current.Session["CanSubmit"] = false;
                return;
            }

            System.Web.HttpContext.Current.Session["CanView"] = assignRights.CanView;
            System.Web.HttpContext.Current.Session["CanAdd"] = assignRights.CanAdd;
            System.Web.HttpContext.Current.Session["CanEdit"] = assignRights.CanEdit;
            System.Web.HttpContext.Current.Session["CanDelete"] = assignRights.CanDelete;
            System.Web.HttpContext.Current.Session["CanSubmit"] = assignRights.CanSubmit;
        }

        #endregion
    }
}