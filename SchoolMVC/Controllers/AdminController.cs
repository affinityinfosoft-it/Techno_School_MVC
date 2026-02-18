using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolMVC.Models;

namespace SchoolMVC.Controllers
{
    public class AdminController : BaseController
    {
        #region RoleMaster
        public ActionResult RoleMasters(int? id)
        {
            if (UserModel == null) return returnLogin("~/Admin/RoleMasters");
            RoleMasterModel RoleMasterModel = new RoleMasterModel();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                RoleMasterModel = service.GetGlobalSelectOne<RoleMasterModel>("RoleMasters", "RoleId", editId);

            }
            return View(RoleMasterModel);
        }      
        [HttpPost]
        public JsonResult RoleMaster(RoleMasterModel RoleMasterModel)
        {


            try
            {
                service.InsertUpdateRoleMaster(RoleMasterModel, "SP_RoleMaster");

                response.ExMessage = "";
                response.IsSuccess = true;
                response.Message = "Record saved successfully...";
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

        [HttpPost]
        public JsonResult GetRoleMasterList(GlobalDataList global)
        {

            var query = service.GetGlobalMasterList<RoleMasterModel>(global);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion RoleMaster
        #region MenuMaster
        public ActionResult MenuMasters(int? id)
        {
            var ModuleList = service.GetGlobalSelect<ModuleMasterModel>("ModuleMasters", "ModuleId", null);
            var ParentMenuList = service.GetGlobalSelect<MenuMasterModel>("MenuMasters", "MenuId", null).Where(x=>x.ParentMenuId==0).ToList();
            if (UserModel == null) return returnLogin("~/Admin/MenuMasters");
            MenuMasterModel MenuMasterModel = new MenuMasterModel();
            Int64 editId = 0;
            if (id != null)
            {
                editId = (Int64)id;
                MenuMasterModel = service.GetGlobalSelectOne<MenuMasterModel>("MenuMasters", "MenuId", editId);

                ViewBag.ModuleId = new SelectList(ModuleList, "ModuleId", "ModuleName", MenuMasterModel.ModuleId);
                ViewBag.ParentMenuId = new SelectList(ParentMenuList, "MenuId", "MenuName", MenuMasterModel.ParentMenuId);
            }
            else
            {
                ViewBag.ModuleId = new SelectList(ModuleList, "ModuleId", "ModuleName");
                ViewBag.ParentMenuId = new SelectList(ParentMenuList, "MenuId", "MenuName");
            }

            return View(MenuMasterModel);


        }
        [HttpPost]
        public JsonResult MenuMaster(MenuMasterModel MenuMasterModel)
        {
            try
            {
                MenuMasterModel.CreatedBy = Convert.ToInt32(UserModel.UM_USERID);
                service.InsertUpdateMenuMaster(MenuMasterModel, "SP_MenuMaster");
                response.ExMessage = "";
                response.IsSuccess = true;
                response.Message = "Record saved successfully...";
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
        [HttpPost]
        public JsonResult GetMenuMasterList(GlobalDataList global)
        {

            var query = service.GetGlobalMasterList<MenuMasterModel>(global);
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        #endregion MenuMaster
        #region AccessRights
        public ActionResult AccessRights()
        {
            if (UserModel == null) return returnLogin("~/Admin/AccessRights");
            var RoleList = service.GetGlobalSelect<RoleMasterModel>("RoleMasters", "RoleId", null);
            ViewBag.RoleId = new SelectList(RoleList, "RoleId", "RoleName");
            
            var ModuleList = service.GetGlobalSelect<ModuleMasterModel>("ModuleMasters", "ModuleId", null);
            ViewBag.ModuleId = new SelectList(ModuleList, "ModuleId", "ModuleName");

            return View();
        }

        [HttpPost]
        public ActionResult AccessRightsList(int RoleId, int ModuleId, string SP_Name)
        {
            AccessRightsVM TEntity = new AccessRightsVM();
            TEntity.RoleId = RoleId;
            TEntity.ModuleId = ModuleId;

            //var query = service.GetAnyList<AccessRightsVM>(TEntity, SP_Name);

            AccessRightsVM ObjRightsVM = new AccessRightsVM();
            ObjRightsVM.lstAccessRights = new List<AccessRights>();
            ObjRightsVM.lstAccessRights = service.GetAccessRightList<AccessRights>(TEntity, SP_Name);

            return Json(ObjRightsVM, JsonRequestBehavior.AllowGet);
            //return View(ObjRightsVM);
        }

        [HttpPost]
        public JsonResult AccessRights(AccessRightsVM AccessRightsVM)
        {

            StatusResponse Status = new StatusResponse();
            try
            {
                AccessRightsVM.CreatedBy = 1;
                //AccessRightsVM.CompanyID = Convert.ToInt32(Session["UserID"]);
                service.InsertUpdateAccessRights(AccessRightsVM, "SP_AccessRights");
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

        #endregion AccessRights
        #region Menu_Master
        //public ActionResult Menu_Master(int? id)
        //{
        //    if (UserModel == null) return returnLogin("~/Admin/Menu_Masters");
        //    _MenuMaster data = new _MenuMaster();
        //    Int64 editId = 0;
        //    if (id == null)
        //    {
        //        ViewBag.ModuleList = service.ModuleddlList(data);
        //    }
        //    else
        //    {
        //        editId = (Int64)id;
        //        ViewBag.ModuleList = service.ModuleddlList(data);
        //        data = service.GetGlobalSelectOne<_MenuMaster>("_MenuMaster", "MenuId", editId);
        //        ViewBag.hdnId = editId;
        //    }
        //    return View(data);
        //}
        //[HttpPost]
        //public JsonResult Menu_Master_InsUp(_MenuMaster obj)
        //{
        //    try
        //    {
        //        obj.CreatedBy = Convert.ToInt32(UserModel.UM_USERID);
        //        service.Menu_Master_InsUp(obj, "SP_Menu_Master");
        //        response.ExMessage = "";
        //        response.IsSuccess = true;
        //        response.Message = "Record saved successfully...";
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Id = -1;
        //        response.IsSuccess = false;
        //        response.ExMessage = ex.Message;
        //        response.Message = "Error...";
        //    }
        //    return Json(response, JsonRequestBehavior.AllowGet);
        //}
        #endregion
    }
}