using AccountManagementSystem.Models;
using SchoolMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolMVC.Repositories
{
    public class LoginRpository : ILoginRepository
    {
        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["School_DbEntity"].ConnectionString;
        }
        //public UserMaster_UM GetLogin(UserMaster_UM user)
        //{
        //    List<SqlParameter> arrParams = new List<SqlParameter>();
        //    UserMaster_UM objUser = new UserMaster_UM();          
        //    UserMaster_UM objUser_scl = null;
        //    DataSet ds=new DataSet();
        //    arrParams.Add(new SqlParameter("@UM_LOGINID", user.UM_LOGINID));
        //    arrParams.Add(new SqlParameter("@UM_PASSWORD", user.UM_PASSWORD));
           
        //    SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
        //    OutPutId.Direction = ParameterDirection.Output;
        //    arrParams.Add(OutPutId);
        //   ds= SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_GetLogin", arrParams.ToArray());
        //   if (ds != null && ds.Tables.Count > 1)
        //   {
        //       if (ds.Tables[0].Rows.Count > 0)
        //       {

        //           objUser.UM_USERID = Convert.ToInt32(ds.Tables[0].Rows[0]["UM_USERID"]);
        //           objUser.UM_LOGINID = Convert.ToString(ds.Tables[0].Rows[0]["UM_LOGINID"]);
        //           objUser.UM_USERNAME = Convert.ToString(ds.Tables[0].Rows[0]["UM_USERNAME"]);
        //           objUser.UM_SCM_SCHOOLID = Convert.ToInt32(ds.Tables[0].Rows[0]["UM_SCM_SCHOOLID"]);
        //           objUser.UM_USERTYPE = Convert.ToString(ds.Tables[0].Rows[0]["UM_USERTYPE"]);
                 
                  

        //       }
        //       if (ds.Tables[1].Rows.Count > 0)
        //       {
        //           foreach (DataRow rdr in ds.Tables[1].Rows)
        //           {
        //               objUser_scl = new UserMaster_UM();
        //               objUser_scl.UM_SCHOOLNAME = Convert.ToString(rdr["SCHOOLLIST"]);
        //               objUser_scl.UM_SCM_SCHOOLID = Convert.ToInt32(rdr["SCHOOLID"]);
        //               objUser.Schoollist.Add(objUser_scl);
        //           }
        //       }
        //       if (ds.Tables[2].Rows.Count > 0)
        //       {
        //           foreach (DataRow rdr in ds.Tables[2].Rows)
        //           {
        //               objUser_scl = new UserMaster_UM();
        //               objUser_scl.UM_SCM_SESSIONID = Convert.ToInt32(rdr["SM_SESSIONID"]);
        //               objUser_scl.UM_SESSIONNAME = Convert.ToString(rdr["SM_SESSIONNAME"]);
        //               objUser.Sessionlist.Add(objUser_scl);
        //           }
        //       }

        //   }
        //   return objUser;

        //}
        #region GetLogin
        public UserMaster_UM GetLogin(UserMaster_UM user)
        {
            List<SqlParameter> arrParams = new List<SqlParameter>();
            UserMaster_UM objUser = new UserMaster_UM();
            UserMaster_UM objUser_scl = null;
            DataSet ds = new DataSet();
            arrParams.Add(new SqlParameter("@UM_LOGINID", user.UM_LOGINID));
            arrParams.Add(new SqlParameter("@UM_PASSWORD", user.UM_PASSWORD));

            SqlParameter OutPutId = new SqlParameter("@OutPutId", SqlDbType.Int);
            OutPutId.Direction = ParameterDirection.Output;
            arrParams.Add(OutPutId);
            ds = SqlHelper.ExecuteDataset(GetConnectionString(), CommandType.StoredProcedure, "SP_GetLogin", arrParams.ToArray());
            if (ds != null && ds.Tables.Count > 1)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {

                    objUser.UM_USERID = Convert.ToInt32(ds.Tables[0].Rows[0]["UM_USERID"]);
                    objUser.UM_LOGINID = Convert.ToString(ds.Tables[0].Rows[0]["UM_LOGINID"]);
                    objUser.UM_USERNAME = Convert.ToString(ds.Tables[0].Rows[0]["UM_USERNAME"]);
                    objUser.UM_SCM_SCHOOLID = Convert.ToInt32(ds.Tables[0].Rows[0]["UM_SCM_SCHOOLID"]);
                    objUser.UM_USERTYPE = Convert.ToString(ds.Tables[0].Rows[0]["UM_USERTYPE"]);
                    objUser.UM_ROLEID = Convert.ToInt64(ds.Tables[0].Rows[0]["UM_ROLEID"]);
                    objUser.UM_FP_ID = ds.Tables[0].Rows[0]["UM_FP_ID"] == DBNull.Value ? (long?)null : Convert.ToInt64(ds.Tables[0].Rows[0]["UM_FP_ID"]);
    
     

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow rdr in ds.Tables[1].Rows)
                    {
                        objUser_scl = new UserMaster_UM();
                        objUser_scl.UM_SCHOOLNAME = Convert.ToString(rdr["SCHOOLLIST"]);
                        objUser_scl.UM_SCM_SCHOOLID = Convert.ToInt32(rdr["SCHOOLID"]);
                        objUser.Schoollist.Add(objUser_scl);
                    }
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    foreach (DataRow rdr in ds.Tables[2].Rows)
                    {
                        objUser_scl = new UserMaster_UM();
                        objUser_scl.UM_SCM_SESSIONID = Convert.ToInt32(rdr["SM_SESSIONID"]);
                        objUser_scl.UM_SESSIONNAME = Convert.ToString(rdr["SM_SESSIONNAME"]);
                        objUser_scl.UM_SCM_SCHOOLID = Convert.ToInt32(rdr["UM_SCM_SCHOOLID"]);
                        objUser.Sessionlist.Add(objUser_scl);
                    }
                }
            }
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    foreach (DataRow rdr in ds.Tables[1].Rows)
            //    {
            //        objUser_scl = new UserMaster_UM();
            //        objUser_scl.UM_SCHOOLNAME = Convert.ToString(rdr["SCHOOLLIST"]);
            //        objUser_scl.UM_SCM_SCHOOLID = Convert.ToInt32(rdr["SCHOOLID"]);
            //        objUser.Schoollist.Add(objUser_scl);
            //    }
            //}
            //if (ds.Tables[2].Rows.Count > 0)
            //{
            //    foreach (DataRow rdr in ds.Tables[2].Rows)
            //    {
            //        objUser_scl = new UserMaster_UM();
            //        objUser_scl.UM_SCM_SESSIONID = Convert.ToInt32(rdr["SM_SESSIONID"]);
            //        objUser_scl.UM_SESSIONNAME = Convert.ToString(rdr["SM_SESSIONNAME"]);
            //        objUser_scl.UM_SCM_SCHOOLID = Convert.ToInt32(rdr["UM_SCM_SCHOOLID"]);
            //        objUser.Sessionlist.Add(objUser_scl);
            //    }
            //}

            return objUser;

        }
        #endregion
    }
}