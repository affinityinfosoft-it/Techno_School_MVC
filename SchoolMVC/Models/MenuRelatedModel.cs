using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolMVC.Models
{
    #region GlobalData
    public class GlobalDataList
    {
        public string StoreProcedure { get; set; }
        public string TransactionType { get; set; }
        public string Param { get; set; }
        public int? CompanyID { get; set; }
        public int? UserID { get; set; }
        public Int64? paramValue { get; set; }
        public string paramString { get; set; }
        public string ParamFromDate { get; set; }
        public string ParamToDate { get; set; }

        public string ParamFromDateValue { get; set; }
        public string ParamToDateValue { get; set; }


    }
    #endregion

    #region CommonUserModel
    public class CommonUserModel
    {
        public long CreatedBy { get; set; }

        [Display(Name = "Created By")]
        public string CreatedByName { get; set; }

        [Display(Name = "Created On")]
        public DateTime? CreatedOn { get; set; }
        public long UpdatedBy { get; set; }

        [Display(Name = "Updated By")]
        public string UpdatedByName { get; set; }

        [Display(Name = "Updated On")]
        public DateTime? UpdatedOn { get; set; }

    }
    #endregion

    #region RoleMasterModel
    public class RoleMasterModel
    {
        [Display(Name = "Role ID")]
        public Int64 RoleId { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

    }
    #endregion

    #region ModuleMasterModel
    public class ModuleMasterModel
    {
        public long ModuleId { get; set; }

        [Display(Name = "Module Name")]
        public string ModuleName { get; set; }

        [Display(Name = "Module Prefix")]
        public string ModulePrefix { get; set; }

        [Required(ErrorMessage = "Fill out this field")]
        public int? HighestApprovalAuthority { get; set; }

        [Display(Name = "Approval Authority")]
        public string HighestAppAuthName { get; set; }

        public string IconFile { get; set; }

    }
    #endregion

    #region MenuMasterModel
    public class MenuMasterModel:CommonUserModel
    {
        public long MenuId { get; set; }

        [Required(ErrorMessage = "Fill out this field")]
        //[RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Menu Name must contain only alphabets")]
        [StringLength(maximumLength: 120, ErrorMessage = "Menu Name must not be more than 120 characters")]
        [Display(Name = "Menu")]
        public string MenuName { get; set; }
        public long? ParentMenuId { get; set; }

        [Display(Name = "Parent Menu")]
        public string ParentMenuName { get; set; }
        public IEnumerable<MenuMasterModel> ParentMenuList { get; set; }

        [Required(ErrorMessage = "Fill out this field")]
        public long? ModuleId { get; set; }

        [Display(Name = "Module")]
        public string ModuleName { get; set; }
        public IEnumerable<ModuleMasterModel> ModuleList { get; set; }


        [Display(Name = "Action Url")]
        public string ActionUrl { get; set; }

        [Required(ErrorMessage = "Please enter Display Position")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Display Position must be numeric")]
        [Range(minimum: 1, maximum: 50, ErrorMessage = "Display position must be greater than 0")]
        [Display(Name = "Display Position")]
        public long DisplayPosition { get; set; }

        public long? SubMenuId { get; set; }

        public long RoleId { get; set; }

    }

    #endregion

    #region AccessRights Model
    public class AccessRights
    {
        //public AccessRights();
        public long AssignRightsId { get; set; }

        [Display(Name = "View and Add Rights")]
        public bool CanAdd { get; set; }
        [Display(Name = "Submit Rights")]
        public bool CanSubmit { get; set; }
        [Display(Name = "View Rights")]
        public bool CanView { get; set; }

        [Display(Name = "Edit Rights")]
        public bool CanEdit { get; set; }
        [Display(Name = "Delete Rights")]
        public bool CanDelete { get; set; }

        public long MenuId { get; set; }
        [Display(Name = "Menu")]
        public string MenuName { get; set; }
        [Display(Name = "Sl No.")]
        public int SrlNo { get; set; }

        public long RoleId { get; set; }
    }
    #endregion

    #region AccessRightsViewModel
    public class AccessRightsVM : CommonUserModel
    {
        // public AccessRightsVM();

        public long AssignRightsId { get; set; }
        public List<AccessRights> lstAccessRights { get; set; }

        [Required(ErrorMessage = " ")]
        public long ModuleId { get; set; }
        public IEnumerable<ModuleMasterModel> ModuleList { get; set; }
        [Display(Name = "Module")]
        public string ModuleName { get; set; }
        public int Ptype { get; set; }
        public string PtypeName { get; set; }
        [Required(ErrorMessage = " ")]
        public long RoleId { get; set; }
        public IEnumerable<RoleMasterModel> RoleList { get; set; }
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
    #endregion
    #region _MenuMaster
    public class _MenuMaster
    {
    //MenuId, MenuName, ActionUrl, ModuleId, IsActive, IsBaseMenu, IsSubMenu, IsMenu, DisplayPosition, CreatedBy, UpdatedBy, CreatedDate, UpdatedDate
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string ActionUrl { get; set; }
        public int ModuleId { get; set; }
        public Boolean IsActive { get; set; }
        public Boolean IsBaseMenu { get; set; }
        public Boolean IsSubMenu { get; set; }
        public Boolean IsMenu { get; set; }
        public int DisplayPosition { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
    #endregion

}