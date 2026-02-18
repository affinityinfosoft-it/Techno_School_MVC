using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SchoolMVC.Models
{
    #region StatusResponse
    public class StatusResponse
    {
        public object Data { get; set; }
        public long Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string ExMessage { get; set; }
        public bool CanRedirect { get; set; }
        public string Param { get; set; }
        public string TransactionId { get; set; } //added for updating misc
        public string TransId { get; set; }
        public string TD_TransId { get; set; }
        public string TR_TransId { get; set; }
        public string HFD_FeesTransId { get; set; }
        public StatusResponse()
        {
            CanRedirect = false;
        }

    }

    #endregion
    #region GolbalClass

    public class GolbalClass
    {
        public GolbalClass()
        {
            SqlParameter = new List<SqlParameter>();
        }
        public string SPName { get; set; }

        public string TransType { get; set; }

        public string TransVal { get; set; }

        public string FieldName { get; set; }
        public string FieldValue { get; set; }
        public List<SqlParameter> SqlParameter { get; set; }
    }
    #region CommanFieldClass
    public class CommonClass : GolbalClass
    {
        public string Branch_name { get; set; }
        public string ItemName { get; set; }
        public string SM_Name { get; set; }
        public string MM_Manufacturer { get; set; }
    }
    #endregion
    #endregion
    #region GlobalData
    public class GlobalData
    {
        public long? INT_ID { get; set; }
        public string STRING_DATA { get; set; }
    }
    #endregion
    #region UserMasters
    public class UserMaster_UM
    {
        public int? Userid { get; set; }

        public long? UM_USERID { get; set; }
        public string UM_LOGINID { get; set; }
        public string UM_PASSWORD { get; set; }
        public string UM_USERNAME { get; set; }
        public string UM_USEREMAIL { get; set; }
        public string UM_USERMOBILE { get; set; }
        public string UM_SECQUES { get; set; }
        public string UM_SECPASSWORD { get; set; }
        public bool UM_ISACTIVE { get; set; }
        public long? UM_SCM_SCHOOLID { get; set; }
        public string UM_CREATEDUID { get; set; }
        public DateTime UM_CREATEDDATE { get; set; }
        public string UM_CREATEDDATES
        {
            get
            {
                return UM_CREATEDDATE.Day.ToString().PadLeft(2, '0') + "/" +
                      UM_CREATEDDATE.Month.ToString().PadLeft(2, '0') + "/" +
                      UM_CREATEDDATE.Year;
            }
        }

        public string UM_USERTYPE { get; set; }
        public string UM_SCHOOLNAME { get; set; }
        public long? UM_SCM_SESSIONID { get; set; }
        public string UM_SESSIONNAME { get; set; }
        public List<UserMaster_UM> Schoollist { get; set; }
        public List<UserMaster_UM> Sessionlist { get; set; }
        public long? UM_FacultyId { get; set; }
        public string UM_FacultyName { get; set; }
        public long? UM_FP_ID { get; set; }
        public long? schhol_Id { get; set; }
        public UserMaster_UM()
        {
            Schoollist = new List<UserMaster_UM>();
            Sessionlist = new List<UserMaster_UM>();
        }
        //Add on 30/11/18
        public long UM_ROLEID { get; set; }
        public string UM_ROLENAME { get; set; }
    }

    //public class RoleMasterModel
    //{
    //    [Display(Name = "Role ID")]
    //    public Int64 RoleId { get; set; }

    //    [Display(Name = "Role")]
    //    public string RoleName { get; set; }

    //}
    #endregion
    #region SchoolMasters_SCM
    public class SchoolMasters_SCM
    {
        public long? SCM_SCHOOLID { get; set; }
        public string SCM_SCHOOLCODE { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SCM_TRUSTTYPE { get; set; }
        public string SCM_TRUSTNAME { get; set; }
        public DateTime? SCM_INCORPORATATIONDATE { get; set; }
        public string SCM_INCORPORATATIONDATES
        {
            get
            {
                return SCM_INCORPORATATIONDATE.HasValue
                    ? SCM_INCORPORATATIONDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SCM_INCORPORATATIONDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SCM_INCORPORATATIONDATE.Value.Year
                    : string.Empty;
            }
        }

        public string SCM_OFFICEADDRESS { get; set; }
        public string SCM_REGISTRATIONNUMBER { get; set; }
        public string SCM_SECRETARYNAME { get; set; }
        public string SCM_SCHOOLADDRESS1 { get; set; }
        public string SCM_SCHOOLADDRESS2 { get; set; }
        public long? SCM_STATEID { get; set; }
        public long? SCM_DISTRICTID { get; set; }
        public long? SCM_NATIONID { get; set; }
        public string SCM_PINCODE { get; set; }
        public string SCM_PHONENO1 { get; set; }
        public string SCM_PHONENO2 { get; set; }
        public string SCM_CONTACTPERSON { get; set; }
        public string SCM_EMAILID { get; set; }
        public string SCM_WEBSITE { get; set; }
        public string SCM_SCHOOLLOGO { get; set; }
        public string SCM_IMAGENAME { get; set; }
        public string SCM_IMAGETYPE { get; set; }
        public long? SCM_CREATEDUID { get; set; }
        public DateTime? SCM_CREATEDDATE { get; set; }
        public string SCM_CREATEDDATES
        {
            get
            {
                return SCM_CREATEDDATE.HasValue
                    ? SCM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SCM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SCM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }

    }
    #endregion
    #region SessionMasters_SM
    public class SessionMasters_SM
    {
        public DateTime? SM_ENDDATE { get; set; }
        public string SM_ENDDATES
        {
            get
            {
                return SM_ENDDATE.HasValue
                    ? SM_ENDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SM_ENDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SM_ENDDATE.Value.Year
                    : string.Empty;
            }
        }

        public long? SM_SCM_SCHOOLID { get; set; }
        public long? SM_CREATEDUID { get; set; }
        public DateTime? SM_CREATEDDATE { get; set; }
        public string SM_CREATEDDATES
        {
            get
            {
                return SM_CREATEDDATE.HasValue
                    ? SM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public long? SM_SESSIONID { get; set; }
        public string SM_SESSIONCODE { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public DateTime? SM_STARTDATE { get; set; }
        public string SM_STARTDATES
        {
            get
            {
                return SM_STARTDATE.HasValue
                    ? SM_STARTDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SM_STARTDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SM_STARTDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
    }
    #endregion
    #region StateMaster_STM
    public class StateMaster_STM : GlobalData
    {
        public long? STM_STATEID { get; set; }
        public long? STM_NATIONID { get; set; }
        public string STM_STATENAME { get; set; }
        public DateTime? STM_CREATEDDATE { get; set; }
        public string STM_CREATEDDATES
        {
            get
            {
                return STM_CREATEDDATE.HasValue
                    ? STM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      STM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      STM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }

    }
    #endregion
    #region  NationMaster_NM
    public class NationMaster_NM
    {
        public long? NM_NATIONID { get; set; }
        public string NM_NATIONNAME { get; set; }
        public string NM_NATIONALITY { get; set; }
        public DateTime? NM_CREATEDDATE { get; set; }
        public string NM_CREATEDDATES
        {
            get
            {
                return NM_CREATEDDATE.HasValue
                    ? NM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      NM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      NM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
    }
    #endregion
    #region DistrictMasters_DM
    public class DistrictMasters_DM
    {
        public long? DM_NATIONID { get; set; }
        public long? DM_DISTRICTID { get; set; }
        public long? DM_STATEID { get; set; }
        public bool DM_ISACTIVE { get; set; }
        public string DM_DISTRICTNAME { get; set; }
        public DateTime? DM_CREATEDDATE { get; set; }
        public string DM_CREATEDDATES
        {
            get
            {
                return DM_CREATEDDATE.HasValue
                    ? DM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      DM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      DM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public string DM_NATIONNAME { get; set; }
        public string DM_STATENAME { get; set; }
        public int? Userid { get; set; }

    }
    #endregion
    #region TermMaster_TM
    public class TermMaster_TM
    {
        public long? TM_Id { get; set; }
        public string TM_TermName { get; set; }
        public DateTime? TM_DateFrom { get; set; }
        public string TM_DateFromS
        {
            get
            {
                return TM_DateFrom.HasValue
                    ? TM_DateFrom.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TM_DateFrom.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TM_DateFrom.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? TM_DateTo { get; set; }
        public string TM_DateToS
        {
            get
            {
                return TM_DateTo.HasValue
                    ? TM_DateTo.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TM_DateTo.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TM_DateTo.Value.Year
                    : string.Empty;
            }
        }
        public long? TM_SchoolId { get; set; }
        public long? TM_SessionId { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region ClassTypeMaster_CTM
    public class ClassTypeMaster_CTM
    {
        public long? CTM_TYPEID { get; set; }
        public string CTM_TYPENAME { get; set; }
        public bool CTM_ISACTIVE { get; set; }
        public long? CTM_CREATEDUID { get; set; }
        public DateTime? CTM_CREATEDDATE { get; set; }
        public string CTM_CREATEDDATES
        {
            get
            {
                return CTM_CREATEDDATE.HasValue
                    ? CTM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CTM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CTM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
    }
    #endregion
    #region ClassMaster_CM
    public class ClassMaster_CM
    {
        public long? CM_CLASSID { get; set; }
        public long? CM_SCM_SCHOOLID { get; set; }
        public long? CM_CTM_TYPEID { get; set; }
        public string CM_CLASSCODE { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string CM_TYPENAME { get; set; }
        public bool? CM_ISACTIVE { get; set; }
        public long? CM_CREATEDUID { get; set; }
        public DateTime? CM_CREATEDDATE { get; set; }
        public string CM_CREATEDDATES
        {
            get
            {
                return CM_CREATEDDATE.HasValue
                    ? CM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public Decimal? CM_FROMAGE { get; set; }
        public Decimal? CM_TOAGE { get; set; }
        public int? Userid { get; set; }
        public int? CM_Preference { get; set; }
        public bool? IsHigherSecondary { get; set; }
    }
    #endregion
    #region SectionMaster_SECM
    public class SectionMaster_SECM
    {
        public SectionMaster_SECM()
        {
            SectionList = new List<SectionMaster_SECM>();
        }
        public long? SECM_SECTIONID { get; set; }
        public long? SECM_CM_CLASSID { get; set; }
        public long? SECM_SCM_SCHOOLID { get; set; }
        public Int32? SECM_OCCUPANCY { get; set; }
        public long? SECM_CREATEDUID { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public bool SECM_ISACTIVE { get; set; }
        public DateTime? SECM_CREATEDDATE { get; set; }
        public string SECM_CREATEDDATES
        {
            get
            {
                return SECM_CREATEDDATE.HasValue
                    ? SECM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SECM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SECM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public long? SECM_SM_SESSIONID { get; set; }
        public string SECM_CM_CLASSNAME { get; set; }
        public int? Userid { get; set; }

        public long? CM_CLASSID { get; set; }

        public List<SectionMaster_SECM> SectionList { get; set; }
    }
    #endregion
    #region BoardMasters_BM
    public class BoardMasters_BM
    {
        public long? BM_BOARDID { get; set; }
        public string BM_BOARDCODE { get; set; }
        public string BM_BOARDNAME { get; set; }
        public bool BM_ISACTIVE { get; set; }
        public long? BM_CREATEDUID { get; set; }
        public DateTime? BM_CREATEDDATE { get; set; }
        public string BM_CREATEDDATES
        {
            get
            {
                return BM_CREATEDDATE.HasValue
                    ? BM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      BM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      BM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
    }
    #endregion
    #region CasteMaster_CSM
    public class CasteMaster_CSM
    {
        public long? CSM_CASTEID { get; set; }
        public string CSM_CASTENAME { get; set; }
        public bool CSM_ISACTIVE { get; set; }
        public long? CSM_CREATEDUID { get; set; }
        public DateTime? CSM_CREATEDDATE { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region VernacularMaster_VM
    public class VernacularMaster_VM
    {
        public long? VM_VERNACULARID { get; set; }
        public string VM_VERNACULARNAME { get; set; }
        public bool VM_ISACTIVE { get; set; }
        public long? VM_CREATEDUID { get; set; }
        public DateTime? VM_CREATEDDATE { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region HouseMasters_HM
    public class HouseMasters_HM
    {
        public long? HM_HOUSEID { get; set; }
        public string HM_HOUSECODE { get; set; }
        public string HM_HOUSENAME { get; set; }
        public bool HM_ISACTIVE { get; set; }
        public long? HM_CREATEDUID { get; set; }
        public long? HM_SCM_SCHOOLID { get; set; }
        public DateTime? HM_CREATEDDATE { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region StreamMasters_STRM
    public class StreamMasters_STRM
    {
        public long? STRM_STREAMID { get; set; }
        public long? STRM_CM_CLASSID { get; set; }
        public string STRM_STREAMNAME { get; set; }
        public bool STRM_ISACTIVE { get; set; }
        public long? STRM_CREATEDUID { get; set; }
        public long? STRM_SCM_SCHOOLID { get; set; }
        public long? STRM_SM_SESSIONID { get; set; }
        public DateTime? STRM_CREATEDDATE { get; set; }
        public string STRM_CM_CLASSNAME { get; set; }

        public int? Userid { get; set; }
    }
    #endregion
    #region BloodGroupMasters_BGM
    public class BloodGroupMasters_BGM
    {
        public long? BGM_BLDGRPID { get; set; }
        public string BGM_BLDGRPNAME { get; set; }
        public bool BGM_ISACTIVE { get; set; }
        public long? BGM_CREATEDUID { get; set; }
        public DateTime? BGM_CREATEDDATE { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region GradeMaster_GM
    public class GradeMaster_GM
    {
        public long? GM_Grade_Id { get; set; }
        public string GM_GradeName { get; set; }
        public string GM_FromGrade { get; set; }
        public string GM_ToGrade { get; set; }
        public long? GM_SCM_SchoolID { get; set; }
        public long? GM_SM_SessionID { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region HolidayMaster_HM
    public class HolidayMaster_HM
    {
        public long? HM_Id { get; set; }
        public string HM_HolidayName { get; set; }
        public DateTime? HM_FromDate { get; set; }
        public string HM_FromDateS
        {
            get
            {
                return HM_FromDate.HasValue
                    ? HM_FromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HM_FromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HM_FromDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? HM_ToDate { get; set; }
        public string HM_ToDateS
        {
            get
            {
                return HM_ToDate.HasValue
                    ? HM_ToDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HM_ToDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HM_ToDate.Value.Year
                    : string.Empty;
            }
        }
        public long? HM_SchoolId { get; set; }
        public long? HM_SessionId { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region NoticeMasters_NM
    public class NoticeMasters_NM
    {
        public string NM_ClassSectionMap { get; set; }
        public long? NM_Id { get; set; }
        public DateTime? NM_EntryDate { get; set; }
        public string NM_EntryDateS
        {
            get
            {
                return NM_EntryDate.HasValue
                    ? NM_EntryDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      NM_EntryDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      NM_EntryDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? NM_ExpDate { get; set; }
        public string NM_ExpDateS
        {
            get
            {
                return NM_ExpDate.HasValue
                    ? NM_ExpDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      NM_ExpDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      NM_ExpDate.Value.Year
                    : string.Empty;
            }
        }

        public string NM_Title { get; set; }
        public string NM_Notice { get; set; }
        public string NM_UploadFile { get; set; }
        public bool? NM_IsPublish { get; set; }
        public long? NM_SchoolId { get; set; }
        public string NM_ClassId { get; set; }
        public string NM_SectionId { get; set; }
        public long? NM_SessionId { get; set; }
        public DateTime? NM_CreatedDate { get; set; }
        public string NM_CreatedDateS
        {
            get
            {
                return NM_CreatedDate.HasValue
                    ? NM_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      NM_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      NM_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? NM_CreatedBy { get; set; }
        public DateTime? NM_EditedDate { get; set; }
        public string NM_EditedDateS
        {
            get
            {
                return NM_EditedDate.HasValue
                    ? NM_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      NM_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      NM_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? NM_EditedBy { get; set; }
        public int? Userid { get; set; }
        public string NM_CM_CLASSNAME { get; set; }
        public string NM_SECM_SECTIONNAME { get; set; }
        public string NM_ClassSectionPair { get; set; }
        public long? NM_FacultyId { get; set; }
        public long? NM_NtId { get; set; }
        public string NM_StudentId { get; set; }
        public string NM_Link { get; set; }



    }
    public class StudentSearchModel
    {
        public string SD_StudentId { get; set; }
        public string SD_AdmissionNo { get; set; }
        public string SD_StudentName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
    }
    #endregion
    #region  PeriodMaster_PM
    public class PeriodMaster_PM
    {
        public long? PM_Id { get; set; }
        public string PM_FromTime { get; set; }
        public string PM_ToTime { get; set; }
        public string PM_Period { get; set; }
        public long? PM_SchoolId { get; set; }
        public long? PM_SessionId { get; set; }
        public long? PM_CreateBy { get; set; }
        public DateTime? PM_CreateDateTime { get; set; }
        public string PM_CreateDateTimeS
        {
            get
            {
                return PM_CreateDateTime.HasValue
                    ? PM_CreateDateTime.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      PM_CreateDateTime.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      PM_CreateDateTime.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? PM_EditDateTime { get; set; }
        public string PM_EditDateTimeS
        {
            get
            {
                return PM_EditDateTime.HasValue
                    ? PM_EditDateTime.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      PM_EditDateTime.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      PM_EditDateTime.Value.Year
                    : string.Empty;
            }
        }
        public long? PM_EditBy { get; set; }
        public int? Userid { get; set; }
        public long? PM_ClassId { get; set; }
        public string CTM_TYPEID { get; set; }
        public string CTM_TYPENAME { get; set; }
        public string PM_ClassName { get; set; }

    }
    #endregion
    #region ClassTypeBoard_CTB
    public class ClassTypeBoard_CTB
    {
        public long? CTB_SCHBRDID { get; set; }
        public long? CTB_TYPEID { get; set; }
        public long? CTB_BOARDID { get; set; }
        public bool? CTB_ISACTIVE { get; set; }
        public long? CTB_SCHOOLID { get; set; }
        public long? CTB_CREATEDUID { get; set; }
        public DateTime? CTB_CREATEDDATE { get; set; }
        public string CTB_CREATEDDATES
        {
            get
            {
                return CTB_CREATEDDATE.HasValue
                    ? CTB_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CTB_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CTB_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public string CTB_CTM_TYPENAME { get; set; }
        public string CTB_BM_BOARDNAME { get; set; }
        public int? Userid { get; set; }

    }
    #endregion
    #region RouteMastes_RT
    public class RouteMastes_RT
    {
        public long? RT_ROUTEID { get; set; }
        public bool RT_ISACTIVE { get; set; }
        public long? RT_SCHOOLID { get; set; }
        public long? RT_CREATEDUID { get; set; }
        public DateTime? RT_CREATEDDATE { get; set; }
        public string RT_CREATEDDATES
        {
            get
            {
                return RT_CREATEDDATE.HasValue
                    ? RT_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      RT_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      RT_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public string RT_ROUTENAME { get; set; }
        public string RT_LOCATION { get; set; }
        public int? Userid { get; set; }

    }
    #endregion
    #region RoutewiseDropMaster_RDM
    public class RoutewiseDropMaster_RDM
    {
        public RoutewiseDropMaster_RDM()
        {
            RouteWiseDropList = new List<RoutewiseDropMaster_RDM>();
        }
        public long? RDM_ROUTEMAPID { get; set; }
        public long? RDM_ROUTEID { get; set; }
        public string RDM_DROPPOINT { get; set; }
        public decimal RDM_DISTANCE { get; set; }
        public decimal RDM_RATE { get; set; }
        public int? RDM_SERIAL { get; set; }
        public bool? RDM_ISACTIVE { get; set; }
        public long? RDM_SCHOOLID { get; set; }
        public long? RDM_CREATEDUID { get; set; }
        public string RDM_RT_ROUTENAME { get; set; }
        public long? RDM_SESSIONID { get; set; }
        public int? Userid { get; set; }
        public List<RoutewiseDropMaster_RDM> RouteWiseDropList { get; set; }
    }
    #endregion
    #region SubjectGroupMaster_SGM
    public class SubjectGroupMaster_SGM
    {
        public long? SGM_SubjectGroupID { get; set; }
        public string SGM_SubjectGroupName { get; set; }
        public long? SGM_SCHOOLID { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region SubjectMaster_SBM
    public class SubjectMaster_SBM
    {
        public long? SBM_Id { get; set; }
        public string SBM_SubjectCode { get; set; }
        public string SBM_SubjectName { get; set; }
        public long? SBM_SchoolId { get; set; }
        public long? SBM_SubGr_Id { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region SubjectGroupWiseSubjectSetting_SGS
    public class SubjectGroupWiseSubjectSetting_SGS
    {
        public long? SGS_Id { get; set; }
        public long? SGS_GM_Id { get; set; }
        public long? SGS_SM_Id { get; set; }
        public long? School_ID { get; set; }
        public int? Userid { get; set; }
        public string SGS_SGM_SubjectGroupName { get; set; }
        public string SGS_SBM_SubjectName { get; set; }
    }
    #endregion
    #region ClsSubGrWiseSubSetting_CSGWS
    public class ClsSubGrWiseSubSetting_CSGWS
    {
        public ClsSubGrWiseSubSetting_CSGWS()
        {
            ClassWiseSubjectList = new List<ClsSubGrWiseSubSetting_CSGWS>();

        }
        public long? CSGWS_SubGr_Id { get; set; }
        public long? CSGWS_Sub_Id { get; set; }
        public long? CSGWS_School_Id { get; set; }
        public long? CSGWS_Class_Id { get; set; }
        public int? Userid { get; set; }
        public long? CSGWS_Id { get; set; }
        public string CSGWS_SGM_SubjectGroupName { get; set; }
        public string CSGWS_SBM_SubjectName { get; set; }
        public string CSGWS_CM_CLASSNAME { get; set; }
        public long? Session_Id { get; set; }
        public List<ClsSubGrWiseSubSetting_CSGWS> ClassWiseSubjectList { get; set; }

    }
    #endregion

    //#region UserMaster_UM

    //public class UserMaster_UM
    //{
    //    public class UserMasterUM
    //    {
    //        public long? UM_UserId { get; set; }
    //        public string UM_LoginId { get; set; }
    //        public string UM_Password { get; set; }
    //        public string UM_UserName { get; set; }
    //        public string UM_UserEmail { get; set; }
    //        public string UM_UserMobile { get; set; }
    //        // public string UM_SecQues { get; set; }
    //        // public string UM_SecPassword { get; set; }
    //        public bool? UM_IsActive { get; set; }
    //        // public long? UM_SCM_SchoolId { get; set; }
    //        public string UM_CreatedUId { get; set; }
    //        public DateTime? UM_CreatedDate { get; set; }
    //        public string UM_UserType { get; set; }
    //        public long? UM_RoleId { get; set; }
    //        // public long? UM_FP_Id { get; set; }
    //        public string TransType { get; set; }
    //        public long? OutPutId { get; set; }
    //    }
    //}
    //#endregion

    #region FacluttyProfileMasters_FPM
    public class FacultyProfileMasters_FPM
    {
        public long? FP_Id { get; set; }
        public string FP_Name { get; set; }
        public long? FP_DesignationId { get; set; }
        public DateTime? FP_DateOfJoining { get; set; }
        public string FP_DateOfJoiningS
        {
            get
            {
                return FP_DateOfJoining.HasValue
                    ? FP_DateOfJoining.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FP_DateOfJoining.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FP_DateOfJoining.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? FP_DateOfBirth { get; set; }
        public string FP_DateOfBirthS
        {
            get
            {
                return FP_DateOfBirth.HasValue
                    ? FP_DateOfBirth.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FP_DateOfBirth.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FP_DateOfBirth.Value.Year
                    : string.Empty;
            }
        }
        public string FP_Address { get; set; }
        public string FP_Degree { get; set; }
        public string FP_Phone { get; set; }
        public string FP_Email { get; set; }
        [StringLength(6, MinimumLength = 6), RegularExpression("[^0-9]*$", ErrorMessage = "The field {0} should be numeric.")]
        public string FP_Pin { get; set; }
        public string FP_FacultyCode { get; set; }
        public string FP_UserType { get; set; }
        public long? FP_SchoolId { get; set; }
        public long? FP_SessionId { get; set; }
        public long? FP_CreateBy { get; set; }
        public DateTime? FP_CreateDate { get; set; }
        public string FP_CreateDateS
        {
            get
            {
                return FP_CreateDate.HasValue
                    ? FP_CreateDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FP_CreateDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FP_CreateDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? FP_EditDate { get; set; }
        public string FP_EditDateS
        {
            get
            {
                return FP_EditDate.HasValue
                    ? FP_EditDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FP_EditDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FP_EditDate.Value.Year
                    : string.Empty;
            }
        }

        public long? FP_EditBy { get; set; }
        public int? Userid { get; set; }
        public long? FP_ClassId { get; set; }
        public long? FP_SubjectId { get; set; }
        public string FP_ShortName { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string DM_Name { get; set; }
        public string FP_Password { get; set; }
        public long? FP_Max_DEGM_Id { get; set; }
    }
    #endregion
    #region  DesignationMaster_DM
    public class DesignationMaster_DM
    {
        public long? DM_Id { get; set; }
        public string DM_Name { get; set; }
        public long? DM_SchoolId { get; set; }
        public int? Userid { get; set; }
    }

    #endregion
    #region  ClassWiseFacultyMasters_CWF
    public class ClassWiseFacultyMasters_CWF
    {
        public ClassWiseFacultyMasters_CWF()
        {
            ClasswiseFacultyList = new List<ClassWiseFacultyMasters_CWF>();
        }
        public long? CWF_Id { get; set; }
        public long? CWF_FacId { get; set; }
        public long? CWF_ClassId { get; set; }
        public long? CWF_SchoolId { get; set; }
        public long? CWF_SessionId { get; set; }
        public DateTime? CWF_CreateDate { get; set; }
        public string CWF_CreateDateS
        {
            get
            {
                return CWF_CreateDate.HasValue
                    ? CWF_CreateDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CWF_CreateDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CWF_CreateDate.Value.Year
                    : string.Empty;
            }
        }

        public long? CWF_CreateBy { get; set; }
        public DateTime? CWF_EditDate { get; set; }
        public string CWF_EditDateS
        {
            get
            {
                return CWF_EditDate.HasValue
                    ? CWF_EditDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CWF_EditDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CWF_EditDate.Value.Year
                    : string.Empty;
            }
        }
        public long? CWF_EditBy { get; set; }
        public string CWF_FacultyName { get; set; }
        public long? CWF_SectionId { get; set; }
        public long? CWF_SubjectId { get; set; }
        public int? Userid { get; set; }
        public string CWF_FPM_FP_Name { get; set; }
        public string CWF_SBM_SubjectName { get; set; }
        public string CWF_CM_CLASSNAME { get; set; }
        public string CWF_SectionName { get; set; }
        public List<ClassWiseFacultyMasters_CWF> ClasswiseFacultyList { get; set; }
    }
    #endregion
    #region  ClassWiseTeacherRoutine_CWTR
    public class ClassWiseTeacherRoutine_CWTR
    {
        public ClassWiseTeacherRoutine_CWTR()
        {
            RoutineList = new List<ClassWiseTeacherRoutine_CWTR>();
        }
        public long? CWTR_Id { get; set; }
        public long? CWTR_Class { get; set; }
        public long? CWTR_Section { get; set; }
        public long? CWTR_Day { get; set; }
        public long? CWTR_Period { get; set; }
        public long? CWTR_Teacher { get; set; }
        public long? CWTR_Subject { get; set; }
        public long? CWTR_SchoolId { get; set; }
        public long? CWTR_SessionId { get; set; }
        public DateTime? CWTR_CreateDate { get; set; }
        public string CWTR_CreateDateS
        {
            get
            {
                return CWTR_CreateDate.HasValue
                    ? CWTR_CreateDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CWTR_CreateDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CWTR_CreateDate.Value.Year
                    : string.Empty;
            }
        }

        public long? CWTR_CreateBy { get; set; }
        public DateTime? CWTR_EditDate { get; set; }
        public string CWTR_EditDateS
        {
            get
            {
                return CWTR_EditDate.HasValue
                    ? CWTR_EditDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CWTR_EditDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CWTR_EditDate.Value.Year
                    : string.Empty;
            }
        }
        public long? CWTR_EditBy { get; set; }
        public int? Userid { get; set; }
        public string CWTR_CM_CLASSNAME { get; set; }
        public string CWTR_SECM_SECTIONNAME { get; set; }
        public string CWTR_DM_DayName { get; set; }
        public string CWTR_SBM_SubjectName { get; set; }
        public string CWTR_FP_Name { get; set; }
        public int? CWTR_ClassPreference { get; set; }
        public string CWTR_UploadFile { get; set; }
        public string CWTR_Title { get; set; }
        public string CWTR_Description { get; set; }
        public bool? Rutine_IsUpload { get; set; }

        public string CWTR_PeriodName { get; set; }
        public List<ClassWiseTeacherRoutine_CWTR> RoutineList { get; set; }

    }
    #endregion
    #region DayMaster_DM
    public class DayMaster_DM
    {
        public long? DM_DayId { get; set; }
        public string DM_DayName { get; set; }
    }

    #endregion
    #region StudentEnquery_ENQ
    public class StudentEnquery_ENQ
    {
        public DateTime? FromDate { get; set; }
        public string FromDateS
        {
            get
            {
                return FromDate.HasValue
                    ? FromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ToDate { get; set; }
        public string ToDateS
        {
            get
            {
                return ToDate.HasValue
                    ? ToDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Year
                    : string.Empty;
            }
        }

        public long? ENQ_Id { get; set; }
        public string ENQ_EnqueryNo { get; set; }
        public string ENQ_StudentName { get; set; }
        public string ENQ_GuardianName { get; set; }
        public string ENQ_MobNo { get; set; }
        [Range(3, 10)]
        public DateTime? ENQ_DOB { get; set; }
        public string ENQ_DOBS
        {
            get
            {
                return ENQ_DOB.HasValue
                    ? ENQ_DOB.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ENQ_DOB.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ENQ_DOB.Value.Year
                    : string.Empty;
            }
        }
        public string ENQ_Age { get; set; }
        public string ENQ_SexId { get; set; }
        public long? ENQ_ClassId { get; set; }
        public long? ENQ_SessionId { get; set; }
        public long? ENQ_CreatedBy { get; set; }
        public DateTime? ENQ_CreatedDate { get; set; }
        public string ENQ_CreatedDateS
        {
            get
            {
                return ENQ_CreatedDate.HasValue
                    ? ENQ_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ENQ_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ENQ_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? ENQ_EditedBy { get; set; }
        public DateTime? ENQ_EditedDate { get; set; }
        public string ENQ_EditedDateS
        {
            get
            {
                return ENQ_EditedDate.HasValue
                    ? ENQ_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ENQ_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ENQ_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ENQ_Date { get; set; }
        public string ENQ_DateS
        {
            get
            {
                return ENQ_Date.HasValue
                    ? ENQ_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ENQ_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ENQ_Date.Value.Year
                    : string.Empty;
            }
        }

        public long? ENQ_SchoolId { get; set; }
        public string ENQ_CM_CLASSNAME { get; set; }
        public bool? ENQ_IsForm { get; set; }
        public bool? ENQ_IsAdmitted { get; set; }
        public string ENQ_FormNo { get; set; }
        public string ENQ_Type { get; set; }
        public decimal? ENQ_FormAmount { get; set; }
        public string ENQ_AlternativeMobNo { get; set; }
        public int? Userid { get; set; }
        //public StudentEnquery_ENQ() {
        //    ENQ_IsForm = true;
        //}

        public string Paymode { get; set; }
        public string payCash { get; set; }
        public string payCheque { get; set; }
        public string payDD { get; set; }
        public string payCard { get; set; }
        public string payOnline { get; set; }

        public string BankName { get; set; }

        public string BranchName { get; set; }

        public string CheqDDNo { get; set; }

        public DateTime? CheqDDDate { get; set; }
        public string CheqDDDateS
        {
            get
            {
                return CheqDDDate.HasValue
                    ? CheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CheqDDDate.Value.Year
                    : string.Empty;
            }
        }


        public string Card_TrnsRefNo { get; set; }
        public string PaymodeType { get; set; }

        public bool? ENQ_IsTest { get; set; }
        public string ENQ_DocFile { get; set; }
        public string ENQ_ObMarks { get; set; }
        public string ENQ_Attributes { get; set; }



    }
    #endregion
    #region STUDENTCATEGORY_CAT
    public class STUDENTCATEGORY_CAT
    {
        public long? CAT_CATEGORYID { get; set; }
        public string CAT_STUDENTCATEGORY { get; set; }
        public bool CAT_ISACTIVE { get; set; }
        public long? CAT_CREATEDUID { get; set; }
        public DateTime? CAT_CREATEDDATE { get; set; }
        public string CAT_CREATEDDATES
        {
            get
            {
                return CAT_CREATEDDATE.HasValue
                    ? CAT_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CAT_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CAT_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public long? CAT_SCHOOLID { get; set; }
        public int? Userid { get; set; }
    }


    #endregion
    #region StudetDetails_SD
    public class StudetDetails_SD : StudentEnquery_ENQ
    {
        public StudetDetails_SD()
        {
            TransportList = new List<TransportType_TT>();
        }
        public List<TransportType_TT> TransportList { get; set; }
        public List<StudetDetails_SD> StudentDetailsList { get; set; }
        public List<FeesDetail_SD> FeesList { get; set; }
        public long? SD_Id { get; set; }
        public string SD_AppliactionNo { get; set; }
        public string SD_FormNo { get; set; }
        public string SD_StudentId { get; set; }
        public string SD_Password { get; set; }
        public DateTime? SD_AppliactionDate { get; set; }
        public string SD_AppliactionDateS
        {
            get
            {
                return SD_AppliactionDate.HasValue
                    ? SD_AppliactionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Year
                    : string.Empty;
            }
        }

        public long? SD_ClassId { get; set; }
        public long? SD_CurrentClassId { get; set; }
        public int? SD_CurrentRoll { get; set; }
        public long? SD_CurrentSectionId { get; set; }
        public string SD_StudentName { get; set; }
        public string Sd_SexId { get; set; }
        public DateTime? SD_DOB { get; set; }
        public string SD_DOBS
        {
            get
            {
                return SD_DOB.HasValue
                    ? SD_DOB.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Year
                    : string.Empty;
            }
        }

        public long? SD_CasteId { get; set; }
        public string SD_FatherName { get; set; }
        public string SD_MotherName { get; set; }
        public string SD_PresentAddress { get; set; }
        public string SD_PermanentAddress { get; set; }
        public long? SD_StateId { get; set; }
        public long? SD_DistrictId { get; set; }
        [StringLength(6, MinimumLength = 6), RegularExpression("[^0-9]*$", ErrorMessage = "The field {0} should be numeric.")]
        public string SD_PinCode { get; set; }
        public string SD_ContactNo1 { get; set; }
        public string SD_ContactNo2 { get; set; }
        public string SD_EmailId { get; set; }
        public string SD_LastSchoolName { get; set; }
        public string SD_TCNo { get; set; }
        public DateTime? SD_TCDate { get; set; }
        public string SD_TCDateS
        {
            get
            {
                return SD_TCDate.HasValue
                    ? SD_TCDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_TCDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_TCDate.Value.Year
                    : string.Empty;
            }
        }

        public string SD_Photo { get; set; }
        public int? SD_StudentCategoryId { get; set; }
        public int? SD_MotherTongueId { get; set; }
        public long? SD_CreatedBy { get; set; }
        public DateTime? SD_CreatedDate { get; set; }
        public string SD_CreatedDateS
        {
            get
            {
                return SD_CreatedDate.HasValue
                    ? SD_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? SD_EditedBy { get; set; }
        public DateTime? SD_EditedDate { get; set; }
        public string SD_EditedDateS
        {
            get
            {
                return SD_EditedDate.HasValue
                    ? SD_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
        public long? SD_SessionId { get; set; }
        public int? SD_BloodGroupId { get; set; }
        public long? SD_SchoolId { get; set; }
        public string SD_CM_CLASSNAME { get; set; }
        public int? SD_SecondLanguageId { get; set; }
        public int? SD_ThirdLanguageId { get; set; }
        public string SD_AadharNo { get; set; }
        public int SD_ReligionId { get; set; }
        public int SD_BGId { get; set; }
        public int SD_TCType { get; set; }
        public int SD_HouseId { get; set; }
        public int SD_PermanentDistrictId { get; set; }
        public string SD_PermanentPin { get; set; }
        public int SD_PermanentStateId { get; set; }
        public bool? SD_IsSingleParent { get; set; }

        public string SD_SMSNo { get; set; }
        public string SD_PoliceStation { get; set; }
        public string SD_LocalGuardianName { get; set; }
        public string SD_LocalGuardianPhoneNo { get; set; }
        public int? SD_NationalityId { get; set; }
        public string SD_PermanentPoliceStation { get; set; }
        public string SD_CurrentSection { get; set; }
        public bool? SD_IsSubjectAssigned { get; set; }
        public int? SD_TransportTypeId { get; set; }
        public bool? SD_IsTransport { get; set; }
        public decimal SD_TransportFare { get; set; }
        public int? SD_RouteId { get; set; }
        public int? SD_PickLocationId { get; set; }
        public int? SD_DropLocationId { get; set; }
        public string SD_ROUTENAME { get; set; }
        public string SD_DropPoint { get; set; }
        public string SD_PickPoint { get; set; }
        public decimal? SD_HostelFare { get; set; }

        public int? SD_TransportMonthId { get; set; }
        public string TransType { get; set; }
        public string SECM_SECTIONNAME { get; set; }

        public long? SD_CurrentSessionId { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SM_SESSIONNAME { get; set; }

        public string SD_GQualification { get; set; }
        public string SD_GOccupation { get; set; }
        public string SD_GDesignation { get; set; }
        public string SD_GIncome { get; set; }
        public string SD_OldStudentId { get; set; }


        public DateTime? StartDate { get; set; }
        public string StartDateS
        {
            get
            {
                return StartDate.HasValue
                    ? StartDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      StartDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      StartDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? EndDate { get; set; }
        public string EndDateS
        {
            get
            {
                return EndDate.HasValue
                    ? EndDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      EndDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      EndDate.Value.Year
                    : string.Empty;
            }
        }
      
    }

    #endregion
    #region DDLList
    public class DDLList
    {
        public int? Value { get; set; }
        public string Text { get; set; }
    }
    #endregion
    #region Marksheet
    public class clsStudentList
    {
        public string StudentId { get; set; }
        public long Roll { get; set; }
        public string StudentName { get; set; }
        public long? ClassId { get; set; }
        public long? SectionId { get; set; }
        public long? SchoolId { get; set; }
        public long? SessionId { get; set; }
        public Boolean? IsAbsent { get; set; }
        public string Edit { get; set; }
        public long? SubgrId { get; set; }
        public long? SubId { get; set; }
        public long? TermId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string TermName { get; set; }
        public long SME_ID { get; set; }
        public long? SD_Id { get; set; }
        public string SD_AppliactionNo { get; set; }
        public string SD_RegistrationNo { get; set; }
        public string SessionName { get; set; }
        public string StudentPromoteStatus { get; set; }
        public List<clsStudentList> PromotedStudentList { get; set; }

        public clsStudentList()
        {
            PromotedStudentList = new List<clsStudentList>();
        }

    }
    public class clsStudentMarksEntry
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public long? SME_Id { get; set; }
        public long? SME_ClassId { get; set; }
        public long? SME_SectionId { get; set; }
        public long? SME_SchoolId { get; set; }
        public long? SME_SessionId { get; set; }
        public long? SME_SubjectId { get; set; }
        public decimal? SME_FullMarks { get; set; }
        public decimal? SME_PassMarks { get; set; }
        public decimal? SME_FullMarksP { get; set; }
        public decimal? SME_PassMarksP { get; set; }
        public string SME_Date { get; set; }
        public long? UserId { get; set; }
        public List<clsStudentMarksTransaction> StudentMarksTransactionList { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public long? SME_TermId { get; set; }
        public string SchoolName { get; set; }
        public string SM_SubjectName { get; set; }
        public bool? IsPractical { get; set; }
        public string TM_TermName { get; set; }
        public string SubGrpName { get; set; }
        public long? SME_SubjectGrpID { get; set; }

        public string SME_ExamGroupId { get; set; }
        public clsStudentMarksEntry()
        {
            StudentMarksTransactionList = new List<clsStudentMarksTransaction>();
        }
    }
    public class clsStudentMarksTransaction
    {
        public long? SMT_Id { get; set; }
        public string StudentId { get; set; }
        public decimal? SMT_MarksObtained { get; set; }
        public decimal? SMT_MarksObtainedP { get; set; }
        public long? SMT_SME_Id { get; set; }
        public string Grade { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }

    }
    #endregion Marksheet
    #region StudentAdditionalDetails_AD
    public class StudentAdditionalDetails_AD
    {
        public long? AD_Id { get; set; }
        public string AD_AdmissionId { get; set; }
        public string AD_FormNo { get; set; }
        public string AD_StudentId { get; set; }
        public string AD_Fathername { get; set; }
        public string AD_FatherQualification { get; set; }
        public string AD_FatherOccupation { get; set; }
        public string AD_FatherDesignation { get; set; }
        public string AD_FatherMoblileNo { get; set; }
        public string AD_FatherEmailId { get; set; }
        public string AD_FatherAadharNo { get; set; }
        public string AD_MotherName { get; set; }
        public string AD_MotherQualification { get; set; }
        public string AD_MotherOccupation { get; set; }
        public string AD_MotherDesignation { get; set; }
        public string AD_MotherMoblileNo { get; set; }
        public string AD_MotherEmailId { get; set; }
        public string AD_MotherAadharNo { get; set; }
        public long? AD_CreatedBy { get; set; }
        public DateTime? AD_CreatedDate { get; set; }
        public string AD_CreatedDateS
        {
            get
            {
                return AD_CreatedDate.HasValue
                    ? AD_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      AD_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      AD_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public string AD_SiblingName2 { get; set; }
        public string AD_SiblingName1 { get; set; }
        public decimal? AD_Sibling1_Age { get; set; }
        public decimal? AD_Sibling2_Age { get; set; }
        public string AD_PreviousSchool { get; set; }
        public int? AD_PreviousSchoolType { get; set; }
        public int? AD_PreviousSchoolLanguageId { get; set; }
        public int? AD_PreviousSchoolBoardId { get; set; }
        public string AD_PreviousSchoolAddress { get; set; }
        public int? AD_PreviousSchoolCity { get; set; }
        public int? AD_PreviousSchoolStateId { get; set; }
        public int? AD_PreviousSchoolCountryId { get; set; }
        public string AD_PreviousSchoolPin { get; set; }
        public string AD_PreviousSchoolPhoneNo { get; set; }
        public string AD_LastClassId { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region UIDDLSelect
    public class UIDDLSelect<T>
    {
        [Required(ErrorMessage = "Please select a value.")]
        public string Text { get; set; }
        public int Value { get; set; }
        public List<T> List { get; set; }
        public UIDDLSelect()
        {
            List = new List<T>();
        }
    }
    #endregion
    #region DDLSelect
    public class DDLSelect<T>
    {
        public string Text { get; set; }
        public int? Value { get; set; }
        public List<T> List { get; set; }
        public DDLSelect()
        {
            List = new List<T>();
        }
    }
    #endregion
    #region ClassWisefees_CF
    public class ClassWisefees_CF
    {
        public ClassWisefees_CF()
        {
            ClassWiseFeesList = new List<ClassWisefees_CF>();
        }

        public long? CF_CLASSFEESID { get; set; }
        public long? CF_SESSIONID { get; set; }
        public Int32? CF_CLASSID { get; set; }
        public Int32? CF_CATEGORYID { get; set; }
        public long? CF_FEESID { get; set; }
        public Int32? CF_NOOFINS { get; set; }
        public decimal? CF_FEESAMOUNT { get; set; }
        public Int32? CF_INSTALLMENTNO { get; set; }
        public decimal? CF_INSAMOUNT { get; set; }
        public DateTime? CF_DUEDATE { get; set; }
        public string CF_DUEDATES
        {
            get
            {
                return CF_DUEDATE.HasValue
                    ? CF_DUEDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CF_DUEDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CF_DUEDATE.Value.Year
                    : string.Empty;
            }
        }

        public bool? CF_ISACTIVE { get; set; }
        public long? CF_SCHOOLID { get; set; }
        public long? CF_CREATEDUID { get; set; }
        public DateTime? CF_CREATEDDATE { get; set; }
        public string CF_CREATEDDATES
        {
            get
            {
                return CF_CREATEDDATE.HasValue
                    ? CF_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CF_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CF_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public long? CF_TBMCLASSFEES { get; set; }
        public bool? CF_ISFEES { get; set; }
        public long? CF_FESS_GEN_NO { get; set; }
        public int? Userid { get; set; }
        public string Testdata { get; set; }
        public string DUEDATES { get; set; }
        public string ClassName { get; set; }
        public string FeesHeadName { get; set; }
        public string Category { get; set; }
        public string CAT_CATEGORYNAME { get; set; }
        public List<ClassWisefees_CF> ClassWiseFeesList { get; set; }
        public bool? IsAdmissionTime { get; set; }

    }

    public class ClassFeesForward
    {
       
        public long? ClassId { get; set; }
      
        public long? SessionId { get; set; }
        public long? SchoolId { get; set; }
        public string ClassName { get; set; }
       
        public List<ClassFeesForward> ClassFeesForwardList { get; set; }

        public ClassFeesForward()
        {
            ClassFeesForwardList = new List<ClassFeesForward>();
        }

    }
    #endregion
    #region InsType_INTYP
    public class InsType_INTYP
    {
        public int INTYP_INSTYPEID { get; set; }
        public string INTYP_INSTYPENAME { get; set; }
        public int INTYP_INSTYPEVALUE { get; set; }
        public long? Userid { get; set; }

    }

    #endregion
    #region FeesMaster_FEM
    public class FeesMaster_FEM
    {
        public long? FEM_FEESID { get; set; }
        public string FEM_FEESNAME { get; set; }
        public long? FEM_INSTYPEID { get; set; }
        public long? FEM_TOTALINSTALLMENT { get; set; }
        public Int32 FEM_NOOFINSTALLMENT { get; set; }
        public bool FEM_ISADMINTIMEMAN { get; set; }
        public bool? FEM_ISREFUNDABLE { get; set; }
        public bool? FEM_ISHOSTELFEES { get; set; }
        public bool? FEM_ISTRANSPORTFEES { get; set; }
        public bool? FEM_ISDUPDOCFEES { get; set; }
        public bool? FEM_ISDUPIDFEES { get; set; }
        public bool? FEM_ISPROCESSINGFEES { get; set; }
        public bool FEM_ISACTIVE { get; set; }
        public bool  FEM_ISONLYADMISSION { get; set; }
        public long? FEM_SCHOOLID { get; set; }
        public long? FEM_CREATEDUID { get; set; }
        public DateTime? FEM_CREATEDDATE { get; set; }
        public string FEM_CREATEDDATES
        {
            get
            {
                return FEM_CREATEDDATE.HasValue
                    ? FEM_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FEM_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FEM_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public bool? FEM_AdditionalFees { get; set; }
        public bool? REFUNDABLE { get; set; }
        public bool? HOSTELFEES { get; set; }
        public bool? TRANSPORTFEES { get; set; }
        public bool? DUPDOCFEES { get; set; }
        public bool? DUPIDFEES { get; set; }
        public bool? PROCESSINGFEES { get; set; }
        public bool? FEM_NONE { get; set; }
        public long? Userid { get; set; }
        public decimal? FEM_AMOUNT { get; set; }
        public List<UIRenderValue> Instypes { get; set; }
        public FeesMaster_FEM()
        {
            Instypes = new List<UIRenderValue>();
        }
    }
    public class UIRenderValue
    {
        public long? ID { get; set; }
        public string Name { get; set; }
        public int value { get; set; }
    }
    #endregion
    #region ReligionMaster_RM
    public class ReligionMaster_RM
    {
        public int? RM_ReligionId { get; set; }
        public string RM_ReligionName { get; set; }
    }
    #endregion
    #region TCTypeMaster_TM
    public class TCTypeMaster_TM
    {
        public int? TCTypeId { get; set; }
        public string TcTypeName { get; set; }


    }
    #endregion
    #region StudentwiseSubjectSetting_SWS
    public class StudentwiseSubjectSetting_SWS
    {
        public StudentwiseSubjectSetting_SWS()
        {
            StudentWiseSubjectList = new List<StudentwiseSubjectSetting_SWS>();
        }
        public long? SWS_Id { get; set; }
        public string SWS_StudentSID { get; set; }
        public string SWS_StudentName { get; set; }
        public long? SWS_SubGroupId { get; set; }
        public long? SWS_SubjectId { get; set; }
        public long? SWS_SubjectGrPreferId { get; set; }
        public long? SWS_CreatedBy { get; set; }
        public DateTime? SWS_CreatedOn { get; set; }
        public string SWS_CreatedOnS
        {
            get
            {
                return SWS_CreatedOn.HasValue
                    ? SWS_CreatedOn.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SWS_CreatedOn.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SWS_CreatedOn.Value.Year
                    : string.Empty;
            }
        }

        public long? SWS_EditedBy { get; set; }
        public DateTime? SWS_EditedOn { get; set; }
        public string SWS_EditedOnS
        {
            get
            {
                return SWS_EditedOn.HasValue
                    ? SWS_EditedOn.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SWS_EditedOn.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SWS_EditedOn.Value.Year
                    : string.Empty;
            }
        }

        public bool? SWS_IsAssigned { get; set; }
        public bool? SWS_isOptional { get; set; }
        public int? SWS_ClassId { get; set; }
        public int? SWS_SecId { get; set; }
        public int? Userid { get; set; }
        public List<StudentwiseSubjectSetting_SWS> StudentWiseSubjectList { get; set; }
        public long? SWS_SchoolId { get; set; }
        public long? SWS_SessionId { get; set; }
        public string SWS_SubjectCode { get; set; }
        public string SWS_SubjectName { get; set; }
        public string SWS_SubjectGroupName { get; set; }

    }
    #endregion
    #region Sec_Roll_Setting_SR
    public class Sec_Roll_Setting_SR
    {
        public Sec_Roll_Setting_SR()
        {
            SecRollList = new List<Sec_Roll_Setting_SR>();
        }
        public long? SR_ID { get; set; }
        public string SR_SETTINGID { get; set; }
        public long? SR_CLASSID { get; set; }
        public string SR_STUDENTID { get; set; }
        public long? SR_SECTIONID { get; set; }
        public string SR_ROLLNO { get; set; }
        public long? SR_SESSIONID { get; set; }
        public long? SR_SCHOOLID { get; set; }
        public long? SR_CREATEDUID { get; set; }
        public DateTime? SR_CREATEDDATE { get; set; }
        public string SR_CREATEDDATES
        {
            get
            {
                return SR_CREATEDDATE.HasValue
                    ? SR_CREATEDDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SR_CREATEDDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SR_CREATEDDATE.Value.Year
                    : string.Empty;
            }
        }

        public bool? SR_SL_No { get; set; }
        public int? Userid { get; set; }
        public List<Sec_Roll_Setting_SR> SecRollList { get; set; }
    }
    #endregion
    #region LateFeesMaster
    public class LateFeesSlap
    {
        public long? Id { get; set; }
        public string Slap_Name { get; set; }
        public decimal? Slap_Amount { get; set; }
        public long Slap_FineTypeID { get; set; }
        public long  FineTypeId   { get; set; }
        public long Slap_SchooIID { get; set; }
        public long Slap_SessionID { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }

        public DateTime dueDate { get; set; }
        public string dueDateS
        {
            get
            {
                return dueDate.Day.ToString().PadLeft(2, '0') + "/" +
                      dueDate.Month.ToString().PadLeft(2, '0') + "/" +
                      dueDate.Year;
            }
        }


    }
    #endregion LateFeesMaster
    #region TransportType_TT
    public class TransportType_TT
    {
        public int? TypeId { get; set; }
        public string TransportType { get; set; }
        public long Userid { get; set; }
        public int? RouteId { get; set; }
        public int? PickUpId { get; set; }
        public int? DropId { get; set; }
        public decimal Fare { get; set; }
        public string SD_StudentId { get; set; }
        public int? TransportMonthId { get; set; }
    }
    #endregion
    #region MSTR_FormAmount
    public class MSTR_FormAmount
    {
        public long? ID { get; set; }
        public string Slap_Name { get; set; }
        public decimal Slap_Amount { get; set; }
        public long? Slap_SchooIID { get; set; }
        public long? Slap_SessionID { get; set; }
    }
    #endregion
    #region StudentAttendenceMaster

    public class StudentAttendanceDetailsRequest
    {
        public DateTime? Date { get; set; }
        public string DateS
        {
            get
            {
                return Date.HasValue
                    ? Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Date.Value.Year
                    : string.Empty;
            }
        }
        public long? SchoolId { get; set; }
        public long? ClassId { get; set; }
        public long? SectionId { get; set; }
        public long? SessionId { get; set; }
    }

    public class StudentAttendanceDetailsResponse
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public long? ClassId { get; set; }
        public string ClassName { get; set; }
        public long? SectionId { get; set; }
        public string SectionName { get; set; }
        public long? Roll { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsHalfDay { get; set; }
        public bool IsLateComing { get; set; }
        public string IsAttendanceTaken { get; set; }
    }
    public class StudentAttendenceMaster_SAM
    {
        public StudentAttendenceMaster_SAM()
        {
            attendnceList = new List<StudentAtendenceTransaction_SAT>();
        }
        public DateTime? FromDateS { get; set; }
        public string FromDateSS
        {
            get
            {
                return FromDateS.HasValue
                    ? FromDateS.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FromDateS.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FromDateS.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ToDateS { get; set; }
        public string ToDateSS
        {
            get
            {
                return ToDateS.HasValue
                    ? ToDateS.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ToDateS.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ToDateS.Value.Year
                    : string.Empty;
            }
        }

        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public int? SAM_Id { get; set; }
        public int? SAM_ClassId { get; set; }
        public int? SAM_SectionId { get; set; }
        public long? SAM_SchoolId { get; set; }
        public long? SAM_SessionId { get; set; }
        public DateTime? SAM_Date { get; set; }
        public string SAM_DateS
        {
            get
            {
                return SAM_Date.HasValue
                    ? SAM_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SAM_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SAM_Date.Value.Year
                    : string.Empty;
            }
        }

        public string SAM_Date_S { get; set; }
        public int? Userid { get; set; }
        public List<StudentAtendenceTransaction_SAT> attendnceList { get; set; }
        public string Edit { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int? ABSEND { get; set; }
        public int? PRESENT { get; set; }
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
        public int? Workingdays { get; set; }
        public decimal? Percentage { get; set; }
        public DateTime? fDate { get; set; }
        public string fDateS
        {
            get
            {
                return fDate.HasValue
                    ? fDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      fDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      fDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? tDate { get; set; }
        public string tDateS
        {
            get
            {
                return tDate.HasValue
                    ? tDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      tDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      tDate.Value.Year
                    : string.Empty;
            }
        }

        public Boolean? IsAbsent { get; set; }
        public long? TotalStudent { get; set; }
        public long? PresentStudent { get; set; }
        public long? HalfDayStudent { get; set; }
        public long? LateComingStudent { get; set; }
        public long? AbsentStudent { get; set; }
        public Boolean? IsHalfDay { get; set; }
        public Boolean? IsLateComing { get; set; }
    }
    #endregion
    #region StudentAtendenceTransaction_SAT
    public class StudentAtendenceTransaction_SAT
    {
        public int? Id { get; set; }
        public string StudentId { get; set; }
        public string Roll { get; set; }
        public string StudentName { get; set; }
        public int? ClassId { get; set; }
        public int? SectionId { get; set; }
        public int? SchoolId { get; set; }
        public int? SessionId { get; set; }
        public Boolean? IsAbsent { get; set; }
        public Boolean? IsHalfDay { get; set; }
        public Boolean? IsLateComing { get; set; }
        public string Edit { get; set; }
        public int? SubgrId { get; set; }
        public int? SubId { get; set; }
        public int? TermId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string TermName { get; set; }
        public long SME_ID { get; set; }
    }
    #endregion
    #region StudentExamAttandanceMaster_SEA
    public class StudentExamAttandanceMaster_SEA
    {
        public StudentExamAttandanceMaster_SEA()
        {
            ExamAttendanceList = new List<StudentExamAttendanceDetails>();
        }
        public DateTime? FromDate { get; set; }
        public string FromDateS
        {
            get
            {
                return FromDate.HasValue
                    ? FromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ToDate { get; set; }
        public string ToDateS
        {
            get
            {
                return ToDate.HasValue
                    ? ToDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Year
                    : string.Empty;
            }
        }

        public long? SEA_Attn_Id { get; set; }
        public DateTime? SEA_Attn_Date { get; set; }
        public string SEA_Attn_DateS
        {
            get
            {
                return SEA_Attn_Date.HasValue
                    ? SEA_Attn_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SEA_Attn_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SEA_Attn_Date.Value.Year
                    : string.Empty;
            }
        }

        public string SEA_Attn_Date_S { get; set; }
        public int? SEA_TermId { get; set; }
        public int? SEA_SubjectId { get; set; }
        public int? SEA_ClassId { get; set; }
        public int? SEA_SecId { get; set; }
        public int? SEA_SubType { get; set; }
        public long? SEA_SchoolId { get; set; }
        public long? SEA_SessionId { get; set; }
        public int? Userid { get; set; }
        public string SEA_StudentName { get; set; }
        public string SEA_StudentId { get; set; }
        public string SEA_ClassName { get; set; }
        public int? SEA_RollNo { get; set; }
        public string SEA_SectionName { get; set; }
        public bool? SEA_isAbsent { get; set; }
        public string SEA_SubjectName { get; set; }
        public string SEA_TermName { get; set; }
        public string SBM_SubjectName { get; set; }

        public List<StudentExamAttendanceDetails> ExamAttendanceList { get; set; }
    }
    #endregion
    #region StudentExamAttendanceDetails
    public class StudentExamAttendanceDetails
    {
        public long? SEAD_Id { get; set; }
        public long? SEAD_Attn_Id { get; set; }
        public string SEAD_StudentId { get; set; }
        public bool? SEAD_IsAbsent { get; set; }
    }
    #endregion
    #region HostelMaster_HM
    public class HostelMaster_HM
    {
        public HostelMaster_HM()
        {
            HostelDataList = new List<HostelMaster_HM>();
        }
        public long? Hostel_Id { get; set; }
        public long? hostel_SchoolId { get; set; }
        public long? hostel_SessionId { get; set; }
        public string Hostel_StudentId { get; set; }
        public int? Hostel_RoomNo { get; set; }
        public decimal? Hoset_Fare { get; set; }
        public int? Hostel_Month { get; set; }
        public int? Hostel_FeesId { get; set; }
        public string Hostel_FeesName { get; set; }
        public List<HostelMaster_HM> HostelDataList { get; set; }
        public long? Hostel_CreatedBy { get; set; }
        public DateTime? Hostel_CreatedDate { get; set; }
        public string Hostel_CreatedDateS
        {
            get
            {
                return Hostel_CreatedDate.HasValue
                    ? Hostel_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Hostel_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Hostel_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? Hostel_EditedBy { get; set; }
        public DateTime? Hostel_EditedDate { get; set; }
        public string Hostel_EditedDateS
        {
            get
            {
                return Hostel_EditedDate.HasValue
                    ? Hostel_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Hostel_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Hostel_EditedDate.Value.Year
                    : string.Empty;
            }
        }

    }
    #endregion
    #region HostelRoomMaster_HR
    public class HostelRoomMaster_HR
    {
        public int? HR_HostelRoomId { get; set; }
        public long? HR_HostelSchoolId { get; set; }
        public long? HR_HostelSessionId { get; set; }
        public int? HR_HostelId { get; set; }
        public string HR_HostelRoomNo { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region HostelTransactionMaster_HTM
    public class HostelTransactionMaster_HTM : Hostel_FeesCollectionDetails
    {
        public HostelTransactionMaster_HTM()
        {
            PaidFeesStructure = new List<HostelTransactionMaster_HTM>();
            FeesStructure = new List<HostelTransactionMaster_HTM>();
        }
        public long? HTM_Id { get; set; }
        public string HTM_TransId { get; set; }
        public long? HTM_FeesHeadId { get; set; }
        public string HTM_TransType { get; set; }
        public long? HTM_InstalmentNo { get; set; }
        public string HTM_StudentId { get; set; }
        public long? HTM_SchoolId { get; set; }
        public long? HTM_SessionId { get; set; }
        public decimal? HTM_MonthlyFare { get; set; }
        public decimal? HTM_PaidAmount { get; set; }
        public long? HTM_SerialNo { get; set; }
        public string HTM_FeesName { get; set; }
        public List<HostelTransactionMaster_HTM> PaidFeesStructure { get; set; }
        public List<HostelTransactionMaster_HTM> FeesStructure { get; set; }
        public long? HTM_CreatedBy { get; set; }
        public DateTime? HTM_CreatedDate { get; set; }
        public string HTM_CreatedDateS
        {
            get
            {
                return HTM_CreatedDate.HasValue
                    ? HTM_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HTM_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HTM_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? HTM_EditedBy { get; set; }
        public DateTime? HTM_EditedDate { get; set; }
        public string HTM_EditedDateS
        {
            get
            {
                return HTM_EditedDate.HasValue
                    ? HTM_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HTM_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HTM_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public string HTM_StudentName { get; set; }
        public string HTM_ClassName { get; set; }

        public DateTime? fromDate { get; set; }
        public string fromDateS
        {
            get
            {
                return fromDate.HasValue
                    ? fromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? toDate { get; set; }
        public string toDateS
        {
            get
            {
                return toDate.HasValue
                    ? toDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Year
                    : string.Empty;
            }
        }

        public long?  ClassId { get; set; }
        public string FeesName { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SCM_FULLNAME { get; set; }
        public string SCM_SCHOOLADDRESS1 { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string SD_StudentName { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public string BankName { get; set; }
       


    }

    #endregion
    #region Hostel_FeesCollectionDetails
    public class Hostel_FeesCollectionDetails
    {
        public long? HFD_Id { get; set; }
        public string HFD_FeesTransId { get; set; }
        public string HFD_Paymentmode { get; set; }
        public decimal HFD_PaidAmount { get; set; }
        public long? HFD_DiscountedBy { get; set; }
        public DateTime? HFD_FeesCollectionDate { get; set; }
        public string HFD_FeesCollectionDateS
        {
            get
            {
                return HFD_FeesCollectionDate.HasValue
                    ? HFD_FeesCollectionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HFD_FeesCollectionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HFD_FeesCollectionDate.Value.Year
                    : string.Empty;
            }
        }

        public Int32? HFD_BankId { get; set; }
        public string HFD_BranchName { get; set; }
        public string HFD_CheqDDNo { get; set; }
        public DateTime? HFD_CheqDDDate { get; set; }
        public string HFD_CheqDDDateS
        {
            get
            {
                return HFD_CheqDDDate.HasValue
                    ? HFD_CheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HFD_CheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HFD_CheqDDDate.Value.Year
                    : string.Empty;
            }
        }

        public string HFD_Card_TrnsRefNo { get; set; }
        public long? HFD_CreatedBy { get; set; }
        public DateTime? HFD_CreatedDate { get; set; }
        public string HFD_CreatedDateS
        {
            get
            {
                return HFD_CreatedDate.HasValue
                    ? HFD_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HFD_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HFD_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? HFD_EditedBy { get; set; }
        public DateTime? HFD_EditedDate { get; set; }
        public string HFD_EditedDateS
        {
            get
            {
                return HFD_EditedDate.HasValue
                    ? HFD_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      HFD_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      HFD_EditedDate.Value.Year
                    : string.Empty;
            }
        }


    }
    #endregion
    #region BankMaster_BM
    public class BankMaster_BM
    {
        public int? BankId { get; set; }
        public string BankName { get; set; }
        public long? SchoolId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedDateS
        {
            get
            {
                return CreatedDate.HasValue
                    ? CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? EditedBy { get; set; }
        public DateTime? EditedDate { get; set; }
        public string EditedDateS
        {
            get
            {
                return EditedDate.HasValue
                    ? EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public int? Userid { get; set; }
    }
    #endregion
    #region MonthMaster_MM
    public class MonthMaster_MM
    {
        public int? MonthId { get; set; }
        public string MonthName { get; set; }
    }
    #endregion
    #region TransportFeesTransaction_TR
    public class TransportFeesTransaction_TR : TranasportTransactionDetails_TD
    {
        public TransportFeesTransaction_TR()
        {
            PaidFeesStructure = new List<TransportFeesTransaction_TR>();
            FeesStructure = new List<TransportFeesTransaction_TR>();
        }
        public string transId { get; set; }
        public long? TR_Id { get; set; }
        public string TR_TransId { get; set; }
        public Int32? TR_FeesHeadId { get; set; }
        public string TR_TransType { get; set; }
        public Int32? TR_InstallmentMonth { get; set; }
        public string TR_StudentId { get; set; }
        public long? TR_SchoolId { get; set; }
        public long? TR_SessionId { get; set; }
        public decimal TR_MonthlyFare { get; set; }
        public long? TR_SerialNo { get; set; }
        public decimal TR_PaidAmount { get; set; }
        public long? TR_CreatedBy { get; set; }
        public DateTime? Tr_CreatedDate { get; set; }
        public string Tr_CreatedDateS
        {
            get
            {
                return Tr_CreatedDate.HasValue
                    ? Tr_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Tr_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Tr_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long TR_EditedBy { get; set; }
        public DateTime? TR_EditedDate { get; set; }
        public string TR_EditedDateS
        {
            get
            {
                return TR_EditedDate.HasValue
                    ? TR_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TR_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TR_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public string TR_FeesName { get; set; }
        public string TR_StudentName { get; set; }
        public string TR_ClassName { get; set; }
        public Int32? TR_ClassId { get; set; }

        public DateTime? TD_FeesCollectionDate { get; set; }
        public string TD_FeesCollectionDateS
        {
            get
            {
                return TD_FeesCollectionDate.HasValue
                    ? TD_FeesCollectionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TD_FeesCollectionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TD_FeesCollectionDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? fromDate { get; set; }
        public string fromDateS
        {
            get
            {
                return fromDate.HasValue
                    ? fromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      fromDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? toDate { get; set; }
        public string toDateS
        {
            get
            {
                return toDate.HasValue
                    ? toDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      toDate.Value.Year
                    : string.Empty;
            }
        }

        public string FeesName { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SCM_FULLNAME { get; set; }
        public string SCM_SCHOOLADDRESS1 { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string SD_StudentName { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public string BankName { get; set; }

        public List<TransportFeesTransaction_TR> PaidFeesStructure { get; set; }
        public List<TransportFeesTransaction_TR> FeesStructure { get; set; }
 

    }
    #endregion
    #region TranasportTransactionDetails_TD
    public class TranasportTransactionDetails_TD
    {
        public long? TD_Id { get; set; }
        public string TD_TransId { get; set; }
        public string TD_Paymentmode { get; set; }
        public long? TD_PaidAmount { get; set; }
        public long? TD_DiscountedBy { get; set; }
        public DateTime? TD_FeesCollectionDate { get; set; }
        public string TD_FeesCollectionDateS
        {
            get
            {
                return TD_FeesCollectionDate.HasValue
                    ? TD_FeesCollectionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TD_FeesCollectionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TD_FeesCollectionDate.Value.Year
                    : string.Empty;
            }
        }

        public Int32? TD_BankId { get; set; }
        public string TD_BranchName { get; set; }
        public string TD_CheqDDNo { get; set; }
        public DateTime? TD_CheqDDDate { get; set; }
        public string TD_CheqDDDateS
        {
            get
            {
                return TD_CheqDDDate.HasValue
                    ? TD_CheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TD_CheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TD_CheqDDDate.Value.Year
                    : string.Empty;
            }
        }

        public string TD_Card_TrnsRefNo { get; set; }
        public long? TD_CreatedBy { get; set; }
        public DateTime? TD_CreatedDate { get; set; }
        public string TD_CreatedDateS
        {
            get
            {
                return TD_CreatedDate.HasValue
                    ? TD_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TD_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TD_CreatedDate.Value.Year
                    : string.Empty;
            }
        }

        public long? TD_EditedBy { get; set; }
        public DateTime? TD_EditedDate { get; set; }
        public string TD_EditedDateS
        {
            get
            {
                return TD_EditedDate.HasValue
                    ? TD_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      TD_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      TD_EditedDate.Value.Year
                    : string.Empty;
            }
        }

    }
    #endregion
    #region TC Master
    //ID, TCTypeId, SM_SESSIONID, SCM_SCHOOLID, CM_CLASSID, SECM_SECTIONID, SD_StudentId, SD_TCNo, SD_TCDate, IsDisplay
    public class TCMaster
    {
        public Int32 ID { get; set; }
        public Int64? UM_USERID { get; set; }
        public Int32 TCTypeId { get; set; }
        public Int64? SM_SESSIONID { get; set; }
        public Int64? SCM_SCHOOLID { get; set; }
        public Int32? CM_CLASSID { get; set; }
        public Int32 SECM_SECTIONID { get; set; }
        public string SD_StudentId { get; set; }
        public string SD_TCNo { get; set; }
        public string SD_TCDate { get; set; }
        public DateTime EditedDate { get; set; }
        public string EditedDateS
        {
            get
            {
                return EditedDate.Day.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Month.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Year;
            }
        }

        public string SD_StudentName { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SD_ContactNo1 { get; set; }
        public string TC_Fees { get; set; }
        public string IsDisplay { get; set; }

        public string TC_ANumber { get; set; }


        public string SD_FatherName { get; set; }
        public string SD_MotherName { get; set; }
        public string SD_AppliactionNo { get; set; }
        public DateTime? SD_AppliactionDate { get; set; }
        public string SD_AppliactionDateS
        {
            get
            {
                return SD_AppliactionDate.HasValue
                    ? SD_AppliactionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? SD_DOB { get; set; }
        public string SD_DOBS
        {
            get
            {
                return SD_DOB.HasValue
                    ? SD_DOB.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Year
                    : string.Empty;
            }
        }

        public string AdmittedClassName { get; set; }
        public string CSM_CASTENAME { get; set; }
        public string NM_NATIONNAME { get; set; }
        public string SGM_SubjectGroupName { get; set; }
        public string SubjectNames { get; set; }




        public string DOB_Words { get; set; }
        public string DOB_Proof { get; set; }
        public string Last_Exam { get; set; }
        public string FailedStatus { get; set; }
        public string Q_Promotion { get; set; }
        public string Last_DuePaid { get; set; }
        public string Fee_Consession { get; set; }
        public string No_Wdays { get; set; }
        public string No_WPdays { get; set; }
        public string NCC_Cadet_Scout_Guide { get; set; }
        public string Games_Activities { get; set; }
        public string School_Category { get; set; }
        public string Genral_Conduct { get; set; }
        public DateTime? AP_Date { get; set; }
        public string AP_DateS
        {
            get
            {
                return AP_Date.HasValue
                    ? AP_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      AP_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      AP_Date.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? Issue_Date { get; set; }
        public string Issue_DateS
        {
            get
            {
                return Issue_Date.HasValue
                    ? Issue_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Issue_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Issue_Date.Value.Year
                    : string.Empty;
            }
        }

        public string Reason_Leave { get; set; }
        public string Remarks { get; set; }

    }
    #endregion
    #region ClassWise Syllabus
    public class ClassWiseSyllabusMasters_CWSM
    {
        public long? SM_Id { get; set; }
        public string StudentId { get; set; }
        public string SM_SyllabusName { get; set; }
        public string SM_UploadFile { get; set; }
        public long? SM_SchoolId { get; set; }
        public long? SM_ClassId { get; set; }
        public long? SM_SessionId { get; set; }
        public DateTime? SM_CreateDate { get; set; }
        public string SM_CreateDateS
        {
            get
            {
                return SM_CreateDate.HasValue
                    ? SM_CreateDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SM_CreateDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SM_CreateDate.Value.Year
                    : string.Empty;
            }
        }

        public long? SM_CreateBy { get; set; }
        public int? Userid { get; set; }
        public string SM_ClassName { get; set; }
        public List<ClassWiseSyllabusMasters_CWSM> ClassWiseSyllabusList { get; set; }
    }
    #endregion
    #region AssignmentMaster_ASM
    public class AssignmentMaster_ASM
    {

        public long? ASM_ID { get; set; }
        public long? ASM_FP_Id { get; set; }
        public long? ASM_SubGr_ID { get; set; }
        public long? ASM_Sub_ID { get; set; }
        public long? ASM_School_ID { get; set; }
        public long? ASM_Class_ID { get; set; }
        public long? ASM_Section_ID { get; set; }
        public long? ASM_Session_ID { get; set; }
        public string ASM_Title { get; set; }
        public string ASM_Desc { get; set; }
        public int? CM_ID { get; set; }
        public DateTime? ASM_StartDate { get; set; }
        public string ASM_StartDateS
        {
            get
            {
                return ASM_StartDate.HasValue
                    ? ASM_StartDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ASM_StartDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ASM_StartDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ASM_ExpDate { get; set; }
        public string ASM_ExpDateS
        {
            get
            {
                return ASM_ExpDate.HasValue
                    ? ASM_ExpDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ASM_ExpDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ASM_ExpDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime Created_Date { get; set; }
        public string Created_DateS
        {
            get
            {
                return Created_Date.Day.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Month.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Year;
            }
        }

        public string ASM_SGM_SubjectGroupName { get; set; }
        public string ASM_SBM_SubjectName { get; set; }
        public string ASM_UploadDoc { get; set; }
        public string ASM_CM_CLASSNAME { get; set; }
        public string ASM_SECM_SECTIONNAME { get; set; }
        public string SBM_SubjectName { get; set; }
        public long? Userid { get; set; }
      

    }

    public class AssignmentMarkUpdate
    {

        public long? ASM_ID { get; set; }
        public string AST_Marks { get; set; }
        public string AST_Remarks { get; set; }
        public string AST_StudentId { get; set; }
        public string AST_TotalMarks { get; set; }
        
    }
    #endregion
    #region ClassWiseLiveclass_CWLS
    public class ClassWiseLiveclass_CWLS
    {
        public long? CWLS_ID { get; set; }
        public long? CWLS_SubGr_ID { get; set; }
        public long? CWLS_Sub_ID { get; set; }
        public long? CWLS_School_ID { get; set; }
        public long? CWLS_Class_ID { get; set; }
        public long? CWLS_Section_ID { get; set; }
        public long? CWLS_Session_ID { get; set; }
        public string CWLS_Title { get; set; }
        public string CWLS_Link { get; set; }
        public int? CM_ID { get; set; }
        public DateTime CWLS_ClassDate { get; set; }
        public string CWLS_ClassDateS
        {
            get
            {
                return  CWLS_ClassDate.Day.ToString().PadLeft(2, '0') + "/" +
                      CWLS_ClassDate.Month.ToString().PadLeft(2, '0') + "/" +
                      CWLS_ClassDate.Year;
            }
        }

        public string CWLS_ClassTime { get; set; }
        public DateTime Created_Date { get; set; }
        public string Created_DateS
        {
            get
            {
                return Created_Date.Day.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Month.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Year;
            }
        }
        public string CWLS_SGM_SubjectGroupName { get; set; }
        public string CWLS_SBM_SubjectName { get; set; }
        public string CWLS_CM_CLASSNAME { get; set; }
        public string CWLS_SECM_SECTIONNAME { get; set; }
        public string SBM_SubjectName { get; set; }
        public long? Userid { get; set; }

    }
    #endregion
    #region LeaveCategory Master
    public class LeaveType_LT
    {
        public int? LeaveTypeId { get; set; }
        public string LeaveTypeCode { get; set; }
        public string LeaveTypeName { get; set; }
        public string LeaveCategory { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public long? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateS
        {
            get
            {
                return CreateDate.Day.ToString().PadLeft(2, '0') + "/" +
                      CreateDate.Month.ToString().PadLeft(2, '0') + "/" +
                      CreateDate.Year;
            }
        }
        public long? ModifyBy { get; set; }
        public DateTime ModifyDate { get; set; }
        public string ModifyDateS
        {
            get
            {
                return ModifyDate.Day.ToString().PadLeft(2, '0') + "/" +
                      ModifyDate.Month.ToString().PadLeft(2, '0') + "/" +
                      ModifyDate.Year;
            }
        }
        public long? SchoolId { get; set; }
        public long? SessionId { get; set; }
        public int? Userid { get; set; }
    }
    #endregion
    #region StudentDiary_STD
    public class StudentDiary_STD
    {
        public long? DiaryId { get; set; }
        public string StudentId { get; set; }
        public long? TeacherId { get; set; }
        public long? ClassId { get; set; }
        public long? SectionId { get; set; }
        public string DiaryType { get; set; }
        public string Topic { get; set; }
        public string AttachmentPath { get; set; }
        public long? SesssioId { get; set; }
        public long? SchoolId { get; set; }
        public DateTime? ApDate { get; set; }
        public string ApDateS
        {
            get
            {
                return ApDate.HasValue
                    ? ApDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ApDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ApDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? CreatedOn { get; set; }
        public string CreatedOnS
        {
            get
            {
                return CreatedOn.HasValue
                    ? CreatedOn.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Value.Year
                    : string.Empty;
            }
        }

        public long? Userid { get; set; }
        public string CLASSNAME { get; set; }
        public string SECTIONNAME { get; set; }
    }
    #endregion
    #region StudentEarlyLeave_ERL
    public class StudentEarlyLeave_ERL
    {
        public long? ERL_ID { get; set; }
        public long? ERL_ClassId { get; set; }
        public long? ERL_SectionId { get; set; }
        public string ERL_StudentId { get; set; }
        public DateTime ERL_Date { get; set; }
        public string ERL_DateS
        {
            get
            {
                return ERL_Date.Day.ToString().PadLeft(2, '0') + "/" +
                      ERL_Date.Month.ToString().PadLeft(2, '0') + "/" +
                      ERL_Date.Year;
            }
        }
        public string ERL_Reason { get; set; }
        public string ERL_Note { get; set; }
        public string ERL_PickupDetails { get; set; }
        public long? ERL_FacultyId { get; set; }
        public long? ERL_SesssioId { get; set; }
        public long? ERL_SchoolId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnS
        {
            get
            {
                return CreatedOn.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Year;
            }
        }

        public long? Userid { get; set; }
        public string CLASSNAME { get; set; }
        public string SECTIONNAME { get; set; }
        public string FACULTYNAME { get; set; }
    }
    #endregion
    #region ID card
    public class StudentModel
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassName { get; set; }
        public string Section { get; set; }
        public DateTime DOB { get; set; }
        public string DOBS
        {
            get
            {
                return DOB.Day.ToString().PadLeft(2, '0') + "/" +
                      DOB.Month.ToString().PadLeft(2, '0') + "/" +
                      DOB.Year;
            }
        }
        public string BloodGroup { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string ContactNo { get; set; }
        public string EmergencyNo { get; set; }
        public string Address { get; set; }
        public string PhotoPath { get; set; }
        public string SessionName { get; set; }



        public string SchoolName { get; set; }
        public string ScAddress { get; set; }
        public string ScPhone1 { get; set; }
        public string ScPhone2 { get; set; }
        public string ScEmail { get; set; }
        public string ScWebsite { get; set; }
        public string ScPhotoPath { get; set; }

        public int CardType { get; set; }
        public string CardTypeName { get; set; }

        public string SchoolCode { get; set; }
        public string AffNo { get; set; }

        public string PrincipalSignature { get; set; }
    }

    #endregion
    #region StudentManual
    public class StudentManual
    {
        public int SD_Id { get; set; }
        public string SD_AppliactionNo { get; set; }
        public string SD_StudentId { get; set; }
        public string SD_Password { get; set; }
        public DateTime SD_AppliactionDate { get; set; }
        public string SD_AppliactionDateS
        {
            get
            {
                return SD_AppliactionDate.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Year;
            }
        }


        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SD_StudentName { get; set; }
        public string SD_FatherName { get; set; }
        public string SD_PresentAddress { get; set; }
        public string SD_PermanentAddress { get; set; }
        public string SD_ContactNo1 { get; set; }
        public string SD_ContactNo2 { get; set; }
        public string SD_EmailId { get; set; }
        public string SessionName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolFullName { get; set; }
        public string SchoolAddress1 { get; set; }
        public string SchoolAddress2 { get; set; }
        public string SchoolOfficeAddress { get; set; }
        public string SchoolContactPerson { get; set; }
        public string SchoolPhoneNo1 { get; set; }
        public string SchoolPhoneNo2 { get; set; }
        public string SchoolSecretaryName { get; set; }
        public string SchoolEmailId { get; set; }
        public string SchoolWebsite { get; set; }
        public string SchoolLogo { get; set; }
        public string SchoolAffNo { get; set; }



    }
    #endregion
    #region ExamGroup
    public class ExamGroupMaster_EGM
    {
        public long EGM_Id { get; set; }
        public long? EGM_TermId { get; set; }
        public string EGM_Name { get; set; }
        public string EGM_Code { get; set; }
        public long? EGM_ClassId { get; set; }
        public long? EGM_SchoolId { get; set; }
        public long? EGM_SessionId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string CreatedOnS
        {
            get
            {
                return CreatedOn.HasValue
                    ? CreatedOn.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Value.Year
                    : string.Empty;
            }
        }
        public long? Userid { get; set; }

        public bool IsFinal { get; set; }


        public string CLASSNAME { get; set; }

        public string ExamType { get; set; }

    }
    public class ExamGroupDetailDto
    {
        public long? TermId { get; set; }
        public long? ClassId { get; set; }
        public bool SelectExam { get; set; }
        public bool IsFinal { get; set; }
      
    }

    public class ExamGroupPostDto
    {
        public long EGM_Id { get; set; }
        public long? EGM_TermId { get; set; }
        public string EGM_Name { get; set; }
        public long? EGM_SchoolId { get; set; }
        public long? EGM_SessionId { get; set; }
        public long? Userid { get; set; }
        public bool IsFinal { get; set; }
        public List<ExamGroupDetailDto> ExamGroupDetails { get; set; }
    }
    #endregion
    #region FineTypeMster
    public class FineTypeMaster
    {
        public long? FineTypeId { get; set; }
        public string FineTypeName { get; set; }


    }
    #endregion

    #region MiscellaneousHeadMaster_MISC
    public class MiscellaneousHeadMaster_MISC
    {
        public long? MISC_Id { get; set; }
        public long? MISC_SchoolId { get; set; }
        public long? MISC_SessionId { get; set; }
        public string MISC_FeeHeadCode { get; set; }
        public string MISC_FeeHeadName { get; set; }
        public decimal? MISC_Amount { get; set; }
        public bool? MISC_IsActive { get; set; }
        public long? MISC_CreatedBy { get; set; }
        public DateTime MISC_CreatedDate { get; set; }
        public string MISC_CreatedDateS
        {
            get
            {
                return MISC_CreatedDate.Day.ToString().PadLeft(2, '0') + "/" +
                      MISC_CreatedDate.Month.ToString().PadLeft(2, '0') + "/" +
                      MISC_CreatedDate.Year;
            }
        }


        public long? MISC_ModifiedBy { get; set; }
        public DateTime MISC_ModifiedDate { get; set; }
        public string MISC_ModifiedDateS
        {
            get
            {
                return MISC_ModifiedDate.Day.ToString().PadLeft(2, '0') + "/" +
                      MISC_ModifiedDate.Month.ToString().PadLeft(2, '0') + "/" +
                      MISC_ModifiedDate.Year;
            }
        }
        public int? Userid { get; set; }
    }
    #endregion

    #region MISC COLLECTION
    public class MiscellaneousFeesDetails
    {
        public long MFD_Id { get; set; }
        public string MFD_FeesTransId { get; set; }
        public string MFD_Paymentmode { get; set; }
        public decimal? MFD_PaidAmount { get; set; }
        public long? MFD_DiscountedBy { get; set; }
        public DateTime? MFD_FeesCollectionDate { get; set; }
        public string MFD_FeesCollectionDateS
        {
            get
            {
                return MFD_FeesCollectionDate.HasValue
                    ? MFD_FeesCollectionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MFD_FeesCollectionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MFD_FeesCollectionDate.Value.Year
                    : string.Empty;
            }
        }
        public int? MFD_BankId { get; set; }
        public string MFD_BranchName { get; set; }
        public string MFD_CheqDDNo { get; set; }
        public DateTime? MFD_CheqDDDate { get; set; }
        public string MFD_CheqDDDateS
        {
            get
            {
                return MFD_CheqDDDate.HasValue
                    ? MFD_CheqDDDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MFD_CheqDDDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MFD_CheqDDDate.Value.Year
                    : string.Empty;
            }
        }
        public string MFD_Card_TrnsRefNo { get; set; }
        public long? MFD_CreatedBy { get; set; }
        public DateTime? MFD_CreatedDate { get; set; }
        public string MFD_CreatedDateS
        {
            get
            {
                return MFD_CreatedDate.HasValue
                    ? MFD_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MFD_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MFD_CreatedDate.Value.Year
                    : string.Empty;
            }
        }
        public long? MFD_EditedBy { get; set; }
        public DateTime? MFD_EditedDate { get; set; }
        public string MFD_EditedDateS
        {
            get
            {
                return MFD_EditedDate.HasValue
                    ? MFD_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MFD_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MFD_EditedDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? MFD__FeesCollectionDate { get; set; }
        public string MFD__FeesCollectionDateS
        {
            get
            {
                return MFD__FeesCollectionDate.HasValue
                    ? MFD__FeesCollectionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MFD__FeesCollectionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MFD__FeesCollectionDate.Value.Year
                    : string.Empty;
            }
        }
    }

    public class MiscellaneousTransactionMaster : MiscellaneousFeesDetails
    {
    
        public long MTM_Id { get; set; }
        public string MTM__TransId { get; set; }
        public int? MTM_FeesHeadId { get; set; }
        public string MTM_TransType { get; set; }
        public string MTM_StudentId { get; set; }
        public long? MTM_SchoolId { get; set; }
        public long? MTM_SessionId { get; set; }
        public decimal? MTM_Amount { get; set; }
        public long? MTM_SerialNo { get; set; }
        public decimal? MTM_PaidAmount { get; set; }
        public long? MTM_CreatedBy { get; set; }
        public long? SD_CurrentClassId { get; set; }

        public long?  CM_CLASSID { get; set; }
        public DateTime? MTM_CreatedDate { get; set; }
        public string MTM_CreatedDateS
        {
            get
            {
                return MTM_CreatedDate.HasValue
                    ? MTM_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MTM_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MTM_CreatedDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? FromDate { get; set; }
        public string FromDateS
        {
            get
            {
                return FromDate.HasValue
                    ? FromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      FromDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? ToDate { get; set; }
        public string ToDateS
        {
            get
            {
                return ToDate.HasValue
                    ? ToDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ToDate.Value.Year
                    : string.Empty;
            }
        }
        public long? MTM_EditedBy { get; set; }
        public DateTime? MTM_EditedDate { get; set; }
        public string MTM_EditedDateS
        {
            get
            {
                return MTM_EditedDate.HasValue
                    ? MTM_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      MTM_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      MTM_EditedDate.Value.Year
                    : string.Empty;
            }
        }

        public string MTM_Narration { get; set; }
        public string MTM_StudentName { get; set; }
        public string MTM_ClassName { get; set; }
        public string MTM_FeesName { get; set; }
        public string SD_StudentName { get; set; }
        public string SECM_SECTIONNAME { get; set; }
        public string SM_SESSIONNAME { get; set; }
        public string SCM_SCHOOLADDRESS1 { get; set; }
        public string SCM_FULLNAME { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string BankName { get; set; }
    }

    #endregion
    #region NoticeType
    public class NoticeType_NT
    {
        public long? NT_ID  { get; set; }
        public string NT_NAME { get; set; }
    }
    #endregion
    ///registration student///
    #region StudentRegistration_ST
    public class StudentRegistration_ST
    {

        [Key]  // <- This tells EF this is the primary key
        public long? ST_ID { get; set; }

        public string ST_CANDIDATEID { get; set; }
        public string ST_PASSWORD { get; set; }

        [Required(ErrorMessage = "Student name is required")]
        public string ST_NAME { get; set; }

        [Required(ErrorMessage = "Candidate DOB is required")]
        public DateTime ST_DOB { get; set; }

        public string ST_DOBS
        {
            get
            {
                return ST_DOB.Day.ToString().PadLeft(2, '0') + "/" +
                      ST_DOB.Month.ToString().PadLeft(2, '0') + "/" +
                      ST_DOB.Year;
            }
        }


        [Required(ErrorMessage = "Parent/guardian name is required")]
        public string ST_PG_DETAILS { get; set; }

        [Required(ErrorMessage = "Mobile No is required")]
        public string ST_MOBILENO { get; set; }

        [Required(ErrorMessage = "Email-Id is required")]
        public string ST_EMAIL { get; set; }
        public DateTime ST_REGDATE { get; set; }
        public string ST_REGDATES
        {
            get
            {
                return ST_REGDATE.Day.ToString().PadLeft(2, '0') + "/" +
                      ST_REGDATE.Month.ToString().PadLeft(2, '0') + "/" +
                      ST_REGDATE.Year;
            }
        }

        public string ST_SESSION { get; set; }
        public int? ST_AUTOID { get; set; }
        public int? ST_CLASS { get; set; }
        public string ST_OTP { get; set; }
        public string ST_ACTIVE { get; set; }
        //public int OutputCode { get; set; }
        //public string OutputMessage { get; set; }
        //public string ST_CANDIDATEID_OUTPUT { get; set; }
        //public string ST_PASSWORD_OUTPUT { get; set; }
    }
    public class SmsGatewayModel
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string SenderID { get; set; }
        public string Phno { get; set; }
        public string Msg { get; set; }
        public string EntityID { get; set; }
        public string TemplateID { get; set; }
        public string DlrUrl { get; set; }
        public int FlashMsg { get; set; }
        public string CampaignName { get; set; }
    }

    public class SmsResponse
    {
        public string Status { get; set; }
        public ResponseDetail Response { get; set; }
    }
    public class ResponseDetail
    {
        public string Message { get; set; }
    }
 



    #endregion
    #region StudentLogin
    public class LoginModel
    {
        [Required(ErrorMessage = "Candidate ID is required.")]
        public string ST_CANDIDATEID { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string ST_PASSWORD { get; set; }
        public string ST_CANDIDATEID_OUTPUT { get; set; }
        public string ST_PASSWORD_OUTPUT { get; set; }
    }
    #endregion
    #region StudentApplication
    public class GetSchoolDdl
    {
        public Int64 SA_ID { get; set; }
        public Int64 SCM_SCHOOLID { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SA_ST_CLASS { get; set; }
        public string SA_ST_SESSION { get; set; }
    }

    public class GetClassDdl
    {
        public Int64 SA_ID { get; set; }
        public Int64 SCM_SCHOOLID { get; set; }
        public Int64 CM_CLASSID { get; set; }
        public string CM_CLASSNAME { get; set; }
    }
    public class StudentApplication_AP
    {
        //public string SCM_SCHOOLNAME { get; set; }
        public Int64 SA_ID { get; set; }
        public Int64 SA_ST_ID { get; set; }
        public string SA_ST_CANDIDATEID { get; set; }
         //[Required(ErrorMessage = "Session is required")]
        public string SA_ST_SESSION { get; set; }
        //[Required(ErrorMessage = "School is required")]
        //public string AP_School { get; set; }
         //[Required(ErrorMessage = "Please select at least one class")]
         public List<string> AP_School { get; set; }
         //[Required(ErrorMessage = "Class is required")]
        public string SA_ST_CLASS { get; set; }
         //[Required(ErrorMessage = "Gender is required")]
         public string AP_Gender { get; set; }
         //[Required(ErrorMessage = "Nationality is required")]
        public string AP_Nationality { get; set; }
        //[Required(ErrorMessage = "Religion is required")]
        public string AP_Religion { get; set; }
           //[Required(ErrorMessage = "Caste is required")]
        public string AP_Caste { get; set; }
            //[Required(ErrorMessage = "Blood group is required")]
        public string AP_BloodGroup { get; set; }
        //[Required(ErrorMessage = "Aadhaar number is required")]
        //[StringLength(12, MinimumLength = 12, ErrorMessage = "Aadhaar must be 12 digits")]
        //[RegularExpression("^[0-9]{12}$", ErrorMessage = "Aadhaar must contain only numbers")]
        public string AP_AadhaarNo { get; set; }
         //[Required(ErrorMessage = "Permanent Address is required")]
        public string AP_PerAddress { get; set; }
         //[Required(ErrorMessage = "Present Address is required")]
        public string AP_PreAddress { get; set; }
        //[Required(ErrorMessage = "State is required")]
        public string state { get; set; }
        //[Required(ErrorMessage = "District is required")]
        public string district { get; set; }
        //[Required(ErrorMessage = "Police Station is required")]
        public string AP_PoliceStation { get; set; }
        //[Required(ErrorMessage = "Pin code is required")]
        //[RegularExpression("^[0-9]{6}$", ErrorMessage = "PIN must be 6 digits")]
        public string AP_Pin { get; set; }
        public string AP_LastSchoolName { get; set; }
        public string AP_TCNO { get; set; }
        public DateTime? AP_TCDate { get; set; }
        public string AP_TCDateS
        {
            get
            {
                return AP_TCDate.HasValue
                    ? AP_TCDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      AP_TCDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      AP_TCDate.Value.Year
                    : string.Empty;
            }
        }
        public string AP_TCTYPE { get; set; }
        public DateTime? AP_AdmissionDate { get; set; }
        public string AP_AdmissionDateS
        {
            get
            {
                return AP_AdmissionDate.HasValue
                    ? AP_AdmissionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      AP_AdmissionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      AP_AdmissionDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? OML_VALID_DATE { get; set; }
        public string OML_VALID_DATES
        {
            get
            {
                return OML_VALID_DATE.HasValue
                    ? OML_VALID_DATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      OML_VALID_DATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      OML_VALID_DATE.Value.Year
                    : string.Empty;
            }
        }
         //[Required(ErrorMessage = "Birth Certificate is required")]
        public HttpPostedFileBase AP_BirthCertificate { get; set; }
          //[Required(ErrorMessage = "ID Proof is required")]
        public HttpPostedFileBase AP_IdProof { get; set; }
        public HttpPostedFileBase AP_Marksheet { get; set; }
        public string ST_NAME { get; set; }
        public DateTime? ST_DOB { get; set; }
        public string ST_DOBS
        {
            get
            {
                return ST_DOB.HasValue
                    ? ST_DOB.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ST_DOB.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ST_DOB.Value.Year
                    : string.Empty;
            }
        }
        public string ST_PG_DETAILS { get; set; }
        public string ST_MOBILENO { get; set; }
            //[Required(ErrorMessage = "Post Office is required")]
        public string AP_PO { get; set; }
        public string AP_State { get; set; }
        public string ST_EMAIL { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public DateTime? ST_REGDATE { get; set; }
        public string ST_REGDATES
        {
            get
            {
                return ST_REGDATE.HasValue
                    ? ST_REGDATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ST_REGDATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ST_REGDATE.Value.Year
                    : string.Empty;
            }
        }
        public string AP_BirthCertificates { get; set; }
        public string AP_IdProofs { get; set; }
        public string AP_Marksheets { get; set; }
        public string AP_ApplicationNumber { get; set; }
        public DateTime? APPLICATION_DATE { get; set; }
        public string APPLICATION_DATES
        {
            get
            {
                return APPLICATION_DATE.HasValue
                    ? APPLICATION_DATE.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      APPLICATION_DATE.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      APPLICATION_DATE.Value.Year
                    : string.Empty;
            }
        }
        public string App_Status { get; set; }
        public string Validity_Status { get; set; }
        public string Admission_Status { get; set; }
    }

    public class OnlineMeritApplicationItem
    {
        public string SaId { get; set; }
        public string CandidateId { get; set; }
        public string Session { get; set; }
        public string Class { get; set; }
    }

    public class ApprovalFilterRequest
    {
        public string CLASS { get; set; }
        public string IsApprove { get; set; }
        public string IsAdmission { get; set; }
    }
    public class OnlineMeritApproval_STD
    {
        public long? SchoolId { get; set; }
        public long? Userid { get; set; }
        public string ValidityDate { get; set; }
        public List<OnlineMeritApplicationItem> Applications { get; set; }
    }
  
    #endregion
    #region othersss
    public class DeleteRequest
    {
        public int? id { get; set; }
    }


    public class DbResult
    {
        public string Result { get; set; }
        public string TransactionId { get; set; }
    }
    #endregion
    #region TCCertificate
    public class TCCertificate
    {
        public string SD_AppliactionNo { get; set; }
        public string SD_StudentId { get; set; }
        public string SD_StudentName { get; set; }
        public string SD_FatherName { get; set; }
        public string SD_MotherName { get; set; }
        public DateTime? SD_AppliactionDate { get; set; }
        public string SD_AppliactionDateS
        {
            get
            {
                return SD_AppliactionDate.HasValue
                    ? SD_AppliactionDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_AppliactionDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? SD_DOB { get; set; }
        public string SD_DOBS
        {
            get
            {
                return SD_DOB.HasValue
                    ? SD_DOB.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      SD_DOB.Value.Year
                    : string.Empty;
            }
        }
        public string SD_CurrentRoll { get; set; }

        // School Master Info
        public string SCM_SCHOOLCODE { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string SCM_FULLNAME { get; set; }
        public string SCM_SCHOOLADDRESS1 { get; set; }
        public string SCM_SCHOOLADDRESS2 { get; set; }
        public string SCM_OFFICEADDRESS { get; set; }
        public string SCM_CONTACTPERSON { get; set; }
        public string SCM_PHONENO1 { get; set; }
        public string SCM_PHONENO2 { get; set; }
        public string SCM_SECRETARYNAME { get; set; }
        public string SCM_EMAILID { get; set; }
        public string SCM_WEBSITE { get; set; }
        public string SCM_SCHOOLLOGO { get; set; }
        public string SCM_IMAGENAME { get; set; }
        public string SCM_AFFNO { get; set; }

        // Nationality & Caste
        public string NM_NATIONNAME { get; set; }
        public string CSM_CASTENAME { get; set; }

        // Class / Section Info
        public string JoinedClass { get; set; }
        public string CurrentClass { get; set; }
        public string SECM_SECTIONNAME { get; set; }

        // Subject Info
        public string SubjectGroupName { get; set; }
        public string SubjectNames { get; set; }

        // Transfer Certificate (TC) Info
        public string TC_ANumber { get; set; }
        public string DOB_Words { get; set; }
        public string DOB_Proof { get; set; }
        public string Last_Exam { get; set; }
        public string FailedStatus { get; set; }
        public string Q_Promotion { get; set; }
        public string Last_DuePaid { get; set; }
        public string Fee_Consession { get; set; }
        public string No_Wdays { get; set; }
        public string No_WPdays { get; set; }
        public string NCC_Cadet_Scout_Guide { get; set; }
        public string Games_Activities { get; set; }
        public string School_Category { get; set; }
        public string Genral_Conduct { get; set; }
        public DateTime? AP_Date { get; set; }
        public string AP_DateS
        {
            get
            {
                return AP_Date.HasValue
                    ? AP_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      AP_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      AP_Date.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? Issue_Date { get; set; }
        public string Issue_DateS
        {
            get
            {
                return Issue_Date.HasValue
                    ? Issue_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      Issue_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      Issue_Date.Value.Year
                    : string.Empty;
            }
        }
        public string Reason_Leave { get; set; }
        public string Remarks { get; set; }
    }
    #endregion
    #region Dropout
    public class DropOut_DOP
    {
        public string DOP_ANumber { get; set; }

        public string SD_StudentId { get; set; }

        //public long CM_CLASSID { get; set; }
        public Int32? CM_CLASSID { get; set; }
                   
        public long? SECM_SECTIONID { get; set; }

        public string DOP_Reason { get; set; }

        public string DOP_Date { get; set; }

        public long? SCM_SCHOOLID { get; set; }
                   
        public long? SM_SESSIONID { get; set; }

        public bool IsDisplay { get; set; }

        public long? UM_USERID { get; set; }

        public DateTime? EditedDate { get; set; }
        public string EditedDateS
        {
            get
            {
                return EditedDate.HasValue
                    ? EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      EditedDate.Value.Year
                    : string.Empty;
            }
        }
        public string SD_StudentName { get; set; }
        public string CM_CLASSNAME { get; set; }
        public string SD_ContactNo1 { get; set; }


    }
    #endregion
    #region CertificateWizard
    //public class CertificateWizardVM
    //{
    //    public int CER_ID { get; set; }
    //    public string StudentId { get; set; }

    //    public List<CertificateType> CertificateList { get; set; }
    //}
    public class CertificateType
    {
        public long? CER_ID { get; set; }
        public string CER_NAME { get; set; }
        public string StudentId { get; set; }
    }
    #endregion

    public class FeesDetail_SD
    {
        public string fem_feesname { get; set; }
        public int? Installment { get; set; }
        public decimal InstallmentAmt { get; set; }
    }
    #region FormMaster_FM
    public class FormMaster_FM
    {
        public long? FM_Form_Id { get; set; }
        public decimal FM_FormAmount { get; set; }
        public long? FM_SCM_SchoolID { get; set; }
        public long? FM_SM_SessionID { get; set; }
        public int? Userid { get; set; }
    }
    #endregion

    #region TeachingAid
    public class TeachingAid_TA
    {
        public long? TA_TeachingAid_Id { get; set; }
        public string TA_TeachingAidName { get; set; }
        public long? TA_SCM_SchoolID { get; set; }
        public long? TA_SM_SessionID { get; set; }
        public int? Userid { get; set; }
    }
    #endregion

    #region LessonPlan
    public class LessonPlan_LP
    {
        public long? LP_Id { get; set; }
        public string Language { get; set; }
        public DateTime? LP_FromDate { get; set; }
        public string LP_FromDateS
        {
            get
            {
                return LP_FromDate.HasValue
                    ? LP_FromDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_FromDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_FromDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? LP_ToDate { get; set; }
        public string LP_ToDateS
        {
            get
            {
                return LP_ToDate.HasValue
                    ? LP_ToDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_ToDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_ToDate.Value.Year
                    : string.Empty;
            }
        }
        public long? LP_FacultyID { get; set; }
        public string LP_ClassId { get; set; }
        public string LP_SectionId { get; set; }
        public long? LP_SubjectId { get; set; }
        public long? LP_Duration { get; set; }
        public string LP_NumberOfPeriods { get; set; }
        public string LP_TeachingAidId { get; set; }
        public string LP_LessonContent { get; set; }
        public string LP_LearningObjectives { get; set; }
        public string LP_LearningOutcomes { get; set; }
        public string LP_TypeOfOutcome { get; set; }
        public string LP_Introduction { get; set; }
        public string LP_TimeBreakDown { get; set; }
        public string LP_TeachingSteps { get; set; }
        public string LP_Activities { get; set; }
        public string LP_PracticeWork { get; set; }
        public string LP_Assesment { get; set; }
        public string LP_HomeWork { get; set; }
        public string LP_Outcome { get; set; }
        public long? LP_CreatedBy { get; set; }
        public DateTime? LP_CreatedDate { get; set; }
        public string LP_CreatedDateS
        {
            get
            {
                return LP_CreatedDate.HasValue
                    ? LP_CreatedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_CreatedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_CreatedDate.Value.Year
                    : string.Empty;
            }
        }
        public int? LP_EditedBy { get; set; }
        public DateTime? LP_EditedDate { get; set; }
        public string LP_EditedDateS
        {
            get
            {
                return LP_EditedDate.HasValue
                    ? LP_EditedDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_EditedDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_EditedDate.Value.Year
                    : string.Empty;
            }
        }
        public long? LP_SchoolId { get; set; }
        public long? LP_SessionId { get; set; }
        public string LP_ClassSectionMap { get; set; }
        public string LP_ClassSectionPair { get; set; }
        public int Class { get; set; }
        public string ClassName { get; set; }
        public string Subject { get; set; }
        public string Chapter { get; set; }
        public int DurationMinutes { get; set; }
    }


    public class LessonPlanResult
   {
       public string title { get; set; } 
       public string Lclass { get; set; }
       public string subject { get; set; } 
       public string chapter { get; set; } 
       public int duration_minutes { get; set; }
   
       public List<string> objectives { get; set; } 
       public List<string> materials_required { get; set; } 
       public string introduction { get; set; } 
       public List<TimeBreakdown> time_breakdown { get; set; } 
       public List<string> teaching_steps { get; set; }
       public List<string> activities { get; set; } 
       public List<string> practice_work { get; set; } 
       public List<string> assessment { get; set; } 
       public List<string> homework { get; set; } 
       public string expected_learning_outcome { get; set; } 
   }
    public class TimeBreakdown
   {
       public string activity { get; set; }
       public int minutes { get; set; }
   }
    public class LessonPlanList_VM
    {
        public long LP_Id { get; set; }
        public DateTime LP_FromDate { get; set; }
        public string LP_FromDateS
        {
            get
            {
                return LP_FromDate.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_FromDate.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_FromDate.Year;
            }
        }

        public DateTime LP_ToDate { get; set; }
        public string LP_ToDateS
        {
            get
            {
                return LP_ToDate.Day.ToString().PadLeft(2, '0') + "/" +
                      LP_ToDate.Month.ToString().PadLeft(2, '0') + "/" +
                      LP_ToDate.Year;
            }
        }

        public string FP_Name { get; set; }
        public string ClassNames { get; set; }
        public string SectionNames { get; set; }
        public string TeachingAidNames { get; set; }
        public string SBM_SubjectName { get; set; }
        public int LP_NumberOfPeriods { get; set; }
        public string LP_LessonContent { get; set; }
        public string LP_LearningObjectives { get; set; }
        public string LP_LearningOutcomes { get; set; }
        public string LP_TypeOfOutcome { get; set; }
        public string LP_Outcome { get; set; }
    }
    #endregion

    #region 
    #region ClassSecFaculty
    public class ClassSecFaculty_CSF
    {
        public long? CSF_Id { get; set; }
        public long? CSF_ClassId { get; set; }
        public long? CSF_SectionId { get; set; }
        public long? CSF_FacultyId { get; set; }
        public long? CSF_SchoolId { get; set; }
        public long? CSF_SessionId { get; set; }
        public int? CSF_CreateBy { get; set; }
        public DateTime? CSF_CreateDate { get; set; }
        public string CSF_CreateDateS
        {
            get
            {
                return CSF_CreateDate.HasValue
                    ? CSF_CreateDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CSF_CreateDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CSF_CreateDate.Value.Year
                    : string.Empty;
            }
        }
        public int? CSF_EditBy { get; set; }
        public DateTime? CSF_EditDate { get; set; }
        public string CSF_EditDateS
        {
            get
            {
                return CSF_EditDate.HasValue
                    ? CSF_EditDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      CSF_EditDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      CSF_EditDate.Value.Year
                    : string.Empty;
            }
        }
       
    }
    #endregion
    #endregion

    #region PettyCash
    public class SessionMaster
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionStartDate { get; set; } //MODIFY 04/02/2016
        public string SessionEndDate { get; set; } //MODIFY 04/02/2016
        public string S_SessionStartDate { get; set; }
        public string S_SessionEndDate { get; set; }
    }
    public class AccountGroup
    {

        public int? GM_CompanyID { get; set; }
        public int? GM_FYId { get; set; }
        public string GM_GroupCode { get; set; }
        public string GM_GroupDescription { get; set; }
        public long? GM_GroupId { get; set; }

    }


    public class AccountMaster
    {

        public AccountMaster()
        {

            SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            AccountMasterList = new List<AccountMaster>();

        }
        public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }

        public long? AM_CompanyID { get; set; }
        public long? AM_FYId { get; set; }
        public int? AM_AccountId { get; set; }
        public int? AM_GroupId { get; set; }

        public string AM_AccountCode { get; set; }
        public string AM_AccDescription { get; set; }
        public string AM_LongName { get; set; }
        public string AM_SubOption { get; set; }
        public int? AM_Intrate { get; set; }
        public int? AM_ParamId { get; set; }
        public Decimal? AM_OpeningBalance { get; set; }
        public string AM_OPeningType { get; set; }
        public string ISSubAc { get; set; }

        public Decimal? AM_TotalDebit { get; set; }
        public Decimal? AM_TotalCredit { get; set; }
        public Decimal? AM_ClosingBalance { get; set; }
        public string AM_ClosingType { get; set; }
        public string AM_SuppressPayee { get; set; }

        public bool IsSuppressPayee
        {
            get { return AM_SuppressPayee == "Y"; }
            set { AM_SuppressPayee = value ? "Y" : "N"; }
        }
        public string AM_GroupCode { get; set; }
        public string SessionName { get; set; }
        public string GM_GroupDescription { get; set; }
        public string GM_GroupCode { get; set; }
        public int? AM_AccountOpId { get; set; }

        public Boolean AM_IsFund { get; set; }

    }

    public class SubAccountMaster
    {

        public SubAccountMaster()
        {

            SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            SubAccountMasterList = new List<SubAccountMaster>();
            AccountMasterList = new List<AccountMaster>();

        }
        public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<SubAccountMaster> SubAccountMasterList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }
        public long? SAM_CompanyID { get; set; }
        public long? SAM_FYID { get; set; }
        public int? SAM_SubId { get; set; }
        public int? SAM_AccountId { get; set; }

        public string SAM_SubCode { get; set; }
        public string AM_AccountCode { get; set; }

        public string SAM_SubDescription { get; set; }
        public string SAM_SubLongDesc { get; set; }
        public string SAM_Address1 { get; set; }
        public string SAM_Address2 { get; set; }
        public string SAM_Address3 { get; set; }
        public string SAM_Address4 { get; set; }
        public string SAM_OPhone { get; set; }
        public string SAM_RPhone { get; set; }
        public string SAM_FAX { get; set; }
        public string SAM_CellNo { get; set; }
        public string SAM_Email { get; set; }
        public string SAM_Website { get; set; }
        public string SAM_PAN { get; set; }
        public string SAM_CST { get; set; }
        public string SAM_SST { get; set; }
        public int? SAM_AccountOpId { get; set; }
        public int? AM_AccountId { get; set; }

        public string AM_AccDescription { get; set; }

        public Decimal? SAM_OpeningBalance { get; set; }
        public string SAM_OPeningType { get; set; }


        public Decimal? SAM_TotalDebit { get; set; }
        public Decimal? SAM_TotalCredit { get; set; }
        public Decimal? SAM_ClosingBalance { get; set; }
        public string SAM_ClosingType { get; set; }
        public Boolean SAM_IsFund { get; set; }


    }




    public class AccountLedger
    {

        public AccountLedger()
        {

            SessionList = new List<SessionMaster>();
            AccountGroupList = new List<AccountGroup>();
            SubAccountMasterList = new List<SubAccountMaster>();
            AccountMasterList = new List<AccountMaster>();
            AccountLedgerList = new List<AccountLedger>();
            AccountVoucherTypeMasterList = new List<AccountVoucherTypeMaster>();
            AccountList = new List<AccountLedger>();
            ParticularsList = new List<AccountLedger>();
        }
        public List<AccountLedger> ParticularsList { get; set; }

        public List<AccountLedger> AccountList { get; set; }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeMasterList { get; set; }
        public List<SessionMaster> SessionList { get; set; }
        public List<AccountGroup> AccountGroupList { get; set; }
        public List<SubAccountMaster> SubAccountMasterList { get; set; }
        public List<AccountMaster> AccountMasterList { get; set; }
        public List<AccountLedger> AccountLedgerList { get; set; }
        public long? LD_CompanyId { get; set; }
        public long? LD_FYId { get; set; }
        public int? LD_LedgerID { get; set; }
        public int? LD_HdrId { get; set; }
        public string LD_Transactiontype { get; set; }
        public string LD_VoucherNo { get; set; }
        public string LD_ReferenceNo { get; set; }
        public DateTime? LD_Date { get; set; }

        public string LD_DateSS

        {
            get
            {
                return LD_Date.HasValue
                    ? LD_Date.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LD_Date.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LD_Date.Value.Year
                    : string.Empty;
            }
        }
        public string LD_DateS { get; set; }
        public int? LD_AccountID { get; set; }
        public int? LD_SubID { get; set; }
        public string LD_AccountCode { get; set; }
        public string LD_SubCode { get; set; }
        public int? LD_CostCenterID { get; set; }
        public string LD_CostCenterCode { get; set; }
        public string LD_DrCr { get; set; }
        public Decimal? LD_DrAmount { get; set; }
        public Decimal? LD_CrAmount { get; set; }

        public string LD_Narration { get; set; }
        public string LD_ChequeNo { get; set; }
        public DateTime? LD_ChequeDate { get; set; }
        public string LD_ChequeDateS
        {
            get
            {
                return LD_ChequeDate.HasValue
                    ? LD_ChequeDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      LD_ChequeDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      LD_ChequeDate.Value.Year
                    : string.Empty;
            }
        }
        public string LD_Payee { get; set; }
        public int? LD_Userid { get; set; }
        public string LD_Remarks { get; set; }
        public string LD_DOID { get; set; }
        public string LD_Automatic { get; set; }
        public string LD_AdjustmentAmt { get; set; }
        public string LD_DOCSrlNo { get; set; }
        public int? LD_InvoiceID { get; set; }
        //public string MOD_FLA { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public string FYear { get; set; }
        public int? StartNo { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool IsCancel { get; set; }
        public int? LD_VoucherTypeId { get; set; }
        public string AccountValue { get; set; }
        public string ParticularsValue { get; set; }
        public string ParticularsText { get; set; }
        public int? LD_FundAccountID { get; set; }
        public int? LD_FundSubID { get; set; }
        public Boolean LD_IsFund { get; set; }
    }


    public class AccountVoucherTypeMaster
    {
        public AccountVoucherTypeMaster()
        {
            AccountVoucherTypeMasterList = new List<AccountVoucherTypeMaster>();
        }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeMasterList { get; set; }
        public int? VoucherTypeId { get; set; }
        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public string FYear { get; set; }
        public int? StartNo { get; set; }

    }

    public class vw_AccountOpeningBalance
    {
        public int AccountId { get; set; }
        public int SubAccountId { get; set; }
        public int FYId { get; set; }
        public decimal OpeningBalance { get; set; }
        public string OPeningType { get; set; }
        public string LongName { get; set; }
    }
    public class vw_AccountLedger
    {
        public vw_AccountLedger()
        {
            vw_AccountLedgerList = new List<vw_AccountLedger>();
            vw_AccountOpeningBalanceList = new List<vw_AccountOpeningBalance>();
        }
        public List<vw_AccountLedger> vw_AccountLedgerList { get; set; }
        public List<vw_AccountOpeningBalance> vw_AccountOpeningBalanceList { get; set; }
        public int LD_CompanyId { get; set; }
        public int LD_FYId { get; set; }
        public int LD_HdrId { get; set; }
        public int LD_VoucherTypeId { get; set; }
        public string LD_VoucherNo { get; set; }
        public string LD_ReferenceNo { get; set; }
        public DateTime LD_Date { get; set; }
        public string LD_DateS
        {
            get
            {
                return LD_Date.Day.ToString().PadLeft(2, '0') + "/" +
                      LD_Date.Month.ToString().PadLeft(2, '0') + "/" +
                      LD_Date.Year;
            }
        }

        public int LD_BankCode { get; set; }
        public int LD_AccountID { get; set; }
        public int LD_SubID { get; set; }
        public int LD_FundAccountID { get; set; }
        public int LD_FundSubID { get; set; }
        public string LD_AccountCode { get; set; }
        public string LD_SubCode { get; set; }
        public int LD_CostCenterID { get; set; }
        public string LD_CostCenterCode { get; set; }
        public string LD_DrCr { get; set; }
        public decimal LD_CrAmount { get; set; }
        public decimal LD_DrAmount { get; set; }
        public string LD_Narration { get; set; }
        public string LD_ChequeNo { get; set; }
        public DateTime LD_ChequeDate { get; set; }
        public string LD_ChequeDateS
        {
            get
            {
                return LD_ChequeDate.Day.ToString().PadLeft(2, '0') + "/" +
                      LD_ChequeDate.Month.ToString().PadLeft(2, '0') + "/" +
                      LD_ChequeDate.Year;
            }
        }

        public string LD_Payee { get; set; }
        public int LD_Userid { get; set; }
        public string LD_Remarks { get; set; }
        public decimal LD_DOID { get; set; }
        public bool LD_Automatic { get; set; }
        public decimal LD_AdjustmentAmt { get; set; }
        public decimal LD_DOCSrlNo { get; set; }
        public decimal LD_InvoiceID { get; set; }
        public string MOD_FLAG { get; set; }
        public int UniqueMaxId { get; set; }
        public bool IsCancel { get; set; }
        public int CompanyID { get; set; }
        public int TrustID { get; set; }
        public string CollegeShortName { get; set; }
        public string CollegeName { get; set; }
        public string CollegeAddress { get; set; }
        public string CollegePinCode { get; set; }
        public string CollegePhone { get; set; }
        public string CollegeEmail { get; set; }
        public string CollegeLogoUrl { get; set; }
        public string PrincipleSignature { get; set; }
        public string CollegeCode { get; set; }
        public byte[] CollegeLogoBinary { get; set; }
        public string SessionName { get; set; }
        public DateTime SessionStartDate { get; set; }
        public string SessionStartDateS
        {
            get
            {
                return SessionStartDate.Day.ToString().PadLeft(2, '0') + "/" +
                      SessionStartDate.Month.ToString().PadLeft(2, '0') + "/" +
                      SessionStartDate.Year;
            }
        }

        public DateTime SessionEndDate { get; set; }
        public string SessionEndDateS
        {
            get
            {
                return SessionEndDate.Day.ToString().PadLeft(2, '0') + "/" +
                      SessionEndDate.Month.ToString().PadLeft(2, '0') + "/" +
                      SessionEndDate.Year;
            }
        }
        public int AM_CompanyID { get; set; }
        public int AM_FYId { get; set; }
        public int AM_GroupId { get; set; }
        public string AM_AccountCode { get; set; }
        public string AM_AccDescription { get; set; }
        public string AM_LongName { get; set; }
        public string AM_SuppressPayee { get; set; }
        public string AM_GroupCode { get; set; }
        public string ISSubAc { get; set; }
        public bool AM_IsFund { get; set; }
        public bool IsFeesHead { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedOnS
        {
            get
            {
                return CreatedOn.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn.Year;
            }
        }

        public DateTime ModifiedOn { get; set; }
        public string ModifiedOnS
        {
            get
            {
                return ModifiedOn.Day.ToString().PadLeft(2, '0') + "/" +
                      ModifiedOn.Month.ToString().PadLeft(2, '0') + "/" +
                      ModifiedOn.Year;
            }
        }

        public int SAM_CompanyID { get; set; }
        public int SAM_FYID { get; set; }
        public int SAM_AccountId { get; set; }
        public string SAM_SubCode { get; set; }
        public string SAM_SubDescription { get; set; }
        public string SAM_SubLongDesc { get; set; }
        public string SAM_Address1 { get; set; }
        public string SAM_Address2 { get; set; }
        public string SAM_Address3 { get; set; }
        public string SAM_Address4 { get; set; }
        public string SAM_OPhone { get; set; }
        public string SAM_RPhone { get; set; }
        public string SAM_FAX { get; set; }
        public string SAM_CellNo { get; set; }
        public string SAM_Email { get; set; }
        public string SAM_Website { get; set; }
        public string SAM_PAN { get; set; }
        public string SAM_CST { get; set; }
        public string SAM_SST { get; set; }
        public bool SAM_IsFund { get; set; }
        public bool IsActive1 { get; set; }
        public DateTime CreatedOn1 { get; set; }
        public string CreatedOn1S
        {
            get
            {
                return CreatedOn1.Day.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn1.Month.ToString().PadLeft(2, '0') + "/" +
                      CreatedOn1.Year;
            }
        }

        public DateTime ModifiedOn1 { get; set; }
        public string ModifiedOn1S
        {
            get
            {
                return ModifiedOn1.Day.ToString().PadLeft(2, '0') + "/" +
                      ModifiedOn1.Month.ToString().PadLeft(2, '0') + "/" +
                      ModifiedOn1.Year;
            }
        }

        public string VoucherType { get; set; }
        public string Prefix { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal CloseingBalance { get; set; }
        public decimal SubBalance { get; set; }
        public decimal FinalBalance { get; set; }
    }
#endregion

    public class ChangePasswordVM
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
    #region  DegreeMasters_DEGM
    public class DegreeMasters_DEGM
{
    public long DEGM_Id { get; set; }
    public string DEGM_DegreeName { get; set; }   // B.Ed, M.Ed, PhD

    public int DEGM_Level { get; set; }           // 1 = UG, 2 = PG, 3 = Doctorate

    public bool DEGM_IsActive { get; set; }

    public DateTime DEGM_CreateDate { get; set; }
    public string DEGM_CreateDateS
    {
        get
        {
            return DEGM_CreateDate.Day.ToString().PadLeft(2, '0') + "/" +
                  DEGM_CreateDate.Month.ToString().PadLeft(2, '0') + "/" +
                  DEGM_CreateDate.Year;
        }
    }

}

    #endregion

    #region Library Master
    public class BookMaster_BM
    {
    public long? BM_BookId { get; set; }
    public long? BM_SchoolId { get; set; }
    public string SchoolName { get; set; }         
    public string BM_Title { get; set; }
    public string BM_ISBN { get; set; }
    public string BM_Author { get; set; }
    public string BM_Publisher { get; set; }
    public string BM_Edition { get; set; }
    public string BM_Language { get; set; }
    public string BM_ClassLevel { get; set; }
    public string BM_ShelfNo { get; set; }
    public bool? BM_IsReferenceOnly { get; set; }
    public long? Userid { get; set; }
    public string BM_IsActive { get; set; }
    public long? BM_CreatedBy { get; set; }
    public string TransType { get; set; }         
    public long? OutPutId { get; set; }      
     
    }

    public class BookCopyMaster_BCM
    {
        public BookCopyMaster_BCM()
        {
            BookCopyMaster_Type = new List<BookCopyMaster_BCM>();
        }
        public long? BCM_CopyId { get; set; }
        public string BCM_CopyCode { get; set; }
        public string BCM_CopyName { get; set; }
        public long? BCM_SchoolId { get; set; }
        public long? BCM_BookId { get; set; }
        public string BCM_AccessionNo { get; set; }
        public string BCM_Barcode { get; set; }
        public DateTime? BCM_PurchaseDate { get; set; }
        public decimal? BCM_Price { get; set; }
        public string BCM_Status { get; set; }
        public string BCM_IsActive { get; set; }
        public long? Userid { get; set; }
        public string SCM_SCHOOLNAME { get; set; }
        public string BM_Title { get; set; }
        public long? BCM_CreatedBy { get; set; }
        public List<BookCopyMaster_BCM> BookCopyMaster_Type { get; set; }

    }
    public class LibrarySetting
    {
        public long?    LS_SettingId { get; set; }
        public long?    LS_SchoolId { get; set; }
        public int?     LS_MaxIssueDays { get; set; }
        public decimal? LS_FinePerDay { get; set; }
        public int?     LS_MaxBooksStudent { get; set; }
        public int?     LS_MaxBooksTeacher { get; set; }
        public int?     LS_MaxRenewLimit { get; set; }
        public int?     LS_GraceDays { get; set; }
        public long?    LS_CreatedBy { get; set; }
        public string   LS_Active { get; set; }     
        public string TransType { get; set; }
        public long? Userid { get; set; }

    }
    public class SupplierMaster
    {
        public long?        SM_SupplierId { get; set; }
        public long?        SM_SchoolId { get; set; }
        public string       SM_SupplierName { get; set; }
        public string       SM_Mobile { get; set; }
        public string       SM_Email { get; set; }
        public string       SM_Address { get; set; }
        public long?        SM_CreatedBy { get; set; }
        public DateTime?    SM_CreatedDate { get; set; }
        public string       SM_Active { get; set; }   
        public string TransType { get; set; }
        public long? Userid { get; set; }

    }
    public class CategoryMaster
    {
        public long?     CM_CategoryId { get; set; }
        public long?     CM_SchoolId { get; set; }
        public string    CM_CategoryCode { get; set; }
        public string    CM_CategoryName { get; set; }
        public long?     CM_ParentCategoryId { get; set; }
        public long?     CM_CreatedBy { get; set; }
        public DateTime? CM_CreatedDate { get; set; }
        public string CM_IsActive { get; set; }   
        public string TransType { get; set; }
        public long? Userid { get; set; }
    }
    public class MemberMaster
    {
        public long?  MM_MemberId { get; set; }
        public long?  MM_SchoolId { get; set; }
        public string MM_MemberCode { get; set; }
        public long? MM_MemberTypeId { get; set; }
        public string TypeName { get; set; }
        public string MM_FirstName { get; set; }
        public string MM_LastName { get; set; }
        public string MemberName { get; set; }
        public string MM_Email { get; set; }
        public string MM_Address { get; set; }
        public string MM_Mobile { get; set; }
        public long? MM_CreatedBy { get; set; }
        public string MM_Active { get; set; }
        public string TransType { get; set; }
        public long? Userid { get; set; }
        public long? MTM_TypeId { get; set; }

    }
    public class MemberTypeMaster_MTM
    {
        public long? MTM_TypeId { get; set; }
        public string MTM_Type { get; set; }
    }

    public class IssueTransaction
    {
        public long? IT_IssueId { get; set; }
        public long? IT_SchoolId { get; set; }
        public long? BCM_BookId { get; set; }
        public string BM_Title { get; set; }
        public long? IT_CopyId { get; set; }
        public string BCM_CopyCode { get; set; }
        public long? IT_MemberId { get; set; }
        public string MM_MemberCode { get; set; }
        public string IT_IssueName { get; set; }
        public DateTime? IT_IssueDate { get; set; }
        public string IT_Status { get; set; }
        public string IT_ResolvedYN { get; set; }
        public string IT_ResolvedRemarks { get; set; }
        public long? IT_CreatedBy { get; set; }
        public string IT_Active { get; set; }
        public int? DueDaysCount { get; set; }
        public string MTM_Type { get; set; }
        public string IT_IssueDateS
        {
            get
            {
                return IT_IssueDate.HasValue
                    ? IT_IssueDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      IT_IssueDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      IT_IssueDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? IT_DueDate { get; set; }
        public string IT_DueDateS
        {
            get
            {
                return IT_DueDate.HasValue
                    ? IT_DueDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      IT_DueDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      IT_DueDate.Value.Year
                    : string.Empty;
            }
        }
        public DateTime? IT_ReturnDate { get; set; }
        public string IT_ReturnDateS
        {
            get
            {
                return IT_ReturnDate.HasValue
                    ? IT_ReturnDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      IT_ReturnDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      IT_ReturnDate.Value.Year
                    : string.Empty;
            }
        }

    }

    #endregion
    #region AssignmentMaster_ASM
    public class DeviceRegistration
    {

        public long? ASM_ID { get; set; }
        public long? ASM_FP_Id { get; set; }
        public long? ASM_SubGr_ID { get; set; }
        public long? ASM_Sub_ID { get; set; }
        public long? ASM_School_ID { get; set; }
        public long? ASM_Class_ID { get; set; }
        public long? ASM_Section_ID { get; set; }
        public long? ASM_Session_ID { get; set; }
        public string ASM_Title { get; set; }
        public string ASM_Desc { get; set; }
        public int? CM_ID { get; set; }
        public DateTime? ASM_StartDate { get; set; }
        public string ASM_StartDateS
        {
            get
            {
                return ASM_StartDate.HasValue
                    ? ASM_StartDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ASM_StartDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ASM_StartDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime? ASM_ExpDate { get; set; }
        public string ASM_ExpDateS
        {
            get
            {
                return ASM_ExpDate.HasValue
                    ? ASM_ExpDate.Value.Day.ToString().PadLeft(2, '0') + "/" +
                      ASM_ExpDate.Value.Month.ToString().PadLeft(2, '0') + "/" +
                      ASM_ExpDate.Value.Year
                    : string.Empty;
            }
        }

        public DateTime Created_Date { get; set; }
        public string Created_DateS
        {
            get
            {
                return Created_Date.Day.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Month.ToString().PadLeft(2, '0') + "/" +
                      Created_Date.Year;
            }
        }

        public string ASM_SGM_SubjectGroupName { get; set; }
        public string ASM_SBM_SubjectName { get; set; }
        public string ASM_UploadDoc { get; set; }
        public string ASM_CM_CLASSNAME { get; set; }
        public string ASM_SECM_SECTIONNAME { get; set; }
        public string SBM_SubjectName { get; set; }
        public long? Userid { get; set; }


    }

  
    #endregion
}





























































































































