using AccountManagementSystem.Models;
using SchoolMVC.Areas.FacultyPortal.Models.Request;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Xml.Linq;
using SchoolMVC.Areas.Notification.Models.Request;
namespace SchoolMVC.Repositories
{

    public class MasterRepository : IMasterRepository
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;
        }
        #region SessionMasters
        public long AddEditSession(SessionMasters_SM Ses)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SM_SESSIONID", Ses.SM_SESSIONID));
            arrParams.Add(new SqlParameter("@SM_STARTDATE", Ses.SM_STARTDATE));
            arrParams.Add(new SqlParameter("@SM_ENDDATE", Ses.SM_ENDDATE));
            arrParams.Add(new SqlParameter("@SM_SESSIONCODE", Ses.SM_SESSIONCODE));
            arrParams.Add(new SqlParameter("@SM_SESSIONNAME", Ses.SM_SESSIONNAME));
            arrParams.Add(new SqlParameter("@SM_SCM_SCHOOLID", Ses.SM_SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SM_CREATEDUID", Ses.SM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SessionMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;

        }
        #endregion
        #region StateMasters
        public long InsertUpdateState(StateMaster_STM states)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@STM_STATEID", states.STM_STATEID));
            arrParams.Add(new SqlParameter("@STM_NATIONID", states.STM_NATIONID));
            arrParams.Add(new SqlParameter("@STM_STATENAME", states.STM_STATENAME));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StateMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region DistrictMaster
        public long InsertUpdateDistrict(DistrictMasters_DM district)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@DM_DISTRICTID", district.DM_DISTRICTID));
            arrParams.Add(new SqlParameter("@DM_NATIONID", district.DM_NATIONID));
            arrParams.Add(new SqlParameter("@DM_STATEID", district.DM_STATEID));
            arrParams.Add(new SqlParameter("@DM_DISTRICTNAME", district.DM_DISTRICTNAME));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_DistrictMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region TermMaster
        public long InsertUpdateTerm(TermMaster_TM term)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TM_Id", term.TM_Id));
            arrParams.Add(new SqlParameter("@TM_TermName", term.TM_TermName));
            arrParams.Add(new SqlParameter("@TM_DateFrom", term.TM_DateFrom));
            arrParams.Add(new SqlParameter("@TM_DateTo", term.TM_DateTo));
            arrParams.Add(new SqlParameter("@TM_SchoolId", term.TM_SchoolId));
            arrParams.Add(new SqlParameter("@TM_SessionId", term.TM_SessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TermMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region ClassTypeMaster
        public long InsertUpdateClassType(ClassTypeMaster_CTM Ct)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CTM_TYPEID", Ct.CTM_TYPEID));
            arrParams.Add(new SqlParameter("@CTM_TYPENAME", Ct.CTM_TYPENAME));
            arrParams.Add(new SqlParameter("@CTM_CREATEDUID", Ct.CTM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassTypeMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;

        }
        #endregion
        #region ClassMaster
        public long InsertUpdateClass(ClassMaster_CM Class)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CM_CLASSID", Class.CM_CLASSID));
            arrParams.Add(new SqlParameter("@CM_CLASSCODE", Class.CM_CLASSCODE));
            arrParams.Add(new SqlParameter("@CM_CLASSNAME", Class.CM_CLASSNAME));
            arrParams.Add(new SqlParameter("@CM_CREATEDUID", Class.CM_CREATEDUID));
            arrParams.Add(new SqlParameter("@CM_FROMAGE", Class.CM_FROMAGE));
            arrParams.Add(new SqlParameter("@CM_TOAGE", Class.CM_TOAGE));
            arrParams.Add(new SqlParameter("@CM_CTM_TYPEID", Class.CM_CTM_TYPEID));
            arrParams.Add(new SqlParameter("@CM_SCM_SCHOOLID", Class.CM_SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@CM_ISACTIVE", Class.CM_ISACTIVE));
            arrParams.Add(new SqlParameter("@CM_Preference", Class.CM_Preference));
            arrParams.Add(new SqlParameter("@IsHigherSecondary", Class.IsHigherSecondary));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region SectionMaster
        public long InsertUpdateSection(SectionMaster_SECM sec)
        {
            Int64 classid = 0;
            DataTable DataList = new DataTable();
            DataList.Columns.Add("SECM_CM_CLASSID", typeof(Int64));
            DataList.Columns.Add("SECM_SECTIONNAME", typeof(string));
            DataList.Columns.Add("SECM_SCM_SCHOOLID", typeof(Int64));
            DataList.Columns.Add("SECM_SM_SESSIONID", typeof(Int64));
            foreach (var data in sec.SectionList)
            {

                DataRow dr = DataList.NewRow();
                dr["SECM_CM_CLASSID"] = data.SECM_CM_CLASSID;
                dr["SECM_SECTIONNAME"] = data.SECM_SECTIONNAME;
                dr["SECM_SCM_SCHOOLID"] = sec.SECM_SCM_SCHOOLID;
                dr["SECM_SM_SESSIONID"] = sec.SECM_SM_SESSIONID;
                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();

            //arrParams.Add(new SqlParameter("@SECM_CM_CLASSID", sec.SECM_CM_CLASSID));
            //arrParams.Add(new SqlParameter("@SECM_SCM_SCHOOLID", sec.SECM_SCM_SCHOOLID));
            //arrParams.Add(new SqlParameter("@SECM_SECTIONID", sec.SECM_SECTIONID));
            //arrParams.Add(new SqlParameter("@SECM_SECTIONNAME", sec.SECM_SECTIONNAME));
            //arrParams.Add(new SqlParameter("@SECM_SM_SESSIONID", sec.SECM_SM_SESSIONID));

            arrParams.Add(new SqlParameter("@SecList", DataList));
            arrParams.Add(new SqlParameter("@SECM_CREATEDUID", sec.SECM_CREATEDUID));
            arrParams.Add(new SqlParameter("@SECM_CM_CLASSID", sec.SECM_CM_CLASSID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SectionMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region BoardMaster
        public long InsertUpdateBoard(BoardMasters_BM board)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@BM_BOARDID", board.BM_BOARDID));
            arrParams.Add(new SqlParameter("@BM_BOARDNAME", board.BM_BOARDNAME));
            arrParams.Add(new SqlParameter("@BM_BOARDCODE", board.BM_BOARDCODE));
            arrParams.Add(new SqlParameter("@BM_CREATEDUID", board.BM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_BoardMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region CasteMaster
        public long InsertUpdateCaste(CasteMaster_CSM caste)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CSM_CASTENAME", caste.CSM_CASTENAME));
            arrParams.Add(new SqlParameter("@CSM_ISACTIVE", caste.CSM_ISACTIVE));
            arrParams.Add(new SqlParameter("@CSM_CREATEDUID", caste.CSM_CREATEDUID));
            arrParams.Add(new SqlParameter("@CSM_CASTEID", caste.CSM_CASTEID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_CasteMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region VernacularMaster
        public long InsertUpdateVernacular(VernacularMaster_VM vm)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@VM_VERNACULARID", vm.VM_VERNACULARID));
            arrParams.Add(new SqlParameter("@VM_VERNACULARNAME", vm.VM_VERNACULARNAME));
            arrParams.Add(new SqlParameter("@VM_ISACTIVE", vm.VM_ISACTIVE));
            arrParams.Add(new SqlParameter("@VM_CREATEDUID", vm.VM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_VernacularMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region HouseMaster
        public long InsertUpdateHouse(HouseMasters_HM house)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@HM_HOUSEID", house.HM_HOUSEID));
            arrParams.Add(new SqlParameter("@HM_HOUSENAME", house.HM_HOUSENAME));
            arrParams.Add(new SqlParameter("@HM_HOUSECODE", house.HM_HOUSECODE));
            arrParams.Add(new SqlParameter("@HM_CREATEDUID", house.HM_CREATEDUID));
            arrParams.Add(new SqlParameter("@HM_ISACTIVE", house.HM_ISACTIVE));
            arrParams.Add(new SqlParameter("@HM_SCM_SCHOOLID", house.HM_SCM_SCHOOLID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HouseMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region StreamMaster
        public long InsertUpdateStream(StreamMasters_STRM stream)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@STRM_STREAMID", stream.STRM_STREAMID));
            arrParams.Add(new SqlParameter("@STRM_SCM_SCHOOLID", stream.STRM_SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@STRM_SM_SESSIONID", stream.STRM_SM_SESSIONID));
            arrParams.Add(new SqlParameter("@STRM_STREAMNAME", stream.STRM_STREAMNAME));
            arrParams.Add(new SqlParameter("@STRM_ISACTIVE", stream.STRM_ISACTIVE));
            arrParams.Add(new SqlParameter("@STRM_CM_CLASSID", stream.STRM_CM_CLASSID));
            arrParams.Add(new SqlParameter("@STRM_CREATEDUID", stream.STRM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StreamMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region BloodGroupMaster
        public long InsertUpdateBloodGroup(BloodGroupMasters_BGM bloodgrp)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@BGM_BLDGRPID", bloodgrp.BGM_BLDGRPID));
            arrParams.Add(new SqlParameter("@BGM_BLDGRPNAME", bloodgrp.BGM_BLDGRPNAME));
            arrParams.Add(new SqlParameter("@BGM_CREATEDUID", bloodgrp.BGM_CREATEDUID));
            arrParams.Add(new SqlParameter("@BGM_ISACTIVE", bloodgrp.BGM_ISACTIVE));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_BloodGroupMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region GradeMaster
        public long InsertUpdateGrade(GradeMaster_GM grade)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@GM_Grade_Id", grade.GM_Grade_Id));
            arrParams.Add(new SqlParameter("@GM_GradeName", grade.GM_GradeName));
            arrParams.Add(new SqlParameter("@GM_SCM_SchoolID", grade.GM_SCM_SchoolID));
            arrParams.Add(new SqlParameter("@GM_SM_SessionID", grade.GM_SM_SessionID));
            arrParams.Add(new SqlParameter("@GM_ToGrade", grade.GM_ToGrade));
            arrParams.Add(new SqlParameter("@GM_FromGrade", grade.GM_FromGrade));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_GradeMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion

        #region FormMaster
        public long InsertUpdateForm(FormMaster_FM form)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FM_Form_Id", form.FM_Form_Id));
            arrParams.Add(new SqlParameter("@FM_FormAmount", form.FM_FormAmount));
            arrParams.Add(new SqlParameter("@FM_SCM_SchoolID", form.FM_SCM_SchoolID));
            arrParams.Add(new SqlParameter("@FM_SM_SessionID", form.FM_SM_SessionID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_FormMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region TeachingAidMaster
        public long InsertUpdateTeachingAid(TeachingAid_TA teachinga)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TA_TeachingAid_Id", teachinga.TA_TeachingAid_Id));
            arrParams.Add(new SqlParameter("@TA_TeachingAidName", teachinga.TA_TeachingAidName));
            arrParams.Add(new SqlParameter("@TA_SCM_SchoolID", teachinga.TA_SCM_SchoolID));
            arrParams.Add(new SqlParameter("@TA_SM_SessionID", teachinga.TA_SM_SessionID));
            arrParams.Add(new SqlParameter("@Userid", teachinga.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TeachingAidMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion



        #region HolidayMaster
        public long InsertUpdateHoliday(HolidayMaster_HM holidays)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@HM_Id", holidays.HM_Id));
            arrParams.Add(new SqlParameter("@HM_HolidayName", holidays.HM_HolidayName));
            arrParams.Add(new SqlParameter("@HM_SchoolId", holidays.HM_SchoolId));
            arrParams.Add(new SqlParameter("@HM_SessionId", holidays.HM_SessionId));
            arrParams.Add(new SqlParameter("@HM_FromDate", holidays.HM_FromDate));
            arrParams.Add(new SqlParameter("@HM_ToDate", holidays.HM_ToDate));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HolidayMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region NoticeMaster

        public long InsertUpdateNotice(NoticeMasters_NM notice)
        {
            long lastId = 0;

            var classSectionPairs = (notice.NM_ClassSectionPair ?? "")
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            // If no class-section pairs, insert once with NULLs
            if (!classSectionPairs.Any())
            {
                lastId = InsertNoticeRecord(notice, null, null);
                return lastId;
            }

            // If class-section pairs exist, insert for each pair
            foreach (var pair in classSectionPairs)
            {
                var ids = pair.Split('|');
                var classId = ids.Length > 0 ? ids[0].Trim() : null;
                var sectionId = ids.Length > 1 ? ids[1].Trim() : null;

                lastId = InsertNoticeRecord(notice, classId, sectionId);

                if (lastId == -11) // Duplicate found
                    return -11;
            }

            return lastId;
        }

        // Helper method to avoid repeating parameter creation logic
        private long InsertNoticeRecord(NoticeMasters_NM notice, string classId, string sectionId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@NM_Id", (object)notice.NM_Id ?? DBNull.Value),
        new SqlParameter("@NM_EntryDate", notice.NM_EntryDate),
        new SqlParameter("@NM_ExpDate", notice.NM_ExpDate),

         new SqlParameter("@NM_StudentId", notice.NM_StudentId),
        new SqlParameter("@NM_FacultyId", notice.NM_FacultyId),
        new SqlParameter("@NM_NtId", notice.NM_NtId),
       


        new SqlParameter("@NM_Title", notice.NM_Title),
        new SqlParameter("@NM_Notice", notice.NM_Notice),
        new SqlParameter("@NM_UploadFile", (object)notice.NM_UploadFile ?? DBNull.Value),
        new SqlParameter("@NM_IsPublish", notice.NM_IsPublish),
        new SqlParameter("@NM_SchoolId", notice.NM_SchoolId),
        new SqlParameter("@NM_ClassId", (object)classId ?? DBNull.Value),
        new SqlParameter("@NM_SectionId", (object)sectionId ?? DBNull.Value),
        new SqlParameter("@NM_SessionId", notice.NM_SessionId),
        new SqlParameter("@NM_CreatedDate", DateTime.Now),
        new SqlParameter("@NM_CreatedBy", notice.NM_CreatedBy),
        new SqlParameter("@NM_EditedDate", DBNull.Value),
        new SqlParameter("@NM_EditedBy", DBNull.Value),
        new SqlParameter("@NM_Link", notice.NM_Link),
    };

            var outParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            parameters.Add(outParam);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_NoticeMaster",
                parameters.ToArray()
            );

            return Convert.ToInt64(outParam.Value);
        }
  

        #endregion
        #region PeriodMaster
        public long InsertUpdatePeriod(PeriodMaster_PM period)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@PM_Id", period.PM_Id));
            arrParams.Add(new SqlParameter("@PM_FromTime", period.PM_FromTime));
            arrParams.Add(new SqlParameter("@PM_ToTime", period.PM_ToTime));
            arrParams.Add(new SqlParameter("@PM_Period", period.PM_Period));
            arrParams.Add(new SqlParameter("@PM_SchoolId", period.PM_SchoolId));
            arrParams.Add(new SqlParameter("@PM_SessionId", period.PM_SessionId));
            arrParams.Add(new SqlParameter("@PM_ClassId", period.PM_ClassId));
            arrParams.Add(new SqlParameter("@PM_CreateBy", period.PM_CreateBy));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_PeriodMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }

        #endregion
        #region ClassTypeboardMappingMaster
        public long InsertUpdateClassTypeboard(ClassTypeBoard_CTB classboard)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CTB_BOARDID", classboard.CTB_BOARDID));
            arrParams.Add(new SqlParameter("@CTB_CREATEDUID", classboard.CTB_CREATEDUID));
            arrParams.Add(new SqlParameter("@CTB_SCHBRDID", classboard.CTB_SCHBRDID));
            arrParams.Add(new SqlParameter("@CTB_SCHOOLID", classboard.CTB_SCHOOLID));
            arrParams.Add(new SqlParameter("@CTB_TYPEID", classboard.CTB_TYPEID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassTypeBoard", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region RouteMaster
        public long InsertUpdateRoute(RouteMastes_RT route)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@RT_ROUTEID", route.RT_ROUTEID));
            arrParams.Add(new SqlParameter("@RT_CREATEDUID", route.RT_CREATEDUID));
            arrParams.Add(new SqlParameter("@RT_ISACTIVE", route.RT_ISACTIVE));
            arrParams.Add(new SqlParameter("@RT_ROUTENAME", route.RT_ROUTENAME));
            arrParams.Add(new SqlParameter("@RT_SCHOOLID", route.RT_SCHOOLID));
            //arrParams.Add(new SqlParameter("@RT_LOCATION", route.RT_LOCATION));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_RouteMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<FacultyProfileMasters_FPM> LoadFacultyByClass(FacultyProfileMasters_FPM obj)
        {
            List<FacultyProfileMasters_FPM> ListObject = new List<FacultyProfileMasters_FPM>();
            FacultyProfileMasters_FPM DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@FP_SchoolId", obj.FP_SchoolId));
            arrParams.Add(new SqlParameter("@FP_SessionId", obj.FP_SessionId));
            arrParams.Add(new SqlParameter("@FP_ClassId", obj.FP_ClassId));
            arrParams.Add(new SqlParameter("@FP_SubjectId", obj.FP_SubjectId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_FacluttyProfileMasters", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new FacultyProfileMasters_FPM();
                    DataObject.FP_Id = Convert.ToInt64(rdr["FP_Id"]);
                    DataObject.FP_Name = Convert.ToString(rdr["FP_Name"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        #endregion
        #region RoutewiseDropMaster
        public long InsertUpdateRoutewiseDrop(RoutewiseDropMaster_RDM obj)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("RDM_ROUTEID", typeof(Int64));
            dt.Columns.Add("RDM_DROPPOINT", typeof(string));
            dt.Columns.Add("RDM_DISTANCE", typeof(decimal));
            dt.Columns.Add("RDM_RATE", typeof(decimal));
            dt.Columns.Add("RDM_SERIAL", typeof(Int32));
            foreach (var data in obj.RouteWiseDropList)
            {
                DataRow dr = dt.NewRow();
                dr["RDM_ROUTEID"] = data.RDM_ROUTEID;
                dr["RDM_DROPPOINT"] = data.RDM_DROPPOINT;
                dr["RDM_DISTANCE"] = data.RDM_DISTANCE;
                dr["RDM_RATE"] = data.RDM_RATE;
                dr["RDM_SERIAL"] = data.RDM_SERIAL;
                dt.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@RouteWiseLocationsType", dt));
            arrParams.Add(new SqlParameter("@RDM_SCHOOLID", obj.RDM_SCHOOLID));
            arrParams.Add(new SqlParameter("@RDM_ROUTEID", obj.RDM_ROUTEID));
            arrParams.Add(new SqlParameter("@RDM_SESSIONID", obj.RDM_SESSIONID));
            arrParams.Add(new SqlParameter("@RDM_CREATEDUID", obj.RDM_CREATEDUID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_RoutewiseDropMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region SubjectGroupMaster
        public long InsertUpdateSubjectGroup(SubjectGroupMaster_SGM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SGM_SCHOOLID", obj.SGM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SGM_SubjectGroupName", obj.SGM_SubjectGroupName));
            arrParams.Add(new SqlParameter("@SGM_SubjectGroupID", obj.SGM_SubjectGroupID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubjectGroupMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region SubjectMaster
        public long InsertUpdateSubject(SubjectMaster_SBM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SBM_Id", obj.SBM_Id));
            arrParams.Add(new SqlParameter("@SBM_SchoolId", obj.SBM_SchoolId));
            //arrParams.Add(new SqlParameter("@SBM_SubGr_Id", obj.SBM_SubGr_Id));
            arrParams.Add(new SqlParameter("@SBM_SubjectCode", obj.SBM_SubjectCode));
            arrParams.Add(new SqlParameter("@SBM_SubjectName", obj.SBM_SubjectName));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubjectMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region ClassSubjectgroupWiseSubjectMaster
        // 05/09/2018 Mousumi
        public long InsertUpdateClassWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            DataTable DataList = new DataTable();

            DataList.Columns.Add("CSGWS_SubGr_Id", typeof(Int64));
            DataList.Columns.Add("CSGWS_Sub_Id", typeof(Int64));
            DataList.Columns.Add("CSGWS_School_Id", typeof(Int64));
            DataList.Columns.Add("CSGWS_Class_Id", typeof(Int64));
            foreach (var data in obj.ClassWiseSubjectList)
            {
                DataRow dr = DataList.NewRow();

                dr["CSGWS_SubGr_Id"] = data.CSGWS_SubGr_Id;
                dr["CSGWS_Sub_Id"] = data.CSGWS_Sub_Id;
                dr["CSGWS_School_Id"] = obj.CSGWS_School_Id;
                dr["CSGWS_Class_Id"] = data.CSGWS_Class_Id;
                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            arrParams.Add(new SqlParameter("@CSGWS_School_Id", obj.CSGWS_School_Id));
            arrParams.Add(new SqlParameter("@ClassWiseSubjectList", DataList));
            arrParams.Add(new SqlParameter("@CSGWS_Class_Id", obj.CSGWS_Class_Id));
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClsSubGrpWiseSubject", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion    ----

        #region InsertUpdateUserMaster
        public long InsertUpdateUserMaster(UserMaster_UM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (obj.UM_USERID > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@UM_USERID", obj.UM_USERID));
            arrParams.Add(new SqlParameter("@UM_LOGINID", obj.UM_LOGINID));
            arrParams.Add(new SqlParameter("@UM_PASSWORD", obj.UM_PASSWORD));
            arrParams.Add(new SqlParameter("@UM_USERNAME", obj.UM_USERNAME));
            arrParams.Add(new SqlParameter("@UM_USEREMAIL", obj.UM_USEREMAIL));
            arrParams.Add(new SqlParameter("@UM_USERMOBILE", obj.UM_USERMOBILE));
            //arrParams.Add(new SqlParameter("@UM_SECQUES", obj.UM_SECQUES));
            //arrParams.Add(new SqlParameter("@UM_SECPASSWORD", obj.UM_SECPASSWORD));
            arrParams.Add(new SqlParameter("@UM_ISACTIVE", obj.UM_ISACTIVE));
            arrParams.Add(new SqlParameter("@UM_SCM_SCHOOLID", obj.UM_SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@UM_CREATEDUID", obj.UM_CREATEDUID));
          //  arrParams.Add(new SqlParameter("@UM_CREATEDDATE", obj.UM_CREATEDDATE));
            arrParams.Add(new SqlParameter("@UM_ROLEID", obj.UM_ROLEID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlParameter OutPutPassword = new SqlParameter("@OutPutPassword", SqlDbType.VarChar,50);
            OutPutPassword.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutPassword);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_User_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 2].Value);
            var Password = Convert.ToString(arrParams[arrParams.Count - 1].Value);

            // EMAIL CONTENT

            string subject = "User Login Credential Details";
            string message =
                "Dear " + obj.UM_USERNAME + "<br/><br/>" +
                "User has been created successfully. Please find the login credentials below:<br/><br/>" +
                "<b>Login ID:</b> " + obj.UM_LOGINID + "<br/>" +
                "<b>Password:</b> " + Password + "<br/><br/>" +
                "Please keep this information safe.<br/><br/>" +
                "Regards,<br/>" +
                "School Administration";

            Utils.sendEmail(
                obj.UM_USEREMAIL,
                subject,
                message,
                false,
                false
                );
            return val;

        }


        public List<UserMaster_UM> UserMasterList(UserMaster_UM obj)
        {
            List<UserMaster_UM> ListObject = new List<UserMaster_UM>();
            UserMaster_UM DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@UM_SCM_SCHOOLID", obj.UM_SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlParameter OutPutPassword = new SqlParameter("@OutPutPassword", SqlDbType.VarChar,50);
            OutPutPassword.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutPassword);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_User_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new UserMaster_UM();
                    DataObject.UM_USERID = Convert.ToInt64(rdr["UM_USERID"]);
                    DataObject.UM_LOGINID = Convert.ToString(rdr["UM_LOGINID"]);
                    DataObject.UM_USERNAME = Convert.ToString(rdr["UM_USERNAME"]);
                    DataObject.UM_USEREMAIL = Convert.ToString(rdr["UM_USEREMAIL"]);
                    DataObject.UM_USERMOBILE = Convert.ToString(rdr["UM_USERMOBILE"]);
                    DataObject.UM_SCM_SCHOOLID = Convert.ToInt64(rdr["UM_SCM_SCHOOLID"]);
                    DataObject.UM_SCHOOLNAME = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    DataObject.UM_USERTYPE = Convert.ToString(rdr["UM_USERTYPE"]);
                    DataObject.UM_ROLEID = Convert.ToInt64(rdr["UM_ROLEID"]);
                    DataObject.UM_ROLENAME = Convert.ToString(rdr["ROLENAME"]);
                    DataObject.UM_ISACTIVE = Convert.ToBoolean(rdr["UM_ISACTIVE"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion


        #region InsertUpdateFaculty
        public long InsertUpdateFaculty(FacultyProfileMasters_FPM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (obj.FP_Id > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@FP_Address", obj.FP_Address));
            arrParams.Add(new SqlParameter("@FP_CreateBy", obj.FP_CreateBy));
            arrParams.Add(new SqlParameter("@FP_DateOfBirth", obj.FP_DateOfBirth));
            arrParams.Add(new SqlParameter("@FP_DateOfJoining", obj.FP_DateOfJoining));
            arrParams.Add(new SqlParameter("@FP_Degree", obj.FP_Degree));
            arrParams.Add(new SqlParameter("@FP_DesignationId", obj.FP_DesignationId));
            arrParams.Add(new SqlParameter("@FP_Email", obj.FP_Email));
            arrParams.Add(new SqlParameter("@FP_FacultyCode", obj.FP_FacultyCode));
            arrParams.Add(new SqlParameter("@FP_Id", obj.FP_Id));
            arrParams.Add(new SqlParameter("@FP_Name", obj.FP_Name));
            arrParams.Add(new SqlParameter("@FP_Phone", obj.FP_Phone));
            arrParams.Add(new SqlParameter("@FP_Pin", obj.FP_Pin));
            arrParams.Add(new SqlParameter("@FP_SchoolId", obj.FP_SchoolId));
            arrParams.Add(new SqlParameter("@FP_SessionId", obj.FP_SessionId));
            //Added on 4th June 2019
            arrParams.Add(new SqlParameter("@FP_ShortName", obj.FP_ShortName));
            arrParams.Add(new SqlParameter("@FP_Max_DEGM_Id", obj.FP_Max_DEGM_Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_FacluttyProfileMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region InsertUpdateClasswiseFaculty
        public long InsertUpdateClasswiseFaculty(ClassWiseFacultyMasters_CWF obj)
        {
            DataTable DataList = new DataTable();

            DataList.Columns.Add("CWF_FacId", typeof(Int64));
            DataList.Columns.Add("CWF_ClassId", typeof(Int64));
            DataList.Columns.Add("CWF_SchoolId", typeof(Int64));
            DataList.Columns.Add("CWF_SessionId", typeof(Int64));
            DataList.Columns.Add("CWF_SubjectId", typeof(Int64));
            DataList.Columns.Add("CWF_SectionId", typeof(Int64));
           
            foreach (var data in obj.ClasswiseFacultyList)
            {
                DataRow dr = DataList.NewRow();

                dr["CWF_FacId"] = data.CWF_FacId != null ? (object)data.CWF_FacId : DBNull.Value;
                dr["CWF_ClassId"] = data.CWF_ClassId != null ? (object)data.CWF_ClassId : DBNull.Value;
                dr["CWF_SchoolId"] = obj.CWF_SchoolId != null ? (object)obj.CWF_SchoolId : DBNull.Value;
                dr["CWF_SessionId"] = obj.CWF_SessionId != null ? (object)obj.CWF_SessionId : DBNull.Value;
                dr["CWF_SubjectId"] = data.CWF_SubjectId != null ? (object)data.CWF_SubjectId : DBNull.Value;
                dr["CWF_SectionId"] = DBNull.Value;

                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CWF_ClassId", obj.CWF_ClassId));
            arrParams.Add(new SqlParameter("@CWF_CreateBy", obj.CWF_CreateBy));
            arrParams.Add(new SqlParameter("@CWF_SchoolId", obj.CWF_SchoolId));
            arrParams.Add(new SqlParameter("@CWF_SessionId", obj.CWF_SessionId));
            arrParams.Add(new SqlParameter("@ClassWiseFacultyList", DataList));

            //arrParams.Add(new SqlParameter("@CWF_SectionId", obj.CWF_SectionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            arrParams.Add(new SqlParameter("@TransType", "INSERT"));
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassWiseFacultyMasters", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }
        public List<ClassWiseFacultyMasters_CWF> GetClassWiseFacultyList(Int64? SchoolId, Int64? SessionId, Int64? ClassId, Int64? SubId, Int64? FacId)
        {
            List<ClassWiseFacultyMasters_CWF> ListObject = new List<ClassWiseFacultyMasters_CWF>();
            ClassWiseFacultyMasters_CWF DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CWF_SchoolId", SchoolId));
            arrParams.Add(new SqlParameter("@CWF_SessionId", SessionId));
            arrParams.Add(new SqlParameter("@CWF_ClassId", ClassId));
            arrParams.Add(new SqlParameter("@CWF_SubjectId", SubId));
            arrParams.Add(new SqlParameter("@CWF_FacId", FacId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassWiseFacultyMasters", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClassWiseFacultyMasters_CWF();
                    DataObject.CWF_Id = Convert.ToInt64(rdr["CWF_Id"]);
                    DataObject.CWF_FacId = Convert.ToInt32(rdr["CWF_FacId"]);
                    DataObject.CWF_ClassId = Convert.ToInt32(rdr["CWF_ClassId"]);
                    DataObject.CWF_SchoolId = Convert.ToInt64(rdr["CWF_SchoolId"]);
                    DataObject.CWF_SessionId = Convert.ToInt64(rdr["CWF_SessionId"]);
                    DataObject.CWF_SubjectId = Convert.ToInt64(rdr["CWF_SubjectId"]);
                    DataObject.CWF_CM_CLASSNAME = Convert.ToString(rdr["CWF_CM_CLASSNAME"]);
                    DataObject.CWF_SBM_SubjectName = Convert.ToString(rdr["CWF_SBM_SubjectName"]);
                    DataObject.CWF_FPM_FP_Name = Convert.ToString(rdr["CWF_FPM_FP_Name"]);
                    //DataObject.CWF_SectionId = Convert.ToInt32(rdr["CWF_SectionId"]);
                   // DataObject.CWF_SectionName = Convert.ToString(rdr["CWF_SectionName"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion

        #region Routine Master
       
        public long InsertUpdateRoutine(ClassWiseTeacherRoutine_CWTR obj)
        {
            DataTable DataList = new DataTable();

            DataList.Columns.Add("CWTR_Class", typeof(Int64));
            DataList.Columns.Add("CWTR_Section", typeof(Int64));
            DataList.Columns.Add("CWTR_Day", typeof(Int64));
            DataList.Columns.Add("CWTR_Period", typeof(Int64));
            DataList.Columns.Add("CWTR_Teacher", typeof(Int64));
            DataList.Columns.Add("CWTR_Subject", typeof(Int64));
            DataList.Columns.Add("CWTR_SchoolId", typeof(Int64));
            DataList.Columns.Add("CWTR_SessionId", typeof(Int64));

            if (obj.Rutine_IsUpload != true)
            {
                foreach (var data in obj.RoutineList)
                {
                    DataRow dr = DataList.NewRow();
                    dr["CWTR_Class"] = data.CWTR_Class;

                    dr["CWTR_Section"] = data.CWTR_Section.HasValue ? (object)data.CWTR_Section.Value : DBNull.Value;
                    dr["CWTR_Day"] = data.CWTR_Day.HasValue ? (object)data.CWTR_Day.Value : DBNull.Value;
                    dr["CWTR_Period"] = data.CWTR_Period.HasValue ? (object)data.CWTR_Period.Value : DBNull.Value;
                    dr["CWTR_Teacher"] = data.CWTR_Teacher.HasValue ? (object)data.CWTR_Teacher.Value : DBNull.Value;
                    dr["CWTR_Subject"] = data.CWTR_Subject.HasValue ? (object)data.CWTR_Subject.Value : DBNull.Value;

                    dr["CWTR_SchoolId"] = obj.CWTR_SchoolId;
                    dr["CWTR_SessionId"] = obj.CWTR_SessionId;

                    DataList.Rows.Add(dr);
                }

            }

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CWTR_Class", obj.CWTR_Class));
            arrParams.Add(new SqlParameter("@TransType", obj.CWTR_Id > 0 ? "UPDATE" : "INSERT"));
            arrParams.Add(new SqlParameter("@CWTR_CreateBy", obj.CWTR_CreateBy));
            arrParams.Add(new SqlParameter("@CWTR_SchoolId", obj.CWTR_SchoolId));
            arrParams.Add(new SqlParameter("@CWTR_SessionId", obj.CWTR_SessionId));
            arrParams.Add(new SqlParameter("@Rutine_IsUpload", obj.Rutine_IsUpload));
            arrParams.Add(new SqlParameter("@CWTR_UploadFile", obj.CWTR_UploadFile));
            arrParams.Add(new SqlParameter("@CWTR_Title", obj.CWTR_Title));
            arrParams.Add(new SqlParameter("@CWTR_Description", obj.CWTR_Description));

            if (obj.CWTR_Id > 0) // Update condition
            {
                arrParams.Add(new SqlParameter("@CWTR_Section", obj.CWTR_Section));
                arrParams.Add(new SqlParameter("@CWTR_Day", obj.CWTR_Day));
                arrParams.Add(new SqlParameter("@CWTR_Period", obj.CWTR_Period));
                arrParams.Add(new SqlParameter("@CWTR_Teacher", obj.CWTR_Teacher));
                arrParams.Add(new SqlParameter("@CWTR_Subject", obj.CWTR_Subject));
            }

            if (obj.Rutine_IsUpload == false)
            {
                arrParams.Add(new SqlParameter("@RoutineList", DataList));
            }

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_RoutineMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<ClassWiseTeacherRoutine_CWTR> GetRoutineList(Int64 SchoolId, Int64 SessionId, int? ClassId)
        {
            List<ClassWiseTeacherRoutine_CWTR> ListObject = new List<ClassWiseTeacherRoutine_CWTR>();
            ClassWiseTeacherRoutine_CWTR DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CWTR_SchoolId", SchoolId));
            arrParams.Add(new SqlParameter("@CWTR_SessionId", SessionId));
            //arrParams.Add(new SqlParameter("@CWTR_Class", ClassId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_RoutineMasters", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClassWiseTeacherRoutine_CWTR();

                    DataObject.CWTR_Id = rdr["CWTR_Id"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Id"]) : 0;
                    DataObject.CWTR_Class = rdr["CWTR_Class"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Class"]) : 0;
                    DataObject.CWTR_CM_CLASSNAME = rdr["CWTR_CM_CLASSNAME"] != DBNull.Value ? Convert.ToString(rdr["CWTR_CM_CLASSNAME"]) : string.Empty;
                    DataObject.CWTR_Section = rdr["CWTR_Section"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Section"]) : 0;
                    DataObject.CWTR_SECM_SECTIONNAME = rdr["CWTR_SECM_SECTIONNAME"] != DBNull.Value ? Convert.ToString(rdr["CWTR_SECM_SECTIONNAME"]) : string.Empty;
                    DataObject.CWTR_Day = rdr["CWTR_Day"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Day"]) : 0;
                    DataObject.CWTR_DM_DayName = rdr["CWTR_DM_DayName"] != DBNull.Value ? Convert.ToString(rdr["CWTR_DM_DayName"]) : string.Empty;
                    DataObject.CWTR_Subject = rdr["CWTR_Subject"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Subject"]) : 0;
                    DataObject.CWTR_SBM_SubjectName = rdr["CWTR_SBM_SubjectName"] != DBNull.Value ? Convert.ToString(rdr["CWTR_SBM_SubjectName"]) : string.Empty;
                    DataObject.CWTR_Period = rdr["CWTR_Period"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Period"]) : 0;
                    DataObject.CWTR_PeriodName = rdr["CWTR_PeriodName"] != DBNull.Value ? Convert.ToString(rdr["CWTR_PeriodName"]) : string.Empty;
                    DataObject.CWTR_Teacher = rdr["CWTR_Teacher"] != DBNull.Value ? Convert.ToInt32(rdr["CWTR_Teacher"]) : 0;
                    DataObject.CWTR_FP_Name = rdr["CWTR_FP_Name"] != DBNull.Value ? Convert.ToString(rdr["CWTR_FP_Name"]) : string.Empty;

                    // Added fields
                    DataObject.CWTR_Title = rdr["CWTR_Title"] != DBNull.Value ? Convert.ToString(rdr["CWTR_Title"]) : string.Empty;
                    DataObject.CWTR_Description = rdr["CWTR_Description"] != DBNull.Value ? Convert.ToString(rdr["CWTR_Description"]) : string.Empty;
                    DataObject.CWTR_UploadFile = rdr["CWTR_UploadFile"] != DBNull.Value ? Convert.ToString(rdr["CWTR_UploadFile"]) : string.Empty;

                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public string CheckDuplicity(ClassWiseTeacherRoutine_CWTR obj)
        {
            string Result = "";
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CWTR_SessionId", obj.CWTR_SessionId));
            arrParams.Add(new SqlParameter("@CWTR_SchoolId", obj.CWTR_SchoolId));
            arrParams.Add(new SqlParameter("@CWTR_Class", obj.CWTR_Class));
            //arrParams.Add(new SqlParameter("@CWTR_Section", obj.CWTR_Section));
            arrParams.Add(new SqlParameter("@CWTR_Subject", obj.CWTR_Subject));
            arrParams.Add(new SqlParameter("@CWTR_Teacher", obj.CWTR_Teacher));
            arrParams.Add(new SqlParameter("@CWTR_Period", obj.CWTR_Period));
            arrParams.Add(new SqlParameter("@CWTR_Day", obj.CWTR_Day));
            arrParams.Add(new SqlParameter("@TransType", "CHECK"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_RoutineMasters", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Result = Convert.ToString(rdr["Result"]);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return Result;
        }
        #endregion
        #region CategoryMaster
        public long InsertUpdateCategory(STUDENTCATEGORY_CAT obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CAT_STUDENTCATEGORY", obj.CAT_STUDENTCATEGORY));
            arrParams.Add(new SqlParameter("@CAT_CREATEDUID", obj.CAT_CREATEDUID));
            arrParams.Add(new SqlParameter("@CAT_CATEGORYID", obj.CAT_CATEGORYID));
            arrParams.Add(new SqlParameter("@CAT_SCHOOLID", obj.CAT_SCHOOLID));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_FeesCategory", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region ClasswiseFees
        public long InsertUpdateClasswiseFees(ClassWisefees_CF obj)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("CF_CLASSID", typeof(Int32));
            DataList.Columns.Add("CF_CATEGORYID", typeof(Int32));
            DataList.Columns.Add("CF_FEESID", typeof(Int32));
            DataList.Columns.Add("CF_NOOFINS", typeof(Int32));
            DataList.Columns.Add("CF_FEESAMOUNT", typeof(decimal));
            DataList.Columns.Add("CF_INSTALLMENTNO", typeof(Int64));
            DataList.Columns.Add("CF_INSAMOUNT", typeof(decimal));
            DataList.Columns.Add("CF_SCHOOLID", typeof(Int64));
            DataList.Columns.Add("CF_SESSIONID", typeof(Int64));
            DataList.Columns.Add("CF_DUEDATE", typeof(string));
            DataList.Columns.Add("IsAdmissionTime", typeof(bool));
            foreach (var data in obj.ClassWiseFeesList)
            {
                DataRow dr = DataList.NewRow();
                dr["CF_CLASSID"] = data.CF_CLASSID;
                dr["CF_CATEGORYID"] = data.CF_CATEGORYID;
                dr["CF_FEESID"] = data.CF_FEESID;
                dr["CF_NOOFINS"] = data.CF_NOOFINS;
                dr["CF_FEESAMOUNT"] = data.CF_FEESAMOUNT;
                dr["CF_INSTALLMENTNO"] = data.CF_INSTALLMENTNO;
                dr["CF_INSAMOUNT"] = data.CF_INSAMOUNT;
                dr["CF_SCHOOLID"] = obj.CF_SCHOOLID;
                dr["CF_SESSIONID"] = obj.CF_SESSIONID;
                dr["CF_DUEDATE"] = data.CF_DUEDATE;
                dr["IsAdmissionTime"] = data.IsAdmissionTime ?? false;
                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CF_CREATEDUID", obj.CF_CREATEDUID));
            arrParams.Add(new SqlParameter("@CF_CLASSID", obj.CF_CLASSID));
            arrParams.Add(new SqlParameter("@CF_FEESID", obj.CF_FEESID));
            arrParams.Add(new SqlParameter("@CF_CATEGORYID", obj.CF_CATEGORYID));
            arrParams.Add(new SqlParameter("@CF_SCHOOLID", obj.CF_SCHOOLID));
            arrParams.Add(new SqlParameter("@CF_SESSIONID", obj.CF_SESSIONID));
            arrParams.Add(new SqlParameter("@ClassWiseFeesList", DataList));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassWiseFees", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public long InsertUpdateClasswiseFeesForward(ClassWisefees_CF obj)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("CF_CLASSID", typeof(Int32));
            DataList.Columns.Add("CF_CATEGORYID", typeof(Int32));
            DataList.Columns.Add("CF_FEESID", typeof(Int32));
            DataList.Columns.Add("CF_NOOFINS", typeof(Int32));
            DataList.Columns.Add("CF_FEESAMOUNT", typeof(decimal));
            DataList.Columns.Add("CF_INSTALLMENTNO", typeof(Int64));
            DataList.Columns.Add("CF_INSAMOUNT", typeof(decimal));
            DataList.Columns.Add("CF_SCHOOLID", typeof(Int64));
            DataList.Columns.Add("CF_SESSIONID", typeof(Int64));
            DataList.Columns.Add("CF_DUEDATE", typeof(string));
            DataList.Columns.Add("IsAdmissionTime", typeof(bool));
            foreach (var data in obj.ClassWiseFeesList)
            {
                DataRow dr = DataList.NewRow();
                dr["CF_CLASSID"] = obj.CF_CLASSID;
                dr["CF_CATEGORYID"] = data.CF_CATEGORYID;
                dr["CF_FEESID"] = data.CF_FEESID;
                dr["CF_NOOFINS"] = data.CF_NOOFINS;
                dr["CF_FEESAMOUNT"] = data.CF_FEESAMOUNT;
                dr["CF_INSTALLMENTNO"] = data.CF_NOOFINS;
                dr["CF_INSAMOUNT"] = data.CF_INSAMOUNT;
                dr["CF_SCHOOLID"] = obj.CF_SCHOOLID;
                dr["CF_SESSIONID"] = data.CF_SESSIONID;
                dr["CF_DUEDATE"] = data.CF_DUEDATE;
                dr["IsAdmissionTime"] = data.IsAdmissionTime ?? false;
                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CF_CREATEDUID", obj.CF_CREATEDUID));
            arrParams.Add(new SqlParameter("@CF_CLASSID", obj.CF_CLASSID));
            arrParams.Add(new SqlParameter("@CF_FEESID", obj.CF_FEESID));
            arrParams.Add(new SqlParameter("@CF_CATEGORYID", obj.CF_CATEGORYID));
            arrParams.Add(new SqlParameter("@CF_SCHOOLID", obj.CF_SCHOOLID));
            arrParams.Add(new SqlParameter("@CF_SESSIONID", obj.ClassWiseFeesList.FirstOrDefault().CF_SESSIONID));
            arrParams.Add(new SqlParameter("@ClassWiseFeesList", DataList));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClassWiseFees", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<ClassWisefees_CF> GetClassWiseFeesList(Int64 SchoolId, Int64 SessionId, int? ClassId)
        {
            List<ClassWisefees_CF> ListObject = new List<ClassWisefees_CF>();
            ClassWisefees_CF DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CF_SCHOOLID", SchoolId));
            arrParams.Add(new SqlParameter("@CF_SESSIONID", SessionId));
            arrParams.Add(new SqlParameter("@CF_CLASSID", ClassId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetClassWiseFeesList", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClassWisefees_CF();
                    DataObject.FeesHeadName = Convert.ToString(rdr["FeesHeadName"]);
                    DataObject.CF_CLASSFEESID = Convert.ToInt32(rdr["CF_CLASSFEESID"]);
                    DataObject.CF_CLASSID = Convert.ToInt32(rdr["CF_CLASSID"]);
                    DataObject.CF_CATEGORYID = Convert.ToInt32(rdr["CF_CATEGORYID"]);
                    DataObject.CF_FEESID = Convert.ToInt32(rdr["CF_FEESID"]);
                    DataObject.CF_NOOFINS = Convert.ToInt32(rdr["CF_NOOFINS"]);
                    DataObject.CF_FEESAMOUNT = Convert.ToDecimal(rdr["CF_FEESAMOUNT"]);
                    DataObject.CF_INSTALLMENTNO = Convert.ToInt32(rdr["CF_INSTALLMENTNO"]);
                    DataObject.CF_INSAMOUNT = Convert.ToDecimal(rdr["CF_INSAMOUNT"]);
                    DataObject.Testdata = Convert.ToString(rdr["Testdata"]);
                    DataObject.DUEDATES = Convert.ToString(rdr["Testdata"]);
                    DataObject.ClassName = Convert.ToString(rdr["ClassName"]);
                    DataObject.CAT_CATEGORYNAME = Convert.ToString(rdr["CAT_CATEGORYNAME"]);
                    DataObject.IsAdmissionTime = rdr["IsAdmissionTime"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsAdmissionTime"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region InsertUpdateFeesMaster
        public long InsertUpdateFees(FeesMaster_FEM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@FEESNAME", obj.FEM_FEESNAME));
            arrParams.Add(new SqlParameter("@FEESAMOUNT", obj.FEM_AMOUNT));
            arrParams.Add(new SqlParameter("@INSTYPEID", obj.FEM_INSTYPEID));
            arrParams.Add(new SqlParameter("@ISACTIVE", obj.FEM_ISACTIVE));
            arrParams.Add(new SqlParameter("@ISONLYADMISSION", obj.FEM_ISONLYADMISSION));
            arrParams.Add(new SqlParameter("@ISADMINTIMEMAN", obj.FEM_ISADMINTIMEMAN));
            arrParams.Add(new SqlParameter("@ISDUPDOCFEES", obj.FEM_ISDUPDOCFEES));
            arrParams.Add(new SqlParameter("@ISDUPIDFEES", obj.FEM_ISDUPIDFEES));
            arrParams.Add(new SqlParameter("@ISHOSTELFEES", obj.FEM_ISHOSTELFEES));
            arrParams.Add(new SqlParameter("@ISPROCESSINGFEES", obj.FEM_ISPROCESSINGFEES));
            arrParams.Add(new SqlParameter("@ISREFUNDABLE", obj.FEM_ISREFUNDABLE));
            arrParams.Add(new SqlParameter("@FEM_NONE", obj.FEM_NONE));
            arrParams.Add(new SqlParameter("@ISTRANSPORTFEES", obj.FEM_ISTRANSPORTFEES));
            arrParams.Add(new SqlParameter("@SCHOOLID", obj.FEM_SCHOOLID));
            arrParams.Add(new SqlParameter("@TOTALINSTALLMENT", obj.FEM_TOTALINSTALLMENT));
            arrParams.Add(new SqlParameter("@CREATEDUID", obj.FEM_CREATEDUID));
            if (obj.FEM_FEESID != null && obj.FEM_FEESID != 0)
            {
                arrParams.Add(new SqlParameter("@TransactionType", "Update"));
                arrParams.Add(new SqlParameter("@FEESID", obj.FEM_FEESID));
            }
            else
            {
                arrParams.Add(new SqlParameter("@TransactionType", "Insert"));
            }
            arrParams.Add(new SqlParameter("@NOOFINSTALLMENT", obj.FEM_NOOFINSTALLMENT));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_FeesMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<FeesMaster_FEM> GetFeesList(bool? IsAdmission)
        {
            List<FeesMaster_FEM> ListObject = new List<FeesMaster_FEM>();
            FeesMaster_FEM DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@ISADMINTIMEMAN", IsAdmission));
            arrParams.Add(new SqlParameter("@TransactionType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_FeesMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new FeesMaster_FEM();

                    DataObject.FEM_FEESID = Convert.ToInt64(rdr["FEM_FEESID"]);
                    DataObject.FEM_FEESNAME = Convert.ToString(rdr["FEM_FEESNAME"]);
                    DataObject.FEM_ISACTIVE = Convert.ToBoolean(rdr["FEM_ISACTIVE"]);
                    int col = rdr.GetOrdinal("FEM_ISONLYADMISSION");
                    DataObject.FEM_ISONLYADMISSION = !rdr.IsDBNull(col) && rdr.GetBoolean(col);
                    DataObject.FEM_ISADMINTIMEMAN = Convert.ToBoolean(rdr["FEM_ISADMINTIMEMAN"]);
                    DataObject.FEM_SCHOOLID = Convert.ToInt64(rdr["FEM_SCHOOLID"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region SchoolMasters
        public long AddEditSchool(SchoolMasters_SCM obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SCM_SCHOOLCODE", obj.SCM_SCHOOLCODE));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLNAME", obj.SCM_SCHOOLNAME));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLADDRESS1", obj.SCM_SCHOOLADDRESS1));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLADDRESS2", obj.SCM_SCHOOLADDRESS2));
            arrParams.Add(new SqlParameter("@SCM_IMAGENAME", obj.SCM_IMAGENAME));
            arrParams.Add(new SqlParameter("@SCM_SCHOOLID", obj.SCM_SCHOOLID));
            arrParams.Add(new SqlParameter("@SCM_CREATEDUID", obj.SCM_CREATEDUID));
            arrParams.Add(new SqlParameter("@SCM_SECRETARYNAME", obj.SCM_SECRETARYNAME));
            arrParams.Add(new SqlParameter("@SCM_STATEID", obj.SCM_STATEID));
            arrParams.Add(new SqlParameter("@SCM_NATIONID", obj.SCM_NATIONID));
            arrParams.Add(new SqlParameter("@SCM_DISTRICTID", obj.SCM_DISTRICTID));
            arrParams.Add(new SqlParameter("@SCM_PINCODE", obj.SCM_PINCODE));
            arrParams.Add(new SqlParameter("@SCM_PHONENO1", obj.SCM_PHONENO1));
            arrParams.Add(new SqlParameter("@SCM_PHONENO2", obj.SCM_PHONENO2));
            arrParams.Add(new SqlParameter("@SCM_CONTACTPERSON", obj.SCM_CONTACTPERSON));
            arrParams.Add(new SqlParameter("@SCM_EMAILID", obj.SCM_EMAILID));
            arrParams.Add(new SqlParameter("@SCM_WEBSITE", obj.SCM_WEBSITE));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SchoolMasters", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;

        }
        #endregion
        #region StudentWiseSubjectMaster
        public List<ClsSubGrWiseSubSetting_CSGWS> GetGroupWiseSubject(ClsSubGrWiseSubSetting_CSGWS obj)
        {
            List<ClsSubGrWiseSubSetting_CSGWS> ListObject = new List<ClsSubGrWiseSubSetting_CSGWS>();
            ClsSubGrWiseSubSetting_CSGWS DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CSGWS_Class_Id", obj.CSGWS_Class_Id));
            arrParams.Add(new SqlParameter("@CSGWS_SubGr_Id", obj.CSGWS_SubGr_Id));
            arrParams.Add(new SqlParameter("@CSGWS_School_Id", obj.CSGWS_School_Id));
            arrParams.Add(new SqlParameter("@Session_Id", obj.Session_Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetSubjectByGroup", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClsSubGrWiseSubSetting_CSGWS();
                    DataObject.CSGWS_Sub_Id = Convert.ToInt32(rdr["CSGWS_Sub_Id"]);
                    DataObject.CSGWS_SBM_SubjectName = Convert.ToString(rdr["CSGWS_SBM_SubjectName"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<StudentwiseSubjectSetting_SWS> GetIndividualStudentWiseSubjectForEdit(string Sid)
        {
            List<StudentwiseSubjectSetting_SWS> ListObject = new List<StudentwiseSubjectSetting_SWS>();
            StudentwiseSubjectSetting_SWS DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SelectforEdit"));
            arrParams.Add(new SqlParameter("SD_StudentId", Sid));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "InserUpdate_StudentwiseSubject_Sp", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentwiseSubjectSetting_SWS();
                    DataObject.SWS_SubjectId = Convert.ToInt32(rdr["SWS_SubjectId"]);
                    DataObject.SWS_SubGroupId = Convert.ToInt32(rdr["SWS_SubGroupId"]);
                    DataObject.SWS_SubjectName = Convert.ToString(rdr["SBM_SubjectName"]);
                    DataObject.SWS_SubjectGroupName = Convert.ToString(rdr["SGM_SubjectGroupName"]);
                    DataObject.SWS_StudentSID = Convert.ToString(rdr["SWS_StudentSID"]);
                    DataObject.SWS_ClassId = Convert.ToInt32(rdr["SWS_ClassId"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public List<StudentwiseSubjectSetting_SWS> StudentWiseSubjectListForEdit(StudentwiseSubjectSetting_SWS obj)
        {
            List<StudentwiseSubjectSetting_SWS> ListObject = new List<StudentwiseSubjectSetting_SWS>();
            StudentwiseSubjectSetting_SWS DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SelectAll"));
            arrParams.Add(new SqlParameter("@SWS_ClassId", obj.SWS_ClassId));
            arrParams.Add(new SqlParameter("@SWS_SchoolId", obj.SWS_SchoolId));
            arrParams.Add(new SqlParameter("@SWS_SessionId", obj.SWS_SessionId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "InserUpdate_StudentwiseSubject_Sp", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new StudentwiseSubjectSetting_SWS();
                    DataObject.SWS_StudentSID = Convert.ToString(rdr["SWS_StudentSID"]);
                    DataObject.SWS_SubjectCode = Convert.ToString(rdr["SWS_SubjectCode"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public long InsertUpdateStudentwiseSubject(StudentwiseSubjectSetting_SWS Subject)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SWS_StudentSID", typeof(string));
            dt.Columns.Add("SWS_SubGroupId", typeof(Int64));
            dt.Columns.Add("SWS_SubjectId", typeof(Int64));
            dt.Columns.Add("SWS_CreatedBy", typeof(Int64));

            foreach (var Sub in Subject.StudentWiseSubjectList)
            {
                DataRow dr = dt.NewRow();
                dr["SWS_StudentSID"] = Sub.SWS_StudentSID;
                dr["SWS_SubGroupId"] = Sub.SWS_SubGroupId;
                dr["SWS_SubjectId"] = Sub.SWS_SubjectId;
                dr["SWS_CreatedBy"] = Subject.SWS_CreatedBy;
                dt.Rows.Add(dr);
            }

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "Insert"));
            arrParams.Add(new SqlParameter("@SWS_SchoolId", Subject.SWS_SchoolId));
            arrParams.Add(new SqlParameter("@SWS_SessionId", Subject.SWS_SessionId));
            arrParams.Add(new SqlParameter("@SWS_ClassId", Subject.SWS_ClassId));
            arrParams.Add(new SqlParameter("@StudentWiseSubjectlist", dt));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "InserUpdate_StudentwiseSubject_Sp",
                arrParams.ToArray()
            );

            long ID = Convert.ToInt64(OutPutId.Value);   

            if (ID == -1)
            {
                throw new Exception("This subject is already assigned to the student.");
            }

            return ID;   
        }

        public List<StudetDetails_SD> GetStudentListForSubjectSettings(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SELECT"));
            arrParams.Add(new SqlParameter("@SWS_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SWS_SessionId", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SWS_ClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "InserUpdate_StudentwiseSubject_Sp", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_CurrentSection = rdr["SD_SECTIONNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_SECTIONNAME"]);
                    DataObject.SD_CurrentRoll = rdr["SD_CurrentRoll"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentRoll"]);
                    DataObject.SD_CurrentSectionId = rdr["SD_CurrentSectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentSectionId"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region SectioRollSettings
        public decimal InsertUpdateSecRollSetting(Sec_Roll_Setting_SR obj)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CLASSID", typeof(Int64));
                dt.Columns.Add("STUDENTID", typeof(string));
                dt.Columns.Add("SECTIONID", typeof(Int64));
                dt.Columns.Add("ROLLNO", typeof(string));
                dt.Columns.Add("SESSIONID", typeof(Int64));
                dt.Columns.Add("SCHOOLID", typeof(Int64));
                foreach (var Sub in obj.SecRollList)
                {
                    if (Sub.SR_ROLLNO != "" && Sub.SR_SECTIONID != 0)
                    {
                        DataRow dr = dt.NewRow();
                        dr["CLASSID"] = Sub.SR_CLASSID;
                        dr["STUDENTID"] = Sub.SR_STUDENTID;
                        dr["SECTIONID"] = Sub.SR_SECTIONID;
                        dr["ROLLNO"] = Sub.SR_ROLLNO;
                        dr["SESSIONID"] = obj.SR_SESSIONID;
                        dr["SCHOOLID"] = obj.SR_SCHOOLID;
                        dt.Rows.Add(dr);
                    }
                }
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransactionType", "Insert"));
                arrParams.Add(new SqlParameter("@SCHOOLID", obj.SR_SCHOOLID));
                arrParams.Add(new SqlParameter("@SESSIONID", obj.SR_SESSIONID));
                arrParams.Add(new SqlParameter("@SecRollList", dt));

                arrParams.Add(new SqlParameter("@CREATEDUID", obj.SR_CREATEDUID));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SectionRollSettings", arrParams.ToArray());
                decimal ID = Convert.ToDecimal(OutPutId.Value);
                return Convert.ToDecimal(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        public List<StudetDetails_SD> GetStudentListRollSecSetting(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransactionType", "SELECT_FOR_ROll&SEC"));
            arrParams.Add(new SqlParameter("@SWS_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SWS_SessionId", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SWS_ClassId", obj.SD_ClassId));

            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "InserUpdate_StudentwiseSubject_Sp", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_CurrentSection = rdr["SD_SECTIONNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_SECTIONNAME"]);
                    DataObject.SD_CurrentRoll = rdr["SD_CurrentRoll"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentRoll"]);
                    DataObject.SD_CurrentSectionId = rdr["SD_CurrentSectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentSectionId"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        #region InsertUpdateLateFeesSlap
        public long InsertUpdateLateFeesSlap(LateFeesSlap LtSlapObj)
        {
            try
            {
                List<SqlParameter> arrParams = new List<SqlParameter>
        {
            new SqlParameter("@Slap_Name", LtSlapObj.Slap_Name),
            new SqlParameter("@Slap_Amount", LtSlapObj.Slap_Amount),
            new SqlParameter("@Slap_FineTypeID", LtSlapObj.Slap_FineTypeID),
            new SqlParameter("@Slap_SchooIID", LtSlapObj.Slap_SchooIID),
            new SqlParameter("@Slap_SessionID", LtSlapObj.Slap_SessionID),
            new SqlParameter("@IsActive", LtSlapObj.IsActive) 
        };

                if (LtSlapObj.Id != null && LtSlapObj.Id != 0)
                {
                    arrParams.Add(new SqlParameter("@TransactionType", "Update"));
                    arrParams.Add(new SqlParameter("@ID", LtSlapObj.Id));
                    arrParams.Add(new SqlParameter("@UpdatedBy", LtSlapObj.UserId));
                }
                else
                {
                    arrParams.Add(new SqlParameter("@TransactionType", "Insert"));
                    arrParams.Add(new SqlParameter("@CreatedBy", LtSlapObj.UserId));
                }
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                {
                    Direction = ParameterDirection.Output
                };
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_LateFeesSlapMaster", arrParams.ToArray());
                return Convert.ToInt64(OutPutId.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
        #region TransportSetting
        public List<StudetDetails_SD> GetStudentListForTransportSettings(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionID", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportSetting", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_CurrentSection = rdr["SD_SECTIONNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_SECTIONNAME"]);
                    DataObject.SD_CurrentRoll = rdr["SD_CurrentRoll"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentRoll"]);
                    DataObject.SD_CurrentSectionId = rdr["SD_CurrentSectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentSectionId"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<StudetDetails_SD> TransportAvailedStudentList(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SelectList"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionID", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportSetting", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_TransportFare = Convert.ToDecimal(rdr["SD_TransportFare"]);
                    DataObject.SD_RouteId = Convert.ToInt32(rdr["SD_RouteId"]);
                    DataObject.SD_ROUTENAME = rdr["RT_ROUTENAME"] == DBNull.Value ? "" : Convert.ToString(rdr["RT_ROUTENAME"]);
                    DataObject.SD_PickLocationId = rdr["SD_PickLocationId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_PickLocationId"]);
                    DataObject.SD_DropPoint = rdr["SD_DropPoint"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_DropPoint"]);
                    DataObject.SD_PickPoint = rdr["SD_PickPoint"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_PickPoint"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public decimal UpdateTransport(StudetDetails_SD data)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("StudentId", typeof(string));
                dt.Columns.Add("RouteId", typeof(Int64));
                dt.Columns.Add("TypeId", typeof(Int64));
                dt.Columns.Add("PickId", typeof(Int64));
                dt.Columns.Add("DropId", typeof(Int64));
                dt.Columns.Add("Fare", typeof(decimal));
                dt.Columns.Add("TransportMonthId", typeof(Int32));

                foreach (var obj in data.TransportList)
                {
                    DataRow dr = dt.NewRow();
                    dr["StudentId"] = obj.SD_StudentId;
                    dr["RouteId"] = obj.RouteId;
                    dr["TypeId"] = obj.TypeId;
                    dr["PickId"] = obj.PickUpId;
                    dr["DropId"] = obj.DropId;
                    dr["Fare"] = obj.Fare;
                    dr["TransportMonthId"] = obj.TransportMonthId;
                    dt.Rows.Add(dr);
                }
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@TransportSettingType", dt));
                arrParams.Add(new SqlParameter("@TransType", "Update"));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportSetting", arrParams.ToArray());
                decimal ID = Convert.ToDecimal(OutPutId.Value);
                return Convert.ToDecimal(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public long DeleteTransportFromStudents(string StudentId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", StudentId));
            arrParams.Add(new SqlParameter("@TransType", "Delete"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportSetting", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region HostelSetting
        public List<StudetDetails_SD> GetStudentListForHostelSettings(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "Select"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionID", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("SD_CurrentClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_HostelSetting", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_CurrentSection = rdr["SD_SECTIONNAME"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_SECTIONNAME"]);
                    DataObject.SD_CurrentRoll = rdr["SD_CurrentRoll"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentRoll"]);
                    DataObject.SD_CurrentSectionId = rdr["SD_CurrentSectionId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentSectionId"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public List<StudetDetails_SD> HostelAviledStudentList(StudetDetails_SD obj)
        {
            if (obj.SD_CurrentSectionId == 0)
            {
                obj.SD_CurrentSectionId = null;
            }
            List<StudetDetails_SD> ListObject = new List<StudetDetails_SD>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SelectList"));
            arrParams.Add(new SqlParameter("@SD_SchoolId", obj.SD_SchoolId));
            arrParams.Add(new SqlParameter("@SD_CurrentSessionID", obj.SD_SessionId));
            arrParams.Add(new SqlParameter("@SD_CurrentClassId", obj.SD_ClassId));
            arrParams.Add(new SqlParameter("@SD_CurrentSectionId", obj.SD_CurrentSectionId));
            arrParams.Add(new SqlParameter("@SD_StudentName", obj.SD_StudentName));
            arrParams.Add(new SqlParameter("@SD_StudentId", obj.SD_StudentId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_HostelSetting", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    StudetDetails_SD DataObject = new StudetDetails_SD();
                    DataObject.SD_StudentName = Convert.ToString(rdr["SD_StudentName"]);
                    DataObject.SD_CM_CLASSNAME = Convert.ToString(rdr["SD_CLASSNAME"]);
                    DataObject.SD_StudentId = rdr["SD_StudentId"] == DBNull.Value ? "" : Convert.ToString(rdr["SD_StudentId"]);
                    DataObject.SD_CurrentClassId = rdr["SD_CurrentClassId"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SD_CurrentClassId"]);
                    DataObject.SD_HostelFare = Convert.ToDecimal(rdr["SD_HostelFare"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        public decimal InsertStudentForHostel(HostelMaster_HM data)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("hostel_SchoolId", typeof(Int64));
                dt.Columns.Add("hostel_SessionId", typeof(Int64));
                dt.Columns.Add("Hostel_StudentId", typeof(string));
                dt.Columns.Add("Hostel_RoomNo", typeof(Int32));
                dt.Columns.Add("Hoset_Fare", typeof(Int64));
                dt.Columns.Add("Hostel_Month", typeof(Int32));
                foreach (var obj in data.HostelDataList)
                {
                    DataRow dr = dt.NewRow();
                    dr["hostel_SchoolId"] = data.hostel_SchoolId;
                    dr["hostel_SessionId"] = data.hostel_SessionId;
                    dr["Hostel_StudentId"] = obj.Hostel_StudentId;
                    dr["Hostel_RoomNo"] = obj.Hostel_RoomNo;
                    dr["Hoset_Fare"] = obj.Hoset_Fare;
                    dr["Hostel_Month"] = obj.Hostel_Month;
                    dt.Rows.Add(dr);
                }
                List<SqlParameter> arrParams = new List<SqlParameter>();
                arrParams.Add(new SqlParameter("@HostelSettingType", dt));
                arrParams.Add(new SqlParameter("@TransType", "Insert"));
                arrParams.Add(new SqlParameter("@Hostel_CreatedBy", data.Hostel_CreatedBy));
                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HostelSetting", arrParams.ToArray());
                decimal ID = Convert.ToDecimal(OutPutId.Value);
                return Convert.ToDecimal(arrParams[arrParams.Count - 1].Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public long DeleteHostelSetting(string StudentId)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SD_StudentId", StudentId));
            arrParams.Add(new SqlParameter("@TransType", "Delete"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HostelSetting", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region Designation
        public long InsertUpdateDesignation(DesignationMaster_DM designation)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@DM_Id", designation.DM_Id));
            arrParams.Add(new SqlParameter("@DM_Name", designation.DM_Name));
            arrParams.Add(new SqlParameter("@DM_SchoolId", designation.DM_SchoolId));
            // arrParams.Add(new SqlParameter("@Userid", designation.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_DesignationMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region Bank
        public long InsertUpdateBank(BankMaster_BM bank)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@BankId", bank.BankId));
            arrParams.Add(new SqlParameter("@BankName", bank.BankName));
            arrParams.Add(new SqlParameter("@CreatedBy", bank.CreatedBy));
            arrParams.Add(new SqlParameter("@SchoolId", bank.SchoolId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_BankMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region TransportType
        public long InsertUpdateTransportType(TransportType_TT transport)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
        new SqlParameter("@TypeId", transport.TypeId),
        new SqlParameter("@TransportType", transport.TransportType),
        new SqlParameter("@CreatedId", transport.Userid)
    };

            SqlParameter outputId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(outputId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_TransportType", arrParams.ToArray());

            return Convert.ToInt64(outputId.Value);
        }
        #endregion
        #region HostelRoomNo
        public long InsertUpdateHostelRoom(HostelRoomMaster_HR hostel)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@HR_HostelRoomId", hostel.HR_HostelRoomId));
            //arrParams.Add(new SqlParameter("@HR_HostelId", hostel.HR_HostelId));
            arrParams.Add(new SqlParameter("@HR_HostelRoomNo", hostel.HR_HostelRoomNo));
            arrParams.Add(new SqlParameter("@HR_HostelSchoolId", hostel.HR_HostelSchoolId));
            arrParams.Add(new SqlParameter("@HR_HostelSessionId", hostel.HR_HostelSessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_HostelMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region Menu Module ddl List
        public List<_MenuMaster> ModuleddlList(_MenuMaster obj)
        {
            List<_MenuMaster> ListObject = new List<_MenuMaster>();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SelectModule"));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Menu_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    _MenuMaster DataObject = new _MenuMaster();
                    DataObject.MenuId = Convert.ToInt32(rdr["MenuId"]);
                    DataObject.MenuName = Convert.ToString(rdr["MenuName"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion
        //Add by Uttaran
        #region ClassWise Syllabus
        public long InsertUpdateSyllabus(ClassWiseSyllabusMasters_CWSM syllabus)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SM_ID", syllabus.SM_Id));
            arrParams.Add(new SqlParameter("@SM_SyllabusName", syllabus.SM_SyllabusName));
            arrParams.Add(new SqlParameter("@SM_UploadFile", syllabus.SM_UploadFile));
            arrParams.Add(new SqlParameter("@SM_SchoolId", syllabus.SM_SchoolId));
            arrParams.Add(new SqlParameter("@SM_ClassId", syllabus.SM_ClassId));
            arrParams.Add(new SqlParameter("@SM_SessionId", syllabus.SM_SessionId));
            arrParams.Add(new SqlParameter("@SM_CreateDate", syllabus.SM_CreateDate));
            arrParams.Add(new SqlParameter("@SM_CreateBy", syllabus.SM_CreateBy));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SyllabusMaster_SM", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region Classwise Assignment Master
        public long InsertUpdateAssignmentMaster(AssignmentMaster_ASM ASM)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = ASM.ASM_ID > 0 ? "UPDATE" : "INSERT";

            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@ASM_ID", ASM.ASM_ID));
            arrParams.Add(new SqlParameter("@ASM_FP_Id", ASM.ASM_FP_Id));
            arrParams.Add(new SqlParameter("@ASM_SubGr_ID", ASM.ASM_SubGr_ID));
            arrParams.Add(new SqlParameter("@ASM_Sub_ID", ASM.ASM_Sub_ID));
            arrParams.Add(new SqlParameter("@ASM_School_ID", ASM.ASM_School_ID));
            arrParams.Add(new SqlParameter("@ASM_Class_ID", ASM.ASM_Class_ID));
            arrParams.Add(new SqlParameter("@ddlASM_Section_ID", ASM.ASM_Section_ID));
            arrParams.Add(new SqlParameter("@ASM_Session_ID", ASM.ASM_Session_ID));
            arrParams.Add(new SqlParameter("@ASM_Title", ASM.ASM_Title));
            arrParams.Add(new SqlParameter("@ASM_Desc", ASM.ASM_Desc));
            arrParams.Add(new SqlParameter("@ASM_StartDate",ASM.ASM_StartDate == null? (object)DBNull.Value: ASM.ASM_StartDate));
            arrParams.Add(new SqlParameter("@ASM_ExpDate", ASM.ASM_ExpDate == null? (object)DBNull.Value: ASM.ASM_ExpDate));
            arrParams.Add(new SqlParameter("@ASM_UploadDoc", ASM.ASM_UploadDoc));
            arrParams.Add(new SqlParameter("@Userid", ASM.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AssignmentMaster", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }


        public long UpdateAssignmentMarks(AssignmentMarkUpdate assignmentMarkUpdate)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", "MarksUPDATE"));
            arrParams.Add(new SqlParameter("@ASM_ID", assignmentMarkUpdate.ASM_ID));
            arrParams.Add(new SqlParameter("@AST_StudentId", assignmentMarkUpdate.AST_StudentId));
            arrParams.Add(new SqlParameter("@marks", assignmentMarkUpdate.AST_Marks));
            arrParams.Add(new SqlParameter("@total_marks", assignmentMarkUpdate.AST_TotalMarks));
            arrParams.Add(new SqlParameter("@remarks", assignmentMarkUpdate.AST_Remarks));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AssignmentMaster", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }


        //public long InsertUpdateStudentAssignment(AssignmentsListRequest ASM)
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();

        //    string transType = "STUDENT-UPDATE";

        //    arrParams.Add(new SqlParameter("@TransType", transType));
        //    arrParams.Add(new SqlParameter("@ASM_ID", ASM.ASM_ID));
        //    arrParams.Add(new SqlParameter("@AST_StudentId", ASM.AST_StudentId));
        //    arrParams.Add(new SqlParameter("@AST_UploadDoc", ASM.AST_UploadDoc));
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    arrParams.Add(OutPutId);

        //    SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AssignmentMaster", arrParams.ToArray());
        //    long val = Convert.ToInt64(OutPutId.Value);
        //    return val;
        //}


        #endregion
        #region ClasswiseLiveclass
        public long InsertUpdateClasswiseLiveclass(ClassWiseLiveclass_CWLS CWLS)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = CWLS.CWLS_ID > 0 ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@CWLS_ID", CWLS.CWLS_ID));
            arrParams.Add(new SqlParameter("@CWLS_SubGr_ID", CWLS.CWLS_SubGr_ID));
            arrParams.Add(new SqlParameter("@CWLS_Sub_ID", CWLS.CWLS_Sub_ID));
            arrParams.Add(new SqlParameter("@CWLS_School_ID", CWLS.CWLS_School_ID));
            arrParams.Add(new SqlParameter("@CWLS_Class_ID", CWLS.CWLS_Class_ID));
            arrParams.Add(new SqlParameter("@ddlCWLS_Section_ID", CWLS.CWLS_Section_ID));
            arrParams.Add(new SqlParameter("@CWLS_Session_ID", CWLS.CWLS_Session_ID));
            arrParams.Add(new SqlParameter("@CWLS_Title", CWLS.CWLS_Title));
            arrParams.Add(new SqlParameter("@CWLS_Link", CWLS.CWLS_Link));
            arrParams.Add(new SqlParameter("@CWLS_ClassDate", CWLS.CWLS_ClassDate));
            arrParams.Add(new SqlParameter("@CWLS_ClassTime", CWLS.CWLS_ClassTime));
            arrParams.Add(new SqlParameter("@Userid", CWLS.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ClasswiseLiveclass", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region LeaveType
        public long InsertUpdateLeaveType(LeaveType_LT leavetype)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@LeaveTypeId ", leavetype.LeaveTypeId));
            arrParams.Add(new SqlParameter("@LeaveTypeCode", leavetype.LeaveTypeCode));
            arrParams.Add(new SqlParameter("@LeaveTypeName", leavetype.LeaveTypeName));
            arrParams.Add(new SqlParameter("@LeaveCategory", leavetype.LeaveCategory));
            arrParams.Add(new SqlParameter("@CreateBy", leavetype.CreateBy));
            arrParams.Add(new SqlParameter("@SchoolId", leavetype.SchoolId));
            arrParams.Add(new SqlParameter("@SessionId", leavetype.SessionId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_LeaveTypeMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion
        #region StudentDiary
        public long InsertUpdateStudentDiary(StudentDiary_STD STD)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = STD.DiaryId > 0 ? "UPDATE" : "INSERT";

            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@DiaryId", STD.DiaryId));
            arrParams.Add(new SqlParameter("@SchoolId", STD.SchoolId));
            arrParams.Add(new SqlParameter("@ClassId", STD.ClassId));
            arrParams.Add(new SqlParameter("@ddlSectionId", STD.SectionId));
            arrParams.Add(new SqlParameter("@SesssioId", STD.SesssioId));
            arrParams.Add(new SqlParameter("@StudentId", STD.StudentId));
            arrParams.Add(new SqlParameter("@ApDate", STD.ApDate));
            arrParams.Add(new SqlParameter("@DiaryType", STD.DiaryType));
            arrParams.Add(new SqlParameter("@Topic", STD.Topic));
            arrParams.Add(new SqlParameter("@AttachmentPath", STD.AttachmentPath));
            arrParams.Add(new SqlParameter("@TeacherId", STD.TeacherId));
            arrParams.Add(new SqlParameter("@Userid", STD.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentDiary", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }


        #endregion
        #region StudentEarlyLeave
        public long InsertUpdateStudentEarlyLeave(StudentEarlyLeave_ERL ERL)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = ERL.ERL_ID > 0 ? "UPDATE" : "INSERT";

            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@ERL_ID", ERL.ERL_ID));
            arrParams.Add(new SqlParameter("@ERL_SchoolId", ERL.ERL_SchoolId));
            arrParams.Add(new SqlParameter("@ERL_ClassId", ERL.ERL_ClassId));
            arrParams.Add(new SqlParameter("@ddlERL_SectionId", ERL.ERL_SectionId));
            arrParams.Add(new SqlParameter("@ERL_SesssioId", ERL.ERL_SesssioId));
            arrParams.Add(new SqlParameter("@ERL_StudentId", ERL.ERL_StudentId));
            arrParams.Add(new SqlParameter("@ERL_FacultyId", ERL.ERL_FacultyId));
            arrParams.Add(new SqlParameter("@ERL_Reason", ERL.ERL_Reason));
            arrParams.Add(new SqlParameter("@ERL_Note", ERL.ERL_Note));
            arrParams.Add(new SqlParameter("@ERL_PickupDetails", ERL.ERL_PickupDetails));
            arrParams.Add(new SqlParameter("@ERL_Date", ERL.ERL_Date));
            arrParams.Add(new SqlParameter("@Userid", ERL.Userid));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentEarlyLeave", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }


        #endregion
        #region ID CARD
        public IEnumerable<StudentModel> GetStudentsByClass(int schoolId, int sessionId, string classIds, string sectionIds, string studentId, int cardType)
        {
            var students = new List<StudentModel>();

            string connStr = ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("usp_GetStudentsByClass", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SchoolId", schoolId);
                cmd.Parameters.AddWithValue("@SessionId", sessionId);
                // ✅ Correct handling: if null or empty, send DBNull
                cmd.Parameters.AddWithValue("@ClassId", string.IsNullOrEmpty(classIds) ? (object)DBNull.Value : classIds);
                cmd.Parameters.AddWithValue("@SectionId", string.IsNullOrEmpty(sectionIds) ? (object)DBNull.Value : sectionIds);
                cmd.Parameters.AddWithValue("@StudentId", string.IsNullOrEmpty(studentId) ? (object)DBNull.Value : studentId);
                cmd.Parameters.AddWithValue("@CardType", cardType);


                con.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DateTime dob = DateTime.MinValue;
                        if (reader["DOB"] != DBNull.Value)
                            DateTime.TryParse(reader["DOB"].ToString(), out dob);

                        students.Add(new StudentModel
                        {

                            StudentId = reader["StudentId"] != DBNull.Value ? reader["StudentId"].ToString() : "",
                            StudentName = reader["StudentName"] != DBNull.Value ? reader["StudentName"].ToString() : "",
                            ClassName = reader["ClassName"] != DBNull.Value ? reader["ClassName"].ToString() : "",
                            Section = reader["Section"] != DBNull.Value ? reader["Section"].ToString() : "",
                            DOB = dob,
                            BloodGroup = reader["BloodGroup"] != DBNull.Value ? reader["BloodGroup"].ToString() : "",
                            FatherName = reader["FatherName"] != DBNull.Value ? reader["FatherName"].ToString() : "",
                            MotherName = reader["MotherName"] != DBNull.Value ? reader["MotherName"].ToString() : "",
                            ContactNo = reader["ContactNo"] != DBNull.Value ? reader["ContactNo"].ToString() : "",
                            EmergencyNo = reader["EmergencyNo"] != DBNull.Value ? reader["EmergencyNo"].ToString() : "",
                            Address = reader["Address"] != DBNull.Value ? reader["Address"].ToString() : "",
                            PhotoPath = reader["PhotoPath"] != DBNull.Value ? reader["PhotoPath"].ToString() : "",
                            SessionName = reader["SessionName"] != DBNull.Value ? reader["SessionName"].ToString() : "",

                            SchoolName = reader["SchoolName"] != DBNull.Value ? reader["SchoolName"].ToString() : "",
                            ScAddress = reader["ScAddress"] != DBNull.Value ? reader["ScAddress"].ToString() : "",
                            ScPhone1 = reader["ScPhone1"] != DBNull.Value ? reader["ScPhone1"].ToString() : "",
                            ScPhone2 = reader["ScPhone2"] != DBNull.Value ? reader["ScPhone2"].ToString() : "",
                            ScEmail = reader["ScEmail"] != DBNull.Value ? reader["ScEmail"].ToString() : "",
                            ScWebsite = reader["ScWebsite"] != DBNull.Value ? reader["ScWebsite"].ToString() : "",
                            ScPhotoPath = reader["ScPhotoPath"] != DBNull.Value ? reader["ScPhotoPath"].ToString() : "",
                            CardTypeName = reader["CardTypeName"] != DBNull.Value ? reader["CardTypeName"].ToString() : "",
                            AffNo = reader["AffNo"] != DBNull.Value ? reader["AffNo"].ToString() : "",
                            SchoolCode = reader["SchoolCode"] != DBNull.Value ? reader["SchoolCode"].ToString() : "",
                            PrincipalSignature = reader["PrincipalSignature"] != DBNull.Value ? reader["PrincipalSignature"].ToString() : ""



                        });
                    }
                }
            }

            return students;
        }


        public StudentManual GetStudentManual(string studentId)
        {
            StudentManual student = null;

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetStudentManual", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            student = new StudentManual
                            {
                                SD_Id = Convert.ToInt32(dr["SD_Id"]),
                                SD_AppliactionNo = dr["SD_AppliactionNo"].ToString(),
                                SD_StudentId = dr["SD_StudentId"].ToString(),
                                SD_Password = dr["SD_Password"].ToString(),
                                SD_AppliactionDate = Convert.ToDateTime(dr["SD_AppliactionDate"]),
                                ClassName = dr["CM_CLASSNAME"].ToString(),
                                SectionName = dr["SECM_SECTIONNAME"].ToString(),
                                SD_StudentName = dr["SD_StudentName"].ToString(),
                                SD_FatherName = dr["SD_FatherName"].ToString(),
                                SD_PresentAddress = dr["SD_PresentAddress"].ToString(),
                                SD_PermanentAddress = dr["SD_PermanentAddress"].ToString(),
                                SD_ContactNo1 = dr["SD_ContactNo1"].ToString(),
                                SD_ContactNo2 = dr["SD_ContactNo2"].ToString(),
                                SD_EmailId = dr["SD_EmailId"].ToString(),
                                SessionName = dr["SM_SESSIONNAME"].ToString(),
                                SchoolName = dr["SCM_SCHOOLNAME"].ToString(),
                                SchoolFullName = dr["SCM_FULLNAME"].ToString(),
                                SchoolAddress1 = dr["SCM_SCHOOLADDRESS1"].ToString(),
                                SchoolAddress2 = dr["SCM_SCHOOLADDRESS2"].ToString(),
                                SchoolOfficeAddress = dr["SCM_OFFICEADDRESS"].ToString(),
                                SchoolContactPerson = dr["SCM_CONTACTPERSON"].ToString(),
                                SchoolPhoneNo1 = dr["SCM_PHONENO1"].ToString(),
                                SchoolPhoneNo2 = dr["SCM_PHONENO2"].ToString(),
                                SchoolSecretaryName = dr["SCM_SECRETARYNAME"].ToString(),
                                SchoolEmailId = dr["SCM_EMAILID"].ToString(),
                                SchoolWebsite = dr["SCM_WEBSITE"].ToString(),
                                SchoolLogo = dr["SCM_SCHOOLLOGO"].ToString(),
                                SchoolAffNo = dr["SCM_AFFNO"].ToString()
                            };
                        }
                    }
                }
            }

            return student;
        }
        #endregion
        #region ExamGroupMaster
        public long InsertUpdateExamGroup(ExamGroupMaster_EGM EGM, List<long> classIds)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            string transType = EGM.EGM_Id > 0 ? "UPDATE" : "INSERT";

            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@EGM_Id", EGM.EGM_Id));
            arrParams.Add(new SqlParameter("@EGM_TermId", EGM.EGM_TermId));
            arrParams.Add(new SqlParameter("@EGM_Name", EGM.EGM_Name));

            // Table-Valued Parameter
            DataTable dtClassIds = new DataTable();
            dtClassIds.Columns.Add("ClassId", typeof(long));
            foreach (var id in classIds)
                dtClassIds.Rows.Add(id);

            var tvpParam = new SqlParameter("@EGM_ClassIds", SqlDbType.Structured)
            {
                TypeName = "dbo.ClassIdList",
                Value = dtClassIds
            };
            arrParams.Add(tvpParam);

            arrParams.Add(new SqlParameter("@EGM_SchoolId", EGM.EGM_SchoolId));
            arrParams.Add(new SqlParameter("@EGM_SessionId", EGM.EGM_SessionId));
            arrParams.Add(new SqlParameter("@IsFinal", EGM.IsFinal));
            arrParams.Add(new SqlParameter("@Userid", EGM.Userid));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_ExamGroup", arrParams.ToArray());
            long val = Convert.ToInt64(OutPutId.Value);
            return val;
        }

        public long DeleteExamGroup(long EGM_Id)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@EGM_Id", EGM_Id));
            arrParams.Add(new SqlParameter("@TransType", "DELETE"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),          // <-- inherited from MasterRepository
                CommandType.StoredProcedure,
                "SP_ExamGroup",
                arrParams.ToArray()
            );

            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        #endregion

        #region MiscellaneousHead
        public long InsertUpdateMiscellaneousHead(MiscellaneousHeadMaster_MISC misc)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@MISC_Id", misc.MISC_Id));
            arrParams.Add(new SqlParameter("@MISC_SchoolId", misc.MISC_SchoolId));
            arrParams.Add(new SqlParameter("@MISC_SessionId", misc.MISC_SessionId));
            arrParams.Add(new SqlParameter("@MISC_FeeHeadCode", misc.MISC_FeeHeadCode));
            arrParams.Add(new SqlParameter("@MISC_FeeHeadName", misc.MISC_FeeHeadName));
            arrParams.Add(new SqlParameter("@MISC_Amount", misc.MISC_Amount));
            arrParams.Add(new SqlParameter("@MISC_IsActive", misc.MISC_IsActive));
            arrParams.Add(new SqlParameter("@MISC_CreatedBy", misc.MISC_CreatedBy));
            arrParams.Add(new SqlParameter("@MISC_ModifiedBy", misc.MISC_ModifiedBy));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_MiscellaneousHeadMaster", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }

        #endregion


        #region Online Admission STUDENT PORTAL DASHBOARD LIST
          public List<StudentApplication_AP> GetApplicationList(string candidateId)
        {
            List<StudentApplication_AP> ListObject = new List<StudentApplication_AP>();
            StudentApplication_AP DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@CandidateId", candidateId));

            SqlDataReader reader = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetStudentApplicationByCandidateId", arrParams.ToArray());
            if (reader != null)
            {
                while (reader.Read())
                {
                    DataObject = new StudentApplication_AP();

                    DataObject.SA_ID = Convert.ToInt64(reader["SA_ID"]);
                    DataObject.SA_ST_CANDIDATEID = reader["SA_ST_CANDIDATEID"].ToString();
                    DataObject.ST_REGDATE = Convert.ToDateTime(reader["ST_REGDATE"]);
                    DataObject.ST_NAME = reader["ST_NAME"].ToString();
                    DataObject.ST_PG_DETAILS = reader["ST_PG_DETAILS"].ToString();
                    DataObject.ST_DOB = Convert.ToDateTime(reader["ST_DOB"]);
                    DataObject.SCM_SCHOOLNAME = reader["SCM_SCHOOLNAME"].ToString();
                    DataObject.SA_ST_CLASS = reader["SA_ST_CLASS"].ToString();
                    DataObject.SA_ST_SESSION = reader["SA_ST_SESSION"].ToString();
                    DataObject.AP_Nationality = reader["AP_Nationality"].ToString();
                    DataObject.AP_Religion = reader["AP_Religion"].ToString();
                    DataObject.AP_Caste = reader["AP_Caste"].ToString();
                    DataObject.AP_BloodGroup = reader["AP_BloodGroup"].ToString();
                    DataObject.AP_Gender = reader["AP_Gender"].ToString();
                    DataObject.AP_AadhaarNo = reader["AP_AadhaarNo"].ToString();
                    DataObject.ST_MOBILENO = reader["ST_MOBILENO"].ToString();
                    DataObject.ST_EMAIL = reader["ST_EMAIL"].ToString();
                    DataObject.AP_PerAddress = reader["AP_PerAddress"].ToString();
                    DataObject.AP_PreAddress = reader["AP_PreAddress"].ToString();
                    DataObject.AP_State = reader["AP_State"].ToString();
                    DataObject.district = reader["district"].ToString();
                    DataObject.AP_PoliceStation = reader["AP_PoliceStation"].ToString();
                    DataObject.AP_PO = reader["AP_PO"].ToString();
                    DataObject.AP_Pin = reader["AP_Pin"].ToString();
                    DataObject.AP_LastSchoolName = reader["AP_LastSchoolName"].ToString();
                    DataObject.AP_TCNO = reader["AP_TCNO"].ToString();
                    DataObject.AP_TCDate = reader["AP_TCDate"] != DBNull.Value ? Convert.ToDateTime(reader["AP_TCDate"]) : (DateTime?)null;
                    DataObject.AP_TCTYPE = reader["AP_TCTYPE"].ToString();
                    DataObject.AP_AdmissionDate = reader["AP_AdmissionDate"] != DBNull.Value ? Convert.ToDateTime(reader["AP_AdmissionDate"]) : (DateTime?)null;
                    DataObject.AP_BirthCertificates = reader["AP_BirthCertificates"].ToString();
                    DataObject.AP_IdProofs = reader["AP_IdProofs"].ToString();
                    DataObject.AP_Marksheets = reader["AP_Marksheets"].ToString();
                    DataObject.App_Status = reader["App_Status"].ToString();
                  //  DataObject.OML_VALID_DATE = Convert.ToDateTime(reader["OML_VALID_DATE"]);

                    DataObject.OML_VALID_DATE = reader["OML_VALID_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["OML_VALID_DATE"]);
                    //DataObject.OML_VALID_DATE = reader["OML_VALID_DATE"] == DBNull.Value? (DateTime?)null: Convert.ToDateTime(reader["OML_VALID_DATE"]);
                    DataObject.Validity_Status = reader["Validity_Status"].ToString();
                    ListObject.Add(DataObject);
                }
                reader.Close();
            }
            reader.Dispose();
            return ListObject;
        }
        
        #endregion
        #region MeritList
        //public int ApproveApplications(OnlineMeritApproval_STD std, out string outputMsg)
        //{
        //    // 1. Build JSON for @approvallistJSON that matches OPENJSON schema
        //    var approvallist = std.Applications.Select(x => new
        //    {
        //        app_no = x.SaId,         // maps to OML_APPNO
        //        candidate_id = x.CandidateId,  // maps to OML_CANDIDATEID
        //        session = x.Session,      // maps to OML_SESSION
        //        school_id = std.SchoolId,   // maps to OML_SCHOOLID
        //        @class = x.Class         // maps to OML_CLASS
        //    }).ToList();

        //    string approvallistJSON = JsonConvert.SerializeObject(approvallist);

        //    // 2. Prepare parameters
        //    List<SqlParameter> arrParams = new List<SqlParameter>();

        //    arrParams.Add(new SqlParameter("@approvallistJSON", SqlDbType.NVarChar)
        //    {
        //        Value = (object)approvallistJSON ?? DBNull.Value
        //    });

        //    arrParams.Add(new SqlParameter("@userId", SqlDbType.VarChar, 200)
        //    {
        //        Value = (object)std.Userid ?? DBNull.Value
        //    });
        //    arrParams.Add(new SqlParameter("@validityDate", SqlDbType.Date)
        //    {
        //        Value = string.IsNullOrEmpty(std.ValidityDate)
        //                    ? DBNull.Value
        //                    : (object)Convert.ToDateTime(std.ValidityDate)
        //    });

        //    SqlParameter outputCodeParam = new SqlParameter("@outputCode", SqlDbType.Int)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    arrParams.Add(outputCodeParam);

        //    SqlParameter outputMsgParam = new SqlParameter("@outputMsg", SqlDbType.NVarChar, 4000)
        //    {
        //        Direction = ParameterDirection.Output
        //    };
        //    arrParams.Add(outputMsgParam);

        //    // 3. Execute SP
        //    SqlHelper.ExecuteNonQuery(
        //        GetConnectionString(),
        //        CommandType.StoredProcedure,
        //        "SP_SaveOnlineMeritListMaster",
        //        arrParams.ToArray()
        //    );

        //    // 4. Read outputs
        //    int outputCode = 0;

        //    if (outputCodeParam.Value != DBNull.Value && outputCodeParam.Value != null)
        //    {
        //        outputCode = Convert.ToInt32(outputCodeParam.Value);
        //    }

        //    outputMsg = outputMsgParam.Value == DBNull.Value
        //        ? string.Empty
        //        : Convert.ToString(outputMsgParam.Value);

        //    return outputCode;
        //}
          public int ApproveApplications(OnlineMeritApproval_STD std, out string outputMsg)
          {
              // 1. Build XML for @approvallistXML
              XDocument approvallistXML = new XDocument(
                  new XElement("root",
                      std.Applications.Select(x => new XElement("row",
                          new XElement("app_no", x.SaId),               // maps to OML_APPNO
                          new XElement("candidate_id", x.CandidateId),  // maps to OML_CANDIDATEID
                          new XElement("session", x.Session),           // maps to OML_SESSION
                          new XElement("school_id", std.SchoolId),      // maps to OML_SCHOOLID
                          new XElement("class", x.Class)                // maps to OML_CLASS
                      ))
                  )
              );

              string approvallistXMLString = approvallistXML.ToString();

              // 2. Prepare parameters
              List<SqlParameter> arrParams = new List<SqlParameter>();

              arrParams.Add(new SqlParameter("@approvallistJSON", SqlDbType.Xml)
              {
                  Value = (object)approvallistXMLString ?? DBNull.Value
              });

              arrParams.Add(new SqlParameter("@userId", SqlDbType.VarChar, 200)
              {
                  Value = (object)std.Userid ?? DBNull.Value
              });

              arrParams.Add(new SqlParameter("@validityDate", SqlDbType.Date)
              {
                  Value = string.IsNullOrEmpty(std.ValidityDate)
                              ? DBNull.Value
                              : (object)Convert.ToDateTime(std.ValidityDate)
              });

              SqlParameter outputCodeParam = new SqlParameter("@outputCode", SqlDbType.Int)
              {
                  Direction = ParameterDirection.Output
              };
              arrParams.Add(outputCodeParam);

              SqlParameter outputMsgParam = new SqlParameter("@outputMsg", SqlDbType.NVarChar, 4000)
              {
                  Direction = ParameterDirection.Output
              };
              arrParams.Add(outputMsgParam);

              // 3. Execute SP
              SqlHelper.ExecuteNonQuery(
                  GetConnectionString(),
                  CommandType.StoredProcedure,
                  "SP_SaveOnlineMeritListMaster",
                  arrParams.ToArray()
              );

              // 4. Read outputs
              int outputCode = 0;

              if (outputCodeParam.Value != DBNull.Value && outputCodeParam.Value != null)
              {
                  outputCode = Convert.ToInt32(outputCodeParam.Value);
              }

              outputMsg = outputMsgParam.Value == DBNull.Value
                  ? string.Empty
                  : Convert.ToString(outputMsgParam.Value);

              return outputCode;
          }

        public List<StudentApplication_AP> GetApprovalList(long SchoolId, string className, string isApproved, string isAdmitted)
        {
            List<StudentApplication_AP> ListObject = new List<StudentApplication_AP>();
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@AP_School", SchoolId));
            arrParams.Add(new SqlParameter("@Class", (object)className ?? DBNull.Value));
            arrParams.Add(new SqlParameter("@IsApproved", (object)isApproved ?? DBNull.Value));
            arrParams.Add(new SqlParameter("@IsAdmitted", (object)isAdmitted ?? DBNull.Value));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            using (SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_StudentApprovalOnlineListCMS", arrParams.ToArray()))
            {
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        var DataObject = new StudentApplication_AP();

                        DataObject.SA_ID = rdr["SA_ID"] != DBNull.Value ? Convert.ToInt64(rdr["SA_ID"]) : 0;
                        DataObject.SA_ST_SESSION = rdr["SA_ST_SESSION"] != DBNull.Value ? rdr["SA_ST_SESSION"].ToString() : string.Empty;
                        DataObject.SA_ST_CANDIDATEID = rdr["SA_ST_CANDIDATEID"] != DBNull.Value ? rdr["SA_ST_CANDIDATEID"].ToString() : string.Empty;
                        DataObject.ST_NAME = rdr["ST_NAME"] != DBNull.Value ? rdr["ST_NAME"].ToString() : string.Empty;
                        //DataObject.ST_PG_DETAILS = rdr["ST_PG_DETAILS"] != DBNull.Value ? rdr["ST_PG_DETAILS"].ToString() : string.Empty;
                        DataObject.ST_DOB = rdr["ST_DOB"] != DBNull.Value ? Convert.ToDateTime(rdr["ST_DOB"]) : DateTime.MinValue;
                        DataObject.SA_ST_CLASS = rdr["SA_ST_CLASS"] != DBNull.Value ? rdr["SA_ST_CLASS"].ToString() : string.Empty;
                        DataObject.ST_MOBILENO = rdr["ST_MOBILENO"] != DBNull.Value ? rdr["ST_MOBILENO"].ToString() : string.Empty;
                        DataObject.ST_EMAIL = rdr["ST_EMAIL"] != DBNull.Value ? rdr["ST_EMAIL"].ToString() : string.Empty;
                        DataObject.App_Status = rdr["App_Status"] != DBNull.Value ? rdr["App_Status"].ToString() : string.Empty;
                        DataObject.APPLICATION_DATE = rdr["APPLICATION_DATE"] != DBNull.Value ? Convert.ToDateTime(rdr["APPLICATION_DATE"]) : DateTime.MinValue;
                        DataObject.Admission_Status = rdr["Admission_Status"] != DBNull.Value ? rdr["Admission_Status"].ToString() : string.Empty;

                        ListObject.Add(DataObject);
                    }
                    rdr.Close();
                }
            }

            return ListObject;
        }
        #endregion
        #region LessonPaln

        public long InsertUpdateLessonPlan(LessonPlan_LP lessonplan)
        {
            long lastId = 0;

            // Handle Class–Section map (multi-select)
            var classSectionPairs = (lessonplan.LP_ClassSectionMap ?? "")
                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

            // If no class-section pairs, insert once with nulls
            if (!classSectionPairs.Any())
            {
                lastId = InsertUpdateLessonPlanSingle(lessonplan, null, null);
                return lastId;
            }

            // Insert for each class-section pair
            foreach (var pair in classSectionPairs)
            {
                var ids = pair.Split('_'); // Format: ClassId_SectionId
                string classId = ids.Length > 0 ? ids[0].Trim() : null;
                string sectionId = ids.Length > 1 ? ids[1].Trim() : null;

                lastId = InsertUpdateLessonPlanSingle(lessonplan, classId, sectionId);

                if (lastId == -11) // Duplicate found
                    return -11;
            }

            return lastId;
        }


        private long InsertUpdateLessonPlanSingle(LessonPlan_LP lessonplan, string classId, string sectionId)
        {
            List<SqlParameter> parameters = new List<SqlParameter>
    {
        new SqlParameter("@LP_Id", lessonplan.LP_Id.HasValue ? (object)lessonplan.LP_Id.Value : DBNull.Value),
        new SqlParameter("@LP_FacultyID", lessonplan.LP_FacultyID.HasValue ? (object)lessonplan.LP_FacultyID.Value : DBNull.Value),
        new SqlParameter("@LP_FromDate", lessonplan.LP_FromDate.HasValue ? (object)lessonplan.LP_FromDate.Value : DBNull.Value),
        new SqlParameter("@LP_ToDate", lessonplan.LP_ToDate.HasValue ? (object)lessonplan.LP_ToDate.Value : DBNull.Value),
        new SqlParameter("@LP_ClassId", string.IsNullOrEmpty(lessonplan.LP_ClassId) ? (object)DBNull.Value : lessonplan.LP_ClassId),
        new SqlParameter("@LP_SectionId", string.IsNullOrEmpty(lessonplan.LP_SectionId) ? (object)DBNull.Value : lessonplan.LP_SectionId),
        new SqlParameter("@LP_TeachingAidId", string.IsNullOrEmpty(lessonplan.LP_TeachingAidId) ? (object)DBNull.Value : lessonplan.LP_TeachingAidId),
        new SqlParameter("@LP_SubjectId", lessonplan.LP_SubjectId.HasValue ? (object)lessonplan.LP_SubjectId.Value : DBNull.Value),
        new SqlParameter("@LP_NumberOfPeriods", !string.IsNullOrEmpty(lessonplan.LP_NumberOfPeriods) ? (object)lessonplan.LP_NumberOfPeriods : DBNull.Value),

        new SqlParameter("@LP_LessonContent", !string.IsNullOrEmpty(lessonplan.LP_LessonContent) ? (object)lessonplan.LP_LessonContent : DBNull.Value),
        new SqlParameter("@LP_LearningObjectives", !string.IsNullOrEmpty(lessonplan.LP_LearningObjectives) ? (object)lessonplan.LP_LearningObjectives : DBNull.Value),
        new SqlParameter("@LP_LearningOutcomes", !string.IsNullOrEmpty(lessonplan.LP_LearningOutcomes) ? (object)lessonplan.LP_LearningOutcomes : DBNull.Value),
        new SqlParameter("@LP_TypeOfOutcome", !string.IsNullOrEmpty(lessonplan.LP_TypeOfOutcome) ? (object)lessonplan.LP_TypeOfOutcome : DBNull.Value),
        new SqlParameter("@LP_Outcome", !string.IsNullOrEmpty(lessonplan.LP_Outcome) ? (object)lessonplan.LP_Outcome : DBNull.Value),
        new SqlParameter("@LP_CreatedBy", lessonplan.LP_CreatedBy.HasValue ? (object)lessonplan.LP_CreatedBy.Value : DBNull.Value),
        new SqlParameter("@LP_CreatedDate", lessonplan.LP_CreatedDate.HasValue ? (object)lessonplan.LP_CreatedDate.Value : (object)DateTime.Now),
        new SqlParameter("@LP_EditedBy", lessonplan.LP_EditedBy.HasValue ? (object)lessonplan.LP_EditedBy.Value : DBNull.Value),
        new SqlParameter("@LP_EditedDate", lessonplan.LP_EditedDate.HasValue ? (object)lessonplan.LP_EditedDate.Value : DBNull.Value),
        new SqlParameter("@LP_SchoolId", lessonplan.LP_SchoolId.HasValue ? (object)lessonplan.LP_SchoolId.Value : DBNull.Value),
        new SqlParameter("@LP_SessionId", lessonplan.LP_SessionId.HasValue ? (object)lessonplan.LP_SessionId.Value : DBNull.Value)
    };

            var outParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };
            parameters.Add(outParam);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_LessonPlanMaster",
                parameters.ToArray()
            );

            return Convert.ToInt64(outParam.Value);
        }


        public List<LessonPlanList_VM> GetLessonPlanList(long? facultyId, long? subjectId,
                                                     DateTime? fromDate, DateTime? toDate,
                                                     long? schoolId, long? sessionId)
        {
            List<LessonPlanList_VM> result = new List<LessonPlanList_VM>();

            using (SqlConnection con = new SqlConnection(GetConnectionString()))  // FIXED
            using (SqlCommand cmd = new SqlCommand("SP_LessonPlan_List", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FacultyId", (object)facultyId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SubjectId", (object)subjectId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@FromDate", (object)fromDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ToDate", (object)toDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SchoolId", (object)schoolId ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SessionId", (object)sessionId ?? DBNull.Value);

                con.Open();   // REQUIRED

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        result.Add(new LessonPlanList_VM()
                        {
                            LP_Id = Convert.ToInt64(dr["LP_Id"]),
                            LP_FromDate = Convert.ToDateTime(dr["LP_FromDate"]),
                            LP_ToDate = Convert.ToDateTime(dr["LP_ToDate"]),
                            FP_Name = dr["FP_Name"].ToString(),
                            ClassNames = dr["ClassNames"].ToString(),
                            SectionNames = dr["SectionNames"].ToString(),
                            TeachingAidNames = dr["TeachingAidNames"].ToString(),
                            SBM_SubjectName = dr["SBM_SubjectName"].ToString(),
                            LP_NumberOfPeriods = Convert.ToInt32(dr["LP_NumberOfPeriods"]),
                            LP_LessonContent = dr["LP_LessonContent"].ToString(),
                            LP_LearningObjectives = dr["LP_LearningObjectives"].ToString(),
                            LP_LearningOutcomes = dr["LP_LearningOutcomes"].ToString(),
                            LP_TypeOfOutcome = dr["LP_TypeOfOutcome"].ToString(),
                            LP_Outcome = dr["LP_Outcome"].ToString()
                        });
                    }
                }
            }

            return result;
        }


        #endregion
        #region ClassSecFaculty Master
        public long InsertUpdateClassSecFaculty(ClassSecFaculty_CSF csf)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>
    {
        new SqlParameter("@TransactionType", "InsertOrUpdate"),
        new SqlParameter("@CSF_Id", csf.CSF_Id),
        new SqlParameter("@CSF_ClassId", csf.CSF_ClassId),
        new SqlParameter("@CSF_SectionId", csf.CSF_SectionId),
        new SqlParameter("@CSF_FacultyId", csf.CSF_FacultyId),
        new SqlParameter("@CSF_SchoolId", csf.CSF_SchoolId),
        new SqlParameter("@CSF_SessionId", csf.CSF_SessionId),
        new SqlParameter("@CSF_CreateBy", csf.CSF_CreateBy)
    };

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt) { Direction = ParameterDirection.Output };
            arrParams.Add(OutPutId);

            SqlHelper.ExecuteNonQuery(
                GetConnectionString(),
                CommandType.StoredProcedure,
                "SP_ClassSecFacultyMaster",
                arrParams.ToArray()
            );

            return Convert.ToInt64(OutPutId.Value);
        }
        public List<ClassSecFaculty_CSF> GetClassSecFaculty(long? schoolId, long? sessionId)
        {
            List<ClassSecFaculty_CSF> list = new List<ClassSecFaculty_CSF>();

            using (SqlConnection con = new SqlConnection(GetConnectionString()))
            {
                using (SqlCommand cmd = new SqlCommand("SP_ClassSecFacultyMaster", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TransactionType", "SELECT");
                    cmd.Parameters.AddWithValue("@CSF_SchoolId", schoolId);
                    cmd.Parameters.AddWithValue("@CSF_SessionId", sessionId);

                    //  OUTPUT parameter to satisfy SP
                    SqlParameter outParam = new SqlParameter("@OutPutId", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outParam);

                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new ClassSecFaculty_CSF
                            {
                                CSF_Id = dr["CSF_Id"] != DBNull.Value ? (long?)dr["CSF_Id"] : null,
                                CSF_ClassId = dr["CSF_ClassId"] != DBNull.Value ? (long?)dr["CSF_ClassId"] : null,
                                CSF_SectionId = dr["CSF_SectionId"] != DBNull.Value ? (long?)dr["CSF_SectionId"] : null,
                                CSF_FacultyId = dr["CSF_FacultyId"] != DBNull.Value ? (long?)dr["CSF_FacultyId"] : null,
                                CSF_SchoolId = dr["CSF_SchoolId"] != DBNull.Value ? (long?)dr["CSF_SchoolId"] : null,
                                CSF_SessionId = dr["CSF_SessionId"] != DBNull.Value ? (long?)dr["CSF_SessionId"] : null,
                                CSF_CreateBy = dr["CSF_CreateBy"] != DBNull.Value ? (int?)dr["CSF_CreateBy"] : null,
                                CSF_CreateDate = dr["CSF_CreateDate"] != DBNull.Value ? (DateTime?)dr["CSF_CreateDate"] : null,
                                CSF_EditBy = dr["CSF_EditBy"] != DBNull.Value ? (int?)dr["CSF_EditBy"] : null,
                                CSF_EditDate = dr["CSF_EditDate"] != DBNull.Value ? (DateTime?)dr["CSF_EditDate"] : null
                            });
                        }
                    }
                }
            }

            return list;
        }
        #endregion

        #region PettyCash
       

        public AccountMaster GetAccountMasterDetails(string TransType, int? Id)
        {
            AccountMaster AccountMaster = new AccountMaster();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster.AM_FYId = Convert.ToInt32(rdr["AM_FYId"]);
                    AccountMaster.AM_GroupId = Convert.ToInt32(rdr["AM_GroupId"]);
                    AccountMaster.AM_CompanyID = Convert.ToInt32(rdr["AM_CompanyID"]);
                    AccountMaster.SessionName = rdr["SessionName"].ToString();
                    AccountMaster.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);


                    AccountMaster.AM_AccountOpId = Convert.ToInt32(rdr["AM_AccountOpId"]);



                    AccountMaster.GM_GroupDescription = rdr["GM_GroupDescription"].ToString();

                    AccountMaster.GM_GroupCode = rdr["GM_GroupCode"].ToString();

                    AccountMaster.AM_AccDescription = rdr["AM_AccDescription"].ToString();

                    AccountMaster.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    AccountMaster.AM_LongName = rdr["AM_LongName"].ToString();
                    AccountMaster.AM_OPeningType = rdr["AM_OPeningType"].ToString();
                    AccountMaster.AM_ClosingType = rdr["AM_ClosingType"].ToString();
                    AccountMaster.AM_SuppressPayee = rdr["AM_SuppressPayee"].ToString();
                    AccountMaster.ISSubAc = rdr["ISSubAc"].ToString();


                    AccountMaster.AM_TotalDebit = Convert.ToDecimal(rdr["AM_TotalDebit"]);
                    AccountMaster.AM_TotalCredit = Convert.ToDecimal(rdr["AM_TotalCredit"]);
                    AccountMaster.AM_ClosingBalance = Convert.ToDecimal(rdr["AM_ClosingBalance"]);
                    AccountMaster.AM_OpeningBalance = Convert.ToDecimal(rdr["AM_OpeningBalance"]);
                    AccountMaster.AM_IsFund = Convert.ToBoolean(rdr["AM_IsFund"]);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return AccountMaster;
        }

        public List<AccountGroup> getAccountGroupList(string TransType, long? PId, long? SId, long? XId)
        {
            List<AccountGroup> DDLObjList = new List<AccountGroup>();
            AccountGroup DDLObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@SId", SId));
            arrParams.Add(new SqlParameter("@XId", XId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalSelect", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DDLObj = new AccountGroup();
                    DDLObj.GM_GroupId = Convert.ToInt32(rdr["Value"]);
                    DDLObj.GM_GroupDescription = Convert.ToString(rdr["Text"]);
                    DDLObjList.Add(DDLObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DDLObjList;
        }

        public int? InsertUpdateAccountMaster(string TransType, AccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", AccountMasterDetails.AM_AccountId));
            arrParams.Add(new SqlParameter("@AM_AccountCode", AccountMasterDetails.AM_AccountCode));
            arrParams.Add(new SqlParameter("@AM_LongName", AccountMasterDetails.AM_LongName));
            arrParams.Add(new SqlParameter("@AM_AccDescription", AccountMasterDetails.AM_AccDescription));
            arrParams.Add(new SqlParameter("@AM_SuppressPayee", AccountMasterDetails.AM_SuppressPayee));
            arrParams.Add(new SqlParameter("@ISSubAc", AccountMasterDetails.ISSubAc));
            arrParams.Add(new SqlParameter("@AM_GroupId", AccountMasterDetails.AM_GroupId));
            arrParams.Add(new SqlParameter("@AM_CompanyID", AccountMasterDetails.AM_CompanyID));
            arrParams.Add(new SqlParameter("@AM_FYId", AccountMasterDetails.AM_FYId));
            arrParams.Add(new SqlParameter("@AM_IsFund", AccountMasterDetails.AM_IsFund));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<AccountMaster> AccountMasterList(string TransType, AccountMaster AccountMaster)
        {
            List<AccountMaster> CFSjList = new List<AccountMaster>();
            AccountMaster CFSObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_GroupId", AccountMaster.AM_GroupId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    CFSObj = new AccountMaster();

                    CFSObj.AM_FYId = Convert.ToInt32(rdr["AM_FYId"]);
                    CFSObj.AM_GroupId = Convert.ToInt32(rdr["AM_GroupId"]);
                    CFSObj.AM_CompanyID = Convert.ToInt32(rdr["AM_CompanyID"]);
                    CFSObj.SessionName = rdr["SessionName"].ToString();
                    CFSObj.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);
                    CFSObj.GM_GroupDescription = rdr["GM_GroupDescription"].ToString();

                    CFSObj.GM_GroupCode = rdr["GM_GroupCode"].ToString();

                    CFSObj.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    CFSObj.AM_LongName = rdr["AM_LongName"].ToString();
                    CFSObj.AM_OPeningType = rdr["AM_OPeningType"].ToString();
                    CFSObj.AM_ClosingType = rdr["AM_ClosingType"].ToString();
                    CFSObj.ISSubAc = rdr["ISSubAc"].ToString();
                    CFSObj.AM_AccDescription = rdr["AM_AccDescription"].ToString();

                    CFSObj.AM_TotalDebit = Convert.ToDecimal(rdr["AM_TotalDebit"]);
                    CFSObj.AM_TotalCredit = Convert.ToDecimal(rdr["AM_TotalCredit"]);
                    CFSObj.AM_ClosingBalance = Convert.ToDecimal(rdr["AM_ClosingBalance"]);
                    CFSObj.AM_OpeningBalance = Convert.ToDecimal(rdr["AM_OpeningBalance"]);
                    CFSObj.AM_IsFund = Convert.ToBoolean(rdr["AM_IsFund"]);

                    CFSjList.Add(CFSObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }

        public List<AccountMaster> GetAccountMasterDropdownList(string TransType, long? PId, long? SId, long? XId)
        {
            List<AccountMaster> DDLObjList = new List<AccountMaster>();
            AccountMaster DDLObj = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));
            arrParams.Add(new SqlParameter("@PId", PId));
            arrParams.Add(new SqlParameter("@SId", SId));
            arrParams.Add(new SqlParameter("@XId", XId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GlobalSelect", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DDLObj = new AccountMaster();
                    DDLObj.AM_AccountId = Convert.ToInt32(rdr["Value"]);
                    DDLObj.AM_AccDescription = Convert.ToString(rdr["Text"]);
                    DDLObj.AM_AccountCode = Convert.ToString(rdr["Code"]);
                    DDLObjList.Add(DDLObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return DDLObjList;
        }

        public SubAccountMaster GetSubAccountMasterDetails(string TransType, int? Id)
        {
            SubAccountMaster AccountMaster = new SubAccountMaster();
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@SAM_SubId", Id));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster.SAM_SubId = Convert.ToInt32(rdr["SAM_SubId"]);
                    AccountMaster.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);

                    AccountMaster.SAM_CompanyID = Convert.ToInt32(rdr["SAM_CompanyID"]);
                    AccountMaster.SAM_FYID = Convert.ToInt32(rdr["SAM_FYID"]);
                    AccountMaster.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    AccountMaster.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();
                    AccountMaster.SAM_SubLongDesc = rdr["SAM_SubLongDesc"].ToString();
                    AccountMaster.SAM_Address1 = rdr["SAM_Address1"].ToString();
                    AccountMaster.SAM_Address2 = rdr["SAM_Address2"].ToString();
                    AccountMaster.SAM_Address3 = rdr["SAM_Address3"].ToString();
                    AccountMaster.SAM_Address4 = rdr["SAM_Address4"].ToString();
                    AccountMaster.SAM_OPhone = rdr["SAM_OPhone"].ToString();
                    AccountMaster.SAM_RPhone = rdr["SAM_RPhone"].ToString();
                    AccountMaster.SAM_FAX = rdr["SAM_FAX"].ToString();
                    AccountMaster.SAM_CellNo = rdr["SAM_CellNo"].ToString();
                    AccountMaster.SAM_Email = rdr["SAM_Email"].ToString();
                    AccountMaster.SAM_Website = rdr["SAM_Website"].ToString();
                    AccountMaster.SAM_PAN = rdr["SAM_PAN"].ToString();
                    AccountMaster.SAM_CST = rdr["SAM_CST"].ToString();
                    AccountMaster.SAM_SST = rdr["SAM_SST"].ToString();
                    AccountMaster.SAM_IsFund = Convert.ToBoolean(rdr["SAM_IsFund"]);

                }
                rdr.Close();
            }
            rdr.Dispose();
            return AccountMaster;
        }

        public int? InsertUpdateSubAccountMaster(string TransType, SubAccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@SAM_CompanyID", AccountMasterDetails.SAM_CompanyID));
            arrParams.Add(new SqlParameter("@SAM_FYID", AccountMasterDetails.SAM_FYID));
            arrParams.Add(new SqlParameter("@SAM_SubId", AccountMasterDetails.SAM_SubId));
            arrParams.Add(new SqlParameter("@SAM_AccountId", AccountMasterDetails.SAM_AccountId));

            arrParams.Add(new SqlParameter("@SAM_SubCode", AccountMasterDetails.SAM_SubCode));
            arrParams.Add(new SqlParameter("@SAM_SubLongDesc", AccountMasterDetails.SAM_SubLongDesc));
            arrParams.Add(new SqlParameter("@SAM_SubDescription", AccountMasterDetails.SAM_SubDescription));
            arrParams.Add(new SqlParameter("@SAM_Address1", AccountMasterDetails.SAM_Address1));
            arrParams.Add(new SqlParameter("@SAM_Address2", AccountMasterDetails.SAM_Address2));
            arrParams.Add(new SqlParameter("@SAM_Address3", AccountMasterDetails.SAM_Address3));
            arrParams.Add(new SqlParameter("@SAM_Address4", AccountMasterDetails.SAM_Address4));
            arrParams.Add(new SqlParameter("@SAM_OPhone", AccountMasterDetails.SAM_OPhone));
            arrParams.Add(new SqlParameter("@SAM_FAX", AccountMasterDetails.SAM_FAX));
            arrParams.Add(new SqlParameter("@SAM_Email", AccountMasterDetails.SAM_Email));
            arrParams.Add(new SqlParameter("@SAM_Website", AccountMasterDetails.SAM_Website));
            arrParams.Add(new SqlParameter("@SAM_PAN", AccountMasterDetails.SAM_PAN));
            arrParams.Add(new SqlParameter("@SAM_CST", AccountMasterDetails.SAM_CST));
            arrParams.Add(new SqlParameter("@SAM_SST", AccountMasterDetails.SAM_SST));
            arrParams.Add(new SqlParameter("@SAM_IsFund", AccountMasterDetails.SAM_IsFund));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        } //10/01/2016 suvendu

        public List<SubAccountMaster> SubAccountMasterList(string TransType, SubAccountMaster SubAccountMaster)
        {
            List<SubAccountMaster> CFSjList = new List<SubAccountMaster>();
            SubAccountMaster AccountMaster = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            //arrParams.Add(new SqlParameter("@AM_GroupId", AccountMaster.AM_GroupId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    AccountMaster = new SubAccountMaster();
                    AccountMaster.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);
                    AccountMaster.SAM_SubId = Convert.ToInt32(rdr["SAM_SubId"]);
                    AccountMaster.SAM_CompanyID = Convert.ToInt32(rdr["SAM_CompanyID"]);
                    AccountMaster.SAM_FYID = Convert.ToInt32(rdr["SAM_FYID"]);
                    AccountMaster.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    AccountMaster.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();
                    AccountMaster.SAM_SubLongDesc = rdr["SAM_SubLongDesc"].ToString();
                    AccountMaster.SAM_Address1 = rdr["SAM_Address1"].ToString();
                    AccountMaster.SAM_Address2 = rdr["SAM_Address2"].ToString();
                    AccountMaster.SAM_Address3 = rdr["SAM_Address3"].ToString();
                    AccountMaster.SAM_Address4 = rdr["SAM_Address4"].ToString();
                    AccountMaster.SAM_OPhone = rdr["SAM_OPhone"].ToString();
                    AccountMaster.SAM_RPhone = rdr["SAM_RPhone"].ToString();
                    AccountMaster.SAM_FAX = rdr["SAM_FAX"].ToString();
                    AccountMaster.SAM_CellNo = rdr["SAM_CellNo"].ToString();
                    AccountMaster.SAM_Email = rdr["SAM_Email"].ToString();
                    AccountMaster.SAM_Website = rdr["SAM_Website"].ToString();
                    AccountMaster.SAM_PAN = rdr["SAM_PAN"].ToString();
                    AccountMaster.SAM_CST = rdr["SAM_CST"].ToString();
                    AccountMaster.SAM_SST = rdr["SAM_SST"].ToString();
                    AccountMaster.SAM_IsFund = Convert.ToBoolean(rdr["SAM_IsFund"]);
                    CFSjList.Add(AccountMaster);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }

        public int? InsertAccountMasterOpeningDetails(string TransType, AccountMaster AccountMasterDetails)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", AccountMasterDetails.AM_AccountId));
            arrParams.Add(new SqlParameter("@AM_AccountOpId", AccountMasterDetails.AM_AccountOpId));
            arrParams.Add(new SqlParameter("@AM_AccountCode", AccountMasterDetails.AM_AccountCode));
            arrParams.Add(new SqlParameter("@AM_OPeningType", AccountMasterDetails.AM_OPeningType));
            arrParams.Add(new SqlParameter("@AM_OpeningBalance", AccountMasterDetails.AM_OpeningBalance));
            //arrParams.Add(new SqlParameter("@AM_CompanyID", AccountMasterDetails.AM_CompanyID));
            arrParams.Add(new SqlParameter("@AM_FYId", AccountMasterDetails.AM_FYId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMasterOpening", arrParams.ToArray());
            int? val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
            return val;
        } //10/01/2016 suvendu

        public List<SubAccountMaster> SubAccountMasterOpeningbalanceList(string TransType, int? Id)
        {
            List<SubAccountMaster> CFSjList = new List<SubAccountMaster>();
            SubAccountMaster CFSObj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", TransType));

            arrParams.Add(new SqlParameter("@AM_AccountId", Id));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMasterOpeningBalance", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    CFSObj = new SubAccountMaster();


                    CFSObj.AM_AccountId = Convert.ToInt32(rdr["AM_AccountId"]);
                    CFSObj.SAM_AccountId = Convert.ToInt32(rdr["SAM_AccountId"]);

                    CFSObj.AM_AccountCode = rdr["AM_AccountCode"].ToString();
                    CFSObj.AM_AccDescription = rdr["AM_AccDescription"].ToString();
                    CFSObj.SAM_SubCode = rdr["SAM_SubCode"].ToString();
                    CFSObj.SAM_SubDescription = rdr["SAM_SubDescription"].ToString();


                    CFSObj.SAM_OPeningType = rdr["SAM_OPeningType"].ToString();
                    CFSObj.SAM_ClosingType = rdr["SAM_ClosingType"].ToString();



                    CFSObj.SAM_TotalDebit = Convert.ToDecimal(rdr["SAM_TotalDebit"]);
                    CFSObj.SAM_TotalCredit = Convert.ToDecimal(rdr["SAM_TotalCredit"]);
                    CFSObj.SAM_ClosingBalance = Convert.ToDecimal(rdr["SAM_ClosingBalance"]);
                    CFSObj.SAM_OpeningBalance = Convert.ToDecimal(rdr["SAM_OpeningBalance"]);

                    CFSjList.Add(CFSObj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return CFSjList;
        }

        public int? InsertSubAccountMasterOpeningDetails(string TransType, SubAccountMaster SubAccountMasterDetails)
        {
            int? val = 0;

            foreach (var EME in SubAccountMasterDetails.SubAccountMasterList)
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();

                arrParams.Add(new SqlParameter("@TransType", TransType));
                arrParams.Add(new SqlParameter("@SAM_AccountId", EME.SAM_AccountId));
                arrParams.Add(new SqlParameter("@SAM_AccountCode", EME.SAM_SubCode));
                arrParams.Add(new SqlParameter("@SAM_OPeningType", EME.SAM_OPeningType));
                arrParams.Add(new SqlParameter("@SAM_OpeningBalance", EME.SAM_OpeningBalance));
                arrParams.Add(new SqlParameter("@SAM_FYId", SubAccountMasterDetails.SAM_FYID));
                arrParams.Add(new SqlParameter("@AM_AccountId", SubAccountMasterDetails.AM_AccountId));

                //arrParams.Add(new SqlParameter("@AM_CompanyID", AccountMasterDetails.AM_CompanyID));

                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_SubAccountMasterOpening", arrParams.ToArray());
                val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);

            } //10/01/2016 suvendu
            return val;
        }

        public string InsertUpdateAccountMasterOpening(int? AccId)
        {
            long val = 0;
            int EnrollmentId = 0;
            string EnqId = "";
            try
            {
                using (var cn = new SqlConnection(GetConnectionString()))
                {
                    //var Subject = SubjectList.FirstOrDefault();

                    string _sql = @"DELETE FROM [dbo].AccountMaster_AMBalance WHERE AM_AccountId = @AM_AccountId ";//AND SessionId = @SessionId
                    var cmd = new SqlCommand(_sql, cn);
                    cmd.Parameters.Add(new SqlParameter("@AM_AccountId", SqlDbType.VarChar, 50)).Value = AccId;

                    //cmd.Parameters.Add(new SqlParameter("@CourseId", SqlDbType.Int)).Value = Subject.CourseId;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                }



                List<SqlParameter> arrParamsN = new List<SqlParameter>();
                arrParamsN.Add(new SqlParameter("@TransType", "UPDATEDETAILS"));
                arrParamsN.Add(new SqlParameter("@AM_AccountId", AccId));

                //arrParamsN.Add(new SqlParameter("@ExamPercentage", EME.ExamPercentage));
                //arrParamsN.Add(new SqlParameter("@YearOfPassing", EME.YearOfPassing));
                SqlParameter OutPutIdN = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutIdN.Direction = ParameterDirection.Output;
                arrParamsN.Add(OutPutIdN);

                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountMasterOpening", arrParamsN.ToArray());
                EnrollmentId = Convert.ToInt32(arrParamsN[arrParamsN.Count - 1].Value);

            }
            //



            catch (Exception Ex)
            {
                throw Ex;
            }
            return EnqId;
        }
        public List<AccountVoucherTypeMaster> AccountVoucherTypeList(AccountVoucherTypeMaster PerObj)
        {
            List<AccountVoucherTypeMaster> List = new List<AccountVoucherTypeMaster>();
            AccountVoucherTypeMaster Obj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@VoucherTypeId", PerObj.VoucherTypeId));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_AccountVoucherTypeMaster", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Obj = new AccountVoucherTypeMaster();

                    Obj.VoucherTypeId = Convert.ToInt32(rdr["VoucherTypeId"]);
                    Obj.VoucherType = rdr["VoucherType"].ToString();

                    List.Add(Obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return List;
        }

        public List<AccountLedger> VoucherEntryList(AccountLedger PerObj)
        {
            List<AccountLedger> List = new List<AccountLedger>();
            AccountLedger Obj = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));
            arrParams.Add(new SqlParameter("@LD_VoucherTypeId", PerObj.LD_VoucherTypeId));
            arrParams.Add(new SqlParameter("@LD_FYId", PerObj.LD_FYId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_LedgerVoucherEntry", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Obj = new AccountLedger();

                    Obj.LD_VoucherNo = Convert.ToString(rdr["LD_VoucherNo"]);
                    Obj.LD_ReferenceNo = Convert.ToString(rdr["LD_ReferenceNo"]);
                    Obj.LD_DateS = Convert.ToString(rdr["LD_DateS"]);
                    Obj.LD_Narration = Convert.ToString(rdr["LD_Narration"]);
                    Obj.LD_CrAmount = Convert.ToDecimal(rdr["LD_CrAmount"]);
                    Obj.LD_DrAmount = Convert.ToDecimal(rdr["LD_DrAmount"]);
                    List.Add(Obj);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return List;
        }

        public string GetVoucherID(long? SessionId, int? VoucherTypeId)
        {
            string Text = "";
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SessionId", SessionId));
            arrParams.Add(new SqlParameter("@VoucherTypeId", VoucherTypeId));
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetAccountVoucherNO", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    Text = Convert.ToString(rdr["VoucherNO"]);

                }
                rdr.Close();
            }
            rdr.Dispose();


            return Text;
        }

        public int? InsertVoucherEntryDetails(string TransType, AccountLedger AccountLedgerS)
        {
            int? val = 0;
            int? LD_LedgerID = 0;
            foreach (var EME in AccountLedgerS.AccountLedgerList)
            {
                List<SqlParameter> arrParams = new List<SqlParameter>();

                arrParams.Add(new SqlParameter("@TransType", TransType));
                arrParams.Add(new SqlParameter("@LD_AccountID", EME.LD_AccountID));
                arrParams.Add(new SqlParameter("@LD_AccountCode", EME.LD_AccountCode));
                arrParams.Add(new SqlParameter("@LD_SubID", EME.LD_SubID));
                arrParams.Add(new SqlParameter("@LD_SubCode", EME.LD_SubCode));
                arrParams.Add(new SqlParameter("@LD_FundAccountID", EME.LD_FundAccountID));
                arrParams.Add(new SqlParameter("@LD_FundSubID", EME.LD_FundSubID));
                arrParams.Add(new SqlParameter("@LD_DrAmount", EME.LD_DrAmount));
                arrParams.Add(new SqlParameter("@LD_CrAmount", EME.LD_CrAmount));
                arrParams.Add(new SqlParameter("@LD_DrCr", EME.LD_DrCr));
                arrParams.Add(new SqlParameter("@LD_Remarks", EME.LD_Remarks));
                arrParams.Add(new SqlParameter("@LD_VoucherTypeId", EME.LD_VoucherTypeId));
                arrParams.Add(new SqlParameter("@LD_FYId", AccountLedgerS.LD_FYId));
                arrParams.Add(new SqlParameter("@LD_CompanyId", AccountLedgerS.LD_CompanyId));
                arrParams.Add(new SqlParameter("@LD_ChequeDate", AccountLedgerS.LD_ChequeDate));
                arrParams.Add(new SqlParameter("@LD_ChequeNo", AccountLedgerS.LD_ChequeNo));
                arrParams.Add(new SqlParameter("@LD_ReferenceNo", AccountLedgerS.LD_ReferenceNo));
                arrParams.Add(new SqlParameter("@LD_Payee", AccountLedgerS.LD_Payee));
                arrParams.Add(new SqlParameter("@LD_Narration", AccountLedgerS.LD_Narration));
                arrParams.Add(new SqlParameter("@LD_VoucherNo", AccountLedgerS.LD_VoucherNo));
                arrParams.Add(new SqlParameter("@LD_LedgerID", LD_LedgerID));
                arrParams.Add(new SqlParameter("@LD_DateS", AccountLedgerS.LD_DateS));


                SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt);
                OutPutId.Direction = ParameterDirection.Output;
                arrParams.Add(OutPutId);
                SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_LedgerVoucherEntry", arrParams.ToArray());
                val = Convert.ToInt32(arrParams[arrParams.Count - 1].Value);
                if (val > 0 && LD_LedgerID == 0)
                {
                    LD_LedgerID = val;
                }
            } //10/01/2016 suvendu
            return val;
        }

    
        #endregion

        #region ClassFeesForward
        public List<ClassWisefees_CF> GetClassFeesForward(ClassFeesForward query)
        {
            List<ClassWisefees_CF> ListObject = new List<ClassWisefees_CF>();
            ClassWisefees_CF DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@CF_SCHOOLID", query.SchoolId));
            arrParams.Add(new SqlParameter("@CF_SESSIONID", query.SessionId));
            arrParams.Add(new SqlParameter("@CF_CLASSID", query.ClassId));
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_GetClassWiseFeesList", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new ClassWisefees_CF();
                    DataObject.FeesHeadName = Convert.ToString(rdr["FeesHeadName"]);
                    DataObject.CF_CLASSFEESID = Convert.ToInt32(rdr["CF_CLASSFEESID"]);
                    DataObject.CF_CLASSID = Convert.ToInt32(rdr["CF_CLASSID"]);
                    DataObject.CF_CATEGORYID = Convert.ToInt32(rdr["CF_CATEGORYID"]);
                    DataObject.CF_FEESID = Convert.ToInt32(rdr["CF_FEESID"]);
                    DataObject.CF_NOOFINS = Convert.ToInt32(rdr["CF_NOOFINS"]);
                    DataObject.CF_FEESAMOUNT = Convert.ToDecimal(rdr["CF_FEESAMOUNT"]);
                    DataObject.CF_INSTALLMENTNO = Convert.ToInt32(rdr["CF_INSTALLMENTNO"]);
                    DataObject.CF_INSAMOUNT = Convert.ToDecimal(rdr["CF_INSAMOUNT"]);
                    DataObject.Testdata = Convert.ToString(rdr["Testdata"]);
                    DataObject.DUEDATES = Convert.ToString(rdr["Testdata"]);
                    DataObject.ClassName = Convert.ToString(rdr["ClassName"]);
                    DataObject.CAT_CATEGORYNAME = Convert.ToString(rdr["CAT_CATEGORYNAME"]);
                    DataObject.IsAdmissionTime = rdr["IsAdmissionTime"] == DBNull.Value ? false : Convert.ToBoolean(rdr["IsAdmissionTime"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion

        #region Library Master
        public long InsertUpdateBookMaster(BookMaster_BM book)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (book.BM_BookId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@BM_BookId", book.BM_BookId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@BM_Title", book.BM_Title));
            arrParams.Add(new SqlParameter("@BM_ISBN", book.BM_ISBN));
            arrParams.Add(new SqlParameter("@BM_Author", book.BM_Author));
            arrParams.Add(new SqlParameter("@BM_Publisher", book.BM_Publisher));
            arrParams.Add(new SqlParameter("@BM_Edition", book.BM_Edition));
            arrParams.Add(new SqlParameter("@BM_Language", book.BM_Language));
            arrParams.Add(new SqlParameter("@BM_ClassLevel", book.BM_ClassLevel));
            arrParams.Add(new SqlParameter("@BM_ShelfNo", book.BM_ShelfNo));
            arrParams.Add(new SqlParameter("@BM_IsReferenceOnly", book.BM_IsReferenceOnly));
            arrParams.Add(new SqlParameter("@BM_CreatedBy", book.BM_CreatedBy));
            arrParams.Add(new SqlParameter("@BM_SchoolId", book.BM_SchoolId));
          
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Book_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<BookMaster_BM> BookMasterList(BookMaster_BM book)
        {
            List<BookMaster_BM> ListObject = new List<BookMaster_BM>();
            BookMaster_BM DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@BM_SchoolId", book.BM_SchoolId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Book_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new BookMaster_BM();
                    DataObject.BM_BookId = Convert.ToInt64(rdr["BM_BookId"]);
                    DataObject.BM_SchoolId = Convert.ToInt64(rdr["BM_SchoolId"]);
                    DataObject.SchoolName = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    DataObject.BM_Title = Convert.ToString(rdr["BM_Title"]);
                    DataObject.BM_Author = Convert.ToString(rdr["BM_Author"]);
                    DataObject.BM_Publisher = Convert.ToString(rdr["BM_Publisher"]);
                    DataObject.BM_Edition = Convert.ToString(rdr["BM_Edition"]);
                    DataObject.BM_Language = Convert.ToString(rdr["BM_Language"]);
                    DataObject.BM_ClassLevel = Convert.ToString(rdr["BM_ClassLevel"]);
                    DataObject.BM_ShelfNo = Convert.ToString(rdr["BM_ShelfNo"]);
                    DataObject.BM_IsReferenceOnly = Convert.ToBoolean(rdr["BM_IsReferenceOnly"]);
                    DataObject.BM_IsActive = Convert.ToString(rdr["BM_IsActive"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long InsertUpdateBookCopyMaster(BookCopyMaster_BCM copy)
        {
            DataTable DataList = new DataTable();
            DataList.Columns.Add("BCM_CopyCode", typeof(string));
            DataList.Columns.Add("BCM_CopyName", typeof(string));
            DataList.Columns.Add("BCM_BookId", typeof(Int64));
            DataList.Columns.Add("BCM_AccessionNo", typeof(string));
            DataList.Columns.Add("BCM_Barcode", typeof(string));
       //     DataList.Columns.Add("BCM_PurchaseDate", typeof(DateTime));
            DataList.Columns.Add("BCM_Price", typeof(string));
            DataList.Columns.Add("BCM_Status", typeof(string));
           	
            foreach (var data in copy.BookCopyMaster_Type)
            {
                DataRow dr = DataList.NewRow();
                dr["BCM_CopyCode"] = data.BCM_CopyCode;
                dr["BCM_CopyName"] = data.BCM_CopyName;
                dr["BCM_BookId"] = data.BCM_BookId;
                dr["BCM_AccessionNo"] = data.BCM_AccessionNo;
                dr["BCM_Barcode"] = data.BCM_Barcode;
               // dr["BCM_PurchaseDate"] = data.BCM_PurchaseDate;
                dr["BCM_Price"] = data.BCM_Price;
                dr["BCM_Status"] = data.BCM_Status;
             //   dr["BCM_CreatedBy"] = copy.BCM_CreatedBy;
                DataList.Rows.Add(dr);
            }
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (copy.BCM_CopyId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@BCM_CopyId", copy.BCM_CopyId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@BCM_SchoolId", copy.BCM_SchoolId));
            arrParams.Add(new SqlParameter("@BCM_CreatedBy", copy.BCM_CreatedBy));
            arrParams.Add(new SqlParameter("@BookCopyMaster_Type", DataList));
        
            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Book_Copy_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<BookCopyMaster_BCM> BookCopyMasterList(BookCopyMaster_BCM copy)
        {
            List<BookCopyMaster_BCM> ListObject = new List<BookCopyMaster_BCM>();
            BookCopyMaster_BCM DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();

            arrParams.Add(new SqlParameter("@BCM_SchoolId", copy.BCM_SchoolId));
            arrParams.Add(new SqlParameter("@BCM_BookId", copy.BCM_BookId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Book_Copy_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new BookCopyMaster_BCM();
                    DataObject.BCM_CopyId = Convert.ToInt64(rdr["BCM_CopyId"]);
                    DataObject.BCM_CopyCode = Convert.ToString(rdr["BCM_CopyCode"]);
                    DataObject.BCM_CopyName = Convert.ToString(rdr["BCM_CopyName"]);
                    DataObject.BCM_SchoolId = Convert.ToInt64(rdr["BCM_SchoolId"]);
                    DataObject.SCM_SCHOOLNAME = Convert.ToString(rdr["SCM_SCHOOLNAME"]);
                    DataObject.BCM_BookId = Convert.ToInt64(rdr["BCM_BookId"]);
                    DataObject.BM_Title = Convert.ToString(rdr["BM_Title"]);
                    DataObject.BCM_AccessionNo = Convert.ToString(rdr["BCM_AccessionNo"]);
                    DataObject.BCM_Barcode = Convert.ToString(rdr["BCM_Barcode"]);
                //    DataObject.BCM_PurchaseDate = Convert.ToDateTime(rdr["BCM_PurchaseDate"]);
                    DataObject.BCM_Price = Convert.ToDecimal(rdr["BCM_Price"]);
                    DataObject.BCM_Status = Convert.ToString(rdr["BCM_Status"]);
                   
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long InsertUpdateLibrarySettingMaster(LibrarySetting setting)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (setting.LS_SettingId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@SettingId", setting.LS_SettingId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@SchoolId", setting.LS_SchoolId));
            arrParams.Add(new SqlParameter("@MaxIssueDays", setting.LS_MaxIssueDays));
            arrParams.Add(new SqlParameter("@FinePerDay", setting.LS_FinePerDay));
            arrParams.Add(new SqlParameter("@MaxBooksStudent", setting.LS_MaxBooksStudent));
            arrParams.Add(new SqlParameter("@MaxBooksTeacher", setting.LS_MaxBooksTeacher));
            arrParams.Add(new SqlParameter("@MaxRenewLimit", setting.LS_MaxRenewLimit));
            arrParams.Add(new SqlParameter("@GraceDays", setting.LS_GraceDays));
            arrParams.Add(new SqlParameter("@CreatedBy", setting.LS_CreatedBy));
            arrParams.Add(new SqlParameter("@Active", setting.LS_Active));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Library_Setting_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<LibrarySetting> LibrarySettingMasterList(LibrarySetting setting)
        {
            List<LibrarySetting> ListObject = new List<LibrarySetting>();
            LibrarySetting DataObject = null;
            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SchoolId", setting.LS_SchoolId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Library_Setting_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new LibrarySetting();
                    DataObject.LS_SettingId = Convert.ToInt64(rdr["LS_SettingId"]);
                    DataObject.LS_SchoolId = Convert.ToInt64(rdr["LS_SchoolId"]);
                    DataObject.LS_MaxIssueDays = Convert.ToInt32(rdr["LS_MaxIssueDays"]);
                    DataObject.LS_FinePerDay = Convert.ToDecimal(rdr["LS_FinePerDay"]);
                    DataObject.LS_MaxBooksStudent = Convert.ToInt32(rdr["LS_MaxBooksStudent"]);
                    DataObject.LS_MaxBooksTeacher = Convert.ToInt32(rdr["LS_MaxBooksTeacher"]);
                    DataObject.LS_MaxRenewLimit = Convert.ToInt32(rdr["LS_MaxRenewLimit"]);
                    DataObject.LS_GraceDays = Convert.ToInt32(rdr["LS_GraceDays"]);
                    DataObject.LS_Active = Convert.ToString(rdr["LS_Active"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long InsertUpdateCategoryMaster(CategoryMaster category)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (category.CM_CategoryId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@CategoryId", category.CM_CategoryId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@SchoolId", category.CM_SchoolId));
            arrParams.Add(new SqlParameter("@CategoryCode", category.CM_CategoryCode));
            arrParams.Add(new SqlParameter("@CategoryName", category.CM_CategoryName));
            arrParams.Add(new SqlParameter("@ParentCategoryId", category.CM_ParentCategoryId));
            arrParams.Add(new SqlParameter("@CreatedBy", category.CM_CreatedBy));
            arrParams.Add(new SqlParameter("@IsActive", category.CM_IsActive));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Category_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<CategoryMaster> CategoryMasterList(CategoryMaster category)
        {
            List<CategoryMaster> ListObject = new List<CategoryMaster>();
            CategoryMaster DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SchoolId", category.CM_SchoolId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Category_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new CategoryMaster();
                    DataObject.CM_CategoryId = Convert.ToInt64(rdr["CategoryId"]);
                    DataObject.CM_SchoolId = Convert.ToInt64(rdr["SchoolId"]);
                    DataObject.CM_CategoryCode = Convert.ToString(rdr["CategoryCode"]);
                    DataObject.CM_CategoryName = Convert.ToString(rdr["CategoryName"]);
                    DataObject.CM_ParentCategoryId = Convert.ToInt32(rdr["ParentCategoryId"]);
                    DataObject.CM_IsActive = Convert.ToString(rdr["IsActive"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long InsertUpdateSupplierMaster(SupplierMaster supplier)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (supplier.SM_SupplierId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@SupplierId", supplier.SM_SupplierId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@SchoolId", supplier.SM_SchoolId));
            arrParams.Add(new SqlParameter("@SupplierName", supplier.SM_SupplierName));
            arrParams.Add(new SqlParameter("@Mobile", supplier.SM_Mobile));
            arrParams.Add(new SqlParameter("@Email", supplier.SM_Email));
            arrParams.Add(new SqlParameter("@Address", supplier.SM_Address));
            arrParams.Add(new SqlParameter("@CreatedBy", supplier.SM_CreatedBy));
            arrParams.Add(new SqlParameter("@Active", supplier.SM_Active));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Supplier_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<SupplierMaster> SupplierMasterList(SupplierMaster supplier)
        {
            List<SupplierMaster> ListObject = new List<SupplierMaster>();
            SupplierMaster DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@SchoolId", supplier.SM_SchoolId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Supplier_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new SupplierMaster();
                    DataObject.SM_SupplierId = Convert.ToInt64(rdr["SupplierId"]);
                    DataObject.SM_SchoolId = Convert.ToInt64(rdr["SchoolId"]);
                    DataObject.SM_SupplierName = Convert.ToString(rdr["SupplierName"]);
                    DataObject.SM_Mobile = Convert.ToString(rdr["Mobile"]);
                    DataObject.SM_Email = Convert.ToString(rdr["Email"]);
                    DataObject.SM_Address = Convert.ToString(rdr["Address"]);
                    DataObject.SM_Active = Convert.ToString(rdr["Active"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public long InsertUpdateMemberMaster(MemberMaster member)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            string transType = (member.MM_MemberId > 0) ? "UPDATE" : "INSERT";
            arrParams.Add(new SqlParameter("@MM_MemberId", member.MM_MemberId));
            arrParams.Add(new SqlParameter("@TransType", transType));
            arrParams.Add(new SqlParameter("@MM_SchoolId", member.MM_SchoolId));
            arrParams.Add(new SqlParameter("@MM_MemberTypeId", member.MM_MemberTypeId));
            arrParams.Add(new SqlParameter("@MM_FirstName", member.MM_FirstName));
            arrParams.Add(new SqlParameter("@MM_LastName", member.MM_LastName));
            arrParams.Add(new SqlParameter("@MM_Address", member.MM_Address));
            arrParams.Add(new SqlParameter("@MM_Mobile", member.MM_Mobile));
            arrParams.Add(new SqlParameter("@MM_Email", member.MM_Email));
            arrParams.Add(new SqlParameter("@CreatedBy", member.MM_CreatedBy));
            arrParams.Add(new SqlParameter("@Active", member.MM_Active));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Member_Master", arrParams.ToArray());
            long val = Convert.ToInt64(arrParams[arrParams.Count - 1].Value);
            return val;
        }
        public List<MemberMaster> MemberMasterList(MemberMaster member)
        {
            List<MemberMaster> ListObject = new List<MemberMaster>();
            MemberMaster DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@MM_SchoolId", member.MM_SchoolId));
            arrParams.Add(new SqlParameter("@MM_MemberTypeId", member.MTM_TypeId));
            arrParams.Add(new SqlParameter("@TransType", "SELECT"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_Member_Master", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new MemberMaster();
                    DataObject.MM_MemberId = Convert.ToInt64(rdr["MM_MemberId"]);
                    DataObject.MM_SchoolId = Convert.ToInt64(rdr["MM_SchoolId"]);
                    DataObject.MM_MemberCode = Convert.ToString(rdr["MM_MemberCode"]);
                    DataObject.MemberName = Convert.ToString(rdr["MemberName"]);
                    DataObject.MM_MemberTypeId = Convert.ToInt64(rdr["MM_MemberTypeId"]);
                    DataObject.TypeName = Convert.ToString(rdr["TypeName"]);
                    DataObject.MM_Email = Convert.ToString(rdr["MM_Address"]);
                    DataObject.MM_Address = Convert.ToString(rdr["MM_Email"]);
                    DataObject.MM_Mobile = Convert.ToString(rdr["MM_Mobile"]);
                    DataObject.MM_Active = Convert.ToString(rdr["MM_Active"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }

        public List<IssueTransaction> GetDueDaysCount(IssueTransaction issue)
        {
            List<IssueTransaction> ListObject = new List<IssueTransaction>();
            IssueTransaction DataObject = null;

            List<SqlParameter> arrParams = new List<SqlParameter>();
            arrParams.Add(new SqlParameter("@IT_SchoolId", issue.IT_SchoolId));
            arrParams.Add(new SqlParameter("@TransType", "DUE_DATE"));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Decimal);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);

            SqlDataReader rdr = SqlHelper.ExecuteReader(GetConnectionString(), CommandType.StoredProcedure, "SP_Insert_Update_IssueTransaction", arrParams.ToArray());
            if (rdr != null)
            {
                while (rdr.Read())
                {
                    DataObject = new IssueTransaction();
                    DataObject.DueDaysCount = Convert.ToInt32(rdr["DueDaysCount"]);
                    ListObject.Add(DataObject);
                }
                rdr.Close();
            }
            rdr.Dispose();
            return ListObject;
        }
        #endregion

        #region RegisterDevice
        public long InsertUpdateDevice(RegistrationRequest obj)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();

            // Passing the necessary parameters to the stored procedure
            arrParams.Add(new SqlParameter("@FcmToken", obj.FcmToken));
            arrParams.Add(new SqlParameter("@UserId", obj.UserId));
            arrParams.Add(new SqlParameter("@UserType", obj.UserType));
            arrParams.Add(new SqlParameter("@DeviceType", obj.DeviceType));
            arrParams.Add(new SqlParameter("@ActionUser", obj.UserId)); // optional action user

            SqlParameter outPutId = new SqlParameter("@OutPutId", SqlDbType.BigInt)
            {
                Direction = ParameterDirection.Output
            };

            arrParams.Add(outPutId);

            // Execute the stored procedure for insert or update based on the FCMToken existence
            SqlHelper.ExecuteNonQuery(GetConnectionString(), CommandType.StoredProcedure, "SP_Notification_RegisterDevice", arrParams.ToArray());

            // Get the output device ID from the procedure
            long deviceId = Convert.ToInt64(outPutId.Value);

            return deviceId;
        }
        #endregion
    }
}

